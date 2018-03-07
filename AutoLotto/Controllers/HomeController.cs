using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoLotto.Models;

namespace AutoLotto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("QuickPicks");
            return View();
        }

        public ActionResult Customized()
        {
            if (User.Identity.IsAuthenticated)
                return View();
            return RedirectToAction("");
        }

        public ActionResult QuickPicks()
        {
            ViewBag.Message = "Your QuickPick page.";
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                ViewBag.Workouts = db.Workouts.ToList();
                //ViewBag.Workouts = db.Workouts.Select(x => new {
                //    x.Id,
                //    x.ImageName,
                //    Exercises = string.Join(",", x.Exercises.ToList().ToArray<int>())
                //}).ToList();
            }
            return View();
        }
    }
}