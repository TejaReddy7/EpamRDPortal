using EPAM.RDPreEducationPortal.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class DashboardModel
    {
        public string TestName { get; set; }
        public string PassedIn1stRound { get; set; }

        public string PassedIn2ndRound { get; set; }
        public string RejectedIn2ndRound { get; set; }

        public string PassedInGD { get; set; }
        public string RejectedInGD { get; set; }


        public string PassedInTechnical { get; set; }
        public string RejectedInTechnical { get; set; }

        public string PassedInHR { get; set; }
        public string RejectedInHR { get; set; }

        public string InterviewerName { get; set; }
        public string InterviewesTaken { get; set; }

        public string Accepted { get; set; }
        public string Rejected { get; set; }

        public string CollegeName { get; set; }
        public int Count { get; set; }

        public List<DashboardModel> RecruitmentSummaryList(int recruitmentId)
        {
            List<DashboardModel> list = new List<DashboardModel>();
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Recruitmentid", recruitmentId);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Report_RecruitmentSummary", parameters);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DashboardModel model = new DashboardModel();
                model.TestName = dr["testname"].ToString();
                model.PassedIn1stRound = dr["PassedIn1stRound"].ToString();
                model.PassedIn2ndRound = dr["PassedIn2ndRound"].ToString();
                //model.RejectedIn2ndRound = dr["PassedIn2ndRound"].ToString();
                model.PassedInGD = dr["PassedInGD"].ToString();
                model.RejectedInGD = dr["RejectedInGD"].ToString();
                model.PassedInTechnical = dr["PassedInTechnical"].ToString();
                model.RejectedInTechnical = dr["RejectedInTechnical"].ToString();
                model.PassedInHR = dr["PassedInHR"].ToString();
                model.RejectedInHR = dr["RejectedInHR"].ToString();
                list.Add(model);

            }
            return list;
        }
        public List<DashboardModel> InterviewPanelwiseSummaryList(int recruitmentId, string role)
        {
            List<DashboardModel> list = new List<DashboardModel>();
            var parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@Recruitmentid", recruitmentId);
            parameters[1] = new SqlParameter("@Role", role);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_TechnicalInterviewsPanelwise", parameters);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DashboardModel model = new DashboardModel();
                model.InterviewerName = dr["InterviewerName"].ToString();
                model.InterviewesTaken = dr["InterviewesTaken"].ToString();
                model.Accepted = dr["Accepted"].ToString();
                model.Rejected = dr["Rejected"].ToString();
                list.Add(model);

            }
            return list;
        }
        public List<DashboardModel> RecruitmentSummaryCollegeWise(int recruitmentId)
        {
            List<DashboardModel> list = new List<DashboardModel>();
            var parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@Recruitmentid", 1);
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_RecruitmentSummary_CollegeWise", parameters);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DashboardModel model = new DashboardModel();
                model.CollegeName = dr["GraduationCollegeName"].ToString();
                model.Count =Convert.ToInt32(dr["Count"]);
                list.Add(model);
            }
            return list.OrderByDescending(x => x.Count).ToList();
        }
    }
}