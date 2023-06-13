using SPL_HOME_TASK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPL_HOME_TASK.Controllers
{
    public class CategoryInformationController : Controller
    {
        // GET: CategoryInformation
        private SPLHOMETASKEntities db = new SPLHOMETASKEntities();
        [HttpGet]
        public ActionResult AllCategory()
        {
            using (SPLHOMETASKEntities dt = new SPLHOMETASKEntities())
            {
                var products = (from p in db.DocumentCategoryInfoes select new { CategoryId = p.CategoryId, CategoryName = p.CategoryName, CategoryNameBangla= p.CategoryNameBangla, Description=p.Description }).ToList();

                //                               select p.CategoryName).ToList()
                //db.DocumentCategoryInfoes.ToList();
                return Json(new { data= products } , JsonRequestBehavior.AllowGet); //Json(products,JsonRequestBehavior.AllowGet);
            }           
        }

        public ActionResult Create()
        {
            var list = db.DocumentCategoryInfoes.ToList();
            ViewBag.list = list;
            return View();
        }


        [HttpPost]
        public ActionResult Create(DocumentCategoryInfo category)
        {
            
            DocumentCategoryInfo val = db.DocumentCategoryInfoes.Find(category.CategoryId);         

            if (val != null)
            {
                var duplicate = db.DocumentCategoryInfoes.Where(z => z.CategoryName == category.CategoryName && z.CategoryId != category.CategoryId ).ToList();
                if (duplicate.Count > 0)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                val.CategoryId = category.CategoryId;
                val.CategoryName = category.CategoryName;
                val.CategoryNameBangla = category.CategoryNameBangla;
                val.Description = category.Description;
                db.SaveChanges();
            }
            else
            {
                var duplicatechk = db.DocumentCategoryInfoes.Where(z => z.CategoryName == category.CategoryName).ToList();
                //var duplicatechk1 =  (from p in db.DocumentCategoryInfoes where p.CategoryName == category.CategoryName
                //                               select p.CategoryName).ToList();
                if (duplicatechk.Count > 0)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                db.DocumentCategoryInfoes.Add(category);
                db.SaveChanges();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var editval = (from p in db.DocumentCategoryInfoes where p.CategoryId==id select new { CategoryId = p.CategoryId, CategoryName = p.CategoryName, CategoryNameBangla = p.CategoryNameBangla, Description = p.Description }).ToList().FirstOrDefault();
            if (editval == null)
            {
                return Json(new { catgory = false }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { catgory = editval }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            DocumentCategoryInfo deletecheck = db.DocumentCategoryInfoes.Find(id);
            if (deletecheck == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }
            var value = db.DocumentInformations.Where(z =>  z.CategoryId == id ).ToList();
            db.DocumentInformations.RemoveRange(value);
            db.SaveChanges();
            db.DocumentCategoryInfoes.Remove(deletecheck);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

    }
}