using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class RecruitmentChartsReportModel
    {
        public string Category { get; set; }
        public int Selected { get; set; }
        public int Rejected { get; set; }
        public int NeedMoreEvaluation { get; set; }
    }
}