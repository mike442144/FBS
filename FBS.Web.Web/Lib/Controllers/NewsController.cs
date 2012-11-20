using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FBS.Service;
using FBS.Service.ActionModels;
using System.Text.RegularExpressions;

namespace ITsds.Web.News.Controllers
{
    [HandleError]
    public class NewsController : Controller
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
        /// 企业新闻列表
        /// </summary>
        /// <param name="index">当前页码</param>
        /// <returns></returns>
        public ActionResult NewsList(string index)
        {
            
            return View();
        }

        /// <summary>
        /// 企业新闻详细信息
        /// </summary>
        /// <param name="ArticleID">新闻编号</param>
        /// <returns></returns>
        public ActionResult News(string ArticleID)
        {
            return View();
        }
    }
}
