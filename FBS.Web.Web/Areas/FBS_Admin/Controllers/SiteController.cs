using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FBS.Service;
using System.IO;

namespace ITsds.Web.News.Areas.FBS_Admin.Controllers
{
    public class SiteController : Controller
    {
        SiteInfoService siteInfo = null;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            this.siteInfo = new SiteInfoService();
            base.Initialize(requestContext);
        }

        //
        // GET: /FBS_Admin/Site/

        public ActionResult Index()
        {
            ViewData.Model = this.siteInfo.GetSiteInfo();
            return View();
        }

        //
        // GET: /FBS_Admin/Site/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /FBS_Admin/Site/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /FBS_Admin/Site/Create

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
        // GET: /FBS_Admin/Site/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /FBS_Admin/Site/Edit/5

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
        // GET: /FBS_Admin/Site/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /FBS_Admin/Site/Delete/5

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
        [HttpGet]
        public ActionResult ChangeTheme(string themeName)
        {
            //check something for themeName

            //setting

            this.HttpContext.Application.Set("themeName", themeName);

            //saved

            //return

            return Redirect("/Home/Index/");
        }

        public ActionResult ShowThemes()
        {
            return View();
        }

        //[]
        public JsonResult FetchThemes()
        {
            string themeDir = this.Server.MapPath("~/Themes/");
            string errorInfo = string.Empty;
            string[] themes;
            IList<Theme> themeSet = new List<Theme>();
            bool isThere = Directory.Exists(themeDir);
            if (!isThere)
            { }
            try
            {
                themes=Directory.GetDirectories(themeDir, "", SearchOption.TopDirectoryOnly);
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            if(themes.Length>0)
                for (int i = 0; i < themes.Length; i++)
                {
                    string smallThumbnail = themes[i] + "\\Thumbnails\\small.jpg";
                    Theme t=new Theme(themes[i]);
                    t.Load(Path.Combine(themes[i], "\\info.txt"));
                    themeSet.Add(t);
                }

            
            return Json(themeSet);
        }
    }

    struct Theme
    {
        private readonly string name;
        private readonly string path;
        private string description;
        private string author;
        private DateTime pubDate;

        public Theme(string path)
        {
            this.path = path;
            this.name = path.Substring(path.LastIndexOf('\\'));
            this.description = string.Empty;
            this.author = string.Empty;
            this.pubDate = DateTime.MinValue;
        }

        /// <summary>
        /// 加载皮肤信息
        /// </summary>
        /// <param name="infoText">信息文件路径</param>
        public void Load(string infoText)
        {
            StreamReader sr=null;
            string info=string.Empty;
            try
            {
                sr= new StreamReader(infoText);
                info=sr.ReadToEnd();
            }
            catch(FileNotFoundException ex)
            {
                throw new FileNotFoundException("皮肤相关信息文件未能找到",ex);
            }
            catch(IOException ex)
            {
                throw new IOException("服务端发生输入输出错误",ex);
            }
            
            string strDescription="[description]";
            string strAuthor="[author]";
            string strPubDate="[pubDate]";
            
            if(info.IndexOf(strAuthor)<0||info.IndexOf(strDescription)<0||info.IndexOf(strPubDate)<0)
                throw new Exception("皮肤信息文件中缺少必要项");

            try
            {
                this.description = info.Substring(info.IndexOf(strDescription) + 
                    strDescription.Length + 1);
                this.author = info.Substring(info.IndexOf(strAuthor) + 
                    strAuthor.Length + 1);
                this.pubDate = Convert.ToDateTime(
                    info.Substring(info.IndexOf(strPubDate) + strPubDate.Length + 1));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("皮肤信息文件信息不完整",ex);
            }
            catch (FormatException ex)
            {
                throw new FormatException("发布时间格式不正确",ex);
            }
        
        }
    }
}
