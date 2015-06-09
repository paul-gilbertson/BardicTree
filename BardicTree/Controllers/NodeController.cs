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
            var node = db.Nodes.Find(id);

            if (User.Identity.GetUserId() != node.CreatorUserID)
            {
                db.NodeVisits.Add(new NodeVisit { NodeID = node.NodeID, UserID = User.Identity.GetUserId(), Visit = DateTime.Now });
                db.SaveChanges();
            }

            ViewBag.NodeID = id;
            ViewBag.NodeTitle = node.Title;
            ViewBag.NodeBody = node.BodyText;
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

        public ActionResult Create(int id)
        {
            var model = new UIAddStoryElement();
            model.ParentNode = id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceText,TitleText,StoryText,ParentNode")] UIAddStoryElement se)
        {
            if (ModelState.IsValid)
            {
                var node = new Node { Title = se.TitleText, BodyText = se.StoryText, CreationDate = DateTime.Now, CreatorUserID = User.Identity.GetUserId() };
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