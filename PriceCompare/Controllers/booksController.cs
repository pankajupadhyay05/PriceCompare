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
    public class booksController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        private const int PageSize = 9;
        //
        // GET: /books/

        public ActionResult Index()
        {
            var catsAndSub = pe.Categories.Where(cat => cat.Id == 5).FirstOrDefault();           
            ViewBag.Title = "Compare prices for Books in India | " + "Pricepan";
            ViewBag.Description = "Find and compare prices of the latest and bestselling books in India accross major online retailers in both fiction and non-fiction genre at Pricepan.";
            return View(catsAndSub);
        }

        public ActionResult genre(string title, int?page)
        {
            Category catalog = pe.Categories.Where(cat => cat.Name.ToLower() == title.ToLower()).Single();
            if (catalog.ParentCategoryId == null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.Title = char.ToUpper(title[0])+title.Substring(1) + " books price and review in India | Pricepan";
                ViewBag.Description = char.ToUpper(title[0]) + title.Substring(1) + " books price comparison in India from various online retialers at Pricepan.";
                int pageIndex = page ?? 1;
                CatProView catProView = new CatProView
                {
                    Name = catalog.Name,
                    SubCategories = catalog.SubCategories,
                    PagedProducts = catalog.Products.OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize)
                };
                return View(catProView);
            }
        }

        [ChildActionOnly]
        public ActionResult RecentlyAddedForCat(string category)
        {
            var products = pe.Products.Where(p => p.Category.ParentCategory.Name == category || p.Category.ParentCategory.ParentCategory.Name == category).OrderByDescending(p => p.Id).Take(12);
            return PartialView(products);
        }
    }
}
