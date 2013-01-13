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
using System.Web.Mail;
using System.Xml;

namespace FBS.Web.News.Admin
{
    public partial class SendEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BT_SendEmail_Click(object sender, EventArgs e)
        {
        MailMessage objMailMessage; 
        MailAttachment objMailAttachment;
        string fpath = HttpContext.Current.Server.MapPath("~/App_Data/EmailServiceConfig.xml");
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(fpath);
        XmlNodeList mylist = xmlDoc.GetElementsByTagName("Server")[0].ChildNodes;
        XmlNode mynode = null;
        foreach(XmlNode node in mylist)
        {
            if (node.Attributes["Name"].Value == "myserver")
            {
                mynode = node; break;
            }
        }
        string[] mystringlist =new string[10];
        XmlNodeList nodelist = mynode.ChildNodes;
        for (int i = 0; i < nodelist.Count; i++)
        {
            mystringlist[i] = nodelist[i].InnerText;
        }
            // 创建一个附件对象 
            //objMailAttachment = new MailAttachment( "d://test.txt" );//发送邮件的附件 
            // 创建邮件消息 
            objMailMessage = new MailMessage();
        objMailMessage.From = mystringlist[6];//源邮件地址 
        objMailMessage.To = "869722304@qq.com";//目的邮件地址，也就是发给我哈 
        objMailMessage.Subject = "邮件发送标题：你好";//发送邮件的标题 
        objMailMessage.Body = "邮件发送标内容：测试一下是否发送成功！";//发送邮件的内容 
        //objMailMessage.Attachments.Add( objMailAttachment );//将附件附加到邮件消息对象中 
        //接着利用sina的SMTP来发送邮件，需要使用Microsoft .NET Framework SDK v1.1和它以上的版本 
        //基本权限 
        objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate","1"); 
        //用户名 
        objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername",mystringlist[4]); 
         //密码 
        objMailMessage.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", mystringlist[5]); 
        //如果没有上述三行代码，则出现如下错误提示：服务器拒绝了一个或多个收件人地址。服务器响应为: 554 : Client host rejected: Access denied 
        //SMTP地址 
        SmtpMail.SmtpServer = mystringlist[2];
        //开始发送邮件 
        SmtpMail.Send( objMailMessage ); 
         //核心代码结束 
        }
    }
}
