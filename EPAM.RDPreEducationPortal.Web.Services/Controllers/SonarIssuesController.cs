using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web.Http;
using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Web.Services.Interfaces;

namespace EPAM.RDPreEducationPortal.Web.Services.Controllers
{
    public class SonarIssuesController : ApiController
    {
        private readonly ISonarIssues _model;

        public SonarIssuesController(ISonarIssues model)
        {
            this._model = model;
        }

        // POST: api/SonarIssues
        public int Post(SonarIssues model)
        {
            return _model.Save(model);
        }
    }
}
