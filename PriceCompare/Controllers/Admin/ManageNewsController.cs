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
    public class ManageNewsController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();

        //
        // GET: /Brand/

        public ViewResult Index()
        {
            var news = db.NewsAndReviews.OrderByDescending(n => n.Id).ToList();
            return View(news);
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
        public ActionResult Create(NewsAndReview news)
        {
            if (ModelState.IsValid)
            {
                news.Title = news.Title.Replace("&", "and");
                db.NewsAndReviews.Add(news);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Brand/Edit/5

        public ActionResult Edit(int id)
        {
            NewsAndReview news = db.NewsAndReviews.Find(id);
            return View(news);
        }

        //
        // POST: /Brand/Edit/5

        [HttpPost]
        public ActionResult Edit(NewsAndReview news, int id)
        {
            if (ModelState.IsValid)
            {
                news.Id = id;
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        //
        // GET: /Brand/Delete/5

        public ActionResult Delete(int id)
        {
            NewsAndReview news = db.NewsAndReviews.Find(id);
            db.NewsAndReviews.Remove(news);
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