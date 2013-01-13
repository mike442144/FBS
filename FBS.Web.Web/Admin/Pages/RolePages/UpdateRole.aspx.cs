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
using System.Collections.Generic;

namespace FBS.Web.News.Admin
{
    public partial class UpdateRole : System.Web.UI.Page
    {
        public string thename;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["id"] != null)
            {
                thename = Request["id"].ToString();

            }
            else
            {
                thename = Request.Form["myhide"];
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

        public XmlNodeList GetMylist()
        {

            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            XmlNodeList myroot = xmlDoc.GetElementsByTagName("Role");
            XmlNodeList k1 = null;
            foreach(XmlNode node in myroot)
            {
                if (node.Attributes["Name"].Value == thename)
                {
                    k1 = node.ChildNodes; break;
                }
            }
            return k1;
        }

        protected void Button1_Click()
        {
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            List<XmlElement> mylist = new List<XmlElement>();
            XmlNode root = xmlDoc.GetElementsByTagName("Roles")[0];
            XmlNode k1 = null;
            XmlNodeList myroot = xmlDoc.GetElementsByTagName("Role");
            foreach (XmlNode node in myroot)
            {
                if (node.Attributes["Name"].Value == thename)
                {
                    k1 = node;
                    break;
                }
            }
            root.RemoveChild(k1);
            XmlElement xe = xmlDoc.CreateElement("Role");
            xe.SetAttribute("Name", thename);
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
            Response.Write("<script>alert('修改成功')</script>");
        }
    }
}
