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
    public partial class UpdataMission : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml"); 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            XmlNode root = xmlDoc.GetElementsByTagName("Tasks")[0];
            XmlNodeList mylist = root.ChildNodes;
            foreach (XmlNode node in mylist)
            {
                if (node.Attributes["Name"].Value == Label1.Text.Trim().ToString())
                {
                   node.Attributes["Name"].Value=Opname.Text.Trim().ToString();
                   node.Attributes["Description"].Value=OpDscr.Text.Trim().ToString();
                   break;
                }
            }
            xmlDoc.Save(fpath);
            Response.Write("<script>alert('修改成功')</script>");
        }

        protected override void OnPreRender(EventArgs e)
        {
            string Name = Request["id"].ToString();
            Label1.Text = Name;
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml"); 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            XmlNode root = xmlDoc.GetElementsByTagName("Tasks")[0];
            XmlNodeList mylist = root.ChildNodes;
            foreach (XmlNode node in mylist)
            {
                if (node.Attributes["Name"].Value == Name)
                {
                    Opname.Text = node.Attributes["Name"].Value;
                    OpDscr.Text = node.Attributes["Description"].Value;
                    break;
                }
            }
            base.OnPreRender(e);
        }
    }
}
