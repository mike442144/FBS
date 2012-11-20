using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITsds.Web.News.Areas.FBS_Admin.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /FBS_Admin/Article/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FBS_Admin/Article/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FBS_Admin/Article/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FBS_Admin/Article/Create

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
        // GET: /FBS_Admin/Article/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FBS_Admin/Article/Edit/5

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
        // GET: /FBS_Admin/Article/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FBS_Admin/Article/Delete/5

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
    }
}
