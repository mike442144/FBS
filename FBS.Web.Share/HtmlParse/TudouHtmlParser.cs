using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FBS.Domain.Aggregate.Entity;
using FBS.Utils;

namespace FBS.Web.Share.HtmlParser
{
    public class TudouHtmlParser : ISiteHtmlParser
    {
        #region ISiteHtmlParser 成员

        public void ParseHtml(string url, ref ShareThread shareThread)
        {
            string htmlContent = HttpCollects.GetHTMLContent(url);
            if (string.IsNullOrEmpty(htmlContent))
                return;
            
            shareThread =new ShareThread(url,GetPlayerUrlString(url),MediaType.Video,HttpCollects.GetTitle(htmlContent, true),HttpCollects.GetDescription(htmlContent, true),GetThumbnailUrlString(htmlContent, true),DateTime.Now);

        }
        #endregion


        /// <summary>
        /// 修改url为播放器地址
        /// </summary>
        /// <param name="url">url地址</param>
        /// <returns></returns>
        public string GetPlayerUrlString(string url)
        {
            string regString = @"^http://www.tudou.com/programs/view/(((?<getcontent>[a-zA-Z\d-_]+)/isRenhe=1)|((?<getcontent>[a-zA-Z\d-_]+)/([a-zA-Z\d-_=&]*)))$";
            string playerFormat = @"http://www.tudou.com/v/{0}";

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
            string regString = @"http://i01.img.tudou.com/data/imgs/i/(?<getcontent>[a-zA-Z\d\/]+).jpg</span>";
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
                return string.Format("http://i01.img.tudou.com/data/imgs/i/{0}.jpg", match.Groups["getcontent"].Value);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
