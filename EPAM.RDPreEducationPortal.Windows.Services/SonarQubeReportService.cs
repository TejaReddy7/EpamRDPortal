using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using EPAM.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Utilities;
using EPAM.RDPreEducationPortal.Windows.Services.Classes;
using Newtonsoft.Json;
using RestSharp;

namespace EPAM.RDPreEducationPortal.Windows.Services
{
    public partial class SonarQubeReportService : ServiceBase
    {
        public SonarQubeReportService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            #region "Bugs"

            var client = new RestClient(System.Configuration.ConfigurationManager.AppSettings["SonarQubeUri"]);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest("/api/issues/search?componentKeys=Slack&s=FILE_LINE&resolved=false&types=CODE_SMELL,BUG,VULNERABILITY", Method.GET);
            request.AddHeader("header", "value");
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            //return client.Execute<List<GitRepositories>>(request).Data;

            #endregion
        }
        //public void OnStart()
        //{
        //    //var client = new RestClient(ConfigurationManager.AppSettings["RestClientBaseUri"]);
        //    //// client.Authenticator = new HttpBasicAuthenticator(username, password);
        //    //var request = new RestRequest("api/GitRepositories", Method.GET);
        //    //request.AddHeader("header", "value");
        //    //// execute the request
        //    //IRestResponse response = client.Execute(request);
        //    //var content = response.Content; // raw content as string
        //    //var gitRepositories = client.Execute<List<GitRepositories>>(request).Data;
        //    //foreach (var gitRepository in gitRepositories)
        //    //{
        //    //    //"https://github.com/trsmca/slack".Split('/').Last();
        //    //}

        //    var client = new RestClient(ConfigurationManager.AppSettings["SonarQubeUri"]);
        //    // client.Authenticator = new HttpBasicAuthenticator(username, password);
        //    var request = new RestRequest("/api/issues/search?componentKeys=slack&s=FILE_LINE&resolved=false&types=BUG", Method.GET);
        //    request.AddHeader("header", "value");
        //    // execute the request
        //    var response = client.Execute(request);
        //    var sonarIssues = JsonConvert.DeserializeObject<SonarIssues>(response.Content);

        //    foreach (var issue in sonarIssues.Issues)
        //    {
        //        client = new RestClient(Utilities.ConfigurationManager.RestClientBaseUri);
        //        // client.Authenticator = new HttpBasicAuthenticator(username, password);
        //        //request.AddParameter("SonarIssueID", issue.s);
        //        request.AddParameter("GitRepositoryID", 0);
        //        request.AddParameter("Key", issue.Key);
        //        request.AddParameter("Rule", issue.Rule);
        //        request.AddParameter("Severity", issue.Severity);
        //        request.AddParameter("Component", issue.Component);
        //        request.AddParameter("Project", issue.Project);
        //        request.AddParameter("Line", issue.Line);
        //        request.AddParameter("Hash", issue.Hash);
        //        request.AddParameter("TextRange", issue.TextRange);
        //        request.AddParameter("Status", issue.Status);
        //        request.AddParameter("Message", issue.Message);
        //        request.AddParameter("Effort", issue.Effort);
        //        request.AddParameter("Debt", issue.Debt);
        //        request.AddParameter("Author", issue.Author);
        //        request.AddParameter("CreationDate", issue.CreationDate);
        //        request.AddParameter("UpdateDate", issue.UpdateDate);
        //        request.AddParameter("Organization", issue.Organization);
        //        request.AddParameter("Who", "Admin");
        //        request.AddParameter("Who", "Admin");

        //        // easily add HTTP Headers
        //        request.AddHeader("header", "value");

        //        // execute the request
        //        response = client.Execute(request);
        //    }
        //    //return client.Execute<List<GitRepositories>>(request).Data;

        //}
        public void OnStart()
        {
            var client = new RestClient(System.Configuration.ConfigurationManager.AppSettings["RestClientBaseUri"]);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest("api/GitRepositories", Method.GET);
            request.AddHeader("header", "value");
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            var gitRepositories = client.Execute<List<GitRepositories>>(request).Data;
            foreach (var gitRepository in gitRepositories)
            {
                //"https://github.com/trsmca/slack".Split('/').Last();
                client = new RestClient(System.Configuration.ConfigurationManager.AppSettings["SonarQubeUri"]);
                // client.Authenticator = new HttpBasicAuthenticator(username, password);
                if (gitRepository.RepositoryUrl != null)
                {
                    request = new RestRequest("/api/issues/search?componentKeys=" + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(gitRepository.ProjectName) + "&s=FILE_LINE&resolved=false", Method.GET);
                    request.AddHeader("header", "value");
                    // execute the request
                    response = client.Execute(request);
                    var sonarIssues = JsonConvert.DeserializeObject<SonarIssues>(response.Content);

                    if (sonarIssues != null)
                    {
                        if (sonarIssues.Issues.Count > 0)
                        {
                            foreach (var data in sonarIssues.Issues)
                            {
                                var parameters = new SqlParameter[18];
                                parameters[0] = new SqlParameter("@SonarIssueID", 0);
                                parameters[1] = new SqlParameter("@GitRepositoryID", gitRepository.GitRepositoryID);
                                parameters[2] = new SqlParameter("@Key", data.Key);
                                parameters[3] = new SqlParameter("@Rule", data.Rule);
                                parameters[4] = new SqlParameter("@Severity", data.Severity);
                                parameters[5] = new SqlParameter("@Component", data.Component);
                                parameters[6] = new SqlParameter("@Project", data.Project);
                                parameters[7] = new SqlParameter("@Line", data.Line);
                                parameters[8] = new SqlParameter("@Hash", data.Hash ?? "");
                                //parameters[9] = new SqlParameter("@TextRange", data.TextRange);
                                parameters[9] = new SqlParameter("@Status", data.Status ?? "");
                                parameters[10] = new SqlParameter("@Message", data.Message ?? "");
                                parameters[11] = new SqlParameter("@Effort", data.Effort ?? "");
                                parameters[12] = new SqlParameter("@Debt", data.Debt ?? "");
                                parameters[13] = new SqlParameter("@Author", data.Author ?? "");
                                parameters[14] = new SqlParameter("@CreationDate", data.CreationDate);
                                parameters[15] = new SqlParameter("@UpdateDate", data.UpdateDate);
                                parameters[16] = new SqlParameter("@Organization", data.Organization ?? "");
                                parameters[17] = new SqlParameter("@Who", "Admin");
                                SqlHelper.ExecuteNonQuery(Utilities.ConfigurationManager.RdConnectionString,
                                    "Update_Sonar_Issues", parameters);
                                //client = new RestClient(Utilities.ConfigurationManager.RestClientBaseUri);
                                //var sonarIssuesrequest = new RestRequest("api/SonarIssues", Method.POST);
                                //// client.Authenticator = new HttpBasicAuthenticator(username, password);
                                ////request.AddParameter("SonarIssueID", issue.s);
                                //sonarIssuesrequest.AddParameter("GitRepositoryID", 0);
                                //sonarIssuesrequest.AddParameter("Key", issue.Key);
                                //sonarIssuesrequest.AddParameter("Rule", issue.Rule);
                                //sonarIssuesrequest.AddParameter("Severity", issue.Severity);
                                //sonarIssuesrequest.AddParameter("Component", issue.Component);
                                //sonarIssuesrequest.AddParameter("Project", issue.Project);
                                //sonarIssuesrequest.AddParameter("Line", issue.Line);
                                //sonarIssuesrequest.AddParameter("Hash", issue.Hash);
                                //sonarIssuesrequest.AddParameter("TextRange", issue.TextRange);
                                //sonarIssuesrequest.AddParameter("Status", issue.Status);
                                //sonarIssuesrequest.AddParameter("Message", issue.Message);
                                //sonarIssuesrequest.AddParameter("Effort", issue.Effort);
                                //sonarIssuesrequest.AddParameter("Debt", issue.Debt);
                                //sonarIssuesrequest.AddParameter("Author", issue.Author);
                                //sonarIssuesrequest.AddParameter("CreationDate", issue.CreationDate);
                                //sonarIssuesrequest.AddParameter("UpdateDate", issue.UpdateDate);
                                //sonarIssuesrequest.AddParameter("Organization", issue.Organization);
                                //sonarIssuesrequest.AddParameter("Who", "Admin");
                                //// easily add HTTP Headers
                                //sonarIssuesrequest.AddHeader("header", "value");

                                //// execute the request
                                //response = client.Execute(sonarIssuesrequest);
                            }
                            //string textBody =
                            //    " <table style='font-family:Calibri;font-size:12px;color:darkslateblue' class='table table-responsive table-striped table-bordered' border=" +
                            //    1 + " cellpadding=" + 5 + " cellspacing=" + 0 +
                            //    "><tr bgcolor='#D3D3D3'><td><b>Component</b></td><td><b>Message</b></td><td><b>Rule</b></td><td><b>Severity</b></td><td><b>Effort</b></td></tr>";
                            StringBuilder sb = new StringBuilder();
                            sb.Append("<table>");
                            string body;
                            DriveInfo cDrive = new DriveInfo(System.Environment.CurrentDirectory);
                            var driverPath = cDrive.RootDirectory;
                            string htmlTemplateURL =
                                "D:\\Raja Sekhar\\RD Lab\\RD Projects\\PreEducationPortal\\EPAM.RDPreEducationPortal.Windows.Services\\EmailTemplates\\StudentSonarReport.html"; // AppDomain.CurrentDomain.BaseDirectory + "\\EmailTemplates\\StudentSonarReport.html";
                            using (var reader = new StreamReader(htmlTemplateURL))
                            {
                                body = reader.ReadToEnd();
                            }
                            body = body.Replace("@@SubmittedDate@@", gitRepository.SubmittedDate);
                            body = body.Replace("@@TaskName@@", gitRepository.TaskName);
                            //body = body.Replace("@@CollegeName@@", drCollegeDetails["CollegeName"].ToString());
                            //body = body.Replace("@@StudentName@@", dr["Name"].ToString());
                            using (var mailMessage = new MailMessage())
                            {
                                mailMessage.From = new MailAddress(Environment.UserName + "@epam.com");
                                var parameters = new SqlParameter[1];
                                parameters[0] = new SqlParameter("@GitRepositoryID", gitRepository.GitRepositoryID);
                                var ds = SqlHelper.FillDataSet(Utilities.ConfigurationManager.RdConnectionString, "Get_UserDetails_SonarReport", parameters);
                                mailMessage.Subject = "EPAM_PEP(Pre-education program)-";
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    body = body.Replace("@@StudentName@@", dr["Name"].ToString());
                                    mailMessage.To.Add(new MailAddress(dr["Email"].ToString()));
                                    var epamAssociates = dr["EpamAssociates"].ToString();
                                    foreach (var bccEmailAddress in epamAssociates.Split(','))
                                    {
                                        mailMessage.Bcc.Add(new MailAddress(bccEmailAddress));
                                    }
                                }
                                foreach (var data in sonarIssues.Issues)
                                {
                                    sb.Append("<tr><td style=\"padding:10px\">" + data.Component + "</td>");
                                    sb.Append("</tr><tr><td><table style=\"width: 700px; background-color: #ffeaea; box-shadow: 0 0 0 1px #ffeaea;\">");
                                    sb.Append("<tr>");
                                    sb.Append("<td style=\"padding-left:10px;padding-top:10px;font-weight:bold;font-size:16px \">");
                                    sb.Append(data.Message);
                                    sb.Append("</td></tr><tr><td style=\"padding-left:10px;padding-top:5px;padding-bottom:5px\">");
                                    sb.Append("<span><b>Severity : </b>" + data.Severity + "</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                                    sb.Append("<span <b>Effort : </b>" + data.Effort + "</span>");
                                    sb.Append("</td></tr></table></td></tr>");
                                    //textBody += "<tr><td>" + data.Message + "</td></tr>";
                                }
                                sb.Append("</table>");
                                body = body.Replace("@@StudentTable@@", sb.ToString());
                                mailMessage.IsBodyHtml = true;

                                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                                LinkedResource res = new LinkedResource(@"D:\Raja Sekhar\RD Lab\RD Projects\PreEducationPortal\EPAM.RDPreEducationPortal.Windows.Services\Content\logo.png");
                                res.ContentId = "myImageID";
                                htmlView.LinkedResources.Add(res);
                                mailMessage.AlternateViews.Add(htmlView);

                                LinkedResource res1 = new LinkedResource(@"D:\Raja Sekhar\RD Lab\RD Projects\PreEducationPortal\EPAM.RDPreEducationPortal.Windows.Services\Content\EpamTrainingCenterLogo.png") { ContentId = "epamTrainingCenterLogo" };
                                htmlView.LinkedResources.Add(res1);
                                mailMessage.AlternateViews.Add(htmlView);

                                mailMessage.Body = body;
                                var smtp = new SmtpClient
                                {
                                    Host = Utilities.ConfigurationManager.SMTPHost,
                                    DeliveryMethod = SmtpDeliveryMethod.Network,
                                    Credentials = System.Net.CredentialCache.DefaultNetworkCredentials,
                                    Port = Convert.ToInt32(Utilities.ConfigurationManager.SMTPPort)
                                };
                                smtp.Send(mailMessage);
                            }
                            #region "Update Send Email Status"
                            var apiclient = new RestClient(System.Configuration.ConfigurationManager.AppSettings["RestClientBaseUri"]);
                                request = new RestRequest("api/GitRepositories/UpdateEmailStatus?gitRepositoryId=" + gitRepository.GitRepositoryID, Method.GET);
                                request.AddHeader("header", "value");
                                // execute the request
                                response = apiclient.Execute(request);
                                content = response.Content; // raw content as string

                                #endregion
                        }
                    }
                }
                //return client.Execute<List<GitRepositories>>(request).Data;
            }
        }
        protected override void OnStop()
        {
        }
        public void SendSonarReportEmail()
        {

        }
    }
}
