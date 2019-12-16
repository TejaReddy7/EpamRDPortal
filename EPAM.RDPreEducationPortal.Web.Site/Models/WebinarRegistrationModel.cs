using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class WebinarRegistrationModel
    {
        public int AttendeeID { get; set; }
        public int WebinarID { get; set; }
        public string WebinarName { get; set; }
        public string ScheduledStartDate { get; set; }
        public string Registered { get; set; }
        public string InterestRating { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public string RegistrationDateTime { get; set; }
        public string JoinTimeLeaveTimeTimeinSession { get; set; }
        public string TimeinSession { get; set; }
        public string Unsubscribed { get; set; }
    }
}