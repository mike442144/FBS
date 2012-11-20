using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace FBS.Utils
{
    public static class HttpCollects
    {
        /// <summary>
        /// 获取文档内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHTMLContent(string url)
        {
            return GetHTMLContent(url, null, null);
        }
        /// <summary>
        /// 获取文档内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHTMLContent(string url, string endRegexString)
        {
            return GetHTMLContent(url, null, endRegexString);
        }
        /// <summary>
        ///  获取html文档
        ///  如果endRegexString不为空，则获取从开头到第一次匹配endTagRegex为止的部分文档内容；
        ///  否则获取整个html文档
        /// </summary>
        public static string GetHTMLContent(string url, Encoding encoding, string endRegexString)
        {
            HttpWebResponse response = null;
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 30000;

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    return null;
                stream = response.GetResponseStream();
                if (!response.ContentType.ToLower().StartsWith("text/"))
                {
                    return null;
                }

                if (encoding == null)
                {
                    #region Get EnCoding
                    try
                    {
                        if (string.IsNullOrEmpty(response.CharacterSet) || response.CharacterSet == "ISO-8859-1")
                            encoding = getEncoding(url);
                        else
                            encoding = Encoding.GetEncoding(response.CharacterSet);
                    }
                    catch  //如果编码不存在，使用默认编码  
                    {
                        encoding = Encoding.UTF8;
                    }

                    #endregion
                }
                request.Timeout = 8000;
                request = (HttpWebRequest)WebRequest.Create(url);
                response = (HttpWebResponse)request.GetResponse();
                stream = response.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                string resultString = null;
                if (string.IsNullOrEmpty(endRegexString))
                    resultString = reader.ReadToEnd();
                else
                {
                    Regex endRegex = new Regex(endRegexString, RegexOptions.IgnoreCase);
                    StringBuilder builder = new StringBuilder();
                    string temp = string.Empty;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        builder.Append(temp);
                        temp = builder.ToString();
                        if (endRegex.IsMatch(temp)) { break; }
                    }
                    resultString = builder.ToString();
                }
                reader.Close();
                stream.Close();
                response.Close();
                return resultString;
            }
            catch (WebException e)
            {
                return null;
            }
            catch (IOException e)
            {
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                if (stream != null)
                    stream.Close();

                if (response != null)
                    response.Close();
            }
        }

        /// <summary>
        /// 从html文档中得到Encoding
        /// </summary>
        private static Encoding getEncoding(string url)
        {
            string htmlString = GetHTMLContent(url, Encoding.UTF8, @"charset\b\s*=\s*(?<charset>[a-zA-Z\d|-]*)");
            Regex reg_charset = new Regex(@"charset\b\s*=\s*(?<charset>[a-zA-Z\d|-]*)");
            Encoding encoding = null;
            if (reg_charset.IsMatch(htmlString))
            {
                foreach (Match match in reg_charset.Matches(htmlString))
                {
                    try
                    {
                        if (string.IsNullOrEmpty(match.Groups["charset"].Value))
                            continue;
                        encoding = Encoding.GetEncoding(match.Groups["charset"].Value);
                        if (encoding != null)
                            break;
                    }
                    catch
                    {
                    }
                }
                if (encoding == null)
                    encoding = Encoding.UTF8;
            }
            return encoding;
        }

        public static string GetMetaString(string html, string regStart, string regEnd, bool ignoreCase)
        {
            string regString = string.Format("{0}(?<getcontent>[\\s|\\S]*?){1}", regStart, regEnd);
            Regex reg;
            if (ignoreCase)
            {
                reg = new Regex(regString, RegexOptions.IgnoreCase);
            }
            else
            {
                reg = new Regex(regString);
            }
            return reg.Match(html).Groups["getcontent"].Value;
        }
        /// <summary>
        /// 获取html代码中的Title
        /// </summary>
        /// <param name="html"></param>
        /// <param name="ignoreCas"></param>
        /// <returns></returns>
        public static string GetTitle(string html, bool ignoreCas)
        {
            string title = HttpCollects.GetMetaString(html, "<meta name=\"title\"([\\s]*)content=\"", "\"([\\s]*)/?>", ignoreCas);
            if (string.IsNullOrEmpty(title))
            {
                string regex = @"(?<=<title.*>)([\s\S]*)(?=</title>)";
                System.Text.RegularExpressions.Regex ex = new System.Text.RegularExpressions.Regex(regex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                return ex.Match(html).Value.Trim();
            }
            return title;
        }
        /// <summary>
        /// 获取html代码中的description
        /// </summary>
        /// <param name="html"></param>
        /// <param name="ignoreCas"></param>
        /// <returns></returns>
        public static string GetDescription(string html, bool ignoreCas)
        {
            string description = HttpCollects.GetMetaString(html, "<meta([\\s]*)name=\"description\"([\\s]*)content=\"", "\"([\\s]*)/?>", ignoreCas);
            if (string.IsNullOrEmpty(description))
                description = HttpCollects.GetMetaString(html, "<meta([\\s]*)content=\"", "\"([\\s]*)name=\"description\"([\\s]*)/?>", ignoreCas);
            return description;
        }
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本<script[^>]*?>.*?</script>
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>([\s|\S])*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = Utils.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        public static string GetHTMLByUrl(string url, string sMethod, string Param, bool bAutoRedirect, System.Text.Encoding ecode)
        {
            sMethod = sMethod.ToUpper();
            sMethod = sMethod != "POST" ? "GET" : sMethod;
            string res = "";
            HttpWebRequest re = (HttpWebRequest)HttpWebRequest.Create(url);
            re.Method = sMethod;
            re.AllowAutoRedirect = bAutoRedirect;
            re.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; MyIE2; .NET CLR 1.1.4322)";
            re.Timeout = 10000;

            //re.Referer = url;
            if (sMethod == "POST") // Post data to Server
            {
                re.ContentType = "application/x-www-form-urlencoded";

                byte[] b = System.Text.Encoding.UTF8.GetBytes(Param);
                re.ContentLength = b.Length;
                try
                {
                    Stream oSRe = re.GetRequestStream();
                    oSRe.Write(b, 0, b.Length);
                    oSRe.Close();
                    oSRe = null;
                }
                catch (Exception)
                {
                    re = null;
                    return "-1";
                }
            }

            HttpWebResponse rep = null;
            Stream oResponseStream = null;
            StreamReader oSReader = null;
            try
            {
                rep = (HttpWebResponse)re.GetResponse();

                oResponseStream = rep.GetResponseStream();
                oSReader = new StreamReader(oResponseStream, ecode);
                res = oSReader.ReadToEnd();
            }
            catch (System.Net.WebException e)
            {
                //res ="-1";

                res = e.ToString();
            }

            if (rep != null)
            {
                rep.Close();
                rep = null;
            }
            if (oResponseStream != null)
            {
                oResponseStream.Close();
                oResponseStream = null;
            }

            if (oSReader != null)
            {
                oSReader.Close();
                oSReader = null;
            }
            re = null;

            return res;

        }

        public static string getHTMLByUrlCook(string url, ref System.Net.CookieContainer cook, string sMethod, string Param, bool bAutoRedirect, System.Text.Encoding ecode)
        {
            sMethod = sMethod.ToUpper();
            sMethod = sMethod != "POST" ? "GET" : sMethod;
            string res = "";
            HttpWebRequest re = (HttpWebRequest)HttpWebRequest.Create(url);
            re.CookieContainer = cook; // attach the cook object 
            re.Method = sMethod;
            re.AllowAutoRedirect = bAutoRedirect;
            re.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; MyIE2; .NET CLR 1.1.4322)";
            re.Timeout = 2000;

            //if (Public.Session.bUsePox) //利用代理
            //{
            //    int Port = 80;
            //    if (Public.IsInt(this.Session.PoxPort))
            //    {
            //        Port = Convert.ToInt32(this.Session.PoxPort);
            //    }
            //    WebProxy pox = new WebProxy(this.Session.PoxIp, Port);
            //    pox = (WebProxy)re.Proxy;
            //}


            //re.Referer="http://expert.csdn.net/Expert/topic/2839/2839298.xml?temp=.2714197";


            //re.ClientCertificates = new System.Security.Cryptography.X509Certificates.X509CertificateCollection();
            //re.ClientCertificates = System.Security.Cryptography.X509Certificates.X509Certificate.CreateFromSignedFile();

            //re.Referer = url;
            if (sMethod == "POST") // Post data to Server
            {
                re.ContentType = "application/x-www-form-urlencoded";

                byte[] b = System.Text.Encoding.UTF8.GetBytes(Param);
                re.ContentLength = b.Length;
                try
                {
                    Stream oSRe = re.GetRequestStream();
                    oSRe.Write(b, 0, b.Length);
                    oSRe.Close();
                    oSRe = null;
                }
                catch (Exception)
                {
                    re = null;
                    return "-1";
                }
            }

            HttpWebResponse rep = null;
            Stream oResponseStream = null;
            StreamReader oSReader = null;
            try
            {
                rep = (HttpWebResponse)re.GetResponse();

                oResponseStream = rep.GetResponseStream();
                oSReader = new StreamReader(oResponseStream, ecode);
                res = oSReader.ReadToEnd();
            }
            catch (System.Net.WebException e)
            {
                //res ="-1";

                res = e.ToString();
            }

            if (rep != null)
            {
                rep.Close();
                rep = null;
            }
            if (oResponseStream != null)
            {
                oResponseStream.Close();
                oResponseStream = null;
            }

            if (oSReader != null)
            {
                oSReader.Close();
                oSReader = null;
            }
            re = null;

            return res;

        }


        #region 带重试功能的获得目标源码

        /// <summary>
        /// 请求URL得到HTML内容,私有函数
        /// </summary>
        /// <param name="sUrl">url地址</param>
        /// <param name="sEncodeType">HTML内容编码方式</param>
        /// <param name="iMaxRetry">如果请求失败，最大重试次数</param>
        /// <param name="iCurrentRetry">当前是第几次请求</param>
        /// <returns>HTML内容</returns>
        private static string GetHtml(string sUrl, Encoding sEncode, int iMaxRetry, int iCurrentRetry)
        {
            string sHtml = string.Empty;
            try
            {
                Uri myUri = new Uri(sUrl);
                WebRequest myWebRequest = WebRequest.Create(myUri);
                WebResponse myWebResponse = myWebRequest.GetResponse();

                Stream ReceiveStream = myWebResponse.GetResponseStream();
                StreamReader readStream = new StreamReader(ReceiveStream, sEncode);

                sHtml = readStream.ReadToEnd();
                readStream.Close();
                myWebResponse.Close();
            }
            catch
            {
                iCurrentRetry++;
                if (iCurrentRetry <= iMaxRetry)
                {
                    GetHtml(sUrl, sEncode, iMaxRetry, iCurrentRetry);
                }
            }
            return sHtml;
        }

        /// <summary>
        /// 请求URL得到HTML内容，单线程
        /// </summary>
        /// <param name="sUrl">url地址</param>
        /// <param name="sEncodeType">HTML内容编码方式</param>
        /// <param name="iMaxRetry">如果请求失败，最大重试次数</param>
        /// <returns>HTML内容</returns>
        public static string GetHtml(string sUrl, Encoding sEncode, int iMaxRetry)
        {
            string sHtml = string.Empty;
            sHtml = GetHtml(sUrl, sEncode, iMaxRetry, 0);
            return sHtml;
        }

        #endregion


        #region 同步通过GET方式发送数据(cookies)
        /// <summary>
        /// 通过GET方式发送数据
        /// </summary>
        /// <param name="Url">url</param>
        /// <param name="postDataStr">GET数据</param>
        /// <param name="cookie">GET容器</param>
        /// <returns></returns>
        public static string GethtmlByCookie(string Url, Encoding sEncode, ref CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            if (cookie.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cookie = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cookie;
            }

            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, sEncode);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public static string GethtmlByCookie(string Url, Encoding sEncode)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

            string cookie = GetsiteCookie("http://www.baidu.com");
            request.Headers.Add("Cookie", cookie);


            request.Method = "GET";
            //request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, sEncode);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            response.Close();

            return retString;
        }


        public static string GetsiteCookie(string URL)
        {
            WebRequest wrt;
            wrt = WebRequest.Create(URL);
            wrt.Credentials = CredentialCache.DefaultCredentials;
            WebResponse wrp;

            wrp = wrt.GetResponse();
            string cookie = wrp.Headers.Get("Set-Cookie");
            wrp.Close();
            return cookie;
        }

        #endregion

    }
}
