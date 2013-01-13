using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBS.Service;
using FBS.Service.ActionModels;

namespace FBS.Web.News.Controllers
{
    public class ArticleController : Controller
    {
        CMSService srv;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            srv = new CMSService();
        }
        //
        // GET: /Article/

        public ActionResult Index()
        {
            var arts = srv.GetLatestArticles(0, 5);
            return View(arts);
        }

        public ActionResult Details(Guid id)
        {
            if (Guid.Empty.Equals(id)) return View("NotFound.html");
            var detail = srv.GetOneArticleContentByID(id);
            return View(detail);
        }
        //
        // GET: /Article/Details/5
        [HttpPost]
        public JsonResult DetailsJ(Guid id)
        {
            var detail = srv.GetOneArticleContentByID(id);
            return Json( new ArticleDetailsModel() {
                 Body=detail==null?"":detail.Body,
                 Title=detail==null?"":detail.Title,
                 CreationDate=detail==null?System.DateTime.Now:detail.CreationDate
            });
        }
    }
}
