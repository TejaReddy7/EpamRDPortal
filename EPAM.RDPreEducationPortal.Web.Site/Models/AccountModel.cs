using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class AccountModel : BaseClass
    {
        public int UserID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required] 
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public int CollegeId { get; set; }
        public string RoleName { get; set; }
        public string Role { get; set; }
        public string CollegeName { get; set; }
        #region "Methods"

        public int ValidateUser(AccountDetails model)
        {
            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UserName", model.UserName);
            parameters[1] = new SqlParameter("@Password", model.Password);
            return (int)SqlHelper.ExecuteScalar(ConfigurationManager.RdConnectionString, "ValidateUser", parameters);
        }
        public AccountModel GetUserOnUserNameAndPassword(AccountDetails model)
        {
            AccountModel _model = new AccountModel();
            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@UserName", model.UserName);
            parameters[1] = new SqlParameter("@Password", model.Password);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_UsersOnUserNameAndPassword", parameters);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                _model.UserID = Convert.ToInt32(row["UserId"]);
                _model.RoleName = row["RoleName"].ToString();
                _model.Role = row["RoleKey"].ToString();
                _model.FirstName = row["FirstName"].ToString();
            }
            return _model;
        }
        #endregion

    }
}