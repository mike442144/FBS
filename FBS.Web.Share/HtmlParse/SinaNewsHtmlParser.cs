using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Utils;
using FBS.Domain.Aggregate.Entity;
using System.Text.RegularExpressions;

namespace FBS.Web.Share.HtmlParse
{
    public class SinaNewsHtmlParser : ISiteHtmlParser
    {
        #region ISiteHtmlParser 成员
        public void ParseHtml(string url, ref ShareThread shareThread)
        {
            string htmlContent = HttpCollects.GetHTMLContent(url);//获取文档内容
            if (string.IsNullOrEmpty(htmlContent))
            {
                return;
            }

            shareThread=new ShareThread(url,GetPlayerUrlString(htmlContent, url),MediaType.Video,HttpCollects.GetTitle(htmlContent, true),HttpCollects.GetDescription(htmlContent, true),GetThumbnailUrlString(htmlContent, true),DateTime.Now);
        }
        #endregion

        /// <summary>
        /// 从页面里获取播放器地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetPlayerUrlString(string html, string url)
        {
            string playerFormat = "value=\"http://vhead.blog.sina.com.cn/player/outer_player.swf\\?(?<getcontent>[a-zA-Z\\d=&%-]+)\"";

            Regex regex = new Regex(playerFormat, RegexOptions.IgnoreCase);
            Match match = regex.Match(html);
            if (match.Success)
            {
                return string.Format("http://vhead.blog.sina.com.cn/player/outer_player.swf?{0}&autoplay=1", match.Groups["getcontent"].Value);
            }
            else
            {
                return url;
            }
        }

        /// <summary>
        /// 从页面里获取缩略图地址
        /// </summary>
        /// <param name="html">html页面文档</param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public string GetThumbnailUrlString(string html, bool ignoreCase)
        {
            string regString = "src=\"http://(?<getcontent1>[a-zA-Z\\d]+).v.iask.com/(?<getcontent2>[\\d\\/_-]+).jpg\"";
            string regString2 = "src=\"http://(?<getcontent3>[a-zA-Z\\d]+).sinaimg.cn/(?<getcontent4>[a-zA-Z\\d\\/_-]+).jpg\"";
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
                return string.Format("http://{0}.v.iask.com/{1}.jpg", match.Groups["getcontent1"].Value, match.Groups["getcontent2"].Value);
            }
            else
            {
                if (ignoreCase)
                {
                    reg = new Regex(regString2, RegexOptions.IgnoreCase);
                }
                else
                {
                    reg = new Regex(regString2);
                }
                match = reg.Match(html);
                if (match.Success)
                {
                    return string.Format("http://{0}.sinaimg.cn/{1}.jpg", match.Groups["getcontent3"].Value, match.Groups["getcontent4"].Value);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
