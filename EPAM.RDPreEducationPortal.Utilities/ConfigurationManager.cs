using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.RDPreEducationPortal.Utilities
{
    public static class ConfigurationManager
    {
        public static string RdConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["EPAMRDPreEducationPortal"].ConnectionString;

        public static string RestClientBaseUri => System.Configuration.ConfigurationManager.AppSettings["RestClientBaseURI"];

        public static string GithubProjectLocation => System.Configuration.ConfigurationManager.AppSettings["GithubProjectLocation"];
        public static string SonarQubeLocation => System.Configuration.ConfigurationManager.AppSettings["SonarQubeLocation"];
        public static string GitCommandsBatFileLocation => System.Configuration.ConfigurationManager.AppSettings["GitCommandsBatFileLocation"];

        public static string SMTPHost => System.Configuration.ConfigurationManager.AppSettings["SMTPHost"];
        public static string SMTPUserName => System.Configuration.ConfigurationManager.AppSettings["SMTPUserName"];
        public static string SMTPPassword => System.Configuration.ConfigurationManager.AppSettings["SMTPPassword"];
        public static string SMTPPort => System.Configuration.ConfigurationManager.AppSettings["SMTPPort"];

    }
}
