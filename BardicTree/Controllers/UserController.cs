using BardicTree.Models;
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
            var u = db.Users.FirstOrDefault(p => p.DisplayName == id);

            if (u == null)
            {
                return HttpNotFound();
            }           

            ViewBag.DisplayName = id;
            ViewBag.Description = u.Description ?? "This author has chosen to remain silent.";
            ViewBag.NodeCount = u.Nodes.Count();
            ViewBag.Joined = u.Joined.ToLongDateString();

            return View();
        }
    }
}