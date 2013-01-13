using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FBS.Web.News.ViewEngine;

namespace FBS.Web.News
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("Admin/");
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
            routes.MapRoute(
                "Article",
                "a/{id}",
                new { controller = "Article", action = "Details",id=UrlParameter.Optional }
            );
            routes.MapRoute("Blog", "b/{id}", new {controller="Blog",action="Details",id=UrlParameter.Optional});
            
            routes.MapRoute("page", "page/{name}", new { controller = "Home", action = "Page",name=UrlParameter.Optional });
            routes.MapRoute("Blogs", "{controller}/{action}/{id}", new {controller="Blog",action="Index",id=UrlParameter.Optional });
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

            routes.MapRoute("install", "{controller}/{action}", new { controller = "Install", action = "Index" });
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            System.Web.Mvc.ViewEngines.Engines.Add(new WebFormThemeViewEngine());
            this.Application.Add("themeName", Helpers.SharedData.ThemeName);
            RegisterRoutes(RouteTable.Routes);
            
        }

        
        bool IgnoreMvcDig(HttpContext c)
        {
            if (c.Request.Url.AbsolutePath.ToLower().EndsWith("mvcdiagnostics.aspx")) return true;
            else return false;
        }
        public override void Init()
        {
            this.BeginRequest += new EventHandler((s, e) =>
            {
                var context = (s as MvcApplication).Context;
                //if (IgnoreMvcDig(context)) return;
                if (
                    !(new string[] { "css", "js", "jpg", "gif", "png", "html", "txt" }).Any(item => context.Request.Url.AbsolutePath.ToLower().EndsWith(item))
                    && !System.IO.File.Exists(context.Server.MapPath("~/installed")))
                {
                    if (!context.Request.Url.AbsolutePath.ToLower().StartsWith(("/install")))
                        context.Response.Redirect("/Install/Index/", false);
                }
                else if (context.Request.Url.AbsolutePath.ToLower().StartsWith(("/install")))
                    context.Response.Redirect("/Home/Index/", false);
            });
            base.Init();
        }
    }
}