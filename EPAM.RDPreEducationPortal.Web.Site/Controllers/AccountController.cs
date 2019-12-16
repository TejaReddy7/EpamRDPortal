using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml;
using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Web.Site.Common;
using EPAM.RDPreEducationPortal.Web.Site.Models;
using log4net;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountController));
        private AccountModel _model;
        public AccountController()
        {
            _model = new AccountModel();
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(AccountDetails model)
        {
            if (ModelState.IsValid)
            {
                AccountModel accountModel = new AccountModel();
                accountModel = _model.GetUserOnUserNameAndPassword(model);
                if (accountModel.UserID > 0)
                {
                    //UserDetailsSingleton.Instance.Name = accountModel.FirstName + accountModel.LastName;
                    //UserDetailsSingleton.Instance.RoleName = accountModel.RoleName;
                    //UserDetailsSingleton.Instance.Role = accountModel.Role;
                    //UserDetailsSingleton.Instance.UserId = accountModel.UserID;
                    accountModel.Email = model.UserName;
                    Session["UserDetails"] = accountModel;
                    Session["Name"] = model.FirstName + "" + model.LastName;
                    Session["UserName"] = model.UserName;
                    Session["Role"] = accountModel.Role;
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.Clear();
                    ModelState.AddModelError("", "User name or Password is incorrect");
                    return View(accountModel);
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Login");
        }
        public ActionResult UploadStudents()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadStudents(HttpPostedFileBase upload)
        {
            DataSet result = new DataSet();
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
                int webinarId = 0;
                if (result.Tables.Count > 0)
                {
                    for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                    {

                        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=EpamRDPreEducation;Integrated Security=True");

                        SqlCommand cmd = new SqlCommand("Update_Users", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", 0);
                        cmd.Parameters.AddWithValue("@UserName", result.Tables[0].Rows[i][3].ToString());
                        //cmd.Parameters.AddWithValue("@Password", result.Tables[0].Rows[i][1].ToString());
                        //cmd.Parameters.AddWithValue("@RoleId", Convert.ToInt32(result.Tables[0].Rows[i][2]));
                        cmd.Parameters.AddWithValue("@FirstName", result.Tables[0].Rows[i][1].ToString());
                        //cmd.Parameters.AddWithValue("@MiddleName", result.Tables[0].Rows[i][4].ToString());
                        //cmd.Parameters.AddWithValue("@LastName", result.Tables[0].Rows[i][5].ToString());
                        cmd.Parameters.AddWithValue("@Gender", result.Tables[0].Rows[i][9]);
                        cmd.Parameters.AddWithValue("@Email", result.Tables[0].Rows[i][10].ToString());
                        cmd.Parameters.AddWithValue("@ContactNumber", "");
                        cmd.Parameters.AddWithValue("@Address", result.Tables[0].Rows[i][2].ToString());
                        cmd.Parameters.AddWithValue("@College", result.Tables[0].Rows[i][4]);
                        cmd.Parameters.AddWithValue("@MarksScored", Convert.ToInt32(result.Tables[0].Rows[i][7]));
                        cmd.Parameters.AddWithValue("@GraduationSpecialty", result.Tables[0].Rows[i][11].ToString());
                        cmd.Parameters.AddWithValue("@Who", "Admin");

                        con.Open();

                        webinarId = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            return View();
        }

    }
}