using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieManager.Models;
using Newtonsoft.Json;

namespace MovieManager.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["Username"] == null)
                return RedirectToAction("Index", "Home");

            SearchParameters parameters = new SearchParameters();
            if (Session["SearchParameters"] != null)
                parameters = (SearchParameters)Session["SearchParameters"];

            List<Media> mediaList = new List<Media>();

            if (Session["MediaList"] == null)
            {
                string username = Session["Username"].ToString();
                string filepath = Server.MapPath("~").ToString() + "Data\\" + username + ".json";

                using (FileStream fs = System.IO.File.Open(filepath, FileMode.Open))
                {
                    StreamReader reader = new StreamReader(fs);
                    string data = reader.ReadToEnd();
                    mediaList = JsonConvert.DeserializeObject<List<Media>>(data);
                    Session["MediaList"] = mediaList;

                    fs.Dispose();
                }
            }
            else
                mediaList = (List<Media>)Session["MediaList"];

            return View(parameters);
        }

        [HttpGet]
        public ActionResult Search(List<Media> mediaList, SearchParameters parameters)
        {


            return View();
        }
    }


}