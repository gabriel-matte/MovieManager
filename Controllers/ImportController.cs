using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieManager.Models;
using ExcelDataReader;
using System.Diagnostics;
using System.Data;
using Newtonsoft.Json;

namespace MovieManager.Controllers
{
    public class ImportController : Controller
    {
        public ImportController()
        {

        }

        // GET: Import
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            List<Media> data = new List<Media>();


            if (file != null)
            {
                string newFileName = new DateTime().ToString("{0:yyyyMMdd}") + file.FileName;
                string filePath = Server.MapPath("~") + "/Data/ImportData/" + newFileName;
                file.SaveAs(filePath);


                //string filename = file.FileName;
                //string targetpath = Server.MapPath("~/Data/ImportData/");
                //file.SaveAs(targetpath + filename);
                //string pathToExcelFile = targetpath + filename;

                List<string> result = new List<string>();

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        //reader.Read();
                        //DataTable dataTable = reader.GetSchemaTable();
                        do
                        {
                            
                            while (reader.Read())
                            {
                                if (reader.Depth == 0)
                                {

                                }
                                else
                                {
                                    Debug.WriteLine($"Row: {reader.Depth}     Values: {reader[0]}, {reader[1]}, {reader[2]}, {reader[3]}, {reader[4]}, {reader[5]}");

                                    Media m = new Media();

                                    if (reader[0] == null)
                                    {
                                        reader.Close();
                                    }
                                    else
                                    {
                                        string col1 = reader[0].ToString();
                                        string[] col1Array = col1.Split('(');
                                        m.Title = col1Array[0].Trim();

                                        string col2 = col1Array[1].Split(')')[0];
                                        m.Type = col2;

                                        string[] years;

                                        years = reader[1].ToString().Split('-');

                                        m.YearStart = Int32.Parse(years[0]);

                                        if (years.Length == 2 && years[1] != null && years[1].ToString() != "")
                                            m.YearEnd = Int32.Parse(years[1].ToString());
                                            
                                        if (reader[2] != null && reader[2].ToString() != "")
                                            m.ImdbRating = Double.Parse(reader[2].ToString());

                                        if (reader[3] != null && reader[3].ToString() != "")
                                        {
                                            string[] genres = reader[3].ToString().Split(',');
                                            for (int i = 0; i < genres.Length; i++)
                                            {
                                                genres[i] = genres[i].Trim();
                                            }
                                            m.Genre = genres;
                                        }

                                        if (reader[4] != null && reader[4].ToString() != "")
                                        {
                                            string[] stars = reader[4].ToString().Split(',');
                                            for (int i = 0; i < stars.Length; i++)
                                            {
                                                stars[i] = stars[i].Trim();
                                            }
                                            m.Starring = stars;
                                        }

                                        data.Add(m);
                                    }  
                                }                               
                            }
                        }
                        while (reader.NextResult());
                    }
                }
            }

            return View(data);
        }

        [HttpPost]
        public ActionResult SaveImportedMedia(string json)
        {
            if (json != null)
            {
                string rootPath = Server.MapPath("~").ToString();
                System.IO.File.WriteAllText(rootPath + "/Data/jsonTest.js", json);
            }                
            else
                return View("Index");

            return View("Index");
        }

        //Show Table for imported media list
        public ActionResult ImportTable()
        {
            return PartialView();
        }


    }
}