using EPAM.RDPreEducationPortal.Web.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EPAM.RDPreEducationPortal.Web.Site.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Users()
        {
            var smtpClient = new SmtpClient
            {
                Host = "MAIL",
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = System.Net.CredentialCache.DefaultNetworkCredentials
            };

            var mailMessage = new MailMessage
            {
                Body = "Testing",
                From = new MailAddress(Environment.UserName + "@epam.com"),
                Subject = "Testing",
                Priority = MailPriority.Normal
            };

            mailMessage.To.Add("Rajasekhar_Tadepalli@epam.com");

            smtpClient.Send(mailMessage);
            return View();
        }
        public ActionResult EmailRecipients()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmailRecipients(EmailRecipientsModel model)
        {
            return View();
        }
    }
}