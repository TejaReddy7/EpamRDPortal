using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.RDPreEducationPortal.Windows.Services
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var obj = new SonarQubeReportService();
            obj.OnStart();
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new SonarQubeReportService()
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
