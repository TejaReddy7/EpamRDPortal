using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.RDPreEducationPortal.CoreModels
{
    public class BaseClass
    {
        public int StudentId { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastEditedBy { get; set; }
        public string LastEditedDate { get; set; }

    }
}
