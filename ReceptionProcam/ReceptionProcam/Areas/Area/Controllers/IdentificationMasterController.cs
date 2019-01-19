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
    public class IdentificationMasterController : Controller
    {
        DBNCSVisitorEntities objAdminEnti = new DBNCSVisitorEntities();

        [HttpGet]
        public ActionResult IdentityMaster()
        {
            IdentityMasters master = new IdentityMasters();
            return View(master);
        }

        [HttpPost]
        public ActionResult IdentityMaster(IdentityMasters objMaster)
        {
            if (ModelState.IsValid)
            {
                tblIdentityMaster master = new tblIdentityMaster();
                master.ProofName = objMaster.ProofName;
                master.ProofCode = objMaster.ProofCode;
                master.CreatedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                master.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                objAdminEnti.tblIdentityMasters.Add(master);
                objAdminEnti.SaveChanges();
                TempData["Success"] = "Id Proof added successfully.";
                return RedirectToAction("IdentityMaster");
            }
            return View();
        }

        [HttpGet]
        public ActionResult IdentityList()
        {
            var identityList = objAdminEnti.tblIdentityMasters.OrderByDescending(x => x.Id).ThenBy(x => x.CreatedDate).ToList();
            ViewBag.AllIdentityDetails = identityList;
            return View();
        }


        [HttpGet]
        public ActionResult EditIdentity(int Id)
        {
                var VisData = objAdminEnti.tblIdentityMasters.Where(s => s.Id == Id).FirstOrDefault();
                IdentityMasters VisDtls = new IdentityMasters { Id = VisData.Id, ProofName = VisData.ProofName, ProofCode = VisData.ProofCode };
                return View(VisDtls);
        }

        [HttpPost]
        public ActionResult EditIdentity(int Id,IdentityMasters objMaster)
        {
            if (ModelState.IsValid)
            {
                var dbIdentity = objAdminEnti.tblIdentityMasters.SingleOrDefault(b => b.Id == Id);
                if (dbIdentity != null)
                {
                    dbIdentity.ProofName = objMaster.ProofName;
                    dbIdentity.ProofCode = objMaster.ProofCode;
                    dbIdentity.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                    objAdminEnti.tblIdentityMasters.Add(dbIdentity);
                    objAdminEnti.tblIdentityMasters.Attach(dbIdentity);
                    objAdminEnti.Entry(dbIdentity).State = EntityState.Modified;
                    objAdminEnti.SaveChanges();
                    TempData["Success"] = "Identification Proof Updated successfully.";
                    return RedirectToAction("IdentityList");
                }
               
            }
            return RedirectToAction("IdentityList");
        }
    }
}