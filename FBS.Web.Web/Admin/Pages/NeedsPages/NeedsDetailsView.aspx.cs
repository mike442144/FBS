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

namespace FBS.Web.News.Admin.Pages.NeedsPages
{
    public partial class NeedsDetailsView : System.Web.UI.Page
    {
        public string mycontent = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    string id = Request.QueryString["id"].ToString();
                    mycontent= new NeedsService().GetOneNeedsContentByID(new Guid(id)).NeedsContent;
                }
            }
        }
    }
}
