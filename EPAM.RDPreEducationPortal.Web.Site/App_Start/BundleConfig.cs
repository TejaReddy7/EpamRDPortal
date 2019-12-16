using System.Web;
using System.Web.Optimization;

namespace EPAM.RDPreEducationPortal.Web.Site
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.3.2.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                   //"~/Scripts/gridmvc.js",
                   "~/Scripts/loader.js",
                   "~/Scripts/site.js",
                   "~/Scripts/datatables.js",
                   "~/Scripts/notify.min.js"));

            bundles.Add(new StyleBundle("~/Content/css/").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom-css/rdpreeducation.layout.style.css",
                      "~/Content/custom-css/datatables.css",
                      "~/Content/site.css",
                      "~/Content/custom-css/loader.css",
                      "~/Content/custom-css/tabs.css",
                      "~/Content/custom-css/message-boxes.css"));

            bundles.Add(new StyleBundle("~/Content/logincss/").Include(
                    "~/Content/login.css")); 
        }
    }
}
