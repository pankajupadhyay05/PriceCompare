using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;
using PriceCompare.ViewModel;
using PagedList;

namespace PriceCompare.Controllers
{
    public class televisionsController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        private const int PageSize = 9;
        //
        // GET: /mobiles/

        public ActionResult Index(int? page)
        {
            Category catalog = pe.Categories.Where(cat => cat.Id == 11).Single();
            ViewBag.Title = "Compare prices for LCD, LED Televisions in India | Pricepan";
            ViewBag.Description = catalog.MetaDescription;
            int pageIndex = page ?? 1;
            CatProView catProView = new CatProView
            {
                Name = catalog.Name,
                SubCategories = catalog.SubCategories,
                PagedProducts = catalog.Products.OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize)
            };

            var brandlist = catalog.Products.Select(p => p.Brand).Distinct();
            ViewBag.Brands = brandlist;
            return View(catProView);
        }

        public ActionResult brand(string title, int? page)
        {
            Category catalog = pe.Categories.Where(cat => cat.Id == 11).Single();
            ViewBag.Title = char.ToUpper(title[0]) + title.Substring(1) + " Televisions Prices and Reviews in India | Pricepan";
            ViewBag.Description = char.ToUpper(title[0]) + title.Substring(1) + " televisios price comparison from various online retailers at pricepan.";
            ViewBag.BrandName = char.ToUpper(title[0]) + title.Substring(1);
            int pageIndex = page ?? 1;
            CatProView catProView = new CatProView
            {
                Name = catalog.Name,
                SubCategories = catalog.SubCategories,
                PagedProducts = catalog.Products.Where(p => p.Brand.Name.ToLower() == title.ToLower()).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize)
            };
            return View(catProView);
        }

        public ActionResult price(int from, int to, int? page)
        {
            Category catalog = pe.Categories.Where(cat => cat.Id == 11).Single();
            ViewBag.Title = "Televisions from Rs." + from.ToString() + " to Rs." + to.ToString() + " in India | Pricepan";
            ViewBag.Description = "Televisions list under INR " + to.ToString() + " in India";
            int pageIndex = page ?? 1;
            CatProView catProView = new CatProView
            {
                Name = catalog.Name,
                SubCategories = catalog.SubCategories,
                PagedProducts = catalog.Products.Where(p => p.BasePrice >= from & p.BasePrice <= to).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize)
            };

            ViewBag.ToPrice = to;
            ViewBag.FromPrice = from;
            return View(catProView);
        }
    }
}
