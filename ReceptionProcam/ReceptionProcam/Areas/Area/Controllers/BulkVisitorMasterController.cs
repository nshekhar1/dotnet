using ReceptionProcam.EntityModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReceptionProcam.Areas.Area.Controllers
{
    public class BulkVisitorMasterController : Controller
    {
        DBNCSVisitorEntities objVisEnti = new DBNCSVisitorEntities();
        // download file format for bulk visitors
        [HttpGet]
        public ActionResult DownloadTemplate()
        {
            string path = Server.MapPath("~/Template/");
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "DataInsertTemplate.xlsx");
            string fileName = "DataInsertTemplate.xlsx";

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpGet]
        public ActionResult UploadTemplate()
        {
            return View();
        }

        public ActionResult UploadTemplate(HttpPostedFileBase postedFile)
        {
            string filePath = string.Empty;
            if (postedFile != null)
            {
                try
                {
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03.
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //Excel 07 and above.
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    DataTable dt = new DataTable();
                    conString = string.Format(conString, filePath);

                    using (OleDbConnection connExcel = new OleDbConnection(conString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;

                                //Get the name of First Sheet.
                                connExcel.Open();
                                DataTable dtExcelSchema;
                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();

                                //Read Data from First Sheet.
                                connExcel.Open();
                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                odaExcel.SelectCommand = cmdExcel;
                                odaExcel.Fill(dt);
                                connExcel.Close();
                            }
                        }
                    }

                    dt.Columns.Add("ImagePath");
                    foreach (DataRow dataRow in dt.Rows)
                    {

                        dataRow["ImagePath"] = @"~\VisitorImage\ProfileIcon.png";
                        string govId = dataRow["GovIdNo"].ToString();
                        //var row = objVisEnti.tblVisitorPersonalDatas.Any(x=>x.GovIdNo.Equals(dataRow["GovIdNo"]));
                        if (objVisEnti.tblVisitorPersonalDatas.Any(x => x.GovIdNo == govId))
                        {
                            dataRow.Delete();
                        }
                    }
                    dt.Select(string.Format("[GovId] = '{0}'", "Aadhar Card")).ToList<DataRow>().ForEach(r => r["GovId"] = 1);
                    dt.Select(string.Format("[GovId] = '{0}'", "Pan Card")).ToList<DataRow>().ForEach(r => r["GovId"] = 2);
                    dt.Select(string.Format("[GovId] = '{0}'", "Voter Card")).ToList<DataRow>().ForEach(r => r["GovId"] = 3);
                    dt.Select(string.Format("[GovId] = '{0}'", "Passport")).ToList<DataRow>().ForEach(r => r["GovId"] = 4);


                    conString = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name.
                            sqlBulkCopy.DestinationTableName = "dbo.tblVisitorPersonalData";

                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("GovIdNo", "GovIdNo");
                            sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                            sqlBulkCopy.ColumnMappings.Add("DOB", "DOB");
                            sqlBulkCopy.ColumnMappings.Add("MobileNo", "MobileNo");
                            sqlBulkCopy.ColumnMappings.Add("EmailId", "EmailId");
                            sqlBulkCopy.ColumnMappings.Add("GovId", "GovId");
                            sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                            sqlBulkCopy.ColumnMappings.Add("ModifiedBy", "ModifiedBy");
                            sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                            sqlBulkCopy.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
                            sqlBulkCopy.ColumnMappings.Add("ImagePath", "ImagePath");

                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                        }
                    }
                    using (SqlConnection con = new SqlConnection(conString))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            //Set the database table name.
                            sqlBulkCopy.DestinationTableName = "dbo.tblVisitorVisitDetails";

                            //[OPTIONAL]: Map the Excel columns with that of the database table
                            sqlBulkCopy.ColumnMappings.Add("GovIdNo", "GovIdNo");
                            sqlBulkCopy.ColumnMappings.Add("AssetId", "AssetId");
                            sqlBulkCopy.ColumnMappings.Add("Location", "Location");
                            sqlBulkCopy.ColumnMappings.Add("ToMeet", "ToMeet");
                            sqlBulkCopy.ColumnMappings.Add("SubLocation", "SubLocation");
                            sqlBulkCopy.ColumnMappings.Add("OfficeLocation", "OfficeLocation");
                            sqlBulkCopy.ColumnMappings.Add("Gate", "Gate");
                            sqlBulkCopy.ColumnMappings.Add("Purpose", "Purpose");
                            sqlBulkCopy.ColumnMappings.Add("TimeIn", "TimeIn");
                            sqlBulkCopy.ColumnMappings.Add("ValidUpto", "ValidUpto");
                            sqlBulkCopy.ColumnMappings.Add("Remark", "Remark");
                            sqlBulkCopy.ColumnMappings.Add("CreatedBy", "CreatedBy");
                            sqlBulkCopy.ColumnMappings.Add("ModifiedBy", "ModifiedBy");
                            //  sqlBulkCopy.ColumnMappings.Add("CreatedDate", "CreatedDate");
                            sqlBulkCopy.ColumnMappings.Add("ModifiedDate", "ModifiedDate");
                            con.Open();
                            sqlBulkCopy.WriteToServer(dt);
                            con.Close();
                            TempData["SuccessUpload"] = "File uploaded successfully";
                        }
                    }
                    return RedirectToAction("UploadTemplate", "BulkVisitorMaster");
                }
                catch 
                {
                    TempData["ErrorUpload"] = "Check file format";
                    return View();
                }
            }
            else
            {
                TempData["ErrorUpload"] = "Please select a file";
                return View();
            }

        
        }
    }
}