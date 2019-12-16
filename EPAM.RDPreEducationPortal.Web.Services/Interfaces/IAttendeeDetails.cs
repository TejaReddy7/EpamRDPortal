using Epam.RDPreEducationPortal.CoreModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.RDPreEducationPortal.Web.Services.Interfaces
{
    public interface IAttendeeDetails
    {
        int Save(AttendeeDetails model);
    }
}