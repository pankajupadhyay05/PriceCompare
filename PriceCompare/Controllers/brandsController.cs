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
    public class brandsController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        private const int PageSize = 8;
        //
        // GET: /Brands/

        public ActionResult name(string id, string category, int?page)
        {
            category = category.Replace("-", " ");
            id = id.Replace("-", " ");
            Category catalog = pe.Categories.Where(cat => cat.Name == category).Single();
            if (catalog.ParentCategoryId == null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.ParentCategory = catalog.ParentCategory.Name;
                ViewBag.Title = catalog.Name + " | " + "Pricepan";
                ViewBag.Description = catalog.MetaKeywords;
                ViewBag.Keywords = catalog.Name + ", Compare and Buy " + catalog.Name + " via Pricepan";
                if (catalog.ParentCategory.ParentCategoryId != null)
                {
                    ViewBag.ParentOfParent = catalog.ParentCategory.ParentCategory.Name;
                }
                int pageIndex = page ?? 1;
                CatProView catProView = new CatProView
                                        {
                                            Name = catalog.Name,
                                            SubCategories = catalog.SubCategories,
                                            PagedProducts = catalog.Products.Where(p => p.Brand.Name == id).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize)
                                        };
                ViewBag.BrandName = id;
                return View(catProView);
            }
        }

        /*public ActionResult Index(string id)
        {
            id = id.Replace("-", " ");
            Category catalog = pe.Categories.Where(cat => cat.Name == id).Single();
            if (catalog.ParentCategoryId == null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.ParentCategory = catalog.ParentCategory.Name;
                ViewBag.Title = catalog.Name + " | " + "Pricepan";
                ViewBag.Description = catalog.MetaKeywords;
                ViewBag.Keywords = catalog.Name + ", Compare and Buy " + catalog.Name + " via Pricepan";
                if (catalog.ParentCategory.ParentCategoryId != null)
                {
                    ViewBag.ParentOfParent = catalog.ParentCategory.ParentCategory.Name;
                }
                int pageIndex = page ?? 1;
                CatProView catProView = new CatProView
                {
                    Name = catalog.Name,
                    SubCategories = catalog.SubCategories,
                    PagedProducts = catalog.Products.Where(p => p.Brand.Name == id).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize)
                };
                ViewBag.BrandName = id;
                return View(catProView);
            }
        }*/
    }
}
