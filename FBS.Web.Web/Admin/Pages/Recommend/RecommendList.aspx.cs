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


namespace FBS.Web.News.Admin.Pages.Recommend
{
    public partial class RecommendList : System.Web.UI.Page
    {
        public int _currentPageIndex = 1;
        public int _companyCount = 0;
        public int _displayCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }

            BlogService service = new BlogService();
            _companyCount = service.AllCompanyCount();
            _displayCount = service.AllDisplayCount();

            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
                {
                    this._currentPageIndex = Convert.ToInt32(Request.QueryString["pageIndex"]);
                }

            }

            if (!string.IsNullOrEmpty(Request.QueryString["op"]) && !string.IsNullOrEmpty(Request.QueryString["userid"]))
            {
                string op = Request.QueryString["op"];
                Guid id = new Guid(Request.QueryString["userid"]);
                if (op == "1")
                {
                    if (_displayCount > 5)
                    {
                        Response.Write("<script language='javascript'>alert('您选择显示的推荐机构已多于TOP5的标准，您可以取消一些显示')</script>");
                        return;
                    }
                    service.SetDisplyCompany(id);
                }
                else if (op == "0")
                {
                    service.CancelDisplayCompany(id);
                }
            }
        }
    }
}
