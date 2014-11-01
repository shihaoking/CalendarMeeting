using System.Web;
using System.Web.Optimization;

namespace MeetingCanlendar
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/main.css",
                "~/Content/css/common.css",
                "~/Content/css/datepicker.css",
                "~/Content/css/chosen.css",
                "~/Content/css/fullcalendar.css",
                "~/Content/css/ns-style-growl.css",
                "~/Content/css/ns-default.css"));

            bundles.Add(new StyleBundle("~/Admin/Content/css").Include(
                "~/Content/css/admin.css",
                "~/Content/css/common.css",
                "~/Content/css/datepicker.css",
                "~/Content/css/chosen.css"));
        }
    }
}