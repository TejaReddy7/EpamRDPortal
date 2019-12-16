using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.RDPreEducationPortal.CoreModels;

namespace EPAM.RDPreEducationPortal.DataAccess.Interfaces
{
    public interface ISonarIssuesData
    {
        int Save(SonarIssues data);
    }
}
