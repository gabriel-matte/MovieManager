using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieManager.Models;
using Newtonsoft.Json;
using System.IO;
using System.Web.SessionState;

namespace MovieManager.Controllers
{
    public class BaseController : Controller
    {
        [HttpGet]
        public ActionResult Logout()
        {
            Session["Username"] = null;

            return RedirectToActionPermanent("Index", "Home");
        }

        public List<Media> ReadMediaFromFile(string filepath)
        {
            List<Media> mediaList = new List<Media>();

            using (FileStream fs = System.IO.File.Open(filepath, FileMode.Open))
            {
                StreamReader reader = new StreamReader(fs);

                string data = reader.ReadToEnd();

                mediaList = JsonConvert.DeserializeObject<List<Media>>(data);

                fs.Dispose();
            }

            return mediaList;
        }

        public (User, bool) VerifyLogin(User user)
        {
            List<User> users = new List<User>();
            string filepath = Server.MapPath("~") + "/Data/Users/users.json";

            using (FileStream fs = System.IO.File.Open(filepath, FileMode.Open))
            {
                StreamReader reader = new StreamReader(fs);

                string data = reader.ReadToEnd();

                users = JsonConvert.DeserializeObject<List<User>>(data);

                foreach(User u in users)
                {
                    if (u.Username == user.Username && u.Password == user.Password)
                    {
                        return (user, true);
                    }
                    else if (u.Username == user.Username && u.Password != user.Password)
                    {
                        ModelState.AddModelError("Password", "Invalid Password");
                    }
                    else
                    {
                        ModelState.AddModelError("Username", "Please enter a valid Username and Password");
                    }
                }

                fs.Dispose();
            }

            return (user, false);
        }

    }
}