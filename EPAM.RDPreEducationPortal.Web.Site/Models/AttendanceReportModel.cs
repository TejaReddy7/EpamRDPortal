using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.Utilities;

namespace EPAM.RDPreEducationPortal.Web.Site.Models
{
    public class AttendanceReportModel
    {
        public List<AttendanceReport> GetAttendanceCollegewise()
        {
            List<AttendanceReport> list = new List<AttendanceReport>();
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString,
                "Report_WebinarAttendance_Collegewise");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                AttendanceReport model = new AttendanceReport
                {
                    CollegeId = Convert.ToInt32(dr["CollegeId"]),
                    CollegeName = dr["CollegeName"].ToString(),
                    NoOfStudentsAttended = Convert.ToInt32(dr["NumberOfStudentsAttended"]),
                    NoOfStudentsRegistered = Convert.ToInt32(dr["Registered"]),
                    WebinarStartDate = dr["ActualStartdate"].ToString()
                };
                list.Add(model);
            }


            return list;
        }

        public List<AttendanceReport> GetAttendanceStudentwise()
        {
            List<AttendanceReport> list = new List<AttendanceReport>();
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString,
                "Report_WebinarAttendance_Studentwise");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                AttendanceReport model = new AttendanceReport
                {
                    StudentId = Convert.ToInt32(dr["StudentId"]),
                    StudentName = dr["StudentName"].ToString(),
                    Email = dr["Email"].ToString(),
                    CollegeName = dr["CollegeName"].ToString(),
                    WebinarRegistrationDate = Convert.ToString(dr["WebinarRegistrationDate"]),
                    AttendedId = dr["AttendedId"] as int?,
                    WebinarStartDate = dr["WebinarDate"].ToString()
                };
                list.Add(model);
            }
            //foreach (var studentId in list.Select(x => x.StudentId).Distinct().ToList())
            //{
            //    foreach (var item in list.Where(x => x.StudentId == studentId))
            //    {
            //    }
            //}
            return list;
        }

        public DataTable GetAttendanceStudentwise_Excel()
        {
            List<AttendanceReportStudentwise> list = new List<AttendanceReportStudentwise>();
            DataSet ds = SqlHelper.FillDataSet(ConfigurationManager.RdConnectionString,
                "Report_WebinarAttendance_Studentwise");
            DataTable dt = new DataTable();
            //dt = ds.Tables[0].Clone();
            dt = ds.Tables[0].DefaultView.ToTable(true, "StudentName", "Email", "CollegeName");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                AttendanceReportStudentwise model = new AttendanceReportStudentwise
                {
                    StudentName = dr["StudentName"].ToString(),
                    Email = dr["Email"].ToString(),
                    CollegeName = dr["CollegeName"].ToString(),
                    WebinarRegistrationDate = Convert.ToString(dr["WebinarRegistrationDate"]),
                    //AttendedId = dr["AttendedId"] as int?,
                    WebinarStartDate = dr["WebinarDate"].ToString()
                };
                list.Add(model);
            }
            foreach (var webinarStartDate in list.Where(x => x.WebinarStartDate != "").Select(x => x.WebinarStartDate)
                .Distinct().ToList())
            {
                //foreach (var item in list.Where(x => x.WebinarStartDate == studentId))
                //{
                dt.Columns.Add(webinarStartDate + "_Registered");
                dt.Columns.Add(webinarStartDate + "_Attended");
                //}
            }
            var i = 0;
            foreach (DataRow row in dt.Rows)
            {
                foreach (var webinarStartDate in list.Where(x => x.WebinarStartDate != "")
                    .Select(x => x.WebinarStartDate).Distinct().ToList())
                {
                    DataRow[] dr = ds.Tables[0].Select("Email ='" + row["Email"].ToString() + "' and WebinarDate = '" + webinarStartDate + "'");
                    if (dr.Length > 0)
                    {
                        if (dr[0]["AttendedId"].ToString() != "")
                        {
                            dt.Rows[i][webinarStartDate + "_Attended"] = "Yes";
                        }
                        else
                        {
                            dt.Rows[i][webinarStartDate + "_Attended"] = "No";
                        }
                        if (dr[0]["WebinarRegistrationDate"].ToString() != "")
                        {
                            dt.Rows[i][webinarStartDate + "_Registered"] = "Yes";
                        }
                        else
                        {
                            dt.Rows[i][webinarStartDate + "_Registered"] = "No";
                        }
                    }
                    else
                    {
                        dt.Rows[i][webinarStartDate + "_Attended"] = "No";
                        dt.Rows[i][webinarStartDate + "_Registered"] = "No";
                    }
                }
                i++;
            }
            return dt;
        }
    }
}