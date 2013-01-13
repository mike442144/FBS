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
    public partial class AddAnswer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["questionid"]) && !string.IsNullOrEmpty(Request.Form["answer"]) && !string.IsNullOrEmpty(Request.Form["formid"]))
            {
                FBS.Service.BlogService service = new FBS.Service.BlogService();

                Service.ActionModels.SheetAnswerModel model = new Service.ActionModels.SheetAnswerModel();
                model.QuestionID = new Guid(Request.Form["questionid"]);
                model.AnswerName = Request.Form["answer"];
                model.FormID = new Guid(Request.Form["formid"]);
                try
                {
                    service.CreateQuestionAnswer(model);
                    Response.Redirect("~/Admin/Pages/SheetAnswer/AddAnswer.aspx?questionid="+model.QuestionID);
                }
                catch
                { }
            }
        }
    }
}
