using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.RDPreEducationPortal.CoreModels;

namespace EPAM.RDPreEducationPortal.Web.Services.Interfaces
{
    public interface ISonarIssues
    {
        int Save(SonarIssues model);
    }
}
