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
    public class MensController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();
        //
        // GET: /Mens/

        public ActionResult Index()
        {
            var mens = db.mens.OrderByDescending(p => p.Id).ToList();
            return View(mens);
        }

        //
        // GET: /Mens/Create

        public ActionResult Create()
        {
            ViewBag.BrandList = new SelectList(db.ClothingBrands.OrderBy(p => p.Name), "Id", "Name");
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            return View();
        } 

        //
        // POST: /Mens/Create

        [HttpPost]
        public ActionResult Create(men men)
        {
            if (ModelState.IsValid)
            {
                men.Url = men.FetchUrl;
                men.Name = men.Name.Replace("&", "and");
                men.Name = men.Name.Replace("/", "-");
                if (men.RetailerId == 2)
                {
                    if (men.Url.Contains("?"))
                        men.Url = men.FetchUrl + "&affid=pankajupad";
                    else
                        men.Url = men.FetchUrl + "?affid=pankajupad";
                    men.Url = men.Url.Replace("www.flipkart.com/", "dl.flipkart.com/dl/");
                }
                if (men.RetailerId == 14)
                {
                    if (men.Url.Contains("?"))
                        men.Url = men.FetchUrl + "&tag=pricepan-21";
                    else
                        men.Url = men.FetchUrl + "?tag=pricepan-21";
                }
                if (men.RetailerId == 18)
                {
                    men.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(men.FetchUrl);
                }
                if (men.RetailerId == 19)
                {
                    men.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(men.FetchUrl);
                }
                db.mens.Add(men);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            ViewBag.BrandList = new SelectList(db.ClothingBrands.OrderBy(p => p.Name), "Id", "Name");
            return View();
        }
        
        //
        // GET: /Mens/Edit/5
 
        public ActionResult Edit(int id)
        {
            men men = db.mens.Find(id);
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            ViewBag.BrandList = new SelectList(db.ClothingBrands.OrderBy(p => p.Name), "Id", "Name");
            return View(men);
        }

        //
        // POST: /Mens/Edit/5

        [HttpPost]
        public ActionResult Edit(men men, int id)
        {
            if (ModelState.IsValid)
            {
                men.Id = id;
                men.Name = men.Name.Replace("&", "and");
                men.Name = men.Name.Replace("/", "-");
                men.Url = men.FetchUrl;
                if (men.RetailerId == 2)
                {
                    men.FetchUrl = men.FetchUrl.Replace("&affid=pankajupad", "");
                    men.FetchUrl = men.FetchUrl.Replace("?affid=pankajupad", "");
                    men.FetchUrl = men.FetchUrl.Replace("dl.flipkart.com/dl/", "www.flipkart.com");
                    men.Url = men.FetchUrl;
                    if (men.Url.Contains("?"))
                        men.Url = men.FetchUrl + "&affid=pankajupad";
                    else
                        men.Url = men.FetchUrl + "?affid=pankajupad";
                    men.Url = men.Url.Replace("www.flipkart.com/", "dl.flipkart.com/dl/");
                }
                if (men.RetailerId == 14)
                {
                    men.FetchUrl = men.FetchUrl.Replace("&tag=pricepan-21", "");
                    men.FetchUrl = men.FetchUrl.Replace("?tag=pricepan-21", "");
                    men.Url = men.FetchUrl;
                    if (men.Url.Contains("?"))
                        men.Url = men.FetchUrl + "&tag=pricepan-21";
                    else
                        men.Url = men.FetchUrl + "?tag=pricepan-21";
                }
                if (men.RetailerId == 18)
                {
                    men.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(men.FetchUrl);
                }
                if (men.RetailerId == 19)
                {
                    men.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(men.FetchUrl);
                }
                db.Entry(men).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            ViewBag.BrandList = new SelectList(db.ClothingBrands.OrderBy(p => p.Name), "Id", "Name");
            return View(men);
        }

        //
        // GET: /Mens/Delete/5

        public ActionResult Delete(int id)
        {
            men men = db.mens.Find(id);
            db.mens.Remove(men);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /Mens/Delete/5

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
    }
}
