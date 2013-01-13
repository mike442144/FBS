using System;
using FBS.Service;
using FBS.Service.ActionModels;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
using System.Web;
using System.Web.Security;

namespace FBS.Web.News.Admin.Pages.BlogPages
{
    public partial class Blogs : System.Web.UI.Page
    {
        BlogService srv = new BlogService();
        Guid userId 
        { 
            get 
            {
                //从cookie获取用户名及用户的ID
                HttpCookie hc = Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket fat = FormsAuthentication.Decrypt(hc.Value);
                return new Guid(fat.Name);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnPreRender(EventArgs e)
        {
            BindGridview();
            anp.RecordCount = srv.GetCountOfStorysByUserID(userId);
            base.OnPreRender(e);
        }
        public void BindGridview()
        {
            this.StoreGridView.DataSource = srv.GetAllBlogStorysSummaryOfUser(userId, 0, anp.PageSize);
            this.StoreGridView.DataBind();
        }
        protected void gvBaidu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果当前行尾数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //添加鼠标在当前行时的background-color属性
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#fcdead';");

                //鼠标离开当前行后
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }
        protected void getPager(object sender, EventArgs e)
        {
            AspNetPager g = (AspNetPager)sender;

            this.StoreGridView.DataSource = srv.GetAllBlogStorysSummaryOfUser(userId, anp.StartRecordIndex - 1, anp.PageSize);
            this.StoreGridView.DataBind();
        }
        protected void Row_Deleted(object sender, GridViewDeleteEventArgs e)
        {
            GridView g = (GridView)sender;
            string k = g.DataKeys[e.RowIndex].Value.ToString();
            //string k = g.Rows[e.RowIndex].Cells[0].Text.ToString();
            srv.RemoveBlogStory(new Guid(k));
            Response.Write("<script>alert('删除成功')</script>");
        }
    }
}