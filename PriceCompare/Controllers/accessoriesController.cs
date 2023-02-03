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
    public class accessoriesController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        private const int PageSize = 8;
        //
        // GET: /mobiles/

        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;
            PagedList.IPagedList<Product> PagedProducts = pe.Products.Where(cat => cat.CategoryId == 6 || cat.CategoryId ==  8 || cat.CategoryId == 10).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
            ViewBag.Title = "Compare prices for Mobils, laptops and other accessories in India | Pricepan";
            ViewBag.Description = "Mobile accessories, computer accessories and all other types of accessory prices are compared in this section";
            var brandlist = PagedProducts.Select(p => p.Brand).Distinct();
            ViewBag.Brands = brandlist;
            return View(PagedProducts);
        }

        public ActionResult brand(string title, int? page)
        {
            int pageIndex = page ?? 1;
            PagedList.IPagedList<Product> PagedProducts = pe.Products.Where(cat => cat.Category.Id == 6 || cat.Category.Id == 8 || cat.Category.Id == 10).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
            ViewBag.Title = "Compare prices for" + char.ToUpper(title[0]) + title.Substring(1) +"Accessories in India | Pricepan";
            ViewBag.Description = "Price Comparison for mobile accessories, laptop accessories and all other sorts of electronic accessories";
            var brandProducts = PagedProducts.Where(p => p.Brand.Name.ToLower() == title).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
            ViewBag.BrandName = char.ToUpper(title[0]) + title.Substring(1);
            return View(brandProducts);
        }
    }
}
