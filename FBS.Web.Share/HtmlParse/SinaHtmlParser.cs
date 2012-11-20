using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FBS.Domain.Aggregate.Entity;
using FBS.Utils;

namespace FBS.Web.Share.HtmlParser
{
    public class SinaHtmlParser : ISiteHtmlParser
    {
        #region ISiteHtmlParser 成员

        public void ParseHtml(string url, ref ShareThread shareThread)
        {
            string htmlContent = HttpCollects.GetHTMLContent(url);
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
            string regString = @"^http://you.video.sina.com.cn/((b/(?<getvid>\d+)-(?<getuid>\d+).html)|(pg/topicdetail/topicPlay.php\?([a-zA-Z\d_=&]*)tid=(?<gettid>\d+)&uid=(?<getuid>\d+)([&t=\d]*)#(?<getvid>\d+)))$";
            string playerFormat = @"http://p.you.video.sina.com.cn/player/outer_player.swf?auto=1&vid={0}&autoplay=1";

            Regex regex = new Regex(regString, RegexOptions.IgnoreCase);
            Match match = regex.Match(url);
            if (match.Success)
            {
                return string.Format(playerFormat, match.Groups["getvid"].Value);
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
            string regString = @"'http://(?<getcontent1>[a-zA-Z\d]+).v.iask.com/(?<getcontent2>[a-zA-Z\d\/_]+).jpg'";
            string regStringList = @"http://(?<getcontent3>[a-zA-Z\d]+).sinaimg.com.cn/(?<getcontent4>[a-zA-Z\d\/]+).gif";
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
                reg = new Regex(regStringList);
                match = reg.Match(html);
                if (match.Success)
                {
                    return string.Format("http://{0}.sinaimg.com.cn/{1}.gif", match.Groups["getcontent3"].Value, match.Groups["getcontent4"].Value);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
    }
}
