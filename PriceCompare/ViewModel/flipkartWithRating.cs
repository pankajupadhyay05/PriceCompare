using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PriceCompare.Models;

namespace PriceCompare.ViewModel
{
    public class flipkartWithRating
    {
        public decimal? pricing { get; set; }
        public Rating rating { get; set; }
    }
}