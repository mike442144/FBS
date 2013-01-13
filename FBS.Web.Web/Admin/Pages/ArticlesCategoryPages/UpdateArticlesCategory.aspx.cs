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
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Web.News.Backstage
{
    public partial class UpdateArticlesCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               string id= Request["id"].ToString();
                if(id!=null)
                {
                  Label1.Text = id;
                  BindCategory(new Guid(id));
                  ArticleCategoryDetailsModel model=new CategoryService().GetArticleCategoryById(new Guid(id));                
                  TBnewCategory.Text = model.CategoryName;
                  TBdescr.Text = model.Description;
                  TByouxianji.Text = model.Priority.ToString();
                }
            }
        }
        public void BindCategory(Guid CategoryID)
        {
            IList<ArticleCategoryModel> mylist = new CategoryService().FetchArticleCategory();
            IList<ListItem> treelist = GetArticleCategory(mylist, Guid.Empty);
            ArticleCategory.Items.Clear();
            ArticleCategory.Items.Add(new ListItem("根分类", Guid.Empty.ToString()));
            int item = 0;
            int count = 1;
            foreach (ListItem ls in treelist)
            {
                ArticleCategory.Items.Add(ls);
                if (ls.Value == new CategoryService().GetArticleCategoryById(CategoryID).ParentID.ToString())
                {
                    item = count;
                }
                count++;
            }
            ArticleCategory.SelectedIndex = item;
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            ArticleCategoryDetailsModel model = new ArticleCategoryDetailsModel();
            model.CategoryName = TBnewCategory.Text.Trim().ToString();
            model.Description = TBdescr.Text.Trim().ToString();
            model.Priority=System.UInt16.Parse(TByouxianji.Text.Trim().ToString());
            model.CategoryID =new Guid( Label1.Text.ToString());
            model.ParentID = new Guid(ArticleCategory.SelectedItem.Value.ToString());
            string t=  ArticleCategory.SelectedItem.Value.ToString();
            if (t == Guid.Empty.ToString())
            {
                model.Deepth = 1;
            }
            else
            {
                model.Deepth = ++new CategoryService().GetArticleCategoryById(new Guid(t)).Deepth;
            } 
           
            new CategoryService().UpdateArticleCategory(model);
            Response.Write("<script>alert('修改成功')</script>");
        }
    }
}
