using BardicTree.Models;
using BardicTree.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BardicTree.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index(string id)
        {
            var md = new MarkdownDeep.Markdown();
            md.ExtraMode = false;
            md.SafeMode = true;
            md.MaxImageWidth = 1;
            md.NoFollowLinks = true;

            var u = db.Users.FirstOrDefault(p => p.DisplayName == id);

            if (u == null)
            {
                return HttpNotFound();
            }

            ViewBag.Authored = db.Nodes.Where(n => n.CreatorUserID == u.Id).ToList();
            ViewBag.DisplayName = id;
            ViewBag.Description = !string.IsNullOrEmpty(u.Description) ? md.Transform(u.Description) : "<p>This author has chosen to remain silent.</p>";
            ViewBag.NodeCount = u.Nodes.Count();
            ViewBag.Joined = u.Joined.ToLongDateString();
            ViewBag.Gravatar = Gravatar.GetURL(u);

            return View();
        }
    }
}