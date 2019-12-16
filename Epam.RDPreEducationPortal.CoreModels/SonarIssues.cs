using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.RDPreEducationPortal.CoreModels;

namespace Epam.RDPreEducationPortal.CoreModels
{
    public class SonarIssues : BaseClass
    {
        public int SonarIssueId { get; set; }
        public int GitRepositoryID { get; set; }
        public string Key { get; set; }
        public string Rule { get; set; }
        public string Severity { get; set; }
        public string Component { get; set; }
        public string Project { get; set; }
        public string Line { get; set; }
        public string Hash { get; set; }
        public string TextRange { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string Effort { get; set; }
        public string Debt { get; set; }
        public string Author { get; set; }
        public string CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public string Organization { get; set; }
    }
}
