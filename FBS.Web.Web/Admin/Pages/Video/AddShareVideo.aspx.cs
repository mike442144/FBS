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

namespace FBS.Web.News.Admin.Pages.Video
{
    public partial class AddShareVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = Request.Form["input_box"];
            string comment = Request.Form["subject"];
            string title=Request.Form["videoname"];
            if (!string.IsNullOrEmpty(url))
            {
                //if (Context.User.Identity.IsAuthenticated && !string.IsNullOrEmpty(Context.User.Identity.Name))
                //{
                    //UserInfoService uis = new UserInfoService();
                    //UserStateModel umodel = uis.GetUserInfo(new Guid(Context.User.Identity.Name));

                    //获取视频信息
                    //NewVideoModel model = new NewVideoModel() { RawUrl = url, Sharer = umodel, Comment = comment };
                try
                {
                    NewVideoModel model = new NewVideoModel() {  RawUrl = url, Sharer = null, Comment = comment, };
                    ShareThreadService mservice = new ShareThreadService();

                    mservice.CreateShareThread(model, url, "新闻", title);
                    Response.Write("<script type='text/javascript'>self.close()</script>");
                    Response.Write("<script type='text/javascript'>alert('创建视频成功！')</script>");
                }
                catch (Exception error)
                {
                    throw new Exception("视频路径不正确");
                }
                //}

            }

        }
    }
}
