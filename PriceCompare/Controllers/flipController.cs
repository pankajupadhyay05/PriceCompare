using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;

namespace PriceCompare.Controllers
{
    public class flipController : Controller
    {
        //
        // GET: /flip/

        public ActionResult Index()
        {
            flipProductAPI flip = new flipProductAPI();

            // This is a category url
            string Category = "https://affiliate-api.flipkart.net/affiliate/feeds/pankajupad/category/t06-r3o.json?expiresAt=1455383749170&sig=1723e3b27afc9af05e2baec3b6393dfe";
            ViewBag.ProductList = flip.ProductList(Category);
            ViewBag.Count = flip.ProductList(Category).Count();
            return View();
        }

    }
}
