using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;
using PriceCompare.ViewModel;
using PagedList;

namespace PriceCompare.Controllers
{
    public class homeController : Controller
    {
        private const int PageSize = 8;
        PriceCompareEntity dbStore = new PriceCompareEntity();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult search(string query, int? page)
        {
            int pageIndex = page ?? 1;
            
            string[] searchArray = query.Split(' ');
            int words = searchArray.Length;
            string word1;
            string word2;
            string word3;
            string word4;
            PagedList.IPagedList<Product> PagedProducts;
            switch(words)
            {
                case 1:
                    PagedProducts = dbStore.Products.Where(p => p.Name.Contains(query)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                    break;
                case 2:
                    word1 = searchArray[0];
                    word2 = searchArray[1];
                    PagedProducts = dbStore.Products.Where(p => p.Name.Contains(word1) && p.Name.Contains(word2)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                    break;
                case 3:
                    word1 = searchArray[0];
                    word2 = searchArray[1];
                    word3 = searchArray[2];
                    PagedProducts = dbStore.Products.Where(p => p.Name.Contains(word1) && p.Name.Contains(word2) && p.Name.Contains(word3)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);                  
                    break;
                case 4:
                    word1 = searchArray[0];
                    word2 = searchArray[1];
                    word3 = searchArray[2];
                    word4 = searchArray[3];
                    PagedProducts = dbStore.Products.Where(p => p.Name.Contains(word1) && p.Name.Contains(word2) && p.Name.Contains(word3) && p.Name.Contains(word4)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                    break;
                default:
                    word1 = searchArray[0];
                    word2 = searchArray[1];
                    word3 = searchArray[2];
                    word4 = searchArray[3];
                    PagedProducts = dbStore.Products.Where(p => p.Name.Contains(word1) && p.Name.Contains(word2) && p.Name.Contains(word3) && p.Name.Contains(word4)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                    break;
            }
            ViewBag.query = query;
            if (PagedProducts.TotalItemCount == 0)
            {
                switch (words)
                {
                    case 1:
                        PagedProducts = dbStore.Products.Where(p => p.FullDescription.Contains(query)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                        break;
                    case 2:
                        word1 = searchArray[0];
                        word2 = searchArray[1];
                        PagedProducts = dbStore.Products.Where(p => p.FullDescription.Contains(word1) && p.FullDescription.Contains(word2)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                        break;
                    case 3:
                        word1 = searchArray[0];
                        word2 = searchArray[1];
                        word3 = searchArray[2];
                        PagedProducts = dbStore.Products.Where(p => p.FullDescription.Contains(word1) && p.FullDescription.Contains(word2) && p.FullDescription.Contains(word3)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                        break;
                    case 4:
                        word1 = searchArray[0];
                        word2 = searchArray[1];
                        word3 = searchArray[2];
                        word4 = searchArray[3];
                        PagedProducts = dbStore.Products.Where(p => p.FullDescription.Contains(word1) && p.FullDescription.Contains(word2) && p.FullDescription.Contains(word3) && p.FullDescription.Contains(word4)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                        break;
                    default:
                        word1 = searchArray[0];
                        word2 = searchArray[1];
                        word3 = searchArray[2];
                        word4 = searchArray[3];
                        PagedProducts = dbStore.Products.Where(p => p.FullDescription.Contains(word1) && p.FullDescription.Contains(word2) && p.FullDescription.Contains(word3) && p.FullDescription.Contains(word4)).OrderByDescending(p => p.Id).ToPagedList(pageIndex, PageSize);
                        break;
                }
            }
            ViewBag.proCount = PagedProducts.TotalItemCount;
            return View(PagedProducts);
        }
    }
}
