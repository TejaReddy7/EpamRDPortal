using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.RDPreEducationPortal.CoreModels
{
    public class AttendanceReport
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int CollegeId{ get; set; }
        public string CollegeName{ get; set; }
        public int NoOfStudentsAttended { get; set; }
        public int NoOfStudentsRegistered { get; set; }
        public string WebinarStartDate { get; set; }

        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string WebinarRegistrationDate { get; set; }
        public int? AttendedId { get; set; }
    }
}
