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
using System.Collections.Generic;
using Wuqi.Webdiyer;
using FBS.Service.ActionModels;

namespace FBS.Web.News.Backstage
{
    public partial class ArticlesCategoryList : System.Web.UI.Page
    {
        CategoryService myservice = new CategoryService();
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
            myservice.DeleteArticleCategory(new Guid(k));


            BindGridView();
            Response.Write("<script>alert('删除成功')</script>");
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
            IList<ArticleCategoryModel> mylist = myservice.FetchArticleCategory();

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
