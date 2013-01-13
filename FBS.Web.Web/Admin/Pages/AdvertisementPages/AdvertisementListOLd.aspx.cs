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
using FBS.Service.ActionModels;
using System.Collections.Generic;

namespace FBS.Web.News.Admin.Pages.AdvertisementPages
{
    public partial class AdvertisementList : System.Web.UI.Page
    {
        AdvertisementService myservice = new AdvertisementService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }


        protected void Row_Deleted(object sender, GridViewDeleteEventArgs e)
        {
            GridView g = (GridView)sender;
            string k = g.DataKeys[e.RowIndex].Value.ToString();
            myservice.DeleteAdvertisementByID(new Guid(k));
            BindGridView();
            //Response.Redirect("ArticlesCategoryList.aspx");
        }
        protected void gvBaidu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果当前行尾数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //添加鼠标在当前行时的background-color属性
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#fcdead';");

                //鼠标离开当前行后
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }

        public void BindGridView()
        {
            IList<AdvertisementDspModel> mylist =myservice.GetSomeAdvertisementContent(0,10);

            this.ArticlesGridView.DataSource = mylist;
            this.ArticlesGridView.DataBind();
        }



        protected void ArticlesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}
