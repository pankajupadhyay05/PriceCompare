using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;
using PriceCompare.ViewModel;
using PagedList;

namespace PriceCompare.Controllers
{
    public class wardrobeController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        private const int PageSize = 12;
        //
        // GET: /wardrobe/

        public ActionResult Index()
        {
            ViewBag.Title = "Sorted Wardrobe Collection for Men and Women | Pricepan";
            ViewBag.Description = "Pricepan Wardrobe is a sorted clothes collection of best styles and popular products from various online reailers in India.";
            return View();
        }

        public ActionResult mens(string id, int? page)
        {
            ViewBag.Title = "Clothing for Men in India | Pricepan";
            ViewBag.Description = "Choose from the best sorted collection of men's clothing at Pricepan. A one stop solution for all your fashion needs.";
            if (id != null && id != "")
            {
                ViewBag.Title = char.ToUpper(id[0]) + id.Substring(1) + " Collection for Men at Pricepan";
                ViewBag.Description = "Browse through the already sorted " + char.ToUpper(id[0]) + id.Substring(1) + " Collection from various retailers in India";
                ViewBag.Keywords = id + " for men, mens " + id + ", clothes for men" ;
                ViewBag.TypeName = char.ToUpper(id[0]) + id.Substring(1);
                int pageIndex = page ?? 1;
                var pagedClothes = pe.mens.Where(c => c.Type == id).OrderByDescending(c => c.Id).ToPagedList(pageIndex, PageSize);
                return View("list", pagedClothes);
            }
            return View();
        }

        public ActionResult womens(string id, int? page)
        {
            ViewBag.Title = "Clothing for Women in India | Pricepan";
            ViewBag.Description = "Choose from the best sorted collection of women's clothing at Pricepan. A one stop solution for all your fashion needs.";
            if (id != null && id != "")
            {
                ViewBag.Title = char.ToUpper(id[0]) + id.Substring(1) + " Collection for Women at Pricepan";
                ViewBag.Description = "Browse through the already sorted " + char.ToUpper(id[0]) + id.Substring(1) + " Collection from various retailers in India";
                ViewBag.Keywords = id + " for women, womens " + id + ", clothes for women";
                ViewBag.TypeName = char.ToUpper(id[0]) + id.Substring(1);
                int pageIndex = page ?? 1;
                var pagedClothes = pe.womens.Where(c => c.Type == id).OrderByDescending(c => c.Id).ToPagedList(pageIndex, PageSize);
                return View("list2", pagedClothes);
            }
            return View();
        }

        public ActionResult item(int id)
        {
            var cloth = pe.mens.Where(c => c.Id == id).FirstOrDefault();
            if (cloth == null)
            {
                return View("NoSuchRootCat");
            }
            if (cloth.RetailerId == 6)
            {
                cloth.Price = PriceCompareStatic.GetSpanFromWebSite(cloth.FetchUrl, "//span[@itemprop='price']");
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 14)
            {
                cloth.Price = PriceCompareStatic.GetMashedUpCode(cloth.FetchUrl, "//span[@class='a-size-medium a-color-price']");
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 2)
            {
                flipkartWithRating flipWithRating = new flipkartWithRating();
                flipWithRating = PriceCompareStatic.GetFlipKart(cloth.FetchUrl, "//span[@class='selling-price omniture-field']");
                cloth.Price = flipWithRating.pricing;
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 18)
            {
                cloth.Price = PriceCompareStatic.GetSpanFromWebSite(cloth.FetchUrl, "//div[@id='product_special_price']");
                if (cloth.Price == null)
                {
                    cloth.Price = PriceCompareStatic.GetSpanFromWebSite(cloth.FetchUrl, "//div[@id='product_price']");
                    if (cloth.Price == null)
                    {
                        cloth.Instock = "no";
                    }
                    else
                    {
                        cloth.Instock = "yes";
                    }
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 19)
            {
                cloth.Price = PriceCompareStatic.GetMashedUpCode(cloth.FetchUrl, "//div[@class='price']");
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 20)
            {
                cloth.Price = PriceCompareStatic.GetSpanFromWebSite(cloth.FetchUrl, "//span[@itemprop='price']");
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            ViewBag.Title = cloth.Name + " | Men's Clothing | Pricepan";
            ViewBag.Description = cloth.Name + " by " + cloth.ClothingBrand.Name + " available on " + cloth.Retailer.Name + " at best possible price";
            ViewBag.TypeName = char.ToUpper(cloth.Type[0]) + cloth.Type.Substring(1);
            pe.SaveChanges();
            return View(cloth);
        }

        public ActionResult product(int id)
        {
            var cloth = pe.womens.Where(c => c.Id == id).FirstOrDefault();
            if (cloth == null)
            {
                return View("NoSuchRootCat");
            }
            if (cloth.RetailerId == 6)
            {
                cloth.Price = PriceCompareStatic.GetSpanFromWebSite(cloth.FetchUrl, "//span[@itemprop='price']");
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 14)
            {
                cloth.Price = PriceCompareStatic.GetMashedUpCode(cloth.FetchUrl, "//span[@class='a-size-medium a-color-price']");
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 2)
            {
                flipkartWithRating flipWithRating = new flipkartWithRating();
                flipWithRating = PriceCompareStatic.GetFlipKart(cloth.FetchUrl, "//span[@class='selling-price omniture-field']");
                cloth.Price = flipWithRating.pricing;
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 18)
            {
                cloth.Price = PriceCompareStatic.GetSpanFromWebSite(cloth.FetchUrl, "//div[@id='product_special_price']");
                if (cloth.Price == null)
                {
                    cloth.Price = PriceCompareStatic.GetSpanFromWebSite(cloth.FetchUrl, "//div[@id='product_price']");
                    if (cloth.Price == null)
                    {
                        cloth.Instock = "no";
                    }
                    else
                    {
                        cloth.Instock = "yes";
                    }
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 19)
            {
                cloth.Price = PriceCompareStatic.GetMashedUpCode(cloth.FetchUrl, "//div[@class='price']");
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            if (cloth.RetailerId == 20)
            {
                cloth.Price = PriceCompareStatic.GetSpanFromWebSite(cloth.FetchUrl, "//span[@itemprop='price']");
                if (cloth.Price == null)
                {
                    cloth.Instock = "no";
                }
                else
                {
                    cloth.Instock = "yes";
                }
                pe.Entry(cloth).State = EntityState.Modified;
            }
            ViewBag.Title = cloth.Name + " | Women's Clothing | Pricepan";
            ViewBag.Description = cloth.Name + " by " + cloth.ClothingBrand.Name + " available on " + cloth.Retailer.Name + " at best possible price";
            ViewBag.TypeName = char.ToUpper(cloth.Type[0]) + cloth.Type.Substring(1);
            pe.SaveChanges();
            return View(cloth);
        }
    }
}
