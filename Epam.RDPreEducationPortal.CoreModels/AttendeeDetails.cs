using EPAM.RDPreEducationPortal.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.RDPreEducationPortal.CoreModels
{
    public class AttendeeDetails : BaseClass
    {
        public int AttendeeID { get; set; }
        public int WebinarID { get; set; }
        public string Attended { get; set; }
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
