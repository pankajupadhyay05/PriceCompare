using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;
using PagedList;

namespace PriceCompare.Controllers
{
    public class newsController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        private const int PageSize = 6;
        //
        // GET: /news-reviews/

        public ActionResult Index(int? page)
        {
            int pageIndex = page ?? 1;
            ViewBag.Title = "Latest news, reviews and happenings from the technology world";
            ViewBag.Description = "Everything covered from the latest mobile launches to smartphone OS updates to latest inventions in the technology space.";
            var lastfourItem = pe.NewsAndReviews.OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
            return View(lastfourItem);
        }

        public ActionResult story(int id, string title)
        {
            NewsAndReview story = pe.NewsAndReviews.Where(p => p.Id == id).First();
            if (story.Title.Replace(", ", " ").Replace(" ", "-").ToLower() != title.Replace(", ", " ").Replace(" ", "-").ToLower())
            {
                return Redirect("/news/" + story.Id + "/" + story.Title.Replace(", ", " ").Replace(" ", "-").ToLower());
            }
            ViewBag.Title = story.Title + " | Pricepan";
            ViewBag.Description = story.MetaDescription;
            ViewBag.LikeType = "article";
            ViewBag.LikeTitle = story.Title;
            ViewBag.LikeUrl = "http://www.pricepan.com/news/" + story.Id + "/" + story.Title.Replace(" ", "-");
            return View(story);
        }
    }
}
