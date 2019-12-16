using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.DataAccess.Interfaces;
using EPAM.RDPreEducationPortal.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.RDPreEducationPortal.Web.Services.Models
{
    public class AttendeeDetailsModel : IAttendeeDetails
    {
        private readonly IAttendeeDetailsData _model;

        //inject dependency
        public AttendeeDetailsModel(IAttendeeDetailsData model)
        {
            this._model = model;
        }

        public int Save(AttendeeDetails model)
        {
            return _model.Save(model);
        }
    }
}