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
    
    public partial class Session
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public Nullable<System.DateTime> ScheduleDate { get; set; }
        public string ScheduleTime { get; set; }
        public string Description { get; set; }
        public string ReferenceLinks { get; set; }
        public string Tutor { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastEditedBy { get; set; }
        public Nullable<System.DateTime> LastEditedDate { get; set; }
    }
}