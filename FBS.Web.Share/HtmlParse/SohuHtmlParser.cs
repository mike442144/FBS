using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FBS.Utils;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Web.Share.HtmlParser
{
    public class SohuHtmlParser : ISiteHtmlParser
    {
        #region ISiteHtmlParser 成员

        string content;
        public void ParseHtml(string url, ref ShareThread shareThread)
        {   
            string htmlContent = HttpCollects.GetHTMLContent(url);
            if (string.IsNullOrEmpty(htmlContent))
                return;
            
            shareThread=new ShareThread(url,GetPlayerUrlString(url, out content),MediaType.Video,HttpCollects.GetTitle(htmlContent, true),HttpCollects.GetDescription(htmlContent, true),GetThumbnailUrlString(htmlContent, true),DateTime.Now);

        }

        #endregion

        /// <summary>
        /// 修改url为播放器地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetPlayerUrlString(string url, out string content)
        {
            string regString = @"^http://v.blog.sohu.com/u/(((?<gettype>[a-zA-Z]+)w/(?<getnumber>\d+))|((?<gettype>[a-zA-Z]+)w/(?<getnumber>\d+)#([a-zA-Z\d_]+)))$";
            string playerFormat = @"http://v.blog.sohu.com/fo/{0}4/{1}";

            Regex regex = new Regex(regString, RegexOptions.IgnoreCase);
            Match match = regex.Match(url);
            if (match.Success)
            {
                content = match.Groups["getnumber"].Value;
                return string.Format(playerFormat, match.Groups["gettype"].Value, match.Groups["getnumber"].Value);
            }
            else
            {
                content = string.Empty;
                return url;
            }
        }

        /// <summary>
        /// 获取缩略图地址
        /// </summary>
        /// <param name="html">html页面文档</param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public string GetThumbnailUrlString(string html, bool ignoreCase)
        {
            string regString = string.Format(@"http://201.img.pp.sohu.com.cn/images/video/(?<getleft>[a-zA-Z\d\/_]+){0}(?<getright>[a-zA-Z\d_]+).jpg", content);
            Regex reg;
            if (ignoreCase)
            {
                reg = new Regex(regString, RegexOptions.IgnoreCase);
            }
            else
            {
                reg = new Regex(regString);
            }
            Match match = reg.Match(html);
            if (match.Success)
            {
                return string.Format("http://201.img.pp.sohu.com.cn/images/video/{0}{1}{2}.jpg", match.Groups["getleft"].Value, content, match.Groups["getright"].Value);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
