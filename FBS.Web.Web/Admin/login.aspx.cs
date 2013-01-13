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
using FBS.Service;
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Security;
using FBS.Utils;

namespace FBS.Web.News.Backstage
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, EventArgs e)
        {
            string username = txtName.Text.ToString().Trim();
            string password = txtPwd.Text.ToString().Trim();
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            { msg.InnerHtml = "用户名或密码为空"; return; }
            UserEntryService userentry = new UserEntryService();
            UserLogOnModel model = new UserLogOnModel() { Email = username, Password = password, RememberMe = false };

            try
            {

                if (userentry.Logon(model) != null)
                {
                    try
                    {
                        AuthorityManager.PermissionCheck("ManageSystem");
                        Response.Redirect("index.html");
                    }
                    catch (AccessForbiddenException)
                    {//访问拒绝
                        msg.InnerHtml = "您不是系统管理员，无法进入系统管理！";
                    }
                    catch (ActionForbiddenException)
                    { //禁止操作
                        msg.InnerHtml = "您的操作被拒绝，你没有进入后台的权限！";
                    }
                    
                }
                else
                {
                    msg.InnerHtml = "用户名或者密码错误";
                }
            }
            catch (LogonException k)
            {//管理员被禁用
                msg.InnerHtml = k.Message;
            }

        }
    }
}
