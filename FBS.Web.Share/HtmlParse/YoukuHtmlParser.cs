using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FBS.Utils;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Web.Share.HtmlParser
{
    public class YoukuHtmlParser : ISiteHtmlParser
    {
        #region ISiteHtmlParser 成员

        public void ParseHtml(string url, ref ShareThread shareThread)
        {
            string htmlContent = HttpCollects.GetHTMLContent(url);
            if (string.IsNullOrEmpty(htmlContent))
                return;

            //新实例
            shareThread = new ShareThread(url,GetPlayerUrlString(url), MediaType.Video, HttpCollects.GetTitle(htmlContent, true), HttpCollects.GetDescription(htmlContent, true), GetThumbnailUrlString(htmlContent, true),DateTime.Now);
        }
        #endregion



        public string GetPlayerUrlString(string url)
        {
            string regString = @"^http://v.youku.com/v_show/id_(?<getcontent>[a-zA-Z\d=]+).html$";
            string playerFormat = @"http://player.youku.com/player.php/sid/{0}&isAutoPlay=true/v.swf";

            Regex regex = new Regex(regString, RegexOptions.IgnoreCase);
            Match match = regex.Match(url);
            if (match.Success)
                return string.Format(playerFormat, match.Groups["getcontent"].Value);
            else
                return url;
        }

        public string GetThumbnailUrlString(string html, bool ignoreCase)
        {
            string regString = @"iku://.*http://(?<getcontent>[a-zA-Z\d]+.ykimg.com/[a-zA-Z\d\-]+)";
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
                return "http://" + match.Groups["getcontent"].Value;
            else
                return string.Empty;
        }
    }
}
