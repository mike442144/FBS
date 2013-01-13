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
    public partial class Question : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Service.BlogService service = new Service.BlogService();
            if (!string.IsNullOrEmpty(Request.QueryString["questionID"])&&!string.IsNullOrEmpty(Request.QueryString["formID"]))
            {
               
                Guid questionID = new Guid(Request.QueryString["questionID"]);
                Guid formID = new Guid(Request.QueryString["formID"]);
                try
                {
                    service.DeleteQuestion(questionID);
                    Response.Write("删除成功");
                    Response.Redirect("Question.aspx?FormID="+formID);

                }
                catch
                {}
            }

            if(!string.IsNullOrEmpty(Request.QueryString["answerid"])&&!string.IsNullOrEmpty(Request.QueryString["formID"]))
            {
                
                Guid answerid = new Guid(Request.QueryString["answerid"]);
                Guid formID = new Guid(Request.QueryString["formID"]);
                try
                {
                    service.DeleteQuestionAnswer(answerid);
                    
                    Response.Write("删除成功");
                    Response.Redirect("Question.aspx?FormID=" + formID);
                }
                catch
                { }
            }

            if (!string.IsNullOrEmpty(Request.Form["formid"]) && !string.IsNullOrEmpty(Request.Form["question"]))
            {
                string formid = Request.Form["formid"];

                Service.ActionModels.AnswerSheetQuestionModel model = new Service.ActionModels.AnswerSheetQuestionModel();
                model.FormID = new Guid(Request.Form["formid"]);
                model.QuestionName = Request.Form["question"];
                try
                {
                    service.CreateQuestion(model);
                    Response.Redirect("Question.aspx?formid=" + formid);
                }
                catch { }
            }


            
        }
    }
}
