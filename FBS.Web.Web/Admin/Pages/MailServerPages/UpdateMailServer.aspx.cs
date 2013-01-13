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
    public partial class UpdateMailServer : System.Web.UI.Page
    {
        string servername = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    servername = Request.QueryString["id"].ToString();
                    Label1.Text = servername;
                    BindPage(servername);
                }
                //else
                //{
                //    servername = "myserver";
                //}
            }
        }

        protected void BT_AddServer_Click(object sender, EventArgs e)
        {
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/EmailServiceConfig.xml"); 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            List<XmlElement> mylist = new List<XmlElement>();
            XmlNode root = xmlDoc.GetElementsByTagName("Server")[0];
            foreach(XmlNode node in root.ChildNodes)
            {
            if(node.Attributes["Name"].Value==Label1.Text.ToString().Trim())
            {
                root.RemoveChild(node); break;
            }
            }
            xmlDoc.Save(fpath);
            XmlElement xe = xmlDoc.CreateElement("EmailServer");
            xe.SetAttribute("Name", TB_ServerName.Text.Trim().ToString());
            root.AppendChild(xe);
            XmlElement xe1 = xmlDoc.CreateElement("Property");
            xe1.SetAttribute("Name", "Priority");
            xe1.InnerText = TB_Priority.Text.ToString().Trim();
            xe.AppendChild(xe1);
            XmlElement xe2 = xmlDoc.CreateElement("Property");
            xe2.SetAttribute("Name", "MailType");
            xe2.InnerText = "SMTP";
            xe.AppendChild(xe2);
            XmlElement xe3 = xmlDoc.CreateElement("Property");
            xe3.SetAttribute("Name", "MailServerAddress");
            xe3.InnerText = TB_ServerAddr.Text.ToString().Trim();
            xe.AppendChild(xe3);
            XmlElement xe4 = xmlDoc.CreateElement("Property");
            xe4.SetAttribute("Name", "MailPort");
            xe4.InnerText = TB_Port.Text.ToString().Trim();
            xe.AppendChild(xe4);
            XmlElement xe5 = xmlDoc.CreateElement("Property");
            xe5.SetAttribute("Name", "MailUser");
            xe5.InnerText = TB_UserName.Text.ToString().Trim();
            xe.AppendChild(xe5);
            XmlElement xe6 = xmlDoc.CreateElement("Property");
            xe6.SetAttribute("Name", "MailPassord");
            xe6.InnerText = TB_Password.Text.ToString().Trim();
            xe.AppendChild(xe6);
            XmlElement xe7 = xmlDoc.CreateElement("Property");
            xe7.SetAttribute("Name", "MailAddress");
            xe7.InnerText = TB_MailAddr.Text.ToString().Trim();
            xe.AppendChild(xe7);
            XmlElement xe8 = xmlDoc.CreateElement("Property");
            xe8.SetAttribute("Name", "MailServer");
            xe8.InnerText = TB_SendUserName.Text.ToString().Trim();
            xe.AppendChild(xe8);
            XmlElement xe9 = xmlDoc.CreateElement("Property");
            xe9.SetAttribute("Name", "SSLSupport");
            xe9.InnerText = DropD_SSLSupport.SelectedItem.Value.ToString();
            xe.AppendChild(xe9);
            XmlElement xe10 = xmlDoc.CreateElement("Property");
            xe10.SetAttribute("Name", "MailServerState");
            if (CHB_ON.Checked)
            {
                xe10.InnerText = "on";
            }
            else
            {
                xe10.InnerText = "off";
            }
            xe.AppendChild(xe10);
            root.AppendChild(xe);
            xmlDoc.Save(fpath);
            Response.Redirect("MailServerList.aspx");
        }

        public void BindPage(string servername)
        {
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/EmailServiceConfig.xml"); 
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            XmlNodeList mylist = xmlDoc.GetElementsByTagName("Server")[0].ChildNodes;
           // XmlNodeList configlsit=xmlDoc.GetElementsByTagName("Server")[0].
            XmlNodeList mynodelist = null;
            foreach(XmlNode node in mylist)
            {
                if (node.Attributes["Name"].Value == servername)
                {
                    mynodelist = node.ChildNodes; break;
                }
            }
            TB_ServerName.Text = servername;
            string[] keylist=new string[10];
            for (int i = 0; i < mynodelist.Count; i++)
            {
                keylist[i] = mynodelist[i].InnerText;
            }
            TB_Priority.Text = keylist[0];
            TB_ServerAddr.Text = keylist[2];
            TB_Port.Text = keylist[3];
            TB_UserName.Text = keylist[4];
            TB_Password.Text=keylist[5];
            TB_MailAddr.Text = keylist[6];
            TB_SendUserName.Text = keylist[7];
            if (keylist[8] == "true")
            {
                DropD_SSLSupport.SelectedIndex = 0;
            }
            else
            {
                DropD_SSLSupport.SelectedIndex = 1;
            }
            if (keylist[9] == "on")
            {
                CHB_ON.Checked=true;
            }
            else
            {
                CHB_OFF.Checked =true;
            }
        }

        protected void CHB_ON_CheckedChanged(object sender, EventArgs e)
        {
            if (CHB_ON.Checked)
            {
                CHB_OFF.Checked = false;
            }
            else
            {
                CHB_OFF.Checked = true;
            }
        }

        protected void CHB_OFF_CheckedChanged(object sender, EventArgs e)
        {
            if (CHB_OFF.Checked)
            {

                CHB_ON.Checked = false;
            }
            else
            {

                CHB_ON.Checked = true;
            }
        }
    }
}
