using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;

namespace PriceCompare.Controllers
{
    public class StoreController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        //
        // GET: /Store/

        [ChildActionOnly]
        public ActionResult RecentSearches()
        {
            var recentSearches = pe.Terms.OrderByDescending(t => t.Id).Take(5);
            return PartialView(recentSearches);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAdded()
        {
            var products = pe.Products.OrderByDescending(p => p.Id).Take(5);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedSlide()
        {
            var products = pe.Products.OrderByDescending(p => p.Id).Take(3);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedForCat(string category)
        {
            var products = pe.Products.Where( p => p.Category.ParentCategory.Name == category || p.Category.ParentCategory.ParentCategory.Name == category).OrderByDescending(p => p.Id).Take(12);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedForBooks() // fiction
        {
            var products = pe.Products.Where(p => p.Category.Id == 12).OrderByDescending(p => p.Id).Take(4);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedForComputers() // laptop
        {
            var products = pe.Products.Where(p => p.Category.Id == 7).OrderByDescending(p => p.Id).Take(4);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedForMobiles() // handsets
        {
            var products = pe.Products.Where(p => p.Category.Id == 9).OrderByDescending(p => p.Id).Take(4);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedForElectronics() // tvs
        {
            var products = pe.Products.Where(p => p.Category.Id == 11).OrderByDescending(p => p.Id).Take(4);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedNews()
        {
            var products = pe.NewsAndReviews.OrderByDescending(p => p.Id).Take(3);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedApps()
        {
            var products = pe.Applications.OrderByDescending(p => p.Id).Take(3);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RelatedProducts(string category) // laptop
        {
            var products = pe.Products.Where(p => p.Category.Name == category).OrderByDescending(p => p.Id).Take(4);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult PopularProducts() // Popular Products
        {
            var products = pe.Products.OrderByDescending(p => p.lastFetch).Take(12);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RelatedProductsForMen(string category) // laptop
        {
            var products = pe.mens.Where(p => p.Type == category).OrderByDescending(p => p.Id).Take(3);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RelatedProductsForWomen(string category) // laptop
        {
            var products = pe.womens.Where(p => p.Type == category).OrderByDescending(p => p.Id).Take(3);
            return PartialView(products);
        }

        [ChildActionOnly]
        public ActionResult RecentForWomen() // Recent Women Clothes
        {
            var products = pe.womens.OrderByDescending(p => p.Id).Take(12);
            return PartialView(products);
        }
    }
}
