using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class CandidateDetails : BaseClass
    {
        public string FirstRoundCodingMarks { get; set; }
        public string PreviousAssessment { get; set; }

        public string Status { get; set; }
        public string SecondRoundMarks { get; set; }
        public int TRScore { get; set; }
        public string TRInterviewer { get; set; }
        public int GDStatus{ get; set; }
    }
}