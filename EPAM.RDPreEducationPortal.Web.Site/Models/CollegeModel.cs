using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using EPAM.RDPreEducationPortal.Utilities;
using EPAM.RDPreEducationPortal.Web.Site.EntityFramework;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class CollegeModel : College
    {
        private EpamRDPreEducationEntities db = new EpamRDPreEducationEntities();
        public string CollegeName { get; set; }
        public string NewCollegeName { get; set; }

        public List<CollegeModel> GetCollegesList()
        {
            var list = new List<CollegeModel>();
            var ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString, "Get_CandidatesCollegesList");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        var model = new CollegeModel
                        {
                            CollegeName = dr["CollegeName"].ToString(),
                        };
                        list.Add(model);
                    }
                }
            }
            return list;
        }
        public List<College> GetColleges()
        {
            var list = db.Colleges.ToList();
            return list;
        }
    }
}