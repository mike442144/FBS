using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBS.Service.ActionModels;
using FBS.Service;

namespace FBS.Web.News.Controllers
{
    public class BlogController : Controller
    {
        BlogService srv;
        CategoryService categorySrv;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            srv = new BlogService();
            categorySrv = new CategoryService();
        }
        //
        // GET: /Blog/
        public ActionResult Index(Guid? id)
        {
            IList<BlogStoryDspModel> posts;
            var name = string.Empty;
            try
            {    
                if (id.HasValue && !Guid.Empty.Equals(id.Value))
                {
                    posts = srv.GetAllBlogStorysByCategory(id.Value, 0, 10);
                    name = categorySrv.GetCategoryNameById(id.Value);
                    if (name != null)
                        ViewData.Add("CategoryName", name);
                }
                else
                    posts = srv.GetRecentBlogStorysSummary(10);
                return View(posts);
            }
            catch (Exception error)
            {
                //TODO:render other page like 404.
                return View();
            }
        }
       

        //
        // GET: /Blog/Details/5

        public ActionResult Details(Guid id)
        {
            var story = srv.GetOneBlogStoryContentByID(id);
            return View(story);
        }

        //
        // GET: /Blog/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Blog/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Blog/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Blog/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Blog/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CategoriesSidebar()
        {
            CategoryService cs = new CategoryService();
            var categories = cs.FetchBlogStoryCategory();
            return PartialView(categories);
        }
    }
}
