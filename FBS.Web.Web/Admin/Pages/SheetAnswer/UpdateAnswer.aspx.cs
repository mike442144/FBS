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
using System.Collections.Specialized;

namespace FBS.Web.News.Pages.Back
{
    public partial class UpdateAnswer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {



          
         if(!string.IsNullOrEmpty(Request.Form["answerid"])&&!string.IsNullOrEmpty(Request.Form["answer"])&&!string.IsNullOrEmpty(Request.Form["questionid"]))
         {

             Guid answerid = new Guid(Request.Form["answerid"]);
             Guid questionid = new Guid(Request.Form["questionid"]);
             string answer = Request.Form["answer"];
            Service.BlogService service = new Service.BlogService();
            Service.ActionModels.SheetAnswerModel model = new Service.ActionModels.SheetAnswerModel();
           
            
            


                model.AnswerID = answerid;
                model.AnswerName = answer;
                model.QuestionID = questionid;
                try
                {
                    service.UpdateSheetAnswer(model);
                    Guid formid = service.GetFormIDByQuestionID(model.QuestionID);
                    Response.Redirect("~/Admin/Pages/SheetQuestion/Question.aspx?FormID="+formid);
                }
                catch
                { }

            



            }
        }
    }
}
