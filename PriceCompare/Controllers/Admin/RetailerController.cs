using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;
using System.IO;
using PriceCompare.ViewModel;

namespace PriceCompare.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RetailerController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();

        //
        // GET: /Retailer/

        public ViewResult Index()
        {
            var retailers = db.Retailers.OrderByDescending(r => r.Id).ToList();
            return View(retailers);
        }

        //
        // GET: /Retailer/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Retailer/Create

        [HttpPost]
        public ActionResult Create(RetailerPicView retailerPic)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(retailerPic.Picture.PictureUrl))
                {
                    Picture picture = new Picture();
                    picture.PictureUrl = retailerPic.Picture.PictureUrl;
                    db.Pictures.Add(picture);
                    retailerPic.Retailer.PictureId = picture.Id;
                }
                db.Retailers.Add(retailerPic.Retailer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        //
        // GET: /Retailer/Edit/5
 
        public ActionResult Edit(int id)
        {
            Retailer retailer = db.Retailers.Find(id);
            Picture picture = new Picture();
            int? picId = retailer.PictureId;
            if (picId != null)
            {
                picture = db.Pictures.Find(picId);
            }

            RetailerPicView retailerPic = new RetailerPicView();
            retailerPic.Retailer = retailer;
            retailerPic.Picture = picture;
            return View(retailerPic);
        }

        //
        // POST: /Retailer/Edit/5

        [HttpPost]
        public ActionResult Edit(RetailerPicView retailerPic, int id)
        {
            if (ModelState.IsValid)
            {
                retailerPic.Retailer.Id = id;
                if (String.IsNullOrEmpty(retailerPic.Picture.PictureUrl))
                {
                    if (retailerPic.Retailer.PictureId != null)
                    {
                        Picture picture = db.Pictures.Find(retailerPic.Retailer.PictureId);
                        db.Pictures.Remove(picture);
                        retailerPic.Retailer.PictureId = null;
                    }
                }
                else
                {
                    if (retailerPic.Retailer.PictureId == null)
                    {
                        Picture picture = new Picture();
                        picture.PictureUrl = retailerPic.Picture.PictureUrl;
                        db.Pictures.Add(picture);
                        retailerPic.Retailer.PictureId = picture.Id;
                    }
                    else
                    {
                        db.Entry(retailerPic.Picture).State = EntityState.Modified;
                    }
                }
                db.Entry(retailerPic.Retailer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(retailerPic);
        }

        //
        // GET: /Retailer/Delete/5
 
        public ActionResult Delete(int id)
        {
            Retailer retailer = db.Retailers.Find(id);
            int? picId = retailer.PictureId;
            db.Retailers.Remove(retailer);
            if (picId != null)
            {
                Picture picture = db.Pictures.Find(picId);
                string fileName = picture.PictureUrl;
                string completFileName = Server.MapPath("~" + fileName);
                System.IO.File.Delete(completFileName);
                db.Pictures.Remove(picture);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
                pictureCount += db.Pictures.Count();
                string extension = Path.GetExtension(fileData.FileName);
                string renamedImage = Server.MapPath("~/Content/Images/Retailers/retailer" + pictureCount + extension);
                fileData.SaveAs(renamedImage);
                return Json("/Content/Images/Retailers/" + Path.GetFileName(renamedImage));
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


        /*************** Update code for existing URLS for OMG sites *******************************/

        public ActionResult UpdateflipUrl()
        {
            var flipUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 2).ToList();
            foreach (var url in flipUrls)
            {
                if (url.FetchUrl != null || url.FetchUrl != "")
                {
                    url.Url = url.FetchUrl.Replace("www.flipkart.com/", "dl.flipkart.com/dl/");
                    if (url.FetchUrl.Contains("?"))
                        url.Url = url.Url + "&affid=pankajupad";
                    else
                        url.Url = url.Url + "?affid=pankajupad";
                    db.Entry(url).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return View();
        }

        public ActionResult UpdateSnapUrl()
        {
            var SnapUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 6).ToList();
            foreach (var url in SnapUrls)
            {
                if (url.FetchUrl.Contains("?"))
                    url.Url = url.FetchUrl + "&utm_source=aff_prog&utm_campaign=afts&offer_id=17&aff_id=18327";
                else
                    url.Url = url.FetchUrl + "?utm_source=aff_prog&utm_campaign=afts&offer_id=17&aff_id=18327";
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            } 
            return View();
        }

        public ActionResult UpdateUreadUrl()
        {
            var SnapUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 11).ToList();
            foreach (var url in SnapUrls)
            {
                if (url.FetchUrl.Contains("?"))
                    url.Url = url.FetchUrl + "&affid=pricepan";
                else
                    url.Url = url.FetchUrl + "?affid=pricepan";
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult UpdateBookAddaUrl()
        {
            var SnapUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 10).ToList();
            foreach (var url in SnapUrls)
            {
                if (url.FetchUrl.Contains("?"))
                    url.Url = url.FetchUrl + "&affiliateID=BA-D09F1AA2";
                else
                    url.Url = url.FetchUrl + "?affiliateID=BA-D09F1AA2";
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult UpdateHomeShopUrl()
        {
            var homeshopUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 3).ToList();
            foreach (var url in homeshopUrls)
            {
                url.FetchUrl = url.FetchUrl.Replace("&utm_source=affilliate&utm_medium=OMG&utm_campaign=text", "");
                url.FetchUrl = url.FetchUrl.Replace("?utm_source=affilliate&utm_medium=OMG&utm_campaign=text", "");
                url.Url = url.FetchUrl;
                url.Url = HttpUtility.UrlEncode(url.Url);
                string standardUrl = "http://track.in.omgpm.com/?AID=480074&MID=331902&PID=9394&CID=3888032&WID=46650&r=";
                string completeURL = standardUrl + url.Url;
                url.Url = completeURL;
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult UpdateIndiaShoppingTimes()
        {
            var indiUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 9).ToList();
            foreach (var url in indiUrls)
            {
                url.FetchUrl = url.FetchUrl.Replace("?utm_source=omg&utm_medium=Affiliate&utm_campaign=productname_date", "");
                url.FetchUrl = url.FetchUrl.Replace("&utm_source=omg&utm_medium=Affiliate&utm_campaign=productname_date", "");
                url.Url = url.FetchUrl;
                url.Url = HttpUtility.UrlEncode(url.Url);
                string standardUrl = "http://track.in.omgpm.com/?AID=480074&MID=387804&PID=11256&CID=4188419&WID=46650&r=";
                string completeURL = standardUrl + url.Url;
                url.Url = completeURL;
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult IndiaTimes()
        {
            var indiaTimesUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 9).ToList();
            foreach (var url in indiaTimesUrls)
            {
                if (url.Url.Contains("?"))
                    url.Url = url.Url + "&utm_source=affiliate&utm_medium=OMG&utm_campaign=text";
                else
                    url.Url = url.Url + "?utm_source=affiliate&utm_medium=OMG&utm_campaign=text";
                url.Url = HttpUtility.UrlEncode(url.Url);
                string standardUrl = "http://track.in.omgpm.com/?AID=480074&MID=387804&PID=10091&CID=3888029&WID=46650&redirect=";
                string completeURL = standardUrl + url.Url;
                url.Url = completeURL;
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult SnapDeal()
        {
            var snapDealUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 6).ToList();
            foreach (var url in snapDealUrls)
            {
                url.FetchUrl = url.FetchUrl.Replace("?utm_source=omegatxn&utm_campaign=afts", "");
                url.FetchUrl = url.FetchUrl.Replace("&utm_source=omegatxn&utm_campaign=afts", "");
                url.Url = url.FetchUrl;
                url.Url = HttpUtility.UrlEncode(url.Url);
                string standardUrl = "http://track.in.omgpm.com/?AID=480074&MID=159526&PID=8053&CID=3888033&WID=46650&r=";
                string completeURL = standardUrl + url.Url;
                url.Url = completeURL;
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }


        /************************ Update code for filling fetch colums ***********************/

        public ActionResult FetchFlip()
        {
            var flipUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 2).ToList();
            foreach (var url in flipUrls)
            {
                url.FetchUrl = url.Url.Replace("&affid=pankajupad", "");
                url.FetchUrl = url.FetchUrl.Replace("?affid=pankajupad", "");
                url.Url = url.FetchUrl;
                if (url.FetchUrl.Contains("?"))
                    url.Url = url.FetchUrl + "&affid=pankajupad";
                else
                    url.Url = url.FetchUrl + "?affid=pankajupad";
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult FetchInfi()
        {
            var infiUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 4).ToList();
            foreach (var url in infiUrls)
            {
                url.FetchUrl = url.FetchUrl.Replace("?trackId=india_bookstore", "");
                url.FetchUrl = url.FetchUrl.Replace("&trackId=india_bookstore", "");
                if (url.FetchUrl.Contains("?"))
                    url.Url = url.FetchUrl + "&trackId=Pankaj5b2";
                else
                    url.Url = url.FetchUrl + "?trackId=Pankaj5b2";
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult FetchIndia()
        {
            var indiaUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 8).ToList();
            foreach (var url in indiaUrls)
            {
                url.FetchUrl = url.Url.Replace("&affid=133527", "");
                url.FetchUrl = url.FetchUrl.Replace("?affid=133527", "");
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult FetchGeneral()
        {
            var generalUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 5 || r.RetailerId == 10 || r.RetailerId == 11 || r.RetailerId == 12 || r.RetailerId == 13).ToList();
            foreach (var url in generalUrls)
            {
                url.FetchUrl = url.Url;
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult FetchSnap()
        {
            var snapUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 6).ToList();
            foreach (var url in snapUrls)
            {
                string encoded = url.Url.Replace("http://track.in.omgpm.com/?AID=480074&MID=159526&PID=8053&CID=3888033&WID=46650&redirect=", "");
                string decoded = HttpUtility.UrlDecode(encoded);
                url.FetchUrl = decoded.Replace("&utm_source=affiliate&utm_medium=OMG&utm_campaign=text", "");
                url.FetchUrl = url.FetchUrl.Replace("?&utm_source=affiliate&utm_medium=OMG&utm_campaign=text","");
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult FetchHome()
        {
            var homeUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 3).ToList();
            foreach (var url in homeUrls)
            {
                string encoded = url.Url.Replace("http://track.in.omgpm.com/?AID=480074&MID=331902&PID=9394&CID=3888032&WID=46650&redirect=", "");
                string decoded = HttpUtility.UrlDecode(encoded);
                url.FetchUrl = decoded.Replace("&utm_source=affiliate&utm_medium=OMG&utm_campaign=text", "");
                url.FetchUrl = url.FetchUrl.Replace("?&utm_source=affiliate&utm_medium=OMG&utm_campaign=text", "");
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult FetchTimes()
        {
            var timesUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 9).ToList();
            foreach (var url in timesUrls)
            {
                string encoded = url.Url.Replace("http://track.in.omgpm.com/?AID=480074&MID=387804&PID=10091&CID=3888029&WID=46650&redirect=", "");
                string decoded = HttpUtility.UrlDecode(encoded);
                url.FetchUrl = decoded.Replace("&utm_source=affiliate&utm_medium=OMG&utm_campaign=text", "");
                url.FetchUrl = url.FetchUrl.Replace("?&utm_source=affiliate&utm_medium=OMG&utm_campaign=text", "");
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult FetchYebhi()
        {
            var yebhiUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 1).ToList();
            foreach (var url in yebhiUrls)
            {
                string encoded = url.Url.Replace("http://track.in.omgpm.com/?AID=480074&MID=248729&PID=8782&CID=3892755&WID=46650&redirect=", "");
                string decoded = HttpUtility.UrlDecode(encoded);
                url.FetchUrl = decoded.Replace("&utm_source=affiliate&utm_medium=OMG&utm_campaign=text", "");
                url.FetchUrl = url.FetchUrl.Replace("?&utm_source=affiliate&utm_medium=OMG&utm_campaign=text", "");
                db.Entry(url).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult UreadIssue()
        {
            var ureadUrls = db.Retailer_Product_Prices.Where(r => r.RetailerId == 11);
            foreach (var url in ureadUrls)
            {
                db.Retailer_Product_Prices.Remove(url);
            }
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult MyntraIssue()
        {
            var ureadUrls = db.womens.Where(r => r.RetailerId == 19);
            var uread22 = db.mens.Where(r => r.RetailerId == 19);
            foreach (var url in ureadUrls)
            {
                db.womens.Remove(url);
            }
            foreach (var url22 in uread22)
            {
                db.mens.Remove(url22);
            }
            db.SaveChanges();
            return View("Index");
        }
    }
}