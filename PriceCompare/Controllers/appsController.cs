using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;
using PagedList;

namespace PriceCompare.Controllers
{
    public class appsController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        private const int PageSize = 6;
        //
        // GET: /apps/

        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;
            ViewBag.Title = "Latest Apps Launch, Apps Updates, Reviews for ios, wp, android and much more | Pricepan";
            ViewBag.Description = "Find latest app launches and app updates for your smartphone's platform be it windows, iOS or android.";
            var lastfourItem = pe.Applications.OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
            return View(lastfourItem);
        }

        public ActionResult ios(int? page)
        {
            int pageIndex = page ?? 1;
            ViewBag.Title = "Apple iStore app launches, updates, reviews and much more | Pricepan";
            ViewBag.Description = "Find all the latest happenings in Apple Store with listing of all new iPhone app launches and app updates for the platform at Pricepan";
            var lastfourItem = pe.Applications.Where(p => p.Tag == "ios").OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
            return View(lastfourItem);
        }

        public ActionResult wp(int? page)
        {
            int pageIndex = page ?? 1;
            ViewBag.Title = "Windows Phone store app launches, app updates, reviews and much more | Pricepan";
            ViewBag.Description = "Find all the latest happenings in Windows Phone Store with listing of all new windows phone app launches and app updates for the platform at Pricepan";
            var lastfourItem = pe.Applications.Where(p => p.Tag == "wp").OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
            return View(lastfourItem);
        }

        public ActionResult android(int? page)
        {
            int pageIndex = page ?? 1;
            ViewBag.Title = "Android App Launches, app updates, reviews and much more | Pricepan";
            ViewBag.Description = "Learn the latest news regarding android app launches, app updates, reviews and much more for your android smartphone only at Pricepan";
            var lastfourItem = pe.Applications.Where(p => p.Tag == "android").OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
            return View(lastfourItem);
        }

        public ActionResult article(int id, string title)
        {
            Application article = pe.Applications.Where(p => p.Id == id).First();
            if (article.ArticleName.Replace(", ", " ").Replace(" ", "-").ToLower() != title.Replace(", ", " ").Replace(" ", "-").ToLower())
            {
                return Redirect("/apps/"+article.Id+"/"+article.ArticleName.Replace(", "," ").Replace(" ","-").ToLower());
            }
            ViewBag.Title = article.ArticleName + " | Pricepan";
            ViewBag.Description = article.MetaDescription;
            ViewBag.LikeType = "article";
            ViewBag.LikeTitle = article.ArticleName;
            ViewBag.LikeUrl = "http://www.pricepan.com/apps/" + article.Id + "/" + article.ArticleName.Replace(" ", "-");
            ViewBag.LikeImage = article.ArticleImage;
            return View(article);
        }
    }
}
