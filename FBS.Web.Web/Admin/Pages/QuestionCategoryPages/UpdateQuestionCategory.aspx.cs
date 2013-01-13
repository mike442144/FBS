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
using Aviator.Service;
using Aviator.Service.ActionModels;

namespace Aviator.Web.Admin.Pages.QuestionCategoryPages
{
    public partial class UpdateQuestionCategory : System.Web.UI.Page
    {
        public string myid = "";
        public string _categoryname = "";
        public string _decription = "";
        public string _priority = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
            {
               
                CategoryService myservice = new CategoryService();
                string id = Request.Form["categoryid"].ToString();
                BlogQuestionCategoryModel model = myservice.GetBlogQuestionCategoryModelContentByID(new Guid(id));
                model.CategoryName = Request.Form["categoryname"].ToString();
                model.Description = Request.Form["decription"].ToString();
                model.OrderPriority = System.UInt16.Parse(Request.Form["priority"].ToString());
                myservice.UpdateBlogQuestionCategory(model);
            }
            else
            {
                myid = Request.QueryString["id"].ToString();
                CategoryService myservice = new CategoryService();
                BlogQuestionCategoryModel model = myservice.GetBlogQuestionCategoryModelContentByID(new Guid(myid));
                _categoryname = model.CategoryName;
                _decription = model.Description;
                _priority = model.OrderPriority.ToString();
            }
        }
    }
}
