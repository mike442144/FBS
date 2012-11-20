using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FBS.Utils;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Web.Share.HtmlParser
{
    public class JoyHtmlParser : ISiteHtmlParser
    {
        #region ISiteHtmlParser 成员

        public void ParseHtml(string url, ref ShareThread shareThread)
        {
            string htmlContent = HttpCollects.GetHTMLContent(url);
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
            string playerFormat = "<embed src=\"http://client.joy.cn/flvplayer/(?<getcontent>[a-zA-Z\\d_]+).swf";

            Regex regex = new Regex(playerFormat, RegexOptions.IgnoreCase);
            Match match = regex.Match(html);
            if (match.Success)
            {
                return string.Format("http://client.joy.cn/flvplayer/{0}.swf", match.Groups["getcontent"].Value);
            }
            else
            {
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
            string regString = "<span class=\"s_pic\">http://(?<getcontent>[\u4e00-\u9fa5a-zA-Z\\d\\/.=()-_]+).jpg";
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
                return string.Format("http://{0}.jpg", match.Groups["getcontent"].Value);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
