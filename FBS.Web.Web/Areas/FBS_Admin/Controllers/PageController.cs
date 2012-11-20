using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITsds.Web.News.Areas.FBS_Admin.Controllers
{
    public class PageController : Controller
    {
        //
        // GET: /FBS_Admin/Page/

        public ActionResult Index()
        {


            return View();
        }

        //
        // GET: /FBS_Admin/Page/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FBS_Admin/Page/Create

        public ActionResult Create()
        {   
            return View();
        } 

        //
        // POST: /FBS_Admin/Page/Create

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
        // GET: /FBS_Admin/Page/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FBS_Admin/Page/Edit/5

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
        // GET: /FBS_Admin/Page/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FBS_Admin/Page/Delete/5

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
