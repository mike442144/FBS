using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

namespace FBS.Web.News.Admin.Pages.AdvertisementPages
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GetXML : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(context.Server.MapPath("~/App_Data/Advertiser.xml"));
            XmlNode rootnode = xmldoc.DocumentElement;
            context.Response.Write(xmldoc.InnerXml);//返回修改后的Xml文档 
            context.Response.End(); 
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
