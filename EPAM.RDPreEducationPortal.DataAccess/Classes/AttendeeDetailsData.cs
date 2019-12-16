using Epam.RDPreEducationPortal.CoreModels;
using EPAM.RDPreEducationPortal.DataAccess.Interfaces;
using EPAM.RDPreEducationPortal.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.RDPreEducationPortal.DataAccess.Classes
{
    public class AttendeeDetailsData : IAttendeeDetailsData
    {
        public int Save(AttendeeDetails data)
        {
                var parameters = new SqlParameter[13];
                parameters[0] = new SqlParameter("@AttendeeID", 0);
                parameters[1] = new SqlParameter("@StudentID", 1);
                parameters[2] = new SqlParameter("@WebinarID", data.WebinarID);
                parameters[3] = new SqlParameter("@Attended", data.Attended);
                parameters[4] = new SqlParameter("@InterestRating", data.InterestRating);
                parameters[5] = new SqlParameter("@LastName", data.LastName);
                parameters[6] = new SqlParameter("@FirstName", data.FirstName);
                parameters[7] = new SqlParameter("@EmailAddress", data.EmailAddress);
                parameters[8] = new SqlParameter("@RegistrationDateTime", data.RegistrationDateTime);
                parameters[9] = new SqlParameter("@JoinTimeLeaveTimeTimeinSession", data.JoinTimeLeaveTimeTimeinSession);
                parameters[10] = new SqlParameter("@TimeinSession", data.TimeinSession);
                parameters[11] = new SqlParameter("@Unsubscribed", data.Unsubscribed);
                parameters[12] = new SqlParameter("@Who", "Raj");

                return SqlHelper.ExecuteNonQuery(ConfigurationManager.RdConnectionString, "Update_AttendeeDetails", parameters);
        }
    }
}
