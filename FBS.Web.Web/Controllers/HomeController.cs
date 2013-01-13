using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBS.Service;
using System.IO;

namespace FBS.Web.News.Controllers
{
    public class HomeController : Controller
    {
        SiteService sis;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            sis = new SiteService();
        }
        //
        // GET: /Home/

        public ActionResult Index()
        {
            
            return View();
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }
        
        public ActionResult About()
        {
            //string template = null;
            //try
            //{
            //    StreamReader sr = new StreamReader(Server.MapPath("~/Content/template.html"));
            //    template = sr.ReadToEnd();
            //    template.Replace("@{Content}", "About us.");
            //    template.Replace("@{Title}", "About us.");
            //}
            //catch (Exception error)
            //{

            //}
            //Response.Write(template);
            return View();
        }
        public ActionResult Page(string name)
        {
            //TODO: seems no need.
            if (string.IsNullOrEmpty(name)) 
            {
                var hasParams = Request.QueryString.Count > 0 && !String.IsNullOrEmpty(Request.QueryString["name"]);
                return hasParams ? View(name) : View("Default");
            }

            return View(name);
        }
        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Header()
        {
            ViewData.Add("site", "fbs.com");
            return PartialView();
        }
        public ActionResult Footer()
        {
            var info = sis.GetSiteInfo();
            return PartialView(info);
        }
    }
}
