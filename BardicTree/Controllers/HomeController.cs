﻿using BardicTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BardicTree.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var db = new BardicTree.Models.ApplicationDbContext();
            ViewBag.NodeCount = db.Nodes.Count();
            ViewBag.UserCount = db.Users.Count();
            ViewBag.Latest = db.Nodes.OrderByDescending(x => x.CreationDate).First();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}