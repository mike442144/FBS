using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using System.Collections.Specialized;
using System.Configuration;
using System.Text.RegularExpressions;

namespace FBS.Web.Share
{
    public class VideoShareProvider
    {
        /// <summary>
        /// 解析url地址获取数据
        /// </summary>
        /// <param name="url">原始Url地址</param>
        /// <param name="shareThread">分享对象</param>
        public static void ParseHtml(string url, ref ShareThread shareThread)
        {
            Instance(url).ParseHtml(url, ref shareThread);
        }

        private static ISiteHtmlParser Instance(string rawUrl)
        {
            NameValueCollection collection = ConfigurationManager.AppSettings;
            string value = string.Empty;
            object target = null;

            foreach (string key in collection.Keys)
            {
                Regex validator = new Regex(key, RegexOptions.IgnoreCase);
                Match m = validator.Match(rawUrl);
                if (m.Success)
                {
                    value = collection[key];
                    break;
                }
            }

            if (!value.Equals(string.Empty))
            {
                target = Activator.CreateInstance(Type.GetType(value));
                return (ISiteHtmlParser)target;
            }
            else
                return null;
        }
    }
}
