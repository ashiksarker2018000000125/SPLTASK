using SPL_HOME_TASK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPL_HOME_TASK.Controllers
{
    public class DocumentDetailsController : Controller
    {
        private SPLHOMETASKEntities db = new SPLHOMETASKEntities();

        public ActionResult AllDocument()
        {
            using (SPLHOMETASKEntities dt = new SPLHOMETASKEntities())
            {
                var products = (from p in db.DocumentInformations join q in db.DocumentCategoryInfoes on p.CategoryId equals q.CategoryId
                                select new { DocumentyIdentity = p.DocumentyIdentity, DocumentCategoryID = p.CategoryId, DocumentCategoryname = q.CategoryName, DocumentReferenceName = p.DocumentReferenceName, DocumentDate = p.DocumentDate,
                                             DocumentName = p.DocumentName, DocumentNameBangla = p.DocumentNameBangla, Description = p.Description}).ToList();
                return Json(new { data = products }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.DocumentCategoryInfoes, "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(DocumentInformation documentInformation)
        {

            DocumentInformation val = db.DocumentInformations.Find(documentInformation.DocumentyIdentity);

            if (val != null)
            {
                var duplicate = db.DocumentInformations.Where(z => z.DocumentName == documentInformation.DocumentName && z.DocumentyIdentity != documentInformation.DocumentyIdentity).ToList();
                if (duplicate.Count > 0)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                val.CategoryId = documentInformation.CategoryId;
                val.DocumentReferenceName = documentInformation.DocumentReferenceName;
                val.DocumentDate = documentInformation.DocumentDate;
                val.DocumentName = documentInformation.DocumentName;
                val.DocumentNameBangla = documentInformation.DocumentNameBangla;
                val.Description = documentInformation.Description;
                db.SaveChanges();
            }
            else
            {
                var duplicatechk = db.DocumentInformations.Where(z => z.DocumentName == documentInformation.DocumentName).ToList();

                if (duplicatechk.Count > 0)
                {
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                db.DocumentInformations.Add(documentInformation);
                db.SaveChanges();
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var editval = (from p in db.DocumentInformations join q in db.DocumentCategoryInfoes on p.CategoryId equals q.CategoryId where p.DocumentyIdentity==id
                            select new
                            {
                                DocumentyIdentity = p.DocumentyIdentity,
                                DocumentCategoryID = p.CategoryId,
                                DocumentCategoryname = q.CategoryName,
                                DocumentReferenceName = p.DocumentReferenceName,
                                DocumentDate = p.DocumentDate,
                                DocumentName = p.DocumentName,
                                DocumentNameBangla = p.DocumentNameBangla,
                                Description = p.Description
                            }).ToList().FirstOrDefault();

            if (editval == null)
            {
                return Json(new { document = false }, JsonRequestBehavior.AllowGet);

            }
            
            return Json(new { document = editval }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            DocumentInformation deletecheck = db.DocumentInformations.Find(id);
            if (deletecheck == null)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

            }
            db.DocumentInformations.Remove(deletecheck);
            db.SaveChanges();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }
    }   
    
}