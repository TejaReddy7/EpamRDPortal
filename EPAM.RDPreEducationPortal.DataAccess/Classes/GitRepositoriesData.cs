using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EPAM.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.DataAccess.Interfaces;
using EPAM.RDPreEducationPortal.Utilities;

namespace EPAM.RDPreEducationPortal.DataAccess.Classes
{
    public class GitRepositoriesData : IGitRepositoriesData
    {
        #region Implementation of IGitRepositoriesData

        public int Get(int repositoryId)
        {
            throw new NotImplementedException();
        }
        public List<GitRepositories> GetRepositories()
        {
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@GitRepositoryID", 0);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "GET_Student_GitRepositories", parameters);
            var list = ds.Tables[0].ToList<GitRepositories>();
            return list;
        }

        public int Save(GitRepositories repository)
        {
            var parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@GitRepositoryID", repository.GitRepositoryID);
            parameters[1] = new SqlParameter("@StudentID", repository.StudentId);
            parameters[2] = new SqlParameter("@TaskName", repository.TaskName);
            parameters[3] = new SqlParameter("@TaskDescription", repository.TaskDescription);
            parameters[4] = new SqlParameter("@RepositoryUrl", repository.RepositoryUrl);
            parameters[5] = new SqlParameter("@Status", repository.Status);
            parameters[6] = new SqlParameter("@Who", repository.CreatedBy ?? "");
            return SqlHelper.ExecuteNonQuery(ConfigurationManager.RdConnectionString, "Update_Student_GitRepositories", parameters);
        }

        public int UpdateEmailStatus(int gitRepositoryId)
        {
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@GitRepositoryID", gitRepositoryId); 
            return SqlHelper.ExecuteNonQuery(ConfigurationManager.RdConnectionString, "Update_GitRepositories_EmailStatus", parameters);
        }
        #endregion
    }
}
