using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Epam.RDPreEducationPortal.CoreModels;

namespace EPAM.RDPreEducationPortal.Web.Site.Common
{
    public sealed class UserDetailsSingleton
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Role{ get; set; }
        public string Email { get; set; }

        private static readonly Lazy<UserDetailsSingleton>
            Lazy =
                new Lazy<UserDetailsSingleton>
                    (() => new UserDetailsSingleton());

        public static UserDetailsSingleton Instance => Lazy.Value;

        private UserDetailsSingleton()
        {
        }

        //public AccountDetails GetUserDetails()
        //{
        //    var accountDetails = new AccountDetails { FirstName = "Raja Sekhar" };
        //    return accountDetails;
        //}
    }
}