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
using System.Collections.Generic;
using System.Xml;

namespace FBS.Web.News.Admin
{
    public partial class AddMission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml"); 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            List<XmlElement> mylist = new List<XmlElement>();
            XmlNode root = xmlDoc.GetElementsByTagName("Tasks")[0];
            XmlElement xe = xmlDoc.CreateElement("Task");
            xe.SetAttribute("Name", Opname.Text.ToString().Trim());
            xe.SetAttribute("Description",OpDscr.Text.ToString().Trim());
            root.AppendChild(xe);
            xmlDoc.Save(fpath);
            Response.Write("<script>alert('添加成功')</script>");
        }
    }
}
