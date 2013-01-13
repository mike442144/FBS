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
    public partial class UpdateFormInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Service.BlogService service = new Service.BlogService();
            if (!string.IsNullOrEmpty(Request.Form["title"]) && !string.IsNullOrEmpty(Request.Form["formid"]) && !string.IsNullOrEmpty(Request.Form["description"]))
            {
                Service.ActionModels.FormInfoModel model = new Service.ActionModels.FormInfoModel();
                model.Title = Request.Form["title"];
                model.Description = Request.Form["description"];
                model.FormID =new Guid( Request.Form["formid"]);
                try
                {
                    service.UpdateFormInfo(model);
                    Response.Redirect("FormInfoList.aspx");

                }
                catch { }
            }

        }
    }
}
