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
using Wuqi.Webdiyer;
using System.Collections.Generic;
using FBS.Service;

namespace FBS.Web.News.Admin.Pages.Suggest
{
    public partial class SuggestList : System.Web.UI.Page
    {
        public int _currentPageIndex = 1;
        public int _suggestCount = 0;

        protected void Page_Load(object sender, EventArgs e)
        {

            //if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            //{
            //    FormsAuthentication.RedirectToLoginPage();
            //    return;
            //}
            SuggestService service = new SuggestService();
            _suggestCount = service.GetSuggestCount();
            if (!IsPostBack)
            {

                if (!string.IsNullOrEmpty(Request.QueryString["pageIndex"]))
                {
                    this._currentPageIndex = Convert.ToInt32(Request.QueryString["pageIndex"]);
                }

            }

            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                Guid id = new Guid(Request.QueryString["id"]);
                
                try
                {
                    service.RemoveSuggest(id);
                    Response.Write("<script language=javascript>alert('删除成功');</script>");
                }
                catch
                { }
            }
        }




    }
}
