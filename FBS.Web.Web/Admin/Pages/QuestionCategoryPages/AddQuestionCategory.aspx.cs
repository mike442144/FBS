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
    public partial class AddQuestionCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form["categoryname"] != null)
            {
                CategoryService myservice = new CategoryService();
                NewQuestionCategoryModel model = new NewQuestionCategoryModel() {  CategoryName  = Request.Form["categoryname"].ToString(),   Description = Request.Form["decription"].ToString(), Icon = "fly_note_icon", Priority = System.UInt16.Parse(Request.Form["priority"].ToString()) };
                myservice.CreateQuestionCategory(model);
                Response.Redirect("QuestionCategoryList.aspx");
            }
        }
    }
}
