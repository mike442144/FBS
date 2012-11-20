using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITsds.Web.News.Controllers
{
    [HandleError]
    public class JobsController : Controller
    {
        //
        // GET: /Jobs/

        public ActionResult Index()
        {
            return View();
        }

    }
}
