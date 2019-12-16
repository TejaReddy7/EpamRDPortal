using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.RDPreEducationPortal.CoreModels;

namespace EPAM.RDPreEducationPortal.DataAccess.Interfaces
{
    public interface IGitRepositoriesData
    {
        int Get(int repositoryId);
        List<GitRepositories> GetRepositories();
        int Save(GitRepositories repository);

        int UpdateEmailStatus(int gitRepositoryId);
    }
}
