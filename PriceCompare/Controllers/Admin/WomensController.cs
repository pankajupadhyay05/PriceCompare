using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;
using PriceCompare.ViewModel;
using System.IO;

namespace PriceCompare.Controllers.Admin
{
    [Authorize(Roles = "Administrator")]
    public class WomensController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();
        //
        // GET: /Womens/

        public ActionResult Index()
        {
            var womens = db.womens.OrderByDescending(p => p.Id).ToList();
            return View(womens);
        }

        //
        // GET: /Womens/Create

        public ActionResult Create()
        {
            ViewBag.BrandList = new SelectList(db.ClothingBrands.OrderBy(p => p.Name), "Id", "Name");
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            return View();
        } 

        //
        // POST: /Womens/Create

        [HttpPost]
        public ActionResult Create(women women)
        {
            if (ModelState.IsValid)
            {
                women.Url = women.FetchUrl;
                women.Name = women.Name.Replace("&", "and");
                women.Name = women.Name.Replace("/", "-");
                if (women.RetailerId == 2)
                {
                    if (women.Url.Contains("?"))
                        women.Url = women.FetchUrl + "&affid=pankajupad";
                    else
                        women.Url = women.FetchUrl + "?affid=pankajupad";
                    women.Url = women.Url.Replace("www.flipkart.com/", "dl.flipkart.com/dl/");
                }
                if (women.RetailerId == 14)
                {
                    if (women.Url.Contains("?"))
                        women.Url = women.FetchUrl + "&tag=pricepan-21";
                    else
                        women.Url = women.FetchUrl + "?tag=pricepan-21";
                }
                if (women.RetailerId == 18)
                {
                    women.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(women.FetchUrl);
                }
                if (women.RetailerId == 19)
                {
                    women.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(women.FetchUrl);
                }
                db.womens.Add(women);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            ViewBag.BrandList = new SelectList(db.ClothingBrands.OrderBy(p => p.Name), "Id", "Name");
            return View();
        }
        
        //
        // GET: /Womens/Edit/5
 
        public ActionResult Edit(int id)
        {
            women women = db.womens.Find(id);
            ViewBag.BrandList = new SelectList(db.ClothingBrands.OrderBy(p => p.Name), "Id", "Name");
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            return View(women);
        }

        //
        // POST: /Womens/Edit/5

        [HttpPost]
        public ActionResult Edit(women women, int id)
        {
            if (ModelState.IsValid)
            {
                women.Id = id;
                women.Name = women.Name.Replace("&", "and");
                women.Name = women.Name.Replace("/", "-");
                women.Url = women.FetchUrl;
                if (women.RetailerId == 2)
                {
                    women.FetchUrl = women.FetchUrl.Replace("&affid=pankajupad", "");
                    women.FetchUrl = women.FetchUrl.Replace("?affid=pankajupad", "");
                    women.FetchUrl = women.FetchUrl.Replace("dl.flipkart.com/dl/", "www.flipkart.com");
                    women.Url = women.FetchUrl;
                    if (women.Url.Contains("?"))
                        women.Url = women.FetchUrl + "&affid=pankajupad";
                    else
                        women.Url = women.FetchUrl + "?affid=pankajupad";
                    women.Url = women.Url.Replace("www.flipkart.com/", "dl.flipkart.com/dl/");
                }
                if (women.RetailerId == 14)
                {
                    women.FetchUrl = women.FetchUrl.Replace("&tag=pricepan-21", "");
                    women.FetchUrl = women.FetchUrl.Replace("?tag=pricepan-21", "");
                    women.Url = women.FetchUrl;
                    if (women.Url.Contains("?"))
                        women.Url = women.FetchUrl + "&tag=pricepan-21";
                    else
                        women.Url = women.FetchUrl + "?tag=pricepan-21";
                }
                if (women.RetailerId == 18)
                {
                    women.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(women.FetchUrl);
                }
                if (women.RetailerId == 19)
                {
                    women.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(women.FetchUrl);
                }
                db.Entry(women).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            ViewBag.BrandList = new SelectList(db.ClothingBrands.OrderBy(p => p.Name), "Id", "Name");
            return View(women);
        }

        //
        // GET: /Womens/Delete/5
 
        public ActionResult Delete(int id)
        {
            women women = db.womens.Find(id);
            db.womens.Remove(women);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult urlIssueMyntra()
        {
            var products = db.womens.Where(p => p.RetailerId == 19).ToList();
            foreach (var product in products)
            {
                product.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(product.FetchUrl);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Index");
        }

        public ActionResult urlIssueMyntraMen()
        {
            var products = db.mens.Where(p => p.RetailerId == 19).ToList();
            foreach (var product in products)
            {
                product.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(product.FetchUrl);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Index");
        }

        //
        // POST: /Womens/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult urlIssue22()
        {
            var products = db.mens.ToList();
            foreach (var product in products)
            {
                product.FetchUrl = product.FetchUrl.Replace(".com", ".com/");
                product.FetchUrl = product.FetchUrl.Replace("//", "/");
                product.FetchUrl = product.FetchUrl.Replace("http:/w", "http://w");
                if (product.RetailerId == 2)
                {
                    product.FetchUrl = product.FetchUrl.Replace("&affid=pankajupad", "");
                    product.FetchUrl = product.FetchUrl.Replace("?affid=pankajupad", "");
                    product.FetchUrl = product.FetchUrl.Replace("dl.flipkart.com/dl/", "www.flipkart.com/");
                    if (product.FetchUrl.Contains("?"))
                        product.Url = product.FetchUrl + "&affid=pankajupad";
                    else
                        product.Url = product.FetchUrl + "?affid=pankajupad";
                    product.Url = product.Url.Replace("www.flipkart.com/", "dl.flipkart.com/dl/");
                }
                if (product.RetailerId == 14)
                {
                    product.FetchUrl = product.FetchUrl.Replace("&tag=pricepan-21", "");
                    product.FetchUrl = product.FetchUrl.Replace("?tag=pricepan-21", "");
                    if (product.FetchUrl.Contains("?"))
                        product.Url = product.FetchUrl + "&tag=pricepan-21";
                    else
                        product.Url = product.FetchUrl + "?tag=pricepan-21";
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Index");
        }
    }
}
