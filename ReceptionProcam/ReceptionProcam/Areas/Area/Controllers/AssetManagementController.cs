using ReceptionProcam.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReceptionProcam.Areas.Area.Models;

namespace ReceptionProcam.Areas.Area.Controllers
{
    public class AssetManagementController : Controller
    {
        DBNCSVisitorEntities objVisEnti = new DBNCSVisitorEntities();
        // GET: Area/AssetManagement
        [HttpGet]
        public ActionResult AssetDetails()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoadData()
        {
            try
            {
                //Creating instance of DatabaseContext class  

                using (DBNCSVisitorEntities dc = new DBNCSVisitorEntities())
                {
                    var data = dc.uspGetAssetDetails().OrderBy(a => a.AssetModelName).ToList();
                    return Json(new { data = data }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public Boolean SubmitAsset(int id)
        {
            var result = objVisEnti.tblAssetIssueDetails.SingleOrDefault(b => b.AssetId == id);
            var assetTbl = objVisEnti.tblAssetDetails.SingleOrDefault(b => b.ID == id);
           
            if (result != null)
            {
                result.IsSubmited = true;
                result.AssetSubmitDateTime = DateTime.Now;
                if (assetTbl != null)
                {
                    assetTbl.IsIssued = false;
                }
                objVisEnti.SaveChanges();
                TempData["SuccessSubmit"] = "Asset Submitted successfully";
                return true;
            }
            else
            {
                return false;
            }
        }
     
        [HttpGet]
        public ActionResult AssetIssue()
        {
            getAllAssets();
            return View();
        }

        [HttpPost]
        public ActionResult AssetIssue(clsAssetIssueDetails objclsAssetIssueDetails)
        {
            try
            {
                foreach (var i in objclsAssetIssueDetails.AssetId)
                {
                    var j = objVisEnti.spInsertAssetIssueDetails(objclsAssetIssueDetails.EmpId, i, "123");
                }
                TempData["Success"] = "Assets assigned to employee succesfully";
            }
            catch
            {
                TempData["Success"] = "Assets assigned to employee is failed";
            }
            return RedirectToAction("AssetIssue");
        }
        [HttpGet]
        public void getAllAssets()
        {
            try
            {
                ViewBag.AllAssets = new SelectList(objVisEnti.uspGetAllActiveAssets(), "ID", "AssetModelName", 0);
            }
            catch
            {

            }
        } 
    }
}