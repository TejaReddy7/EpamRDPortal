//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EPAM.RDPreEducationPortal.Web.Site.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestsHosted
    {
        public int TestId { get; set; }
        public int RecruitmentId { get; set; }
        public string TestName { get; set; }
        public string TestKey { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> CutoffPercentage { get; set; }
        public Nullable<int> TotalScore { get; set; }
        public string Duration { get; set; }
        public Nullable<int> LocationId { get; set; }
        public Nullable<int> CodingWeightageOnSelection1 { get; set; }
        public Nullable<int> TechnicalWeightageOnSelection { get; set; }
        public Nullable<int> HRWeightageOnSelection { get; set; }
    }
}