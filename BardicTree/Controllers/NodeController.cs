using BardicTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace BardicTree.Controllers
{
    [Authorize]
    public class NodeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Node
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult View(int id)
        {
            var md = new MarkdownDeep.Markdown();
            md.ExtraMode = false;
            md.SafeMode = true;
            md.MaxImageWidth = 1;
            md.NoFollowLinks = true;

            var node = db.Nodes.Find(id);

            if (User.Identity.GetUserId() != node.CreatorUserID)
            {
                db.NodeVisits.Add(new NodeVisit { NodeID = node.NodeID, UserID = User.Identity.GetUserId(), Visit = DateTime.Now });
                db.SaveChanges();
            }

            ViewBag.NodeID = id;
            ViewBag.NodeTitle = node.Title;
            ViewBag.NodeBody = md.Transform(node.BodyText);
            ViewBag.NodeQuestion = node.Question;
            ViewBag.NodeCreator = node.Creator.DisplayName;
            ViewBag.NodeDate = node.CreationDate;

            ViewBag.NodeVisits = node.Visits.Count();

            ViewBag.NodeChoices = new Dictionary<int, string>();

            foreach (var c in node.NodeChoices)
            {
                ViewBag.NodeChoices.Add(c.childNodeID, c.text);
            }

            return View();
        }

        public ActionResult Create(int id, string choice)
        {
            if (db.Nodes.Find(id).NodeChoices.Count(c => c.text == choice) > 0) 
            {
                ViewBag.pid = id;
                return View("CreateChoiceDuplicateError");
            }

            var model = new UIAddStoryElement();
            model.ParentNode = id;
            model.ChoiceText = choice;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceText,TitleText,StoryText,QuestionText,ParentNode")] UIAddStoryElement se)
        {
            if (db.Nodes.Find(se.ParentNode).NodeChoices.Count(c => c.text == se.ChoiceText) > 0)
            {
                ViewBag.pid = se.ParentNode;
                return View("CreateChoiceDuplicateError");
            }


            if (ModelState.IsValid)
            {
                var node = new Node { Title = se.TitleText, BodyText = se.StoryText, Question = se.QuestionText, CreationDate = DateTime.Now, CreatorUserID = User.Identity.GetUserId() };
                var nNode = db.Nodes.Add(node);
                db.SaveChanges();
                db.Nodes.Find(se.ParentNode).NodeChoices.Add(new NodeChoice { childNodeID = nNode.NodeID, text = se.ChoiceText });
                db.SaveChanges();
                return RedirectToAction("View", "Node", new { id = nNode.NodeID });
            }

            return View(se);
        }
    }
}