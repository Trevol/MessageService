using System.Web;
using System.Web.Optimization;

namespace MessageService.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            var libs = new ScriptBundle("~/bundles/libs")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/bootstrap.js", "~/Scripts/respond.js")
                .Include("~/Scripts/knockout-3.4.1.js")
                .Include("~/Scripts/q.min.js")
                .Include("~/Scripts/underscore.min.js");
            bundles.Add(libs);

            var app = new ScriptBundle("~/bundles/app")
                .IncludeDirectory("~/Scripts/app", "*.js", true);
            bundles.Add(app);
            

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
