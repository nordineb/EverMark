using System.Web.Optimization;

namespace EvernoteMvcExample
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.cookie.js",
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/markdown").Include(
            "~/Scripts/pagedown/Markdown.Converter.js",
            "~/Scripts/pagedown/Markdown.Sanitizer.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Bootstrap/bootstrap.css",
                "~/Content/Bootstrap/bootstrap-theme.css",
                "~/Content/Site.css"));
        }
    }
}