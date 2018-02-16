using System.Web.Optimization;

namespace WebInventory
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Content/js/jquery.min.js",
                      "~/Content/js/bootstrap.min.js",
                      "~/Content/js/easing.min.js",
                      "~/Content/js/jquery.magnific-popup.min.js",
                      "~/Content/js/owl-carousel.min.js",
                      "~/Content/js/flickity.pkgd.min.js",
                      "~/Content/js/modernizr.min.js",
                      "~/Content/js/scripts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                      "~/Content/css/font-icons.css",
                      "~/Content/css/style.css",
                      "~/Content/css/color.css"));
        }
    }
}
