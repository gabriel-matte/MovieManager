using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieManager.Models
{
    public class SearchParameters
    {
        public string SearchTitle { get; set; }

        public string SearchType { get; set; }

        public int? SearchYearStart { get; set; }

        public int? SearchYearEnd { get; set; }

        public double? SearchImdbRating { get; set; }

        public string SearchGenre { get; set; }

        public string SearchStarring { get; set; }
    }
}