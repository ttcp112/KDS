using System.Web;
using System.Web.Optimization;

namespace NSWebKDS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"
                         ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/select2.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Icheck.css",
                      "~/Content/home.css",
                      "~/Content/select2-bootstrap4.min.css"));

            bundles.Add(new ScriptBundle("~/ng").Include(
                              "~/Scripts/angular.min.js",
                               "~/Scripts/angular-route.js",
                                  "~/Scripts/angular-cookies.js"
                ));
            bundles.Add(new ScriptBundle("~/app")
                .Include(
                "~/Scripts/app/ObjectModels.js",
                "~/Scripts/app/app.js",
                "~/Scripts/app/ordermanagementCtrl.js",
                 "~/Scripts/app/homeCtrl.js"
                ));
            bundles.Add(new ScriptBundle("~/sk")
               .Include(
               "~/Scripts/socket.io.js"
               ));
        }
    }
}
