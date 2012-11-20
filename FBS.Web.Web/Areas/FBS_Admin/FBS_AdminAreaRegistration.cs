using System.Web.Mvc;

namespace ITsds.Web.News.Areas.FBS_Admin
{
    public class FBS_AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FBS_Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "FBS_Admin_default",
                "FBS_Admin/{controller}/{action}/{id}",
                new {controller="Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
