using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.XPath;
using System.Collections.Generic;


namespace FBS.Web.News.Admin
{
    public partial class AddRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["TB_name"] != null)
            {
                Button1_Click();
            }
        }

        public XmlNodeList BindControl()
        {
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml"); 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            XmlNodeList root = xmlDoc.GetElementsByTagName("Tasks")[0].ChildNodes;
            return root;
        }

        protected void Button1_Click()
        {
            string fpath = Server.MapPath("~/App_Data/Permissions.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            List<XmlElement> mylist = new List<XmlElement>(); 
            XmlNode root = xmlDoc.GetElementsByTagName("Roles")[0];
            XmlElement xe = xmlDoc.CreateElement("Role");
            xe.SetAttribute("Name", Request.Form["TB_name"]);
            for (int i = 0; i < BindControl().Count; i++)
            {
                XmlElement xe1 = xmlDoc.CreateElement("Task");
                if (Request.Form["CHB" + i.ToString()] != null)
                {
                    xe1.SetAttribute("Name", Request.Form["CHB" + i.ToString()]);
                    mylist.Add(xe1);
                }
            }
            root.AppendChild(xe);
            foreach (XmlElement xmlnode in mylist)
            {
                xe.AppendChild(xmlnode);
            }
            
            xmlDoc.Save(fpath);
            Response.Write("<script>alert('添加成功')</script>");
        }
        
    }
}
