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

namespace FBS.Web.News.Backstage
{
    public partial class ArticlesList : System.Web.UI.Page
    {
        CMSService cmssservice = new CMSService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
                {
                    if (Request.QueryString["Type"] == "add")
                    {
                        Response.Write("<script type='text/javascript'>alert('添加成功！')</script>");
                    }
                    else if (Request.QueryString["Type"] == "update")
                    {
                        Response.Write("<script type='text/javascript'>alert('修改成功！')</script>");
 
                    }
                }
                
               // BindCategory(new Guid("5328ebeb-4683-4e3b-8724-0daaa32d74bd"));
                BindCategory(new Guid("d30be3bb-5c2b-416f-af72-e8b8a29683b6"));
                //anp.RecordCount = cmssservice.GetArticleCountOfCategory(new Guid("5328ebeb-4683-4e3b-8724-0daaa32d74bd"));
                anp.RecordCount = cmssservice.GetArticleCountOfCategory(new Guid("d30be3bb-5c2b-416f-af72-e8b8a29683b6"));
            }
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

        public void BindGridview( Guid CategoryID)
        {
            this.ArticlesGridView.DataSource = cmssservice.FetchArticleByCategoryID(CategoryID, 0, anp.PageSize);
            this.ArticlesGridView.DataBind();
        }

        protected void getPager(object sender, EventArgs e)
        {
            AspNetPager g = (AspNetPager)sender;

            GetPageList(g,new Guid(ArticlesCategoryDP.SelectedItem.Value));
        }

       
        private void GetPageList(AspNetPager anp,Guid CategoryID)
        {
            this.ArticlesGridView.DataSource = cmssservice.FetchArticleByCategoryID(CategoryID, anp.StartRecordIndex-1, anp.PageSize);
            this.ArticlesGridView.DataBind();
        }

        //绑定文章类别
        public void BindCategory(Guid CategoryID)
        {
            IList<ArticleCategoryModel> mylist = new CategoryService().FetchArticleCategory();
            IList<ListItem> treelist = GetArticleCategory(mylist, Guid.Empty);
            ArticlesCategoryDP.Items.Clear();
            int  item=0;
            int count = 0;
            foreach (ListItem ls in treelist)
            {
                
                ArticlesCategoryDP.Items.Add(ls);
                if (ls.Value == CategoryID.ToString())
                {
                    item = count;
                }
                count++;
            }
            ArticlesCategoryDP.SelectedIndex=item;
            //ArticlesCategoryDP.SelectedItem.Value=CategoryID.ToString();
            anp.RecordCount = cmssservice.GetArticleCountOfCategory(CategoryID);
            BindGridview(CategoryID);
        }

        //获取文章类别森林
        IList<ListItem> treelist = new List<ListItem>();
        public IList<ListItem> GetArticleCategory(IList<ArticleCategoryModel> mylist, Guid ParentID)
        {
            foreach (ArticleCategoryModel model in mylist)
            {
               
                if (ParentID.Equals(model.ParentID))
                {
                    if (Guid.Empty.Equals(model.ParentID))
                    {
                        treelist.Add(new ListItem(model.CategoryName, model.CategoryID.ToString()));
                    }
                    else
                    {
                        string index = "";
                        for (int i = 0; i < model.Deepth; i++)
                        {
                            index += "--";
                        }
                        index += "┣";
                        treelist.Add(new ListItem(index + model.CategoryName, model.CategoryID.ToString()));
                    }
                    GetArticleCategory(mylist, model.CategoryID);
                }

            }
            return treelist;

        }

        protected void ArticlesCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            anp.RecordCount = cmssservice.GetArticleCountOfCategory(new Guid(ArticlesCategoryDP.SelectedItem.Value));
            BindGridview(new Guid(ArticlesCategoryDP.SelectedItem.Value));
        }

        protected void Row_Deleted(object sender, GridViewDeleteEventArgs e)
        {
            GridView g = (GridView)sender;
            string k = g.DataKeys[e.RowIndex].Value.ToString();
            //string k = g.Rows[e.RowIndex].Cells[0].Text.ToString();
            cmssservice.RemoveArticleByID(new Guid(k));
            Response.Write("<script>alert('删除成功')</script>");
        }

        
    }


}
