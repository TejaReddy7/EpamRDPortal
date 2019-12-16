using EPAM.RDPreEducationPortal.DataAccess.Classes;
using EPAM.RDPreEducationPortal.DataAccess.Interfaces;
using System.Web.Http;
using EPAM.RDPreEducationPortal.Web.Services.Models;
using Unity;
using Unity.WebApi;
using EPAM.RDPreEducationPortal.Web.Services.Interfaces;

namespace EPAM.RDPreEducationPortal.Web.Services
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IGitRepositories, GitRepositoriesModel>();
            container.RegisterType<IGitRepositoriesData, GitRepositoriesData>();
            container.RegisterType<ISonarIssues, SonarIssuesModel>();
            container.RegisterType<ISonarIssuesData, SonarIssuesData>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}