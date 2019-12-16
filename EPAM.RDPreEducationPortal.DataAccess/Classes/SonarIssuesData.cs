using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.DataAccess.Interfaces;
using EPAM.RDPreEducationPortal.Utilities;

namespace EPAM.RDPreEducationPortal.DataAccess.Classes
{
    public class SonarIssuesData : ISonarIssuesData
    {
        #region Implementation of ISonarIssuesData

        /// <inheritdoc />
        public int Save(SonarIssues data)
        {
            var parameters = new SqlParameter[19];
            parameters[0] = new SqlParameter("@SonarIssueID", data.SonarIssueId);
            parameters[1] = new SqlParameter("@GitRepositoryID", data.GitRepositoryID);
            parameters[2] = new SqlParameter("@Key", data.Key);
            parameters[3] = new SqlParameter("@Rule", data.Rule);
            parameters[4] = new SqlParameter("@Severity", data.Severity);
            parameters[5] = new SqlParameter("@Component", data.Component);
            parameters[6] = new SqlParameter("@Project", data.Project);
            parameters[7] = new SqlParameter("@Line", data.Line);
            parameters[8] = new SqlParameter("@Hash", data.Hash);
            parameters[9] = new SqlParameter("@TextRange", data.TextRange);
            parameters[10] = new SqlParameter("@Status", data.Status);
            parameters[11] = new SqlParameter("@Message", data.Message);
            parameters[12] = new SqlParameter("@Effort", data.Effort);
            parameters[13] = new SqlParameter("@Debt", data.Debt);
            parameters[14] = new SqlParameter("@Author", data.Author);
            parameters[15] = new SqlParameter("@CreationDate", data.CreationDate);
            parameters[16] = new SqlParameter("@UpdateDate", data.UpdateDate);
            parameters[17] = new SqlParameter("@Organization", data.Organization);
            parameters[18] = new SqlParameter("@Who", "Admin");
            return SqlHelper.ExecuteNonQuery(ConfigurationManager.RdConnectionString, "Update_Sonar_Issues", parameters);
        }

        #endregion
    }
}
