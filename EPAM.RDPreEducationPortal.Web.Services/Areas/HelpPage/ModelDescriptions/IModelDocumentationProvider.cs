using System;
using System.Reflection;

namespace EPAM.RDPreEducationPortal.Web.Services.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}