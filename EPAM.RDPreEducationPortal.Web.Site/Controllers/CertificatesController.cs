using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EPAM.RDPreEducationPortal.Utilities;
using Microsoft.Office.Interop.Word;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class CertificatesController : Controller
    {
        // GET: Certificates
        public ActionResult Index()
        {
            try
            {
                DataSet dsColleges = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_PEPStudents");
                foreach (DataRow dr in dsColleges.Tables[0].Rows)
                {
                    string candidateName = dr["CandidateName"].ToString().Trim();
                    // Opens the template document
                    // load the template containing merged fields
                    var document = new Aspose.Words.Document(@"D:\Test.docx");
                    // fill the fields with user data
                    document.MailMerge.Execute(
                        new string[] { "Testtt" },
                        new object[] { "John Doe"});
                    // save the result
                    document.Save(@"D:\merged.doc");
                    SendAbsenteesListToCollege("", "", "", candidateName);
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        static void FindAndReplace(Microsoft.Office.Interop.Word.Application fileOpen, object findText, object replaceWithText)
        {
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace
            fileOpen.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }
        public void SendAbsenteesListToCollege(string facultyEmailhtmlTemplateURL, string logoPath, string epamTrainingCenterlogoPath, string candidateName)
        {
            //var parameters = new SqlParameter[1];
            //parameters[0] = new SqlParameter("@CollegeID", 0);
            //DataSet dsColleges = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_Colleges", parameters);
            //foreach (DataRow drCollegeDetails in dsColleges.Tables[0].Rows)
            //{
            string textBody = "";
            string body = string.Empty;
            //using (StreamReader reader = new StreamReader(facultyEmailhtmlTemplateURL))
            //{
            //    body = reader.ReadToEnd();
            //}
            //body = body.Replace("@@CollegeName@@", drCollegeDetails["CollegeName"].ToString());
            //body = body.Replace("@@StudentName@@", dr["Name"].ToString());
            using (System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage())
            {
                mailMessage.From = new MailAddress("Rajasekhar_Tadepalli@epam.com");//new MailAddress(Environment.UserName + "@epam.com");
                mailMessage.Subject = "EPAM_PEP(Pre-education program) -E-Certificate";

                //mailMessage.To.Add(new MailAddress(dr["EmailAddressToSendNotifications"].ToString()));
                //string epamAssociates = drCollegeDetails["EpamAssociates"].ToString();
                //foreach (var ccEmailAddress in epamAssociates.Split(','))
                //{
                //    mailMessage.CC.Add(new MailAddress(ccEmailAddress));
                //}
                //string collegeFaculties = drCollegeDetails["CollegeFacultyEmails"].ToString();
                //foreach (var toEmailAddress in collegeFaculties.Split(','))
                //{
                //    mailMessage.To.Add(new MailAddress(toEmailAddress));
                //}
                mailMessage.To.Add(new MailAddress("Rajasekhar_Tadepalli@epam.com"));
                body = body.Replace("@@StudentTable@@", textBody);
                mailMessage.IsBodyHtml = true;
                mailMessage.Attachments.Add(new Attachment(@"D:\" + candidateName + ".pdf"));
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                //LinkedResource res = new LinkedResource(logoPath);
                //res.ContentId = "myImageID";
                //htmlView.LinkedResources.Add(res);
                //mailMessage.AlternateViews.Add(htmlView);

                //LinkedResource res1 = new LinkedResource(epamTrainingCenterlogoPath) { ContentId = "epamTrainingCenterLogo" };
                //htmlView.LinkedResources.Add(res1);
                //mailMessage.AlternateViews.Add(htmlView);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.SMTPHost;//ConfigurationManager.AppSettings["Host"];
                                                          //smtp.EnableSsl = true;//Convert.ToBoolean(Confi gurationManager.AppSettings["EnableSsl"]);ZHYSRFGFYWEEKBDUEDBEBCDJHNC. 
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smtp.Credentials = new NetworkCredential("Rajasekhar_Tadepalli@epam.com", "Epam@123456$$$$$");
                smtp.Port = Convert.ToInt32(ConfigurationManager.SMTPPort);//int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config  
                smtp.Send(mailMessage);
            }
            //}

        }
    }
}