using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PriceCompare.Models;

namespace PriceCompare.ViewModel
{
    public class ProductPicView
    {
        public Product Product { get; set; }
        public Picture Picture { get; set; }
        public RetailerUrls RetailerUrls { get; set; }
    }
}