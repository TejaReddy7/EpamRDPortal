using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.DataAccess.Interfaces;
using EPAM.RDPreEducationPortal.Web.Services.Interfaces;

namespace EPAM.RDPreEducationPortal.Web.Services.Models
{
    public class SonarIssuesModel: ISonarIssues
    {
        private readonly ISonarIssuesData _model;

        //inject dependency
        public SonarIssuesModel(ISonarIssuesData model)
        {
            this._model = model;
        }

        public int Save(SonarIssues model)
        {
            return _model.Save(model);
        }
    }
}