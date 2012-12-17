using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FBS.Web.News.Controllers
{
    struct Theme
    {
        private readonly string name;
        private readonly string path;
        private readonly string smallThumbnail;
        private string description;
        private string author;
        private DateTime pubDate;

        public string Name
        {
            get { return this.name; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public string Author
        {
            get { return this.author; }
        }

        public string PubDate
        {
            get { return this.pubDate.ToString("yyyy-MM-dd"); }
        }

        public string SmallThumbnail
        {
            get { return this.smallThumbnail; }
        }

        public Theme(string path)
        {
            this.path = path;
            this.name = path.Substring(path.LastIndexOf('\\')+1);
            this.description = string.Empty;
            this.author = string.Empty;
            this.pubDate = DateTime.MinValue;
            this.smallThumbnail = "/Themes/"+name + "/Thumbnails/small.jpg";//path + "\\Thumbnails\\small.jpg";
        }

        /// <summary>
        /// 加载皮肤信息
        /// </summary>
        /// <param name="infoText">信息文件路径</param>
        public void Load(string infoText)
        {
            StreamReader sr = null;
            string info = string.Empty;
            try
            {
                sr = new StreamReader(infoText);
                info = sr.ReadToEnd();
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException("皮肤相关信息文件未能找到", ex);
            }
            catch (IOException ex)
            {
                throw new IOException("服务端发生输入输出错误", ex);
            }

            string strDescription = "[description]";
            string strAuthor = "[author]";
            string strPubDate = "[pubDate]";

            int idxDescription = info.IndexOf(strDescription);
            int idxAuthor = info.IndexOf(strAuthor);
            int idxPubDate = info.IndexOf(strPubDate);

            if (idxDescription < 0 || idxAuthor < 0 || idxPubDate < 0)
                throw new Exception("皮肤信息文件中缺少必要项");

            try
            {
                this.description = info.Substring(idxDescription +strDescription.Length + 1,
                    idxAuthor-idxDescription-strDescription.Length-3);
                this.author = info.Substring(info.IndexOf(strAuthor) +strAuthor.Length + 1,
                    idxPubDate-idxAuthor-strAuthor.Length-3);
                this.pubDate = Convert.ToDateTime(
                    info.Substring(info.IndexOf(strPubDate) + strPubDate.Length + 1));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("皮肤信息文件信息不完整", ex);
            }
            catch (FormatException ex)
            {
                throw new FormatException("发布时间格式不正确", ex);
            }

        }
    }
    public class SiteController : Controller
    {
        //
        // GET: /Site/

        public ActionResult Index()
        {
            return View();
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

        [HttpPost]
        public JsonResult FetchThemes()
        {
            string themeDir = this.Server.MapPath("~/Themes/");
            string errorInfo = string.Empty;
            IList<string> themes=new List<string>();
            IList<Theme> themeSet = new List<Theme>();
            bool isThere = Directory.Exists(themeDir);
            if (!isThere)
            { }
            try
            {
                foreach (string d in Directory.GetDirectories(themeDir))
                {
                    if (!d.Substring(d.LastIndexOf('\\')+1).StartsWith("."))
                        themes.Add(d);
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            if (themes.Count() > 0)
                foreach (string cur in themes)
                {
                    string smallThumbnail = cur + "\\Thumbnails\\small.jpg";
                    Theme t = new Theme(cur);
                    t.Load(cur+ "\\info.txt");
                    themeSet.Add(t);
                }


            return Json(themeSet);
        }
    }
}
