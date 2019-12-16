using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using EPAM.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Utilities;
using EPAM.RDPreEducationPortal.Web.Site.Common;
using RestSharp;
using RestSharp.Authenticators;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class GitRepositoriesModel : BaseClass
    {
        #region "Properties"
        public int GitRepositoryId { get; set; }

        [DisplayName("Git Repository Url")]
        public string RepositoryUrl { get; set; }

        [DisplayName("Task Name")]
        public string TaskName { get; set; }

        [DisplayName("Task Description")]
        public string TaskDescription { get; set; }

        public bool Status { get; set; }
        #endregion

        #region "Methods"

        public void ValidateGithubAccount(string repositoryUrl)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient("https://github.com/");
            var url = repositoryUrl.Replace("https://github.com/", "");
            var request = new RestRequest(url, Method.GET);
            request.AddHeader("header", "value");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!response.Content.Contains("sonar-project.properties"))
                {
                    ErrorMessage = "Please add sonar-project.properties file to your github project";
                }
            }
            else
            {
                ErrorMessage = "Please enter valid Github Url";
            }
        }
        public int Save(GitRepositories model)
        {
            var client = new RestClient(ConfigurationManager.RestClientBaseUri);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest("api/GitRepositories", Method.POST);
            request.AddParameter("GitRepositoryID", model.GitRepositoryID);
            request.AddParameter("StudentID", UserDetailsSingleton.Instance.UserId); // adds to POST or URL querystring based on Method
            request.AddParameter("TaskName", model.TaskName);
            request.AddParameter("TaskDescription", model.TaskDescription);
            request.AddParameter("RepositoryUrl", model.RepositoryUrl);
            request.AddParameter("Status", model.Status);
            request.AddParameter("CreatedBy", UserDetailsSingleton.Instance.Name);
            //UserDetailsSingleton.Instance.Name
            // easily add HTTP Headers
            request.AddHeader("header", "value");

            // execute the request
            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                SuccessMessage = "Record updated successfully.";
                //GenerateJenkinsfile(model.RepositoryUrl);
                System.Diagnostics.Process.Start(ConfigurationManager.GitCommandsBatFileLocation);

            }
            return Convert.ToInt32(response.Content);
        }
        public void SendTaskSubmissionEmail(string taskName, string taskSubmissionEmailTemplate, string logoPath, string epamTrainingCenterlogoPath)
        {
            //var parameters = new SqlParameter[1];
            //parameters[0] = new SqlParameter("@CollegeID", 0);
            //DataSet dsColleges = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_Colleges", parameters);
            //foreach (DataRow drCollegeDetails in dsColleges.Tables[0].Rows)
            //{
            //string webinarDate1 = Convert.ToDateTime(webinarDate).ToString("MMMM") + " " + Convert.ToDateTime(webinarDate).Day + "," + Convert.ToDateTime(webinarDate).Year;
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(taskSubmissionEmailTemplate))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("@@StudentName@@", UserDetailsSingleton.Instance.Name);
            body = body.Replace("@@TaskName@@", taskName);
            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.From = new MailAddress(Environment.UserName + "@epam.com");
                mailMessage.Subject = "EPAM_PEP(Pre-education program) -";

                mailMessage.To.Add(new MailAddress(UserDetailsSingleton.Instance.Email));
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
                mailMessage.IsBodyHtml = true;

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                LinkedResource res = new LinkedResource(logoPath);
                res.ContentId = "myImageID";
                htmlView.LinkedResources.Add(res);
                mailMessage.AlternateViews.Add(htmlView);
                LinkedResource res1 = new LinkedResource(epamTrainingCenterlogoPath) { ContentId = "epamTrainingCenterLogo" };
                htmlView.LinkedResources.Add(res1);
                mailMessage.AlternateViews.Add(htmlView);
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.SMTPHost;//ConfigurationManager.AppSettings["Host"];
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
                smtp.Port = Convert.ToInt32(ConfigurationManager.SMTPPort);//int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config  
                smtp.Send(mailMessage);
            }
            //}
        }
        public void CloneRepository(string gitUrl)
        {
            Process procStartInfo = new Process();

            procStartInfo.StartInfo.FileName = "cmd.exe";
            procStartInfo.StartInfo.CreateNoWindow = true;
            procStartInfo.StartInfo.RedirectStandardInput = true;
            procStartInfo.StartInfo.RedirectStandardOutput = true;
            procStartInfo.StartInfo.UseShellExecute = false;
            procStartInfo.Start();
            procStartInfo.StandardInput.WriteLine("cd D:\\Raja Sekhar\\RD Lab\\PEP\\Repository");
            procStartInfo.StandardInput.WriteLine("D:");
            procStartInfo.StandardInput.WriteLine("git clone " + gitUrl + "");
            procStartInfo.StandardInput.Flush();
            procStartInfo.StandardInput.Close();
            //procStartInfo.CreateNoWindow = true;
            procStartInfo.WaitForExit();
            Console.WriteLine(procStartInfo.StandardOutput.ReadToEnd());
            //Console.ReadKey();
        }
        public void RunJenkin(string gitUrl)
        {
            var projectName = gitUrl.Split('/')[gitUrl.Split('/').Length - 1].Split('.').First();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var client = new RestClient("http://localhost:8080/");
            client.Authenticator = new HttpBasicAuthenticator("admin", "123456");
            var url = "job/TestFolder1/buildWithParameters?token=1147be7874477324c8a20f042faa71f808&PROJECT_NAME=" + projectName + "";
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("header", "value");

            IRestResponse response = client.Execute(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (!response.Content.Contains("sonar-project.properties"))
                {
                    //ErrorMessage = "Please add sonar-project.properties file to your github project";
                }
            }
        }
        public void GenerateJenkinsfile(string url)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("node {");

            sb.AppendLine("stage('Checkout " + url + "') {");
            sb.AppendLine(
                "checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [],");
            sb.AppendLine("userRemoteConfigs: [[url: '" + url + "']]])");
            sb.AppendLine("}");
            sb.AppendLine("stage('SonarQube analysis')");
            sb.AppendLine("{");
            sb.AppendLine("def scannerHome = tool 'Sonar Qube';");
            sb.AppendLine("withSonarQubeEnv('SonarQube') {");
            //sb.AppendLine(
            //    @"sh ""C:/'Program Files (x86)'/Jenkins/tools/hudson.plugins.sonar.SonarRunnerInstallation/Sonar_Qube/bin/sonar-scanner""");
            sb.AppendLine(string.Format("sh \"{0}\"", ConfigurationManager.SonarQubeLocation));
            sb.AppendLine("}");
            sb.AppendLine("}");
            sb.AppendLine("}");
            System.IO.File.WriteAllText(ConfigurationManager.GithubProjectLocation, sb.ToString());
            System.Diagnostics.Process.Start(ConfigurationManager.GitCommandsBatFileLocation);
            //var list = GetRepositoriesList();
            //StringBuilder sb = new StringBuilder();
            //sb.AppendLine("node {");
            //foreach (var item in list)
            //{
            //    sb.AppendLine("stage('Checkout " + item.RepositoryUrl + "') {");
            //    sb.AppendLine(
            //        "checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [],");
            //    sb.AppendLine("userRemoteConfigs: [[url: '" + item.RepositoryUrl + "']]])");
            //    sb.AppendLine("}");
            //    sb.AppendLine("stage('SonarQube analysis')");
            //    sb.AppendLine("{");
            //    sb.AppendLine("def scannerHome = tool 'Sonar Qube';");
            //    sb.AppendLine("withSonarQubeEnv('SonarQube') {");
            //    //sb.AppendLine(
            //    //    @"sh ""C:/'Program Files (x86)'/Jenkins/tools/hudson.plugins.sonar.SonarRunnerInstallation/Sonar_Qube/bin/sonar-scanner""");
            //    sb.AppendLine(string.Format("sh \"{0}\"", ConfigurationManager.SonarQubeLocation));
            //    sb.AppendLine("}");
            //    sb.AppendLine("}");
            //}
            //sb.AppendLine("}");
            //System.IO.File.WriteAllText(ConfigurationManager.GithubProjectLocation, sb.ToString());
            //System.Diagnostics.Process.Start(ConfigurationManager.GitCommandsBatFileLocation);
            //RunJenkin();
        }
        public void GenerateJenkinsfile_Old()
        {
            var list = GetRepositoriesList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("node {");
            foreach (var item in list)
            {
                sb.AppendLine("stage('Checkout " + item.RepositoryUrl + "') {");
                sb.AppendLine(
                    "checkout([$class: 'GitSCM', branches: [[name: '*/master']], doGenerateSubmoduleConfigurations: false, extensions: [], submoduleCfg: [],");
                sb.AppendLine("userRemoteConfigs: [[url: '" + item.RepositoryUrl + "']]])");
                sb.AppendLine("}");
                sb.AppendLine("stage('SonarQube analysis')");
                sb.AppendLine("{");
                sb.AppendLine("def scannerHome = tool 'Sonar Qube';");
                sb.AppendLine("withSonarQubeEnv('SonarQube') {");
                //sb.AppendLine(
                //    @"sh ""C:/'Program Files (x86)'/Jenkins/tools/hudson.plugins.sonar.SonarRunnerInstallation/Sonar_Qube/bin/sonar-scanner""");
                sb.AppendLine(string.Format("sh \"{0}\"", ConfigurationManager.SonarQubeLocation));
                sb.AppendLine("}");
                sb.AppendLine("}");
            }
            sb.AppendLine("}");
            System.IO.File.WriteAllText(ConfigurationManager.GithubProjectLocation, sb.ToString());
            System.Diagnostics.Process.Start(ConfigurationManager.GitCommandsBatFileLocation);
        }
        public List<GitRepositories> GetRepositoriesList()
        {
            var client = new RestClient(ConfigurationManager.RestClientBaseUri);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest("api/GitRepositories", Method.GET);
            request.AddHeader("header", "value");
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            return client.Execute<List<GitRepositories>>(request).Data;
        }

        public void Edit(int id)
        {
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@GitRepositoryID", id);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_Student_GitRepositories", parameters);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                GitRepositoryId = (int)dr["GitRepositoryID"];
                RepositoryUrl = dr["RepositoryUrl"].ToString();
                TaskName = dr["TaskName"].ToString();
                TaskDescription = dr["TaskDescription"].ToString();
            }
        }
        public void Delete(int id)
        {
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@GitRepositoryID", id);
            SqlHelper.ExecuteNonQuery(ConfigurationManager.RdConnectionString, "Delete_Student_GitRepositories", parameters);
        }
        #endregion
    }
}