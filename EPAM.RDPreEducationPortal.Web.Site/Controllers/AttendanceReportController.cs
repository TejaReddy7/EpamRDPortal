using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Web.Site.Models;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class ChartModel
    {
        public IList GetChartData()
        {
            return new ArrayList
            {
                 new { X = "BVRIT", Y = 200 },
                 new { X = "BVRIT", Y = 300 },
                 new { X = "BVRIT", Y = 500 }
            };
        }
    }
    public class Student
    {
        public string Name { get; set; }
        public int StudentId { get; set; }
        public int Age { get; set; }
    }
    public class AttendanceReportController : Controller
    {
        private AttendanceReportModel _model;
        public AttendanceReportController()
        {
            _model = new AttendanceReportModel();
        }
        //GET: AttendanceReport
        public ActionResult Index()
        {
            //var model = new ChartModel();
            //var data = model.GetChartData();
            return View(_model);
        }
        public void ExporttoExcelStudentWise()
        {
            var studentsAttendenceList = _model.GetAttendanceStudentwise_Excel();
            var converter = new ListtoDataTableConverter();
            DataTable dt = _model.GetAttendanceStudentwise_Excel();//converter.ToDataTable(studentsAttendenceList);
            HttpContext.Response.Clear();
            HttpContext.Response.ClearContent();
            HttpContext.Response.ClearHeaders();
            HttpContext.Response.Buffer = true;
            HttpContext.Response.ContentType = "application/ms-excel";
            HttpContext.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");

            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            //sets font
            HttpContext.Response.Write("<font style='font-size:20.0pt; font-family:Calibri;'>");
            HttpContext.Response.Write("<BR><BR><BR>");
            //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
            HttpContext.Response.Write("<Table border='1' bgColor='#FF0000' " +
              "borderColor='#ccc' cellSpacing='0' cellPadding='0' " +
              "style='font-size:12.0pt; font-family:Calibri; background:#FFF;'> <TR>");
            //am getting my grid's column headers
            int columnscount = dt.Columns.Count;

            for (int j = 0; j < columnscount; j++)
            {      //write in new column
                HttpContext.Response.Write("<Td>");
                //Get column headers  and make it as bold in excel columns
                HttpContext.Response.Write("<B>");
                HttpContext.Response.Write(dt.Columns[j].ToString());//GridView_Result.Columns[j].HeaderText.ToString());
                HttpContext.Response.Write("</B>");
                HttpContext.Response.Write("</Td>");
            }
            HttpContext.Response.Write("</TR>");
            foreach (DataRow row in dt.Rows)
            {//write in new row
                HttpContext.Response.Write("<TR>");
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    HttpContext.Response.Write("<Td>");
                    HttpContext.Response.Write(row[i].ToString());
                    HttpContext.Response.Write("</Td>");
                }

                HttpContext.Response.Write("</TR>");
            }
            HttpContext.Response.Write("</Table>");
            HttpContext.Response.Write("</font>");
            HttpContext.Response.Flush();
            HttpContext.Response.End();
            //return View();
        }

        public void SendEmail()
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/WebinarNotAttended.html")))
            {
                body = reader.ReadToEnd();
            }
            using (MailMessage mailMessage = new MailMessage())
            {

                mailMessage.From = new MailAddress("trsmca35@gmail.com");

                mailMessage.Subject = "Test-7";

                mailMessage.Body = body;

                mailMessage.IsBodyHtml = true;

                mailMessage.To.Add(new MailAddress("RajaSekhar_Tadepalli@epam.com"));
                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";//ConfigurationManager.AppSettings["Host"];

                smtp.EnableSsl = true;//Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = "trsmca35@gmail.com";// ConfigurationManager.AppSettings["UserName"]; //reading from web.config 
                NetworkCred.Password = "tasson@35";//ConfigurationManager.AppSettings["Password"]; //reading from web.config  

                smtp.UseDefaultCredentials = true;

                smtp.Credentials = NetworkCred;

                smtp.Port = 587;//int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config  

                smtp.Send(mailMessage);

            }
            //var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    Credentials = new NetworkCredential("trsmca35@gmail.com", "tasson@35"),
            //    EnableSsl = true
            //};
            //string body = string.Empty;
            ////using streamreader for reading my htmltemplate   
            //using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplates/WebinarNotAttended.html")))
            //{
            //    body = reader.ReadToEnd();
            //}
            //client.Send("RajaSekhar_Tadepalli@epam.com", "RajaSekhar_Tadepalli@epam.com", "test-2", body);
        }

        
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }

                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }

                    dataTable.Rows.Add(values);

                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
    }
}