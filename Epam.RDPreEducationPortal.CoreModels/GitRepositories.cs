using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EPAM.RDPreEducationPortal.CoreModels
{
    public class GitRepositories: BaseClass
    {
        public int GitRepositoryID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public string RepositoryUrl { get; set; }
        public string ProjectName{ get; set; }
        public string SubmittedBy{ get; set; }
        public string SubmittedDate { get; set; }
        public string Email{ get; set; }

    }
}
