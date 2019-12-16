using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Web;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class AttendeeDetailsModel : BaseClass
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();
        #region "Properties"
        public int AttendeeID { get; set; }
        public int WebinarID { get; set; }
        public string Attended { get; set; }
        public string InterestRating { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public string RegistrationDateTime { get; set; }
        public string JoinTimeLeaveTimeTimeinSession { get; set; }
        public string TimeinSession { get; set; }
        public string Unsubscribed { get; set; }
        #endregion
        #region "Methods"
        public void SendEmailToAbsentees(string webinarDate, string htmlTemplateURL, string logoPath, string epamTrainingCenterlogoPath)
        {
            string webinarDate1 = Convert.ToDateTime(webinarDate).ToString("MMMM") + " " + Convert.ToDateTime(webinarDate).Day + "," + Convert.ToDateTime(webinarDate).Year;
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@WebinarDate", Convert.ToDateTime(webinarDate));
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_WebinarAbsentees", parameters);
            int i = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Thread.Sleep(2000);
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(htmlTemplateURL))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("@@WebinarDate@@", webinarDate1);
                body = body.Replace("@@StudentName@@", dr["Name"].ToString());
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(Environment.UserName + "@epam.com");
                        mailMessage.Subject = "EPAM__PEP(Pre-education program) - " + webinarDate1 + "";
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;

                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                        LinkedResource res = new LinkedResource(logoPath) { ContentId = "myImageID" };
                        htmlView.LinkedResources.Add(res);
                        mailMessage.AlternateViews.Add(htmlView);

                        LinkedResource res1 = new LinkedResource(epamTrainingCenterlogoPath) { ContentId = "epamTrainingCenterLogo" };
                        htmlView.LinkedResources.Add(res1);
                        mailMessage.AlternateViews.Add(htmlView);

                        mailMessage.To.Add(new MailAddress(dr["EmailAddressToSendNotifications"].ToString()));
                        string epamAssociates = dr["EpamAssociates"].ToString();
                        foreach (var ccEmailAddress in epamAssociates.Split(','))
                        {
                            mailMessage.CC.Add(new MailAddress(ccEmailAddress));
                        }

                        var obj = new Email
                        {
                            ToEmail = dr["EmailAddressToSendNotifications"].ToString(),
                            FromEmail = Environment.UserName + "@epam.com",
                            CC = epamAssociates,
                            Subject = "EPAM__PEP(Pre-education program) - " + webinarDate + "",
                            EmailContent = body,
                            WebinarDate = Convert.ToDateTime(webinarDate),
                            CreatedDate = DateTime.Now,
                            Status = true
                        };
                        db.Emails.Add(obj);
                        db.SaveChanges();

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = ConfigurationManager.SMTPHost;//ConfigurationManager.AppSettings["Host"];
                        //smtp.EnableSsl = true;//Convert.ToBoolean(Confi gurationManager.AppSettings["EnableSsl"]);ZHYSRFGFYWEEKBDUEDBEBCDJHNC. 
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        //System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        //NetworkCred.UserName = ConfigurationManager.SMTPUserName;// ConfigurationManager.AppSettings["UserName"]; //reading from web.config 
                        //NetworkCred.Password = ConfigurationManager.SMTPPassword;//ConfigurationManager.AppSettings["Password"]; //reading from web.config  
                        //smtp.UseDefaultCredentials = true;
                        smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                        smtp.Port = Convert.ToInt32(ConfigurationManager.SMTPPort);
                        //ConfigurationManager.AppSettings["Host"];
                        //Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                        //System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                        //NetworkCred.UserName = ConfigurationManager.SMTPUserName;// ConfigurationManager.AppSettings["UserName"]; //reading from web.config 
                        //NetworkCred.Password = ConfigurationManager.SMTPPassword;//ConfigurationManager.AppSettings["Password"]; //reading from web.config  
                        //smtp.UseDefaultCredentials = true;
                        //smtp.Credentials = NetworkCred;
                        //int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config  
                        //smtp.Timeout = 1200000;
                        //smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                        smtp.Send(mailMessage);
                }

                #endregion
            }
        }
        public List<Colleges> GetColleges()
        {
            List<Colleges> list = new List<Colleges>();
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@CollegeID", 0);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_Colleges", parameters);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Colleges model = new Colleges();
                model.CollegeId = (int)dr["CollegeID"];
                model.CollegeName = dr["CollegeName"].ToString();
                list.Add(model);
            }
            return list;
        }
        public void SendAbsenteesListToCollege(string webinarDate, string facultyEmailhtmlTemplateURL, string logoPath, string epamTrainingCenterlogoPath)
        {
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@CollegeID", 0);
            DataSet dsColleges = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_Colleges", parameters);
            foreach (DataRow drCollegeDetails in dsColleges.Tables[0].Rows)
            {
                Thread.Sleep(2000);
                string webinarDate1 = Convert.ToDateTime(webinarDate).ToString("MMMM")+ " " + Convert.ToDateTime(webinarDate).Day + "," + Convert.ToDateTime(webinarDate).Year;
                string textBody = " <table style='font-family:Calibri;font-size:12px;color:darkslateblue' class='table table-responsive table-striped table-bordered' border=" + 1 + " cellpadding=" + 5 + " cellspacing=" + 0 + "><tr bgcolor='#D3D3D3'><td><b>Student Name</b></td> <td> <b> Student Email</b> </td></tr>";
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(facultyEmailhtmlTemplateURL))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("@@WebinarDate@@", webinarDate1);
                body = body.Replace("@@CollegeName@@", drCollegeDetails["CollegeName"].ToString());
                //body = body.Replace("@@StudentName@@", dr["Name"].ToString());
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(Environment.UserName + "@epam.com");
                    mailMessage.Subject = "EPAM_PEP(Pre-education program) - " + webinarDate1 + "";

                    //mailMessage.To.Add(new MailAddress(dr["EmailAddressToSendNotifications"].ToString()));
                    string epamAssociates = drCollegeDetails["EpamAssociates"].ToString();
                    foreach (var ccEmailAddress in epamAssociates.Split(','))
                    {
                        mailMessage.CC.Add(new MailAddress(ccEmailAddress));
                    }
                    string collegeFaculties = drCollegeDetails["CollegeFacultyEmails"].ToString();
                    foreach (var toEmailAddress in collegeFaculties.Split(','))
                    {
                        mailMessage.To.Add(new MailAddress(toEmailAddress));
                    }
                    parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@WebinarDate", webinarDate);
                    parameters[1] = new SqlParameter("@CollegeID", Convert.ToInt32(drCollegeDetails["CollegeID"]));
                    DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_WebinarAbsenteesCollegewise", parameters);
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            textBody += "<tr><td>" + dr["Name"] + "</td><td> " + dr["Email"] + "</td></tr>";
                        }
                    }
                    textBody += "</table>";
                    body = body.Replace("@@StudentTable@@", textBody);
                    body = body.Replace("@@webinardate@@", Convert.ToDateTime(webinarDate).ToShortDateString());
                    mailMessage.IsBodyHtml = true;

                    AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                    LinkedResource res = new LinkedResource(logoPath);
                    res.ContentId = "myImageID";
                    htmlView.LinkedResources.Add(res);
                    mailMessage.AlternateViews.Add(htmlView);

                    LinkedResource res1 = new LinkedResource(epamTrainingCenterlogoPath) { ContentId = "epamTrainingCenterLogo" };
                    htmlView.LinkedResources.Add(res1);
                    mailMessage.AlternateViews.Add(htmlView);
                    //string htmlBody = @"<img src='cid:" + res.ContentId + @"'/>";
                    //mailMessage.Body = htmlBody;

                    //var smtpClient = new SmtpClient
                    //{
                    //    Host = "MAIL",
                    //    Port = 25,
                    //    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //    Credentials = System.Net.CredentialCache.DefaultNetworkCredentials
                    //};

                    //var mailMessage = new MailMessage
                    //{
                    //    Body = "Testing",
                    //    From = new MailAddress(Environment.UserName + "@epam.com"),
                    //    Subject = "Testing",
                    //    Priority = MailPriority.Normal
                    //};

                    //mailMessage.To.Add("Rajasekhar_Tadepalli@epam.com");

                    //smtpClient.Send(mailMessage);

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.SMTPHost;//ConfigurationManager.AppSettings["Host"];
                                                              //smtp.EnableSsl = true;//Convert.ToBoolean(Confi gurationManager.AppSettings["EnableSsl"]);ZHYSRFGFYWEEKBDUEDBEBCDJHNC. 
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    //NetworkCred.UserName = ConfigurationManager.SMTPUserName;// ConfigurationManager.AppSettings["UserName"]; //reading from web.config 
                    //NetworkCred.Password = ConfigurationManager.SMTPPassword;//ConfigurationManager.AppSettings["Password"]; //reading from web.config  
                    //smtp.UseDefaultCredentials = true;
                    smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                    smtp.Port = Convert.ToInt32(ConfigurationManager.SMTPPort);//int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config  
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            smtp.Send(mailMessage);
                        }
                    }
                }

            }
        }
    }
}