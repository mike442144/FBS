using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Web;
using FBS.EntLibHelper;
using System.IO;
using System.Xml.Linq;

namespace FBS.Repository
{
    internal class XmlData
    {
        /// <summary>
        /// .ctor
        /// </summary>
        public XmlData()
        {
            LoadXml();
        }

        /// <summary>
        /// 加载Xml
        /// </summary>
        private void LoadXml()
        {
            //this.Doc = new XmlDocument();
            /*为节省空间,不将对象载入缓存.将xml文件以字符串形式读出并存入缓存,XmlDocument直接加载字符流*/

            //缓存键
            string cacheKey = "FBS.Provinces.xml";

            //检测缓存
            if (null == EntLibHelper.EntLibHelper.GetCachedObject(cacheKey))
            {
                //文件路径
                this._source = HttpContext.Current.Server.MapPath("~/App_Data/Provinces.xml");

                //数据读取流
                StreamReader reader = new StreamReader(this._source);

                //Xml形式字符串
                string xmlStream = string.Empty;
                try
                {
                    //读取整个文档
                    xmlStream = reader.ReadToEnd();
                }
                catch (OutOfMemoryException merror)
                {
                    throw new Exception("Xml文件: '" + this._source + "' 过大,无法读入内存!", merror);
                }
                catch (IOException ioerror)
                {
                    throw new Exception("读取Xml文件" + this._source + "出错,输入输出错误!", ioerror);
                }
                //Xml字符串存入缓存,并以文件作为依赖项
                EntLibHelper.EntLibHelper.StoreInCache(cacheKey, xmlStream, this._source);
            }
            //加载字符串,建立XmlDocument对象
            this.Doc = XDocument.Parse(EntLibHelper.EntLibHelper.GetCachedObject(cacheKey).ToString(),LoadOptions.None);
        }

        public XDocument Doc;
        private string _source;
    }
}
