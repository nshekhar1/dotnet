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
    public class EmployeeMasterController : Controller
    {
        DBNCSVisitorEntities objAdminEnti = new DBNCSVisitorEntities();

        [HttpGet]
        public ActionResult EmployeeMaster()
        {
            GetDesignationList();
            return View();
        }
        [HttpGet]
        public void GetDesignationList()
        {
            ViewBag.DesignationList = new SelectList(objAdminEnti.tblEmpDesignationMasters, "ID", "EmpDesignationName", 0);
        }

        [HttpPost]
        public ActionResult EmployeeMaster(EmployeeMasters objMaster)
        {
                tblEmployeeDetail master = new tblEmployeeDetail();
                master.EmpName = objMaster.EmpName;
                master.EmpCode = objMaster.EmpCode;
                master.EmpDesignationID = objMaster.EmpDesignationID;
                master.EmpDept = objMaster.EmpDept;
                master.PhoneNo = objMaster.PhoneNo;
                master.CreatedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                master.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                master.IsActive = true;
                objAdminEnti.tblEmployeeDetails.Add(master);
                objAdminEnti.SaveChanges();
                TempData["Success"] = "Employee added successfully.";
                return RedirectToAction("EmployeeMaster");
            
           // return View();
        }

        [HttpGet]
        public ActionResult EmployeeList()
        {
            //var employeeList = objAdminEnti.tblEmployeeDetails.OrderByDescending(x => x.ID).ThenBy(x => x.CreatedDate).ToList();
            var employeeList = objAdminEnti.uspGetEmployeeList().ToList();
            ViewBag.AllEmployeeDetails = employeeList;
            GetDesignationList();
            return View();
        }


        [HttpGet]
        public ActionResult EditEmployee(int Id)
        {
            var empData = objAdminEnti.tblEmployeeDetails.Where(s => s.ID == Id).FirstOrDefault();
            EmployeeMasters empDtls = new EmployeeMasters { ID = empData.ID, EmpName = empData.EmpName, EmpCode = empData.EmpCode, EmpDept = empData.EmpDept, EmpDesignationID = empData.EmpDesignationID, IsActive = Convert.ToBoolean(empData.IsActive) };
            GetDesignationList();
            return View(empDtls);
        }

        [HttpPost]
        public ActionResult EditEmployee(int Id, EmployeeMasters objMaster)
        {
                var dbEmployee = objAdminEnti.tblEmployeeDetails.SingleOrDefault(b => b.ID == Id);
                if (dbEmployee != null)
                {
                    dbEmployee.EmpName = objMaster.EmpName;
                    dbEmployee.EmpCode = objMaster.EmpCode;
                    dbEmployee.EmpDesignationID = objMaster.EmpDesignationID;
                    dbEmployee.EmpDept = objMaster.EmpDept;
                    dbEmployee.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy hh:mm"));
                    dbEmployee.PhoneNo = objMaster.PhoneNo;
                    dbEmployee.IsActive = objMaster.IsActive;
                    objAdminEnti.tblEmployeeDetails.Add(dbEmployee);
                    objAdminEnti.tblEmployeeDetails.Attach(dbEmployee);
                    objAdminEnti.Entry(dbEmployee).State = EntityState.Modified;
                    objAdminEnti.SaveChanges();
                    TempData["Success"] = "Employee Updated successfully.";
                    return RedirectToAction("EmployeeList");
                }
                return RedirectToAction("EmployeeList");
        }
    }
}