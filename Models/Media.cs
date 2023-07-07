using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieManager.Models
{
    public class Media
    {
        public string Title { get; set; }

        public string Type { get; set; }

        [Display(Name = "Year Start")]
        public int YearStart { get; set; }
        
        [Display(Name = "Year End")]
        public int? YearEnd { get; set; }

        [Display(Name = "IMDB Link")]
        public string ImdbLink { get; set; }

        [DisplayFormat(DataFormatString = "{0:D1}")]
        public double ImdbRating { get; set; }

        public string[] Genre { get; set; }

        public string[] Starring { get; set; }

        public string ImagePath { get; set; }
    }
}