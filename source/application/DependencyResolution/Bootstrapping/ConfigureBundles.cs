using System.Web.Optimization;
using Autofac;

namespace Intrigma.DonorSpace.Infrastructure.DependencyResolution.Bootstrapping
{
    public class ConfigureBundles : IStartable
    {
        public void Start()
        {
            RegisterBundles(BundleTable.Bundles);
        }
        
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        private static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                            .Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui")
                            .Include("~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval")
                            .Include("~/Scripts/jquery.unobtrusive*",
                                     "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                            .Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/appInfrastructure")
                            .Include("~/Scripts/Infrastructure/spinner.js"));

            bundles.Add(new ScriptBundle("~/bundles/appAdministration")
                            .Include("~/Scripts/application/administration/charityAdministration.js"));

            bundles.Add(new ScriptBundle("~/bundles/appUserAdministration")
                            .Include("~/Scripts/application/administration/userAdministration.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap")
                            .Include("~/Scripts/bootstrap/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap/css/bootstrap.css",
                        "~/Content/bootstrap/css/bootstrap-responsive.min.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css")
                            .Include(
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

        }

        
    }
}