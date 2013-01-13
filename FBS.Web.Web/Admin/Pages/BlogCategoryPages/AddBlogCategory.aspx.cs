using System;
using System.Web;
using System.Web.UI;
using FBS.Service;
using FBS.Service.ActionModels;

namespace FBS.Web.News.Admin.Pages.BlogCategoryPages
{
    public partial class AddBlogCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["categoryname"] != null)
            {
                CategoryService myservice = new CategoryService();
                NewBlogStoryCategoryModel model = new NewBlogStoryCategoryModel() { CategoryName = Request.Form["categoryname"].ToString(), Description = Request.Form["decription"].ToString(), Icon = "fly_note_icon", Priority = System.UInt16.Parse(Request.Form["priority"].ToString()) };
                myservice.CreateBlogStoryCategory(model);
                Response.Write("<script>alert('添加成功')</script>");
            }
            else {

                Response.Write("<script>alert('填写不完整！')</script>");
            }
        }

        
    }
}
