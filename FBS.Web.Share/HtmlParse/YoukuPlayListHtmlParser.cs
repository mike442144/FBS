using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FBS.Utils;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Web.Share.HtmlParser
{
    public class YoukuPlayListHtmlParser:ISiteHtmlParser
    {
        #region ISiteHtmlParser 成员

        public void ParseHtml(string url, ref ShareThread shareThread)
        {
            string ThumbnailUrlString = string.Empty;
            string htmlContent = HttpCollects.GetHTMLContent(url);
            if (string.IsNullOrEmpty(htmlContent))
                return;
            
            
            string PlayerUrl = GetPlayerUrlString(url, htmlContent, out ThumbnailUrlString);//换取url里的参数来设置播放器地址并且从页面里获取播放器地址的参数Sid和缩略图        

            //新实例
            shareThread = new ShareThread(url,PlayerUrl, MediaType.Video, HttpCollects.GetTitle(htmlContent, true), HttpCollects.GetDescription(htmlContent, true), ThumbnailUrlString,DateTime.Now);

            return;
        }
        #endregion


        /// <summary>
        /// //获取url里的参数来设置播放器地址并且从页面里获取播放器地址的参数Sid和缩略图
        /// </summary>
        /// <param name="url">url地址</param>
        /// <param name="html"></param>
        /// <returns></returns>
        public string GetPlayerUrlString(string url, string html, out string ThumbnailUrlString)
        {
            string regUrlString = @"^http://v.youku.com/v_playlist/f(?<getfid>\d+)o(?<getob>\d+)p(?<getpt>\d+).html$";
            string regPlayerSidString = "var videoId2= '(?<getsid>[a-zA-Z\\d=._]+)';";
            string regThumbnailUrlString = "\\|http://(?<getcontent1>[a-zA-Z\\d]+).ykimg.com/(?<getcontent2>[a-zA-Z\\d-]+)\\|";

            //获取url里的参数来设置播放器地址
            Regex regex = new Regex(regUrlString, RegexOptions.IgnoreCase);
            Match matchUrl = regex.Match(url);
            if (matchUrl.Success)
            {
                //从页面里获取播放器地址的参数Sid
                regex = new Regex(regPlayerSidString, RegexOptions.IgnoreCase);
                Match matchPlayerSid = regex.Match(html);
                if (matchPlayerSid.Success)
                {
                    //获取页面里的缩略图
                    regex = new Regex(regThumbnailUrlString, RegexOptions.IgnoreCase);
                    Match matchThumbnailUrl = regex.Match(html);
                    if (matchThumbnailUrl.Success)
                    {
                        ThumbnailUrlString = string.Format("http://{0}.ykimg.com/{1}", matchThumbnailUrl.Groups["getcontent1"].Value, matchThumbnailUrl.Groups["getcontent2"].Value);
                    }
                    else
                    {
                        ThumbnailUrlString = string.Empty;
                    }
                    return string.Format("http://player.youku.com/player.php/Type/Folder/Fid/{0}/Ob/{1}/Pt/{2}/sid/{3}&isAutoPlay=true/v.swf", matchUrl.Groups["getfid"].Value, matchUrl.Groups["getob"].Value, matchUrl.Groups["getpt"].Value, matchPlayerSid.Groups["getsid"].Value);
                }
                else
                {
                    ThumbnailUrlString = string.Empty;
                    return url;
                }
            }
            else
            {
                ThumbnailUrlString = string.Empty;
                return url;
            }
        }
    }
}
