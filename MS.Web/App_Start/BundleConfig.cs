using System.Web;
using System.Web.Optimization;

namespace MS.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Contents/css").Include(
                      "~/Areas/Admin/Content/bootstrap.min.css",
                       "~/Areas/Admin/Content/ionicons.min.css",
                        "~/Areas/Admin/Content/bootstrap3-wysihtml5.min.css",
                         "~/Areas/Admin/Content/AdminLTE.css",
                         "~/Areas/Admin/Content/select2.css",
                       "~/Areas/Admin/Content/font-awesome.min.css"));

             bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerydatatable").Include(
                            "~/areas/admin/Script/jquery.dataTables.js",
                           "~/areas/admin/Script/dataTables.bootstrap.js"
                           ));



            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/DataTables-1.10.4/jquery.dataTables.js",
                        "~/Scripts/DataTables-1.10.4/dataTables.bootstrap.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
           

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/bundles/datatable/css").Include("~/Areas/Admin/Content/bootstrap-switch.css",
                "~/Areas/Admin/Content/dataTables.bootstrap.css"));



            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

                bundles.Add(new ScriptBundle("~/bundles/jqueri").Include( "~/Areas/Admin/Script/bootstrap.min.js",
                        "~/Scripts/jquery.validate.js",
                         "~/Areas/Admin/Script/jquery.sparkline.min.js",
                        "~/Areas/Admin/Script/app.js"
                        ));

        }
    }
}
