using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieManager.Models
{
    public class Media
    {
        public string Title { get; set; }

        public string Type { get; set; }

        public int YearStart { get; set; }

        public int? YearEnd { get; set; }

        public double ImdbRating { get; set; }

        public string[] Genre { get; set; }

        public string[] Starring { get; set; }
    }
}