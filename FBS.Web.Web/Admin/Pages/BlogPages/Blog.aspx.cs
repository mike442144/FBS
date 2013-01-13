using System;
using System.Collections.Generic;
using FBS.Service;
using FBS.Service.ActionModels;
using System.Web.Security;
using System.Web;

namespace FBS.Web.News.Admin.Pages.BlogPages
{
    public partial class Blog : System.Web.UI.Page
    {
        BlogService srv = new BlogService();
        CategoryService categorySrv = new CategoryService();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender,EventArgs e)
        {
            if (this.selectCategory.SelectedItem == null || this.txtTitle.Text.Trim() == string.Empty) return;
            NewBlogStoryModel m = new NewBlogStoryModel();
            m.Title = this.txtTitle.Text.Trim();
            m.Body = Request.Form["editorcontent"];

            m.CategoryID = new Guid(this.selectCategory.SelectedValue);
            m.CategoryName = this.selectCategory.SelectedItem.Text;

            //从cookie获取用户名及用户的ID
            HttpCookie hc = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket fat = FormsAuthentication.Decrypt(hc.Value);

            m.AccountID =new Guid( fat.Name);
            m.UserName = fat.UserData;
            try
            {
                srv.CreateBlogStory(m);
            }
            catch (Exception error) { }

        }

        protected override void OnPreRender(EventArgs e)
        {
            var categories = categorySrv.GetBlogCategoryList();
            this.selectCategory.DataSource = categories;
            this.selectCategory.DataTextField = "CategoryName";
            this.selectCategory.DataValueField = "BlogCategoryID";
            this.selectCategory.DataBind();
            base.OnPreRender(e);

        }
    }
}