using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBS.Service;
using FBS.Service.ActionModels;
using FBS.App;

namespace FBS.Web.News.Controllers
{
    public class InstallController : Controller
    {
        InstallStep install;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (install == null) install = new InstallStep();
            base.Initialize(requestContext);
        }
        //
        // GET: /Install/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult EnvironmentCheck()
        {
            return Json(
                install.Check(),
                JsonRequestBehavior.AllowGet
                );
        }
        [HttpPost]
        public string DbCnf(string DbAddr, string DbName, string DbUser, string DbPsd)
        {
            string msg = string.Empty;
            try
            {
                var filePath=Server.MapPath("~/installed");
                if (System.IO.File.Exists(filePath))
                    throw new Exception("网站已经安装过");
                
                install.SetDbCnf(new DbCnf() {
                    DbAddr=DbAddr, DbName=DbName, DbPsd=DbPsd, DbUser=DbUser 
                });
                install.SetSiteCnf(new SiteCnf() {
                     FounderEmail="admin@admin.com", FounderPsd="passw0rd", SiteDesc="", SiteName="", FounderName="Heilsberg", SiteUrl=""
                });
                using (var writer = System.IO.File.CreateText(filePath))
                {
                    writer.Write("site installed at [{0}]",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            catch(Exception e)
            {
                msg = e.Message;
            }
            return msg;
        }
        
        public ActionResult SiteCnf()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SiteCnf(SiteCnf model)
        {
            try
            {
                install.SetSiteCnf(model);
            }
            catch 
            {
                return View("Success");
            }
            return View("Success");
        }
    }
}
