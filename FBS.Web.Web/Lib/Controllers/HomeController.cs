using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBS.Service.ActionModels;
using FBS.Service;
using FBS.App;

namespace ITsds.Web.News.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private CMSService cmsService;
        private CategoryService caService;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            this.cmsService = new CMSService();
            this.caService = new CategoryService();
            base.Initialize(requestContext);
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 企业简介
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
        /// <summary>
        /// 联系我们
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {

            return View();
        }
        /// <summary>
        /// 找不到指定项目
        /// </summary>
        /// <returns></returns>
        public ActionResult NotFound()
        {
            return View();
        }
        /// <summary>
        /// 通用错误处理页面
        /// </summary>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public ActionResult CommonError(string error)
        {
            if (!string.IsNullOrEmpty(error))
            {
                ViewData["CommonError"] = error;
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
