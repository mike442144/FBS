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

namespace FBS.Web.News.Admin.Pages.BlogCategoryPages
{
    public partial class UpdateBlogCategory : System.Web.UI.Page
    {
        public string myid = "";
        public string _categoryname = "";
        public string _decription = "";
        public string _priority = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["categoryid"]))
            {
                CategoryService myservice = new CategoryService();
                BlogCategoryModel model = myservice.GetBlogCategoryModelContentByID(new Guid(Request.Form["categoryid"]));
                model.CategoryName = Request.Form["categoryname"].ToString();
                model.Description = Request.Form["decription"].ToString();
                model.OrderPriority = System.UInt16.Parse(Request.Form["priority"].ToString());
                myservice.UpdateBlogCategoryModelContent(model);
                Response.Write("<script>alert('修改成功')</script>");
            }
            else
            {
                myid = Request.QueryString["id"].ToString();
                CategoryService myservice = new CategoryService();
                BlogCategoryModel model = myservice.GetBlogCategoryModelContentByID(new Guid(myid));
                _categoryname = model.CategoryName;
                _decription = model.Description;
                _priority = model.OrderPriority.ToString();
            }
        }
    }
}
