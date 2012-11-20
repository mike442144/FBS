using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FBS.Service.ActionModels;
using FBS.Service;

namespace ITsds.Web.News.Areas.FBS_Admin.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /FBS_Admin/Account/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /FBS_Admin/Account/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FBS_Admin/Account/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FBS_Admin/Account/Create

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
        // GET: /FBS_Admin/Account/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FBS_Admin/Account/Edit/5

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
        // GET: /FBS_Admin/Account/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FBS_Admin/Account/Delete/5

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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogOnModel usr)
        {
            UserEntryService ues = new UserEntryService();
            try
            {
                ues.Logon(usr);//登录
            }
            catch (Exception error)
            {
                ModelState.AddModelError("", error);
                return View();
            }

            return Redirect("FBS_Admin/");//重定向到默认页
            
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }
    }
}
