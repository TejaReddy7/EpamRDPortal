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
using System.Xml;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class AttendeeDetailsController : Controller
    {
        private AttendeeDetailsModel _model;

        public AttendeeDetailsController()
        {
            _model = new AttendeeDetailsModel();
        }

        // GET: AttendeeDetails/Create
        public ActionResult Create()
        {
            return View(_model);
        }

        // POST: AttendeeDetails/Create
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase upload, FormCollection collection)
        {
            try
            {
                string webinarDate = "";
                DataSet result = new DataSet();
                int webinarId = 0;

                if (Request.Files[0].ContentLength > 0)
                {
                    string fileExtension =
                                         System.IO.Path.GetExtension(Request.Files[0].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files[0].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {

                            System.IO.File.Delete(fileLocation);
                        }
                        Request.Files[0].SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(result);
                        }
                    }
                    if (fileExtension.ToString().ToLower().Equals(".xml"))
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }

                        Request.Files["FileUpload"].SaveAs(fileLocation);
                        XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                        // DataSet ds = new DataSet();
                        result.ReadXml(xmlreader);
                        xmlreader.Close();
                    }

                    int webnairCount = 0;
                    int count = 0;
                    if (result.Tables.Count > 0)
                    {
                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {
                            if (result.Tables[0].Rows[i][0].ToString() == "Webinar ID")
                            {
                                webnairCount = i + 1;
                            }
                            if (i == webnairCount && webnairCount != 0)
                            {
                                SqlConnection con = new SqlConnection(EPAM.RDPreEducationPortal.Utilities.ConfigurationManager.RdConnectionString);

                                SqlCommand cmd = new SqlCommand("Update_Webinar", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@WebinarID", 0);
                                string sessionDateTime = result.Tables[0].Rows[i][1].ToString().Replace("IST", "");
                                cmd.Parameters.AddWithValue("@ActualStartDate", Convert.ToDateTime(sessionDateTime));
                                webinarDate = sessionDateTime;
                                cmd.Parameters.AddWithValue("@Duration", result.Tables[0].Rows[i][2].ToString());
                                cmd.Parameters.AddWithValue("@Registered", Convert.ToInt32(result.Tables[0].Rows[i][3]));
                                cmd.Parameters.AddWithValue("@Attended", Convert.ToInt32(result.Tables[0].Rows[i][4]));
                                cmd.Parameters.AddWithValue("@ClickedRegistrationLink", Convert.ToInt32(result.Tables[0].Rows[i][5]));
                                cmd.Parameters.AddWithValue("@OpenedInvitation", Convert.ToInt32(result.Tables[0].Rows[i][6]));
                                con.Open();

                                webinarId = (int)cmd.ExecuteScalar();
                                webnairCount = 0;
                                con.Close();
                            }
                            if (result.Tables[0].Rows[i][0].ToString() == "Attendee Details")
                            {
                                count = i + 1;
                            }
                            if (i > count && count != 0)
                            {

                                SqlConnection con = new SqlConnection(Utilities.ConfigurationManager.RdConnectionString);
                                SqlCommand cmd = new SqlCommand("Update_AttendeeDetails", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@AttendeeID", 0);
                                cmd.Parameters.AddWithValue("@StudentID", 1);
                                cmd.Parameters.AddWithValue("@WebinarID", webinarId);
                                cmd.Parameters.AddWithValue("@Attended", result.Tables[0].Rows[i][0].ToString());
                                cmd.Parameters.AddWithValue("@InterestRating", result.Tables[0].Rows[i][1].ToString());
                                cmd.Parameters.AddWithValue("@LastName", result.Tables[0].Rows[i][2].ToString());
                                cmd.Parameters.AddWithValue("@FirstName", result.Tables[0].Rows[i][3].ToString());
                                cmd.Parameters.AddWithValue("@EmailAddress", result.Tables[0].Rows[i][4].ToString());
                                cmd.Parameters.AddWithValue("@RegistrationDateTime", result.Tables[0].Rows[i][5].ToString());
                                cmd.Parameters.AddWithValue("@JoinTimeLeaveTimeTimeinSession", result.Tables[0].Rows[i][6].ToString());
                                cmd.Parameters.AddWithValue("@TimeinSession", result.Tables[0].Rows[i][7].ToString());
                                cmd.Parameters.AddWithValue("@Unsubscribed", result.Tables[0].Rows[i][8].ToString());
                                cmd.Parameters.AddWithValue("@Who", "Raj");
                                con.Open();

                                cmd.ExecuteNonQuery();

                                con.Close();

                            }
                        }
                    }
                }

                if (Request.Files[1].ContentLength > 0)
                {
                    result = new DataSet();
                    string fileExtension =
                                         System.IO.Path.GetExtension(Request.Files[1].FileName);

                    if (fileExtension == ".xls" || fileExtension == ".xlsx")
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files[1].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {

                            System.IO.File.Delete(fileLocation);
                        }
                        Request.Files[1].SaveAs(fileLocation);
                        string excelConnectionString = string.Empty;
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        //connection String for xls file format.
                        if (fileExtension == ".xls")
                        {
                            excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        }
                        //connection String for xlsx file format.
                        else if (fileExtension == ".xlsx")
                        {
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                            fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        }
                        //Create Connection to Excel work book and add oledb namespace
                        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                        excelConnection.Open();
                        DataTable dt = new DataTable();

                        dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        if (dt == null)
                        {
                            return null;
                        }

                        String[] excelSheets = new String[dt.Rows.Count];
                        int t = 0;
                        //excel data saves in temp file here.
                        foreach (DataRow row in dt.Rows)
                        {
                            excelSheets[t] = row["TABLE_NAME"].ToString();
                            t++;
                        }
                        OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);


                        string query = string.Format("Select * from [{0}]", excelSheets[0]);
                        using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                        {
                            dataAdapter.Fill(result);
                        }
                    }
                    if (fileExtension.ToString().ToLower().Equals(".xml"))
                    {
                        string fileLocation = Server.MapPath("~/Content/") + Request.Files[1].FileName;
                        if (System.IO.File.Exists(fileLocation))
                        {
                            System.IO.File.Delete(fileLocation);
                        }

                        Request.Files[1].SaveAs(fileLocation);
                        XmlTextReader xmlreader = new XmlTextReader(fileLocation);
                        // DataSet ds = new DataSet();
                        result.ReadXml(xmlreader);
                        xmlreader.Close();
                    }
                    int count = 0;
                    //int webinarId = 0;
                    if (result.Tables.Count > 0)
                    {
                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {
                            if (result.Tables[0].Rows[i][0].ToString() == "Registrants")
                            {
                                count = i + 1;
                            }
                            if (i > count && count != 0)
                            {
                                var subscribed = false;
                                SqlConnection con =
                                    new SqlConnection(Utilities.ConfigurationManager.RdConnectionString);
                                SqlCommand cmd = new SqlCommand("Update_Webinar_Registrants", con);
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@RegistrantId", 0);
                                cmd.Parameters.AddWithValue("@WebinarID", webinarId);
                                cmd.Parameters.AddWithValue("@FirstName", result.Tables[0].Rows[i][0].ToString());
                                cmd.Parameters.AddWithValue("@LastName", result.Tables[0].Rows[i][1].ToString());
                                cmd.Parameters.AddWithValue("@Email", result.Tables[0].Rows[i][2].ToString());
                                string registrationDate = result.Tables[0].Rows[i][3].ToString().Replace("IST", "");
                                cmd.Parameters.AddWithValue("@RegistrationDate", registrationDate);
                                if (result.Tables[0].Rows[i][4].ToString() == "No")
                                {
                                    subscribed = false;
                                }
                                else
                                {
                                    subscribed = true;
                                }
                                cmd.Parameters.AddWithValue("@Unsubscribed", subscribed);
                                cmd.Parameters.AddWithValue("@Who", "Raj");
                                con.Open();

                                cmd.ExecuteNonQuery();

                                con.Close();

                            }
                        }
                    }
                }
                if (!string.IsNullOrEmpty(webinarDate))
                {
                    string htmlTemplateURL = Server.MapPath("~/EmailTemplates/WebinarNotAttended.html");
                    string facultyEmailhtmlTemplateURL = Server.MapPath("~/EmailTemplates/TaskSubmissionTemplate.html");
                    string logoPath = Server.MapPath("~/Content/Images/logo.png");
                    string epamTrainingCenterlogoPath = Server.MapPath("~/Content/Images/EpamTrainingCenterLogo.png");
                    if (collection["SendEmail"] == "on")
                    {
                        _model.SendAbsenteesListToCollege(webinarDate, facultyEmailhtmlTemplateURL, logoPath, epamTrainingCenterlogoPath);
                        //_model.SendEmailToAbsentees(webinarDate, htmlTemplateURL, logoPath, epamTrainingCenterlogoPath);
                        //_model.SendAbsenteesListToCollege(webinarDate, facultyEmailhtmlTemplateURL, logoPath, epamTrainingCenterlogoPath);
                    }
                }
                //if (ModelState.IsValid)
                //{

                //    if (upload != null && upload.ContentLength > 0)
                //    {
                //        // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                //        // to get started. This is how we avoid dependencies on ACE or Interop:
                //        Stream stream = upload.InputStream;

                //        // We return the interface, so that
                //        IExcelDataReader reader = null;


                //        if (upload.FileName.EndsWith(".xls"))
                //        {
                //            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                //        }
                //        else if (upload.FileName.EndsWith(".xlsx"))
                //        {
                //            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //        }
                //        else
                //        {
                //            ModelState.AddModelError("File", "This file format is not supported");
                //            return View();
                //        }

                //        reader.IsFirstRowAsColumnNames = true;

                //        DataSet result = reader.AsDataSet();
                //        reader.Close();
                //int webnairCount = 0;
                //int count = 0;
                //int webinarId = 0;
                //if (result.Tables.Count > 0)
                //{
                //    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                //    {
                //        if (result.Tables[0].Rows[i][0].ToString() == "Webinar ID")
                //        {
                //            webnairCount = i + 1;
                //        }
                //        if (i == webnairCount && webnairCount != 0)
                //        {
                //            SqlConnection con = new SqlConnection(Utilities.ConfigurationManager.RdConnectionString);

                //            SqlCommand cmd = new SqlCommand("Update_Webinar", con);
                //            cmd.CommandType = CommandType.StoredProcedure;
                //            cmd.Parameters.AddWithValue("@WebinarID", 0);
                //            cmd.Parameters.AddWithValue("@ActualStartDate", result.Tables[0].Rows[i][1].ToString());
                //            cmd.Parameters.AddWithValue("@Duration", result.Tables[0].Rows[i][2].ToString());
                //            cmd.Parameters.AddWithValue("@Registered", Convert.ToInt32(result.Tables[0].Rows[i][3]));
                //            cmd.Parameters.AddWithValue("@Attended", Convert.ToInt32(result.Tables[0].Rows[i][4]));
                //            cmd.Parameters.AddWithValue("@ClickedRegistrationLink", Convert.ToInt32(result.Tables[0].Rows[i][5]));
                //            cmd.Parameters.AddWithValue("@OpenedInvitation", Convert.ToInt32(result.Tables[0].Rows[i][6]));
                //            con.Open();

                //            webinarId = cmd.ExecuteNonQuery();
                //            webnairCount = 0;
                //            con.Close();
                //        }
                //        if (result.Tables[0].Rows[i][0].ToString() == "Attendee Details")
                //        {
                //            count = i + 1;
                //        }
                //        if (i > count && count != 0)
                //        {

                //            SqlConnection con = new SqlConnection(Utilities.ConfigurationManager.RdConnectionString);
                //            SqlCommand cmd = new SqlCommand("Update_AttendeeDetails", con);
                //            cmd.CommandType = CommandType.StoredProcedure;
                //            cmd.Parameters.AddWithValue("@AttendeeID", 0);
                //            cmd.Parameters.AddWithValue("@StudentID", 1);
                //            cmd.Parameters.AddWithValue("@WebinarID", webinarId);
                //            cmd.Parameters.AddWithValue("@Attended", result.Tables[0].Rows[i][0].ToString());
                //            cmd.Parameters.AddWithValue("@InterestRating", result.Tables[0].Rows[i][1].ToString());
                //            cmd.Parameters.AddWithValue("@LastName", result.Tables[0].Rows[i][2].ToString());
                //            cmd.Parameters.AddWithValue("@FirstName", result.Tables[0].Rows[i][3].ToString());
                //            cmd.Parameters.AddWithValue("@EmailAddress", result.Tables[0].Rows[i][4].ToString());
                //            cmd.Parameters.AddWithValue("@RegistrationDateTime", result.Tables[0].Rows[i][5].ToString());
                //            cmd.Parameters.AddWithValue("@JoinTimeLeaveTimeTimeinSession", result.Tables[0].Rows[i][6].ToString());
                //            cmd.Parameters.AddWithValue("@TimeinSession", result.Tables[0].Rows[i][7].ToString());
                //            cmd.Parameters.AddWithValue("@Unsubscribed", result.Tables[0].Rows[i][8].ToString());
                //            cmd.Parameters.AddWithValue("@Who", "Raj");
                //            con.Open();

                //            cmd.ExecuteNonQuery();

                //            con.Close();

                //        }
                //    }
                //}

                //        return View(result.Tables[0]);
                //    }
                //    else
                //    {
                //        ModelState.AddModelError("File", "Please Upload Your file");
                //    }
                //}
                _model.SuccessMessage = "Attendence Sheet uploaded successfully.";
            }
            catch (Exception ex)
            {
                _model.ErrorMessage = ex.Message;
            }
            return View(_model);
        }

        // GET: AttendeeDetails/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AttendeeDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: AttendeeDetails/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AttendeeDetails/Delete/5
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
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //       Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
