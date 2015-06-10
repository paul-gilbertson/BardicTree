using BardicTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BardicTree.Controllers
{
    public class StoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Story
        public ActionResult Index()
        {
            ViewBag.Stories = db.Stories.ToList();
            return View();
        }
    }
}