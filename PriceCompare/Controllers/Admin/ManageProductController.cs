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
    public class ManageProductController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();

        //
        // GET: /Product/

        public ViewResult Index()
        {
            var products = db.Products.OrderByDescending(p =>p.Id).ToList();
            return View(products);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = PriceCompareStatic.groupList(db);
            ViewBag.BrandId = new SelectList(db.Brands.OrderBy(p =>p.Name), "Id", "Name");
            return View();
        }

        //
        // POST: /Category/Create
        [HttpPost]
        public ActionResult Create(ProductPicView productPic)
        {
            if (ModelState.IsValid)
            {
                Picture picture = new Picture();
                picture.PictureUrl = productPic.Picture.PictureUrl;
                db.Pictures.Add(picture);
                productPic.Product.PictureId = picture.Id;
                productPic.Product.Name = productPic.Product.Name.Replace("&", "and");
                db.Products.Add(productPic.Product);
                db.SaveChanges();
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url1))
                {
                    Retailer_Product_Price flipkart = new Retailer_Product_Price();
                    flipkart.RetailerId = 2;
                    flipkart.ProductId = productPic.Product.Id;
                    flipkart.FetchUrl = productPic.RetailerUrls.Url1;
                    if (productPic.RetailerUrls.Url1.Contains("?"))
                        productPic.RetailerUrls.Url1 = productPic.RetailerUrls.Url1 + "&affid=pankajupad";
                    else
                        productPic.RetailerUrls.Url1 = productPic.RetailerUrls.Url1 + "?affid=pankajupad";
                    flipkart.Url = productPic.RetailerUrls.Url1.Replace("www.flipkart.com/", "dl.flipkart.com/dl/");
                    flipkart.Offer = productPic.RetailerUrls.Url1Offer;
                    db.Retailer_Product_Prices.Add(flipkart);
                    Rating rating = new Rating();
                    rating.ProductId = productPic.Product.Id;
                    rating.RatingCount = "0";
                    rating.RatingValue = "0";
                    db.Ratings.Add(rating);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url2))
                {
                    Retailer_Product_Price homeshop = new Retailer_Product_Price();
                    homeshop.RetailerId = 3;
                    homeshop.ProductId = productPic.Product.Id;
                    homeshop.FetchUrl = productPic.RetailerUrls.Url2;
                    productPic.RetailerUrls.Url2 = HttpUtility.UrlEncode(productPic.RetailerUrls.Url2);
                    string standardUrl = "http://track.in.omgpm.com/?AID=480074&MID=331902&PID=9394&CID=3888032&WID=46650&r=";
                    string completeURL = standardUrl + productPic.RetailerUrls.Url2;
                    homeshop.Url = completeURL;
                    homeshop.Offer = productPic.RetailerUrls.Url2Offer;
                    db.Retailer_Product_Prices.Add(homeshop);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url3))
                {
                    Retailer_Product_Price snapdeal = new Retailer_Product_Price();
                    snapdeal.RetailerId = 6;
                    snapdeal.ProductId = productPic.Product.Id;
                    snapdeal.FetchUrl = productPic.RetailerUrls.Url3;
                    if (productPic.RetailerUrls.Url3.Contains("?"))
                        productPic.RetailerUrls.Url3 = productPic.RetailerUrls.Url3 + "&utm_source=aff_prog&utm_campaign=afts&offer_id=17&aff_id=18327";
                    else
                        productPic.RetailerUrls.Url3 = productPic.RetailerUrls.Url3 + "?utm_source=aff_prog&utm_campaign=afts&offer_id=17&aff_id=18327";
                    snapdeal.Url = productPic.RetailerUrls.Url3;
                    snapdeal.Offer = productPic.RetailerUrls.Url3Offer;
                    db.Retailer_Product_Prices.Add(snapdeal);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url4))
                {
                    Retailer_Product_Price amazon = new Retailer_Product_Price();
                    amazon.RetailerId = 14;
                    amazon.ProductId = productPic.Product.Id;
                    amazon.FetchUrl = productPic.RetailerUrls.Url4;
                    if (productPic.RetailerUrls.Url4.Contains("?"))
                        productPic.RetailerUrls.Url4 = productPic.RetailerUrls.Url4 + "&tag=pricepan-21";
                    else
                        productPic.RetailerUrls.Url4 = productPic.RetailerUrls.Url4 + "?tag=pricepan-21";
                    amazon.Url = productPic.RetailerUrls.Url4;
                    amazon.Offer = productPic.RetailerUrls.Url4Offer;
                    db.Retailer_Product_Prices.Add(amazon);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url6))
                {
                    Retailer_Product_Price infibeam = new Retailer_Product_Price();
                    infibeam.RetailerId = 4;
                    infibeam.ProductId = productPic.Product.Id;
                    infibeam.FetchUrl = productPic.RetailerUrls.Url6;
                    if (productPic.RetailerUrls.Url6.Contains("?"))
                        productPic.RetailerUrls.Url6 = productPic.RetailerUrls.Url6 + "&trackId=Pankaj5b2";
                    else
                        productPic.RetailerUrls.Url6 = productPic.RetailerUrls.Url6 + "?trackId=Pankaj5b2";
                    infibeam.Url = productPic.RetailerUrls.Url6;
                    infibeam.Offer = productPic.RetailerUrls.Url6Offer;
                    db.Retailer_Product_Prices.Add(infibeam);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url7))
                {
                    Retailer_Product_Price saholic = new Retailer_Product_Price();
                    saholic.RetailerId = 15;
                    saholic.ProductId = productPic.Product.Id;
                    saholic.FetchUrl = productPic.RetailerUrls.Url7;
                    saholic.Url = productPic.RetailerUrls.Url7;
                    saholic.Offer = productPic.RetailerUrls.Url7Offer;
                    db.Retailer_Product_Prices.Add(saholic);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url8))
                {
                    Retailer_Product_Price indiatimes = new Retailer_Product_Price();
                    indiatimes.RetailerId = 9;
                    indiatimes.ProductId = productPic.Product.Id;
                    indiatimes.FetchUrl = productPic.RetailerUrls.Url8;
                    productPic.RetailerUrls.Url8 = HttpUtility.UrlEncode(productPic.RetailerUrls.Url8);
                    string standardUrl = "http://track.in.omgpm.com/?AID=480074&MID=387804&PID=11256&CID=4188419&WID=46650&r=";
                    string completeURL = standardUrl + productPic.RetailerUrls.Url8;
                    indiatimes.Url = completeURL;
                    indiatimes.Offer = productPic.RetailerUrls.Url8Offer;
                    db.Retailer_Product_Prices.Add(indiatimes);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url9))
                {
                    Retailer_Product_Price bookadda = new Retailer_Product_Price();
                    bookadda.RetailerId = 10;
                    bookadda.ProductId = productPic.Product.Id;
                    bookadda.FetchUrl = productPic.RetailerUrls.Url9;
                    if (productPic.RetailerUrls.Url9.Contains("?"))
                        productPic.RetailerUrls.Url9 = productPic.RetailerUrls.Url9 + "&affiliateID=BA-D09F1AA2";
                    else
                        productPic.RetailerUrls.Url9 = productPic.RetailerUrls.Url9 + "?affiliateID=BA-D09F1AA2";
                    bookadda.Url = productPic.RetailerUrls.Url9;
                    bookadda.Offer = productPic.RetailerUrls.Url9Offer;
                    db.Retailer_Product_Prices.Add(bookadda);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url10))
                {
                    Retailer_Product_Price uread = new Retailer_Product_Price();
                    uread.RetailerId = 11;
                    uread.ProductId = productPic.Product.Id;
                    uread.FetchUrl = productPic.RetailerUrls.Url10;
                    if (productPic.RetailerUrls.Url10.Contains("?"))
                        productPic.RetailerUrls.Url10 = productPic.RetailerUrls.Url10 + "&affid=pricepan";
                    else
                        productPic.RetailerUrls.Url10 = productPic.RetailerUrls.Url10 + "?affid=pricepan";
                    uread.Url = productPic.RetailerUrls.Url10;
                    uread.Offer = productPic.RetailerUrls.Url10Offer;
                    db.Retailer_Product_Prices.Add(uread);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url11))
                {
                    Retailer_Product_Price crossword = new Retailer_Product_Price();
                    crossword.RetailerId = 12;
                    crossword.ProductId = productPic.Product.Id;
                    crossword.FetchUrl = productPic.RetailerUrls.Url11;
                    crossword.Url = productPic.RetailerUrls.Url11;
                    crossword.Offer = productPic.RetailerUrls.Url11Offer;
                    db.Retailer_Product_Prices.Add(crossword);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url12))
                {
                    Retailer_Product_Price landmark = new Retailer_Product_Price();
                    landmark.RetailerId = 13;
                    landmark.ProductId = productPic.Product.Id;
                    landmark.FetchUrl = productPic.RetailerUrls.Url12;
                    landmark.Url = productPic.RetailerUrls.Url12;
                    landmark.Offer = productPic.RetailerUrls.Url12Offer;
                    db.Retailer_Product_Prices.Add(landmark);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url16))
                {
                    Retailer_Product_Price croma = new Retailer_Product_Price();
                    croma.RetailerId = 16;
                    croma.ProductId = productPic.Product.Id;
                    croma.FetchUrl = productPic.RetailerUrls.Url16;
                    productPic.RetailerUrls.Url16 = HttpUtility.UrlEncode(productPic.RetailerUrls.Url16);
                    croma.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + productPic.RetailerUrls.Url16;
                    croma.Offer = productPic.RetailerUrls.Url16Offer;
                    db.Retailer_Product_Prices.Add(croma);
                    db.SaveChanges();
                }
                if (!String.IsNullOrEmpty(productPic.RetailerUrls.Url17))
                {
                    Retailer_Product_Price mobilestore = new Retailer_Product_Price();
                    mobilestore.RetailerId = 17;
                    mobilestore.ProductId = productPic.Product.Id;
                    mobilestore.FetchUrl = productPic.RetailerUrls.Url17;
                    productPic.RetailerUrls.Url17 = HttpUtility.UrlEncode(productPic.RetailerUrls.Url17);
                    mobilestore.Url = "http://linksredirect.com?pub_id=4696CL4460&url=" + productPic.RetailerUrls.Url17;
                    mobilestore.Offer = productPic.RetailerUrls.Url17Offer;
                    db.Retailer_Product_Prices.Add(mobilestore);
                    db.SaveChanges();
                }
                return RedirectToAction("Index"); 
            }
            ViewBag.CategoryId = PriceCompareStatic.groupList(db);
            ViewBag.BrandId = new SelectList(db.Brands.OrderBy(p => p.Name), "Id", "Name");
            return View();
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            Picture picture = db.Pictures.Find(product.PictureId);

            ProductPicView productPic = new ProductPicView();
            productPic.Product = product;
            productPic.Picture = picture;
            ViewBag.CategoryId = PriceCompareStatic.groupList(db);
            ViewBag.BrandId = new SelectList(db.Brands.OrderBy(p => p.Name), "Id", "Name");
            return View(productPic);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(ProductPicView productPic, int id)
        {
            if (ModelState.IsValid)
            {
                productPic.Product.Id = id;
                db.Entry(productPic.Picture).State = EntityState.Modified;
                db.Entry(productPic.Product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = PriceCompareStatic.groupList(db);
            ViewBag.BrandId = new SelectList(db.Brands.OrderBy(p => p.Name), "Id", "Name");
            return View(productPic);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id)
        {
            Product product = db.Products.Find(id);
            Picture picture = db.Pictures.Find(product.PictureId);
            string fileName = picture.PictureUrl;
            string completFileName = Server.MapPath("~" + fileName);
            System.IO.File.Delete(completFileName);
            db.Products.Remove(product);
            db.Pictures.Remove(picture);
            db.SaveChanges();
            return RedirectToAction("Index");
            //Category category = db.Categories.Find(id);
            //return View(category);
        }

        //
        // POST: /Category/Delete/5

        /*[HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }*/

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null && fileData.ContentLength > 0)
            {
                //var fileName = Server.MapPath("~/Content/Images/" + Path.GetFileName(fileData.FileName));
                int pictureCount = 800000;
                pictureCount += db.Pictures.Max(p=>p.Id);
                pictureCount++;
                string extension = Path.GetExtension(fileData.FileName);
                string renamedImage = Server.MapPath("~/Content/Images/Products/product" + pictureCount + extension);
                fileData.SaveAs(renamedImage);
                return Json("/Content/Images/Products/" + Path.GetFileName(renamedImage));
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult Remove(string fileName)
        {
            string completFileName = Server.MapPath("~" + fileName);
            System.IO.File.Delete(completFileName);
            return Json(true);
        }

        public ActionResult fixFetchUrlAmazon()
        {
            var pricings = db.Retailer_Product_Prices.Where(r => r.RetailerId == 14).ToList();
            foreach (var pricing in pricings)
            {
                pricing.FetchUrl = pricing.FetchUrl.Replace("?tag=pricepan-21", "");
                pricing.FetchUrl = pricing.FetchUrl.Replace("&tag=pricepan-21", "");
                db.Entry(pricing).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Index");
        }

        public ActionResult fixFetchUrlFlip()
        {
            var pricings = db.Retailer_Product_Prices.Where(r => r.RetailerId == 2).ToList();
            foreach (var pricing in pricings)
            {
                pricing.FetchUrl = pricing.FetchUrl.Replace("?affid=pankajupad", "");
                pricing.FetchUrl = pricing.FetchUrl.Replace("&affid=pankajupad", "");
                db.Entry(pricing).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View("Index");
        }
    }
}