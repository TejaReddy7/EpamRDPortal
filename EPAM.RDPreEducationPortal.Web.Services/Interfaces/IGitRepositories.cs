using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.RDPreEducationPortal.CoreModels;

namespace EPAM.RDPreEducationPortal.Web.Services.Interfaces
{
    public interface IGitRepositories
    {
        int Save(GitRepositories model);
        IEnumerable<GitRepositories> GitRepositoriesList();

        int UpdateEmailStatus(int gitRepositoryId);
    }
}
