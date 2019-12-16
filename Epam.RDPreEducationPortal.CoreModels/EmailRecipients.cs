using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.RDPreEducationPortal.CoreModels
{
    public class EmailRecipientsModel
    {
        public int EmailRecipientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int CollegeId { get; set; }
        public int IsActive { get; set; }
    }
}