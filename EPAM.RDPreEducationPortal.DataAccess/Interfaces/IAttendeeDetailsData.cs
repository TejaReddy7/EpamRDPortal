using Epam.RDPreEducationPortal.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.RDPreEducationPortal.DataAccess.Interfaces
{
    public interface IAttendeeDetailsData
    {
        int Save(AttendeeDetails data);
    }
}
