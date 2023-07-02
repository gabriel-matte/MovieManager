using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieManager.Models;

namespace MovieManager.Controllers
{
    public class HomeController : BaseController
    {

        [HttpGet]
        public ActionResult Index()
        {
            User user = new User();

            if (ViewData["User"] != null)
                user = (User)ViewData["User"];
            else
                ModelState.Clear();

            return View(user);
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            (User userResult, bool valid) = VerifyLogin(user);
            if (valid)
            {
                Session.Add("Username", user.Username);
                return RedirectToAction("Index", "Dashboard");
            }

            ViewData["User"] = user;

            return View();
        }


    }
}