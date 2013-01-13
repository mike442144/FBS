using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Aviator.Service.ActionModels;
using Aviator.Service;
using System.Collections.Generic;

namespace Aviator.Web.Pages.Back
{
    public partial class FormInfoList : CheckAdminUser
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
 
            }
            Service.BlogService service = new BlogService();
            Service.ActionModels.FormInfoModel model = new FormInfoModel();
            if (!string.IsNullOrEmpty(Request.QueryString["FormID"]))
            {
                string formid = Request.QueryString["FormID"];
                try
                {
                    service.DeleteFormInfo(new Guid( formid));
                }
                catch
                { }
 
            }

            if (!string.IsNullOrEmpty(Request.Form["FormID"]) )
            {
                Guid id =new Guid( Request.Form["FormID"]);
                int count = service.IsHaveTwoQuestion(id);
                if (count > 2)
                {
                    Response.Write("<script language='javascript'>alert('您选择的问卷下的问题数量多于两条，您可以选择其他问卷或删除该问卷下的一些问题！');</script>");
                }
                else
                {
                    try
                    {
                        service.DisPlayForm(id);

                    }
                    catch
                    { }
                }
            }

            
            if (!string.IsNullOrEmpty(Request.Form["title"]) && !string.IsNullOrEmpty(Request.Form["description"]))
            {

                model = new FormInfoModel { FormID = new Guid(), Title = Request.Form["title"], Description = Request.Form["description"] };
                try
                {
                    service.CreateFormInfo(model);
                    
                }
                catch (Exception error)
                {
                    Response.Write("添加失败,原因为：" + error.Message);
                }
            }
        }
    }
}
