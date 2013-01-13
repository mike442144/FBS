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

namespace FBS.Web.News.Backstage
{
    public partial class AddArticlesCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
            }
        }


       
        protected void Button2_Click(object sender, EventArgs e)
        {
            NewArticleCategoryModel model = new NewArticleCategoryModel();
            model.CategoryName = TBnewCategory.Text.ToString().Trim();

            string t = ArticleCategory.SelectedItem.Value.ToString();
            if (t == Guid.Empty.ToString())
            {
                model.Deepth = 1;
            }
            else
            {
                model.Deepth = ++new CategoryService().GetArticleCategoryById(new Guid(t)).Deepth;
            } 
            model.Description = TBdescr.Text.Trim().ToString();
            model.Icon = String.Empty;
            model.ParentID = new Guid(ArticleCategory.SelectedItem.Value.ToString());
            model.Priority = System.UInt16.Parse(TByouxianji.Text.Trim().ToString());
            new CategoryService().CreateArticleCategory(model);
            Response.Write("<script>alert('添加成功')</script>");
        }

        

        public void BindCategory()
        {
            IList<ArticleCategoryModel> mylist = new CategoryService().FetchArticleCategory();
            IList<ListItem> treelist = GetArticleCategory(mylist, Guid.Empty);
            ArticleCategory.Items.Clear();
            ArticleCategory.Items.Add(new ListItem( "*根分类",Guid.Empty.ToString()));
            foreach (ListItem ls in treelist)
            {
                ArticleCategory.Items.Add(ls);
            }
        }
        IList<ListItem> treelist = new List<ListItem>();
        public IList<ListItem> GetArticleCategory(IList<ArticleCategoryModel> mylist, Guid ParentID)
        {
            foreach (ArticleCategoryModel model in mylist)
            {
                if (ParentID.Equals(model.ParentID))
                {
                    if (Guid.Empty.Equals(model.ParentID))
                    {
                        treelist.Add(new ListItem("-"+model.CategoryName, model.CategoryID.ToString()));
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
    }
}
