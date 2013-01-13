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

using FBS.Service.ActionModels;
using FBS.Service;

namespace FBS.Web.News.Admin.Pages.Suggest
{
    public partial class ReplySuggest : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.Form["txtContent"]) && !string.IsNullOrEmpty(Request.Form["sid"]))
            {
                SuggestService service = new SuggestService();
                SuggestModel model = new SuggestModel();
                model.SuggestID = new Guid(Request.Form["sid"]);
                model.Reply = Request.Form["txtContent"];
              
                try
                {
                    service.ReplySuggest(model);
                    Response.Redirect("SuggestList.aspx");
                }
                catch
                { }
 
            }
        }

     

    }

}
