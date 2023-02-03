using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;

namespace PriceCompare.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class PricingController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();

        //
        // GET: /Pricing/

        public ViewResult Index()
        {
            var retailer_product_prices = db.Retailer_Product_Prices.OrderByDescending(p => p.Id).ToList();
            return View(retailer_product_prices);
        }

        //
        // GET: /Pricing/Create

        public ActionResult Create(string productId)
        {
            ViewBag.RetailerId = new SelectList(db.Retailers, "Id", "Name");
            return View();
        } 

        //
        // POST: /Pricing/Create

        [HttpPost]
        public ActionResult Create(Retailer_Product_Price retailer_product_price)
        {
            if (ModelState.IsValid)
            {
                /*if (retailer_product_price.RetailerId == 1)
                {
                    if (retailer_product_price.Url.Contains("?"))
                        retailer_product_price.Url = retailer_product_price.Url + "&affid=pankajupad";
                    else
                        retailer_product_price.Url = retailer_product_price.Url + "?affid=pankajupad";
                }*/
                retailer_product_price.FetchUrl = retailer_product_price.Url;
                if (retailer_product_price.RetailerId == 16 || retailer_product_price.RetailerId == 17)
                    retailer_product_price.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(retailer_product_price.FetchUrl);
                db.Retailer_Product_Prices.Add(retailer_product_price);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            return View(retailer_product_price);
        }
        
        //
        // GET: /Pricing/Edit/5
 
        public ActionResult Edit(int id)
        {
            Retailer_Product_Price retailer_product_price = db.Retailer_Product_Prices.Find(id);
            ViewBag.RetailerList = new SelectList(db.Retailers, "Id", "Name");
            return View(retailer_product_price);
        }

        //
        // POST: /Pricing/Edit/5

        [HttpPost]
        public ActionResult Edit(Retailer_Product_Price retailer_product_price, int id)
        {
            if (ModelState.IsValid)
            {
                retailer_product_price.Id = id;
                if (retailer_product_price.RetailerId == 16 || retailer_product_price.RetailerId == 17)
                {
                    retailer_product_price.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + HttpUtility.UrlEncode(retailer_product_price.FetchUrl);
                }
                db.Entry(retailer_product_price).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RetailerId = new SelectList(db.Retailers, "Id", "Name");
            return View(retailer_product_price);
        }

        //
        // GET: /Pricing/Delete/5
 
        public ActionResult Delete(int id)
        {
            Retailer_Product_Price retailer_product_price = db.Retailer_Product_Prices.Find(id);
            db.Retailer_Product_Prices.Remove(retailer_product_price);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult UpdateSome()
        {
            return View();
        }
    }
}