using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PriceCompare.Models;
using PagedList;
using PriceCompare.ViewModel;

namespace PriceCompare.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class CategoryController : Controller
    {
        private PriceCompareEntity db = new PriceCompareEntity();

        //
        // GET: /Category/

        public ViewResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {

            ViewBag.ParentCategoryId = PriceCompareStatic.groupList(db);
            return View();
        } 

        //
        // POST: /Category/Create
        [HttpPost]
        public ActionResult Create(CatPicView catPic)
        {
            if (ModelState.IsValid)
            {
                if (!String.IsNullOrEmpty(catPic.Picture.PictureUrl))
                {
                    Picture picture = new Picture();
                    picture.PictureUrl = catPic.Picture.PictureUrl;
                    db.Pictures.Add(picture);
                    catPic.Category.PictureId = picture.Id;
                }
                db.Categories.Add(catPic.Category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        
        //
        // GET: /Category/Edit/5
 
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            Picture picture = new Picture();
            int? picId = category.PictureId;
            if (picId != null)
            {
                picture = db.Pictures.Find(picId);
            }
            
            CatPicView catPic = new CatPicView();
            catPic.Category = category;
            catPic.Picture = picture;
            ViewBag.ParentCategoryId = PriceCompareStatic.groupList(db);
            return View(catPic);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(CatPicView catPic, int id)
        {
            if (ModelState.IsValid)
            {
                catPic.Category.Id = id;
                if (String.IsNullOrEmpty(catPic.Picture.PictureUrl))
                {
                    if (catPic.Category.PictureId != null)
                    {
                        Picture picture = db.Pictures.Find(catPic.Category.PictureId);
                        db.Pictures.Remove(picture);
                        catPic.Category.PictureId = null;
                    }
                }
                else
                {
                    if (catPic.Category.PictureId == null)
                    {
                        Picture picture = new Picture();
                        picture.PictureUrl = catPic.Picture.PictureUrl;
                        db.Pictures.Add(picture);
                        catPic.Category.PictureId = picture.Id;
                    }
                    else
                    {
                        db.Entry(catPic.Picture).State = EntityState.Modified;
                    }
                }
                db.Entry(catPic.Category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ParentCategoryId = PriceCompareStatic.groupList(db);
            return View(catPic);
        }

        //
        // GET: /Category/Delete/5
 
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            int? picId = category.PictureId;
            db.Categories.Remove(category);
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
                pictureCount += db.Pictures.Count();
                string extension = Path.GetExtension(fileData.FileName);
                string renamedImage = Server.MapPath("~/Content/Images/Categories/cat" + pictureCount + extension);
                fileData.SaveAs(renamedImage);
                return Json("/Content/Images/Categories/" + Path.GetFileName(renamedImage));
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
    }
}