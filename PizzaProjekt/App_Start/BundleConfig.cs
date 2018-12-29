using System.Web.Optimization;

namespace PizzaProjekt
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/Helpers").Include(
                "~/Scripts/helpers.js"));

            bundles.Add(new ScriptBundle("~/bundles/Chosen").Include(
                "~/Scripts/chosen.jquery.js"));

            bundles.Add(new StyleBundle("~/Content/Chosen").Include(
                "~/Content/chosen-bootstrap.css"));
        }
    }
}