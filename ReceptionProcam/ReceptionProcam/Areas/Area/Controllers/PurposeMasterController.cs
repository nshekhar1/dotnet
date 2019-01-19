using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReceptionProcam.Areas.Area.Models;
using ReceptionProcam.EntityModel;
using System.Data.Entity;

namespace ReceptionProcam.Areas.Area.Controllers
{
    public class PurposeMasterController : Controller
    {
        DBNCSVisitorEntities objAdminEnti = new DBNCSVisitorEntities();

        [HttpGet]
        public ActionResult PurposeMaster()
        {
            PurposeMasters master = new PurposeMasters();
            return View(master);
        }

        [HttpPost]
        public ActionResult PurposeMaster(PurposeMasters objMaster)
        {
            if (ModelState.IsValid)
            {
                tblPurposeMaster master = new tblPurposeMaster();
                master.PurposeName = objMaster.PurposeName;
                master.PurposeCode = objMaster.PurposeCode;
                master.CreatedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                master.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                objAdminEnti.tblPurposeMasters.Add(master);
                objAdminEnti.SaveChanges();
                TempData["Success"] = "Purpose added successfully.";
                return RedirectToAction("PurposeMaster");
            }
            return View();
        }

        [HttpGet]
        public ActionResult PurposeList()
        {
            var purposeList = objAdminEnti.tblPurposeMasters.OrderByDescending(x => x.Id).ThenBy(x => x.CreatedDate).ToList();
            ViewBag.AllPurposeDetails = purposeList;
            return View();
        }


        [HttpGet]
        public ActionResult EditPurpose(int Id)
        {
                var VisData = objAdminEnti.tblPurposeMasters.Where(s => s.Id == Id).FirstOrDefault();
                PurposeMasters VisDtls = new PurposeMasters { Id = VisData.Id, PurposeName = VisData.PurposeName, PurposeCode = VisData.PurposeCode };
                return View(VisDtls);
        }

        [HttpPost]
        public ActionResult EditPurpose(int Id,PurposeMasters objMaster)
        {
            if (ModelState.IsValid)
            {
                var dbPurpose = objAdminEnti.tblPurposeMasters.SingleOrDefault(b => b.Id == Id);
                if (dbPurpose != null)
                {
                    dbPurpose.PurposeName = objMaster.PurposeName;
                    dbPurpose.PurposeCode = objMaster.PurposeCode;
                    dbPurpose.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                    objAdminEnti.tblPurposeMasters.Add(dbPurpose);
                    objAdminEnti.tblPurposeMasters.Attach(dbPurpose);
                    objAdminEnti.Entry(dbPurpose).State = EntityState.Modified;
                    objAdminEnti.SaveChanges();
                    TempData["Success"] = "Purpose Updated successfully.";
                    return RedirectToAction("PurposeList");
                }
               
            }
            return RedirectToAction("PurposeList");
        }
    }
}