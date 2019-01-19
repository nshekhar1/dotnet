using ReceptionProcam.EntityModel;
using ReceptionProcam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceptionProcam.Controllers
{
    public class LoginController : Controller
    {
        DBNCSVisitorEntities objVisEnti = new DBNCSVisitorEntities();
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            //if (ModelState.IsValid)
            //{

                var user = (from userlist in objVisEnti.tblUserLogins
                            where userlist.UserName == login.UserName && userlist.Password == login.Password
                            select new
                            {
                                userlist.UserID,
                                userlist.UserName,
                                userlist.Role,
                                userlist.Password
                            }).ToList();
                if (user.FirstOrDefault() != null)
                {

                    Session["UserName"] = user.FirstOrDefault().UserName;
                    Session["UserID"] = user.FirstOrDefault().UserID;
                    Session["Role"] = user.FirstOrDefault().Role;
                    if (Session["Role"].ToString() == "Security")
                    {
                        return Redirect("/home/index");

                    }
                    else
                    {
                        return Redirect("/Area/Welcome/index");
                    }
                }
                else
                {
                    TempData["Failure"] = "Please enter valid username and password.";
                    return RedirectToAction("Login", TempData["Failure"]);
                }
                return View();
            }
        
            //else
            //{
            //    return Redirect(Request.UrlReferrer.ToString());
            //    //return View("Login", login);

            //}
        //}

        public ActionResult Logout()
        {
            Session["UserName"] = null;
            Session["UserID"] = null;
            Session["Role"] = null;
            return Redirect("/login/login");
        
        }
    }

}