using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieManager.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.ApplicationInsights;

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

        [HttpPost]
        public ActionResult Index(SearchParameters parameters)
        {
            List<Media> mediaList = (List<Media>)Session["MediaList"];
            List<Media> results = new List<Media>();
            results.AddRange(mediaList);

            if (parameters.SearchTitle != null && parameters.SearchTitle != "")
                results = results.Where(m => m.Title.ToLower().Contains(parameters.SearchTitle)).ToList();
            if (parameters.SearchType != null && parameters.SearchType != "")
                results = results.Where(m => m.Type.ToLower().Contains(parameters.SearchType)).ToList();

            if (parameters.SearchYearStart > 0 && parameters.SearchYearEnd > parameters.SearchYearStart)
                results = results.Where(m => m.YearStart >= parameters.SearchYearStart && m.YearStart <= parameters.SearchYearEnd).ToList();
            else if (parameters.SearchYearStart > 0 && (parameters.SearchYearEnd == null || parameters.SearchYearEnd == 0))
                results = results.Where(m => m.YearStart == parameters.SearchYearStart).ToList();



            if (parameters.SearchGenre != null && parameters.SearchGenre != "")
                results = results.Where(m => {
                    bool x = false;
                    if (m.Genre != null)
                    {
                        x = string.Join(" ", m.Genre).ToLower().Contains(parameters.SearchGenre.ToLower());
                        Debug.WriteLine("DEBUG --------------->   m.Genre: " + string.Join(" ", m.Genre).ToLower());
                    }
                    else
                        Debug.WriteLine("DEBUG --------------->   m.Genre is null");
                    return x;
                }).ToList();

            if (parameters.SearchStarring != null && parameters.SearchStarring != "")
                results = results.Where(m => {
                    bool x = false;
                    if (m.Starring != null)
                    {
                        x = string.Join(" ", m.Starring).ToLower().Contains(parameters.SearchStarring.ToLower());
                        Debug.WriteLine("DEBUG --------------->   m.Starring: " + string.Join(" ", m.Starring).ToLower());
                    }
                    else
                        Debug.WriteLine("DEBUG --------------->   m.Starring is null");
                    return x;
                }).ToList();

            Session["SearchResults"] = results;
            Session["ShowFiltered"] = "true";

            return View(parameters);
        }

        [HttpPost]
        public ActionResult EditModal(Media item)
        {
            List<Media> mediaList = (List<Media>)Session["MediaList"];
            List<Media> searchList = (List<Media>)Session["SearchResults"];

            item.Genre = JsonConvert.DeserializeObject<string[]>(item.Genre[0]);
            item.Starring = JsonConvert.DeserializeObject<string[]>(item.Starring[0]);

            Media updatedItem = UpdateMediaItem(mediaList.FirstOrDefault(m => m.Title == item.Title), item);

            if (searchList != null)
            {
                Media searchItem = UpdateMediaItem(searchList.FirstOrDefault<Media>(m => m.Title == item.Title), item);
                Session["SearchResults"] = searchList;
            }

            string jsonList = JsonConvert.SerializeObject(mediaList);

            if (jsonList != null)
            {
                string rootPath = Server.MapPath("~").ToString();
                string filePath = "/Data/" + Session["Username"].ToString() + ".json";
                System.IO.File.WriteAllText(rootPath + filePath, jsonList);
            }

            return RedirectToActionPermanent("Index", "Dashboard");
        }

        public Media UpdateMediaItem(Media current, Media updated)
        {
            current.Title = updated.Title;
            current.Type = updated.Type;
            current.YearStart = updated.YearStart;
            current.YearEnd = updated.YearEnd;
            current.ImdbLink = updated.ImdbLink;
            current.ImdbRating = updated.ImdbRating;
            current.Genre = updated.Genre;
            current.Starring = updated.Starring;
            current.ImagePath = updated.ImagePath;

            return current;
        }
    }


}