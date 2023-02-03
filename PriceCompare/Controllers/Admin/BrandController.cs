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

namespace PriceCompare.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BrandController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();

        //
        // GET: /Brand/

        public ViewResult Index()
        {
            var brands = db.Brands.OrderBy(p =>p.Name).ToList();
            return View(brands);
        }

        //
        // GET: /Brand/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Brand/Create

        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        //
        // GET: /Brand/Edit/5
 
        public ActionResult Edit(int id)
        {
            Brand brand = db.Brands.Find(id);
            return View(brand);
        }

        //
        // POST: /Brand/Edit/5

        [HttpPost]
        public ActionResult Edit(Brand brand, int id)
        {
            if (ModelState.IsValid)
            {
                brand.Id = id;
                db.Entry(brand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        //
        // GET: /Brand/Delete/5
 
        public ActionResult Delete(int id)
        {
            Brand brand = db.Brands.Find(id);
            db.Brands.Remove(brand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}