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


namespace FBS.Web.News.Pages.Back
{
    public partial class UpdateQuestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["formid"])&&!string.IsNullOrEmpty(Request.Form["questionid"]) && !string.IsNullOrEmpty(Request.Form["question"]))
            {
                Guid formid = new Guid(Request.Form["formid"]);
                Service.BlogService service = new Service.BlogService();
                Service.ActionModels.AnswerSheetQuestionModel model = new Service.ActionModels.AnswerSheetQuestionModel();
                model.QuestionID = new Guid( Request.Form["questionid"]);
                model.QuestionName = Request.Form["question"];
                try
                {
                    service.UpdateSheetQuestion(model);
                    Response.Redirect("Question.aspx?FormID="+formid);
                }
                catch
                { }
            }
        }
    }
}
