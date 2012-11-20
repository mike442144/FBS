using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBS.App;
using FBS.Service.ActionModels;
using System.Configuration;
using System.Web.Configuration;
using System.Reflection;
using System.IO;

namespace ITsds.Web.News.Controllers
{
    public class InstallController : Controller
    {
        private InstallStep steps;
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //安装历史检测
            string path = this.Server.MapPath("~/INSTALL.INFO");
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                filterContext.Result = View("Installed");//RedirectToAction("Installed"); 
            }
            base.OnAuthorization(filterContext);
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            steps = new InstallStep();
            base.Initialize(requestContext);
        }
        public ActionResult Index(string step)
        {
            StepOfLicense sol = steps.ShowLicense();//许可协议
            if (sol == null)
            {
                sol = new StepOfLicense();
                sol.Info = "许可协议加载失败";
            }

            ViewData.Model = sol;//传递到View中

            return View();
        }

        public ActionResult EnvironmentCheck()
        {
            StepOfCheck soc = steps.Check();//环境检测对象
            if (soc == null)
            {
                soc = new StepOfCheck();
                soc.OSVersion = string.Empty;
                soc.RunTimeVersion = string.Empty;
            }

            ViewData.Model = soc;//传递对象至View
            return View();
        }

        public ActionResult DbCnf()
        {
            StepOfDbCnf cnf = new StepOfDbCnf();//生成默认数据库配置项
            
            cnf.DbAddr = "localhost";
            cnf.DbName = "fbs";
            cnf.DbUser = "sa";
            cnf.DbPsd = string.Empty;
            cnf.DbTablePrefix = "fbs_";

            ViewData.Model = cnf;//传递对象至View
            
            return View();
        }

        [HttpPost]
        public ActionResult DbCnf(StepOfDbCnf sodc)
        {
            if (sodc != null)
            {
                try
                {
                    this.steps.SetDbCnf(sodc);//处理配置
                    return RedirectToAction("SiteCnf");//数据库配置成功，进行下一步网站配置
                }
                catch(Exception error)
                {
                    ModelState.AddModelError(string.Empty, error);
                    return View();//数据库配置项存在错误，重新执行当前步骤
                }
            }
            else
                return View();
        }

        public ActionResult SiteCnf()
        {
            
            StepOfSiteCnf sosc = new StepOfSiteCnf();//默认站点配置对象

            sosc.SiteName = "FBS建站——3分钟搭建炫丽站点";
            sosc.SiteDesc = "FBS建站通——国内第一款以用户体验及易用性为中心的傻瓜建站工具，您只需要3分钟时间即可拥有完美站点";
            sosc.SiteUrl = Request.UrlReferrer.Host;
            sosc.FounderName = "admin";
            sosc.FounderEmail = string.Empty;
            ViewData.Model = sosc;//传递对象至View

            return View();
        }

        [HttpPost]
        public ActionResult SiteCnf(StepOfSiteCnf sosc)
        {
            string cpy = "FBS";//网站版权

            Version appVersion = Assembly.GetExecutingAssembly().GetName().Version;//获取当前网站程序集的版本
            try
            {
                sosc.Version = appVersion;
                sosc.CopyRight = cpy;
                this.steps.SetSiteCnf(sosc);
                
                string path = this.Server.MapPath("~/INSTALL.INFO");
                FileStream fs = System.IO.File.Create(path);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("system installed at "+DateTime.Now.ToString());
                sw.Close();
                fs.Close();

                return View("Success");//网站信息设置成功，返回网站安装成功页面
            }
            catch(Exception error)
            {
                ModelState.AddModelError(string.Empty, error);

                return View();//网站信息填写有错，重新返回填写
            }
        }

        
    }
}
