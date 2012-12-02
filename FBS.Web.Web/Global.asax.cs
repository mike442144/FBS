using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ITsds.Web.News.ViewEngine;

namespace ITsds.Web.News
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               "newslist", // Route name
               "News/NewsList/{index}", // URL with parameters
               new { controller = "News", action = "NewsList" } // Parameter defaults
           );

            routes.MapRoute(
               "news", // Route name
               "News/News/{ArticleID}", // URL with parameters
               new { controller = "News", action = "News" } // Parameter defaults
           );
            routes.MapRoute("install", "{controller}/", new { controller="Install",action="Index"});
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
            routes.MapRoute(
                "Articles",
                "Articles/",
                new { controller="Article",action = "Index"}
            );
            routes.MapRoute(
                "Article",
                "a/{id}",
                new { controller="Article",action="Details" }
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Mvc.ViewEngines.Engines.Add(new WebFormThemeViewEngine());
            this.Application.Add("themeName", "Default");
            RegisterRoutes(RouteTable.Routes);
        }
    }
}