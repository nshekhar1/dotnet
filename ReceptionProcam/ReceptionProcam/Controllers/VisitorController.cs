using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReceptionProcam.Models;
using ReceptionProcam.EntityModel;
using System.IO;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Net.Mail;
using System.Data.Entity.Core.Objects;
using System.Web.Services.Description;


namespace ReceptionProcam.Controllers
{
    public class VisitorController : Controller
    {
        // GET: Visitor
        DBNCSVisitorEntities objVisEnti = new DBNCSVisitorEntities();
        public ActionResult VisitorDetails()
        {
            GetAllVisitorsDetails();
            return View();
        }

        // GET: Visitor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        // GET: Visitor/CreateVisitor
        public ActionResult CreateVisitor()
        {
            try
            {
                getAllPurpose();
                getAllIdentity();
                getAllGate();
                Session["CapturedImage"] = "";
                clsVisitor personal = new clsVisitor();
                var lastVisitorPassNumber = objVisEnti.tblVisitorVisitDetails.OrderByDescending(c => c.Id).FirstOrDefault();
                var date = DateTime.Now.ToString("yyyyMMdd");
                personal.ImagePath = string.Empty;
                if (lastVisitorPassNumber == null)
                {
                    personal.VisitorId = "NCSPUN" + Convert.ToString(date) + "1";
                }
                else
                {
                    personal.VisitorId = "NCSPUN" + Convert.ToString(date) + (Convert.ToInt32(lastVisitorPassNumber.Id) + 1);
                }
                return View(personal);
            }
            catch (Exception)
            {

                return View();
            }
        }

        [HttpPost]
        public ActionResult Capture()
        {

            if (Request.InputStream.Length > 0)
            {
                using (StreamReader reader = new StreamReader(Request.InputStream))
                {
                    string hexString = Server.UrlEncode(reader.ReadToEnd());
                    string imageName = DateTime.Now.ToString("ddMMyyhhmmssfff");
                    string imagePath = string.Format("~/VisitorImage/{0}.png", imageName);
                    System.IO.File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
                    Session["CapturedImage"] = VirtualPathUtility.ToAbsolute(imagePath);

                }
            }

            return View();

            //else
            //{
            //    string fp = "." + Session["CapturedImage"].ToString();
            //    if (System.IO.File.Exists(fp))
            //    {
            //        System.IO.File.Delete(fp);
            //        if (Request.InputStream.Length > 0)
            //        {
            //            using (StreamReader reader = new StreamReader(Request.InputStream))
            //            {
            //                string hexString = Server.UrlEncode(reader.ReadToEnd());
            //                string imageName = DateTime.Now.ToString("ddMMyyhhmmsstt");
            //                string imagePath = string.Format("~/VisitorImage/{0}.png", imageName);
            //                System.IO.File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
            //                Session["CapturedImage"] = VirtualPathUtility.ToAbsolute(imagePath);
            //            }

            //        }
            //    }

            //return View();
            //}
        }
        [HttpPost]
        public string GetCapture()
        {
            string url = Session["CapturedImage"].ToString();
            return url;
        }
        private static byte[] ConvertHexToBytes(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }



        // POST: Visitor/Create
        [HttpPost]
        public ActionResult CreateVisitor(clsVisitor objVisitor)
        {
            try
            {
                if (Session["CapturedImage"].ToString() != null)
                {
                    // TODO: Add insert logic here
                    if (ModelState.IsValid)
                    {
                        //tblVisitor dbVis = new tblVisitor();
                        //dbVis.VisitorId = objVisitor.VisitorId;
                        //dbVis.Name = objVisitor.Name.ToUpper();
                        //dbVis.EmailId = objVisitor.Email;
                        //dbVis.MobileNo = objVisitor.MobileNo.ToString();
                        //dbVis.AssetId = objVisitor.AssetId.ToString();
                        //dbVis.Form = objVisitor.Form;
                        //dbVis.ToMeet = objVisitor.ToMeet;
                        //dbVis.SubLocation = objVisitor.SubLocation;
                        //dbVis.Building = objVisitor.Building;
                        //dbVis.Gate = objVisitor.Gate;
                        //dbVis.Purpose = objVisitor.Purpose;
                        //dbVis.TimeIn = objVisitor.TimeIn.ToString();
                        //dbVis.ValidUpto = objVisitor.ValidUpto.ToString();
                        //dbVis.Remark = objVisitor.Remark;
                        //dbVis.ImagePath = Session["CapturedImage"].ToString();
                        //dbVis.GovId = objVisitor.GovId.ToString();
                        //dbVis.DOB = objVisitor.DOB.ToString();
                        //dbVis.CreatedBy = objVisitor.CreatedBy;
                        //dbVis.CreatedDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //objVisEnti.tblVisitors.Add(dbVis);
                        var sss = Session["CapturedImage"].ToString() == "" ? objVisitor.ImagePath : Session["CapturedImage"].ToString();
                        ObjectParameter returnId = new ObjectParameter("Id", typeof(int));

                        var result = objVisEnti.uspInsertVisitorDetails(objVisitor.EmpId, objVisitor.GovIdNo.ToUpper(), objVisitor.Name, objVisitor.DOB, objVisitor.MobileNo, objVisitor.Email, objVisitor.GovId, sss, objVisitor.VisitorId, objVisitor.AssetId, objVisitor.Location, objVisitor.ToMeet, objVisitor.SubLocation, objVisitor.OfficeLocation, objVisitor.Gate, objVisitor.Purpose, objVisitor.TimeIn, objVisitor.ValidUpto, objVisitor.Remark, "123", System.DateTime.Now.ToString("dd-MM-yyyy"), returnId);
                        int VisId = Convert.ToInt32(returnId.Value);
                        TempData["Success"] = "Visitor added Successfully & Mail Sent to concern person!";
                        //SendEmail(objVisitor);
                        //SendSMS(objVisitor);
                        return RedirectToAction("PrintPass", new { id = VisId });

                    }
                    else
                    {
                        return View(objVisitor);
                    }
                }
                else
                {
                    TempData["Success"] = "please take Photo first!";
                    return View(objVisitor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Save" + ex.Message);
                return View(objVisitor);
            }

        }

        private void SendEmail(clsVisitor objVisitor)
        {
            System.Net.Mail.MailMessage mail = new MailMessage();
            var fromAddress = "kavan.mehta@ncs.com.sg";
            const string fromPassword = "dec@2018";
            mail.To.Add("suraj.mane@ncs.com.sg");
            mail.From = new MailAddress("kavan.mehta@ncs.com.sg");
            mail.Subject = objVisitor.VisitorId + " - " + "Coming to meet you";
            string Body = "Visitor Name :" + objVisitor.Name + "<br/>" + "Visitor's Purpose : " + objVisitor.Purpose + "<br/>" + "Visitor From :" + objVisitor.OfficeLocation;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "imap-mail.outlook.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword); // Enter seders User name and password  
            smtp.EnableSsl = true;
            smtp.Send(mail);
            //return RedirectToAction("PrintPass", new { id = dbVis.Id }); 
        }

        [HttpGet]
        // GET: Visitor/Edit/5
        public ActionResult EditVisitor(int Id)
        {
            try
            {
                //var VisData = objVisEnti.tblVisitors.Where(s => s.Id == Id).FirstOrDefault();
                getAllPurpose();
                getAllIdentity();
                getAllGate();

                var VisData = objVisEnti.uspGetVisitorDetailsById(Id).FirstOrDefault();

                var lastVisitorPassNumber = objVisEnti.tblVisitorVisitDetails.OrderByDescending(c => c.Id).FirstOrDefault();
                var date = DateTime.Now.ToString("yyyyMMdd");

                if (lastVisitorPassNumber == null)
                {
                    VisData.VisitorId = "NCSPUN" + Convert.ToString(date) + "1";
                }
                else
                {
                    VisData.VisitorId = "NCSPUN" + Convert.ToString(date) + (Convert.ToInt32(lastVisitorPassNumber.Id) + 1);
                }
                clsVisitor VisDtls = new clsVisitor { Id = VisData.Id, VisitorId = Convert.ToString(VisData.VisitorId), Name = Convert.ToString(VisData.Name), DOB = Convert.ToString(VisData.DOB), Location = Convert.ToString(VisData.Location), ToMeet = Convert.ToString(VisData.ToMeet), SubLocation = Convert.ToString(VisData.SubLocation), AssetId = Convert.ToString(VisData.AssetId), MobileNo = Convert.ToString(VisData.MobileNo), Email = Convert.ToString(VisData.EmailId), ValidUpto = Convert.ToString(VisData.ValidUpto), OfficeLocation = Convert.ToString(VisData.OfficeLocation), Gate = Convert.ToString(VisData.Gate), Purpose = Convert.ToString(VisData.Purpose), TimeIn = Convert.ToString(VisData.TimeIn), Remark = Convert.ToString(VisData.Remark), ImagePath = Convert.ToString(VisData.ImagePath), CreatedBy = Convert.ToString(VisData.CreatedBy), CreatedDate = Convert.ToString(VisData.CreatedDate), ModifiedBy = Convert.ToString(VisData.ModifiedBy), ModifiedDate = Convert.ToString(VisData.ModifiedDate), GovIdNo = Convert.ToString(VisData.GovIdNo), GovId = Convert.ToString(VisData.GovId), EmpId = Convert.ToString(VisData.EmpId), IsPassReturned = (bool)VisData.IsPassReturned };
                Session["CapturedImage"] = VisDtls.ImagePath;
                return View(VisDtls);
            }
            catch (Exception)
            {
                return View();
            }

        }

        // POST: Visitor/EditVisitor/5
        [HttpPost]
        public ActionResult EditVisitor(clsVisitor objVisitor)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    //var dbVis = objVisEnti.tblVisitors.SingleOrDefault(b => b.Id == Id);
                    //if (dbVis != null)
                    //{
                    //    dbVis.VisitorId = objVisitor.VisitorId;
                    //    dbVis.Name = objVisitor.Name.ToUpper();
                    //    dbVis.EmailId = objVisitor.Email;
                    //    dbVis.MobileNo = objVisitor.MobileNo.ToString().Trim();
                    //    dbVis.AssetId = objVisitor.AssetId.ToString();
                    //    dbVis.Form = objVisitor.Location;
                    //    dbVis.ToMeet = objVisitor.ToMeet;
                    //    dbVis.SubLocation = objVisitor.SubLocation;
                    //    dbVis.Building = objVisitor.OfficeLocation;
                    //    dbVis.Gate = objVisitor.Gate;
                    //    dbVis.Purpose = objVisitor.Purpose;
                    //    dbVis.TimeIn = objVisitor.TimeIn.ToString();
                    //    dbVis.ValidUpto = objVisitor.ValidUpto.ToString();
                    //    dbVis.Remark = objVisitor.Remark;
                    //    dbVis.ImagePath = objVisitor.ImagePath;
                    //    dbVis.ModifiedBy = objVisitor.CreatedBy;
                    //    dbVis.ModifiedDate = Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    //    dbVis.GovId = objVisitor.GovId.ToString();
                    //    dbVis.DOB = objVisitor.DOB.ToString();
                    //    objVisEnti.tblVisitors.Add(dbVis);

                    //    objVisEnti.tblVisitors.Attach(dbVis);
                    //    objVisEnti.Entry(dbVis).State = EntityState.Modified;
                    //    objVisEnti.SaveChanges();
                    //    TempData["Success"] = "Visitor updated Successfully!";
                    //    return RedirectToAction("VisitorDetails");
                    //}
                    objVisitor.ImagePath = Session["CapturedImage"].ToString();
                    objVisEnti.uspUpdatePersonalandVisitorData(Convert.ToString(objVisitor.EmpId), objVisitor.GovIdNo.ToUpper(), objVisitor.Name.ToUpper(), objVisitor.DOB, objVisitor.MobileNo, objVisitor.Email, objVisitor.GovId, Convert.ToString(objVisitor.ImagePath), objVisitor.Id, objVisitor.VisitorId, objVisitor.AssetId, objVisitor.Location, objVisitor.ToMeet, objVisitor.SubLocation, objVisitor.OfficeLocation, objVisitor.Gate, objVisitor.Purpose, objVisitor.TimeIn, objVisitor.ValidUpto, objVisitor.Remark, "123", System.DateTime.Now.ToString("dd-MM-yyyy"));
                    TempData["SuccessUpdate"] = "Visitor updated Successfully";
                    return RedirectToAction("VisitorDetails");
                }

                else
                {
                    return RedirectToAction("VisitorDetails");
                }
            }
            catch
            {
                return View(objVisitor);
            }
        }

        // GET: Visitor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Visitor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void GetAllVisitorsDetails()
        {
            try
            {
                //var AllVisitors = objVisEnti.tblVisitors.ToList().OrderByDescending(m => m.Id);
                var AllVisitors = objVisEnti.uspGetVisitorDetailsNew().ToList();
                ViewBag.AllVisitorsDetalis = AllVisitors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public ActionResult LoadData()
        {
            try
            {
                //Creating instance of DatabaseContext class  
                
                    using (DBNCSVisitorEntities dc = new DBNCSVisitorEntities())
                    {
                        var data = dc.uspGetVisitorDetailsNew().OrderBy(a => a.GovIdNo).ToList();
                        return Json(new { data = data }, JsonRequestBehavior.AllowGet);
                    }  
                
            }
            catch (Exception)
            {
                throw;
            }

        }  
        public ActionResult ViewVisitor()
        {
            try
            {
                var AllVisitors = objVisEnti.tblVisitors.ToList();
                ViewBag.AllVisitorsDetalis = AllVisitors;
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult PrintPass(int Id)
        {
            try
            {
                //var VisData = objVisEnti.tblVisitors.Where(s => s.Id == Id).FirstOrDefault();
                var VisData = objVisEnti.uspGetVisitorDetailsById(Id).FirstOrDefault();
                //clsVisitor VisDtls = new clsVisitor { Id = VisData.Id, VisitorId = VisData.VisitorId, Name = VisData.Name, Location = VisData.Form, ToMeet = VisData.ToMeet, SubLocation = VisData.SubLocation, AssetId = VisData.AssetId, MobileNo = VisData.MobileNo, Email = VisData.EmailId, ValidUpto = VisData.ValidUpto, OfficeLocation = VisData.Building, Gate = VisData.Gate, Purpose = VisData.Purpose, TimeIn = VisData.TimeIn, Remark = VisData.Remark, ImagePath = VisData.ImagePath, CreatedBy = VisData.CreatedBy, CreatedDate = VisData.CreatedDate.ToString(), ModifiedBy = VisData.ModifiedBy, ModifiedDate = VisData.ModifiedDate.ToString() };
                clsVisitor VisDtls = new clsVisitor { Id = VisData.Id, VisitorId = Convert.ToString(VisData.VisitorId), Name = Convert.ToString(VisData.Name), DOB = Convert.ToString(VisData.DOB), Location = Convert.ToString(VisData.Location), ToMeet = Convert.ToString(VisData.ToMeet), SubLocation = Convert.ToString(VisData.SubLocation), AssetId = Convert.ToString(VisData.AssetId), MobileNo = Convert.ToString(VisData.MobileNo), Email = Convert.ToString(VisData.EmailId), ValidUpto = Convert.ToString(VisData.ValidUpto), OfficeLocation = Convert.ToString(VisData.OfficeLocation), Gate = Convert.ToString(VisData.Gate), Purpose = Convert.ToString(VisData.Purpose), TimeIn = Convert.ToString(VisData.TimeIn), Remark = Convert.ToString(VisData.Remark), ImagePath = Convert.ToString(VisData.ImagePath), CreatedBy = Convert.ToString(VisData.CreatedBy), CreatedDate = Convert.ToString(VisData.CreatedDate), ModifiedBy = Convert.ToString(VisData.ModifiedBy), ModifiedDate = Convert.ToString(VisData.ModifiedDate), GovIdNo = Convert.ToString(VisData.GovIdNo), GovId = Convert.ToString(VisData.GovId), EmpId = Convert.ToString(VisData.EmpId), PurposeText = Convert.ToString(VisData.PurposeText) };
                return View(VisDtls);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Print" + ex.Message);
                return View();

                //    return View();
            }
        }

        [HttpGet]
        public JsonResult Visitorperdaycount()
        {
            try
            {
                var result = objVisEnti.uspGetVisitorPerDayCount();
                if (result != null)
                {
                    return this.Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                { return this.Json("No Data", JsonRequestBehavior.AllowGet); }
            }
            catch
            {
                return this.Json("Error", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public JsonResult getVisitorDataByGovId(string VisType, string govId)
        {
            try
            {
                var result = objVisEnti.uspGetVisitorDetailsByGovId(VisType, govId).ToList();
                if (result != null)
                {
                    return this.Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                { return this.Json("No Data", JsonRequestBehavior.AllowGet); }
            }
            catch
            {
                return this.Json("Error", JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public Boolean ReturnPass(string id)
        {
            var result = objVisEnti.tblVisitorVisitDetails.SingleOrDefault(b => b.GovIdNo == id);
            if (result != null)
            {
                result.IsPassReturned = true;
                objVisEnti.SaveChanges();
                TempData["SuccessReturn"] = "Pass returned successfully";
                return true;
            }
            else
            {
                return false;
            }
        }


        [HttpGet]
        public void getAllPurpose()
        {
            try
            {
                ViewBag.AllPurpose = new SelectList(objVisEnti.uspGetAllPurpose(), "Purpose", "PurposeText", 0);
            }
            catch
            {

            }
        }
        [HttpGet]
        public void getAllIdentity()
        {
            try
            {
                ViewBag.AllIdentity = new SelectList(objVisEnti.uspGetAllIdentity(), "GovId", "ProofName", 0);
            }
            catch
            {

            }
        }

        [HttpGet]
        public void getAllGate()
        {
            try
            {
                ViewBag.AllGate = new SelectList(objVisEnti.uspGetAllGate(), "Gate", "GateName", 0);
            }
            catch
            {

            }
        }
    }
}
