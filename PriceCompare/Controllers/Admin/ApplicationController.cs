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
    public class ApplicationController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();

        //
        // GET: /Brand/

        public ViewResult Index()
        {
            var applications = db.Applications.OrderByDescending(n => n.Id).ToList();
            return View(applications);
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
        public ActionResult Create(Application application)
        {
            if (ModelState.IsValid)
            {
                application.ArticleName = application.ArticleName.Replace("&", "and");
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Brand/Edit/5

        public ActionResult Edit(int id)
        {
            Application application = db.Applications.Find(id);
            return View(application);
        }

        //
        // POST: /Brand/Edit/5

        [HttpPost]
        public ActionResult Edit(Application application, int id)
        {
            if (ModelState.IsValid)
            {
                application.Id = id;
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(application);
        }

        //
        // GET: /Brand/Delete/5

        public ActionResult Delete(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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