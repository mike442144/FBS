using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using FBS.Utils;
using System.Text.RegularExpressions;

namespace FBS.Web.Share.HtmlParser
{
    public class Ku6HtmlParser : ISiteHtmlParser
    {
        #region ISiteHtmlParser 成员

        public void ParseHtml(string url, ref ShareThread shareThread)
        {   
            string endreg = "id=\"commentList\"";
            string htmlContent = HttpCollects.GetHTMLContent(url, endreg);
            if (string.IsNullOrEmpty(htmlContent))
                return;
            shareThread=new ShareThread(url,GetPlayerUrlString(url),MediaType.Video,HttpCollects.GetTitle(htmlContent, true),HttpCollects.GetDescription(htmlContent, true),GetThumbnailUrlString(htmlContent, true),DateTime.Now);

        }

        #endregion

        /// <summary>
        /// 修改url为播放器地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetPlayerUrlString(string url)
        {
            string regString = @"^http://v.ku6.com/((show/(?<getcontent>[a-zA-Z\d-_]+))|(special/show_([\d]+)/(?<getcontent>[a-zA-Z\d-_]+))).html$";
            string playerFormat = @"http://player.ku6.com/refer/{0}/v.swf&auto=1";

            Regex regex = new Regex(regString, RegexOptions.IgnoreCase);
            Match match = regex.Match(url);
            if (match.Success)
            {
                return string.Format(playerFormat, match.Groups["getcontent"].Value);
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
            string regString = @"http://i(?<getnumber>[\d]+).ku6.com/(?<getcontent>[a-zA-Z\d\/]+).jpg</span>";
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
                return string.Format("http://i{0}.ku6.com/{1}.jpg", match.Groups["getnumber"].Value, match.Groups["getcontent"].Value);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
