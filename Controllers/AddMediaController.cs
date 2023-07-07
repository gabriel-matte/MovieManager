using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieManager.Models;
using Newtonsoft.Json;

namespace MovieManager.Controllers
{
    public class AddMediaController : Controller
    {
        // GET: AddMedia
        public ActionResult Index(List<Media> mediaList = null)
        {
            if (mediaList == null)
                mediaList = new List<Media>();

            return View(mediaList);
        }

        public ActionResult AddItem(Media item)
        {
            List<Media> addList;

            item.Genre = JsonConvert.DeserializeObject<string[]>(item.Genre[0]);
            item.Starring = JsonConvert.DeserializeObject<string[]>(item.Starring[0]);

            if (ViewData["AddList"] == null)
                addList = new List<Media>();
            else
                addList = (List<Media>)ViewData["AddList"];

            addList.Add(item);

            return View("Index", addList);
        }

        public ActionResult AddAllItems(string jsonAdd)
        {
            List<Media> addList = JsonConvert.DeserializeObject<List<Media>>(jsonAdd);

            List<Media> mediaList = (List<Media>)Session["MediaList"];

            mediaList.AddRange(addList);

            string jsonList = JsonConvert.SerializeObject(mediaList);

            
            if (jsonList != null)
            {
                string rootPath = Server.MapPath("~").ToString();
                string filePath = "/Data/" + Session["Username"].ToString() + ".json";
                System.IO.File.WriteAllText(rootPath + filePath, jsonList);
            }

            return RedirectToActionPermanent("Index", "Dashboard");
        }
    }
}