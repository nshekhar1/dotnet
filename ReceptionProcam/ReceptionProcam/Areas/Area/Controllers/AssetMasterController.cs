using ReceptionProcam.Areas.Area.Models;
using ReceptionProcam.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceptionProcam.Areas.Area.Controllers
{
    public class AssetMasterController : Controller
    {
        DBNCSVisitorEntities objAdminEnti = new DBNCSVisitorEntities();
        // GET: Area/AssetMaster
        [HttpGet]
        public ActionResult AssetMaster()
        {
            GetAssetCompanyList();
            GetAssetTypeList();
            return View();
        }

        public void GetAssetCompanyList()
        {
            ViewBag.AssetCompanyList = new SelectList(objAdminEnti.tblAssetCompanies, "ID", "AssetCompanyName", 0);
        }
        public void GetAssetTypeList()
        {
            ViewBag.AssetTypeList = new SelectList(objAdminEnti.tblAssetTypes, "ID", "AssetTypeName", 0);
        }

        [HttpPost]
        public ActionResult AssetMaster(AssetMasters objMaster)
        {
            tblAssetDetail master = new tblAssetDetail();
            master.AssetTypeID = objMaster.AssetTypeID;
            master.AssetCompanyID = objMaster.AssetCompanyID;
            master.AssetModelName = objMaster.AssetModelName;
            master.ManufacturingDate = objMaster.ManufacturingDate;
            master.ExpiryDate = objMaster.ExpiryDate;
            master.LicesenceNo = objMaster.LicesenceNo;
            master.CreatedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
            master.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
            master.CreatedBy = "admin";
            objAdminEnti.tblAssetDetails.Add(master);
            objAdminEnti.SaveChanges();
            TempData["Success"] = "Asset added successfully.";
            return RedirectToAction("AssetMaster");
        }

        [HttpGet]
        public ActionResult AssetList()
        {
            //var employeeList = objAdminEnti.tblEmployeeDetails.OrderByDescending(x => x.ID).ThenBy(x => x.CreatedDate).ToList();
            var assetList = objAdminEnti.uspGetAssetList().ToList();
            ViewBag.AllAssetDetails = assetList;
            GetAssetCompanyList();
            GetAssetTypeList();
            return View();
        }

        [HttpGet]
        public ActionResult EditAsset(int Id)
        {
            var assetData = objAdminEnti.tblAssetDetails.Where(s => s.ID == Id).FirstOrDefault();
            AssetMasters assetDtls = new AssetMasters { ID = assetData.ID, AssetTypeID = Convert.ToInt32(assetData.AssetTypeID), AssetCompanyID = Convert.ToInt32(assetData.AssetCompanyID), AssetModelName = assetData.AssetModelName, ManufacturingDate = assetData.ManufacturingDate, ExpiryDate = assetData.ExpiryDate , LicesenceNo = assetData.LicesenceNo };
            GetAssetCompanyList();
            GetAssetTypeList();
            return View(assetDtls);
        }

        [HttpPost]
        public ActionResult EditAsset(int Id, AssetMasters objMaster)
        {
            var dbAsset = objAdminEnti.tblAssetDetails.SingleOrDefault(b => b.ID == Id);
            if (dbAsset != null)
            {
                dbAsset.AssetTypeID = objMaster.AssetTypeID;
                dbAsset.AssetCompanyID = objMaster.AssetCompanyID;
                dbAsset.AssetModelName = objMaster.AssetModelName;
                dbAsset.ManufacturingDate = objMaster.ManufacturingDate;
                dbAsset.ExpiryDate = objMaster.ExpiryDate;
                dbAsset.LicesenceNo = objMaster.LicesenceNo;
                dbAsset.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                dbAsset.CreatedBy = "admin";
                objAdminEnti.tblAssetDetails.Add(dbAsset);
                objAdminEnti.tblAssetDetails.Attach(dbAsset);
                objAdminEnti.Entry(dbAsset).State = EntityState.Modified;
                objAdminEnti.SaveChanges();
                TempData["Success"] = "Asset Updated successfully.";
                return RedirectToAction("AssetList");
            }
            return RedirectToAction("AssetList");
        }
    }
}