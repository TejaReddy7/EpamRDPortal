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
    
    public partial class Sonar_Issues
    {
        public int SonarIssueID { get; set; }
        public Nullable<int> GitRepositoryID { get; set; }
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
        public Nullable<System.DateTime> CreationDate { get; set; }
        public string UpdateDate { get; set; }
        public string Organization { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedDate { get; set; }
    }
}