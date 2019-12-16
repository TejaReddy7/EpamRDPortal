using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EPAM.RDPreEducationPortal.Web.Services.Controllers
{
    public class AttendeeDetailsController : ApiController
    {
        private readonly IAttendeeDetails _model;

        //inject dependency
        public AttendeeDetailsController(IAttendeeDetails model)
        {
            this._model = model;
        }
        // POST: api/AttendeeDetails
        public void Post(AttendeeDetails model)
        {
            _model.Save(model);
        }
    }
}
