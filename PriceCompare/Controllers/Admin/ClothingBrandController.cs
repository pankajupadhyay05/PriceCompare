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
    public class ClothingBrandController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();
        //
        // GET: /ClothingBrand/

        public ActionResult Index()
        {
            var clothingBrands = db.ClothingBrands.OrderBy(p => p.Name).ToList();
            return View(clothingBrands);
        }

        //
        // GET: /ClothingBrand/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ClothingBrand/Create

        [HttpPost]
        public ActionResult Create(ClothingBrand clothingBrand)
        {
            if (ModelState.IsValid)
            {
                db.ClothingBrands.Add(clothingBrand);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        //
        // GET: /ClothingBrand/Edit/5
 
        public ActionResult Edit(int id)
        {
            ClothingBrand clothingBrand = db.ClothingBrands.Find(id);
            return View(clothingBrand);
        }

        //
        // POST: /ClothingBrand/Edit/5

        [HttpPost]
        public ActionResult Edit(ClothingBrand clothingBrand, int id)
        {
            if (ModelState.IsValid)
            {
                clothingBrand.Id = id;
                db.Entry(clothingBrand).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clothingBrand);
        }

        //
        // GET: /ClothingBrand/Delete/5

        public ActionResult Delete(int id)
        {
            ClothingBrand clothingBrand = db.ClothingBrands.Find(id);
            db.ClothingBrands.Remove(clothingBrand);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // POST: /ClothingBrand/Delete/5

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
