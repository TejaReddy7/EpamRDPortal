using EPAM.RDPreEducationPortal.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class ReportModel : Candidate_Details
    {
        #region "Properties"
        public string TestName { get; set; }
        public string CodingWeightageOnSelection { get; set; }
        public string TechnicalWeightageOnSelection { get; set; }
        public string HRWeightageOnSelection { get; set; }
        public string SelectionStatus { get; set; }
        public string TotalWeightage { get; set; }
        public string TestLocation { get; set; }
        #endregion
        public List<ReportModel> RecruitmentWeightagesList(int recruitmentId)
        {
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@RecruitmentId", recruitmentId);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_RecruitmentWeightages", parameters);
            List<ReportModel> list = new List<ReportModel>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ReportModel model = new ReportModel();
                    model.TestName = dr["TestName"].ToString();
                    model.CodingWeightageOnSelection = dr["CodingWeightageOnSelection1"].ToString();
                    model.TechnicalWeightageOnSelection = dr["TechnicalWeightageOnSelection"].ToString();
                    model.HRWeightageOnSelection = dr["HRWeightageOnSelection"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
    }
}