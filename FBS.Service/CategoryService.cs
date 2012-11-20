using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;

namespace FBS.Service
{
    public class CategoryService
    {
        #region 文章分类

        /// <summary>
        /// 创建文章分类
        /// </summary>
        /// <param name="model">新建文章分类模型</param>
        public void CreateArticleCategory(NewArticleCategoryModel model)
        {
            IRepository<ArticleCategory> articleRep = Factory.Factory<IRepository<ArticleCategory>>.GetConcrete<ArticleCategory>();

            try
            {
                if (model.ParentID.Equals(Guid.Empty)||model.Deepth==1)
                    articleRep.Add(new ArticleCategory(model.CategoryName, model.Description, model.Icon, model.Priority));
                else
                    articleRep.Add(new ArticleCategory(model.CategoryName, model.Description, model.Icon, model.Priority, model.Deepth, model.ParentID));

                articleRep.PersistAll();
            }

            catch { }
        }




        /// <summary>
        /// 列出所有文章分类
        /// </summary>
        /// 
        public IList<ArticleCategoryModel> FetchArticleCategory()
        {
            IRepository<ArticleCategory> articleRep = Factory.Factory<IRepository<ArticleCategory>>.GetConcrete<ArticleCategory>();
            IList<ArticleCategoryModel> mylist = new List<ArticleCategoryModel>();
            IList<ArticleCategory> acList = articleRep.FindAll(new Specification<ArticleCategory>(a => a.Id != Guid.Empty).Skip(0).Take(100000).OrderBy(ac => ac.Priority));
            foreach (ArticleCategory myarti in acList)
            {
                mylist.Add(new ArticleCategoryModel() {  CategoryID=myarti.Id, CategoryName=myarti.Name, Deepth=myarti.Deepth, Description=myarti.Description, IconName=myarti.Icon, ParentID=myarti.ParentID, Priority=myarti.Priority}); 
            }
            return mylist;
        }

        /// <summary>
        /// 删除文章分类
        /// </summary>
        /// <param name="cid">编号</param>
        public void DeleteArticleCategory(Guid cid)
        {
            IRepository<ArticleCategory> categoryRep = Factory.Factory<IRepository<ArticleCategory>>.GetConcrete<ArticleCategory>();
            CMSService myservice = new CMSService();

            IList<ArticleCategoryModel> mylist = this.FetchArticleCategory();

            foreach (ArticleCategoryModel model in mylist)
            {
                if (model.ParentID == cid)//还有子分类则删除
                {
                    DeleteArticleCategory(model.CategoryID);
                }
            }
            try
            {
                foreach (ArticleDspModel model in myservice.FetchAllArticleByCategoryID(cid))
                {
                    myservice.RemoveArticleByID(model.ArticleID);
                }
                categoryRep.RemoveByKey(cid);
                categoryRep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 修改文章分类
        /// </summary>
        /// <param name="cid">编号</param>
        public void UpdateArticleCategory( ArticleCategoryDetailsModel model)
        {
            IRepository<ArticleCategory> categoryRep = Factory.Factory<IRepository<ArticleCategory>>.GetConcrete<ArticleCategory>();

            try
            {
                ArticleCategory mymodel = categoryRep.GetByKey(model.CategoryID);
                mymodel.Deepth = model.Deepth;
                mymodel.Description = model.Description;
                mymodel.Icon = model.Icon;
               
                mymodel.Name = model.CategoryName;
                mymodel.ParentID = model.ParentID;
                mymodel.Priority = model.Priority;
                categoryRep.Update(mymodel);

                categoryRep.PersistAll();
            }

            catch { }
        }

        /// <summary>
        /// 按分类名称获取文章信息
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <returns>文章分类信息</returns>
        public ArticleCategoryDetailsModel GetArticleCategoryByName(string name)
        {
            IRepository<ArticleCategory> categoryRep = Factory.Factory<IRepository<ArticleCategory>>.GetConcrete<ArticleCategory>();
            ArticleCategory tmp = null;
            ArticleCategoryDetailsModel target = new ArticleCategoryDetailsModel();
            try
            {
                tmp = categoryRep.Find(new Specification<ArticleCategory>(c => c.Name == name));
                target.CategoryID = tmp.Id;
                target.CategoryName = tmp.Name;
                target.Deepth = tmp.Deepth;
                target.Description = tmp.Description;
                target.Icon = tmp.Icon;
                target.ParentID = tmp.ParentID;
                target.Priority = tmp.Priority;
            }
            catch 
            {
            }

            return target;
        }


        public ArticleCategoryDetailsModel GetArticleCategoryById(Guid CategoryID)
        {
            IRepository<ArticleCategory> categoryRep = Factory.Factory<IRepository<ArticleCategory>>.GetConcrete<ArticleCategory>();
            ArticleCategory tmp = null;
            ArticleCategoryDetailsModel target = null;
            try
            {
                //tmp = categoryRep.Find(new Specification<ArticleCategory>(c => c.Name == name));
                target = new ArticleCategoryDetailsModel();

                tmp = categoryRep.GetByKey(CategoryID);

                if (tmp == null)
                    return null;
                target.CategoryName = tmp.Name;
                target.Deepth = tmp.Deepth;
                target.Description = tmp.Description;
                target.Icon = tmp.Icon;
                target.ParentID = tmp.ParentID;
                target.Priority = tmp.Priority;
                target.CategoryID = CategoryID;
            }
            catch
            {
                return target;
            }

            return target;
        }


        #endregion

        #region 问答分类

        /// <summary>
        /// 添加问答分类
        /// </summary>
        /// <param name="model">新建问答分类模型</param>
        public void CreateQuestionCategory(NewQuestionCategoryModel model)
        {
            IRepository<BlogQuestionCategory> categoryRep = Factory.Factory<IRepository<BlogQuestionCategory>>.GetConcrete<BlogQuestionCategory>();

            try
            {
                categoryRep.Add(new BlogQuestionCategory(model.CategoryName, model.Description, model.Icon, model.Priority));
                categoryRep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 删除问答分类
        /// </summary>
        /// <param name="cid">分类编号</param>
        public void DeleteQuestionCategory(Guid cid)
        {
            IRepository<BlogQuestionCategory> categoryRep = Factory.Factory<IRepository<BlogQuestionCategory>>.GetConcrete<BlogQuestionCategory>();
            try
            {
                categoryRep.RemoveByKey(cid);
                categoryRep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 获取所有问题分类
        /// </summary>
        ///
        public IList<BlogQuestionCategoryModel> FetchQuestionCategory()
        {
            IRepository<BlogQuestionCategory> storyCategoryRep = Factory.Factory<IRepository<BlogQuestionCategory>>.GetConcrete<BlogQuestionCategory>();
            IList<BlogQuestionCategoryModel> mylist = new List<BlogQuestionCategoryModel>();
            foreach (BlogQuestionCategory model in storyCategoryRep.FindAll())
            {
                mylist.Add(new BlogQuestionCategoryModel() { CategoryName=model.CategoryName, Description=model.Description, IconName=model.Icon, OrderPriority=model.Priority, QuestionCategory=model.QuestionCategory });
            }
            return mylist;
        }

        public BlogQuestionCategoryModel GetBlogQuestionCategoryModelContentByID(Guid aid)
        {
            IRepository<BlogQuestionCategory> storyCategoryRep = Factory.Factory<IRepository<BlogQuestionCategory>>.GetConcrete<BlogQuestionCategory>();
            BlogQuestionCategory model = storyCategoryRep.GetByKey(aid);
            return new BlogQuestionCategoryModel() { CategoryName=model.CategoryName, QuestionCategory=model.QuestionCategory,  Description=model.Description, IconName=model.Icon, OrderPriority=model.Priority};
        }

        public void UpdateBlogQuestionCategory(BlogQuestionCategoryModel model)
        {
            IRepository<BlogQuestionCategory> storyCategoryRep = Factory.Factory<IRepository<BlogQuestionCategory>>.GetConcrete<BlogQuestionCategory>();

            try
            {
                BlogQuestionCategory mymodel = storyCategoryRep.GetByKey(model.QuestionCategory);
                mymodel.CategoryName = model.CategoryName;
                mymodel.Description = model.Description;
                mymodel.Icon = model.IconName;
                
                mymodel.Priority = model.OrderPriority;
                mymodel.QuestionCategory = model.QuestionCategory;
                storyCategoryRep.Update(mymodel);

                storyCategoryRep.PersistAll();
            }

            catch { }
        }


        #endregion

        #region 博文分类

        /// <summary>
        /// 创建博文分类
        /// </summary>
        /// <param name="model">新建博文分类模型</param>
        public void CreateBlogStoryCategory(NewBlogStoryCategoryModel model)
        {
            IRepository<BlogStoryCategory> storyCategoryRep = Factory.Factory<IRepository<BlogStoryCategory>>.GetConcrete<BlogStoryCategory>();

            try
            {
                storyCategoryRep.Add(new BlogStoryCategory(model.CategoryName, model.Description, model.Icon, model.Priority));
                storyCategoryRep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        ///  获取分类名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogCategoryModel GetCategoryNameById(Guid id)
        {
            IRepository<BlogStoryCategory> bRep = Factory.Factory<IRepository<BlogStoryCategory>>.GetConcrete<BlogStoryCategory>();
            BlogStoryCategory bs = bRep.Find(new Specification<BlogStoryCategory>(b => b.Id == id));
            BlogCategoryModel m = new BlogCategoryModel();
            m.BlogCategoryID = bs.Id;
            m.CategoryName = bs.CategoryName;

            return m;

        }


        /// <summary>
        /// 获取博文分类列表
        /// </summary>
        /// <returns>博文分类列表</returns>
        public IList<BlogCategoryModel> GetBlogCategoryList()
        {
            IRepository<BlogStoryCategory> storyRep = Factory.Factory<IRepository<BlogStoryCategory>>.GetConcrete<BlogStoryCategory>();
            int categoryCount = storyRep.FindAll().Count;
            IList<BlogStoryCategory> storys = storyRep.FindAll(new Specification<BlogStoryCategory>(s => s.Id != Guid.Empty).Skip(0).Take(categoryCount).OrderBy(st => st.OrderPriority));
            IList<BlogCategoryModel> targets = new List<BlogCategoryModel>();
            foreach (BlogStoryCategory story in storys)
            {
                BlogCategoryModel tmp = new BlogCategoryModel()
                {
                    BlogCategoryID = story.Id,
                    CategoryName = story.CategoryName,
                    Description = story.Description,
                    IconName = story.IconName,
                    OrderPriority = story.OrderPriority

                };
                targets.Add(tmp);

            }
            return targets;
        }

        /// <summary>
        /// 根据分类获取
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public BlogCategoryModel GetBlogCategoryModelByCategoryName(string CategoryName)
        {
            IList<BlogCategoryModel> mylist = GetBlogCategoryList();
            foreach (BlogCategoryModel model in mylist)
            {
                if (model.CategoryName == CategoryName)
                {
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogCategoryModel GetBlogCategoryModelByCategoryID(Guid id)
        {
            IList<BlogCategoryModel> mylist = GetBlogCategoryList();
            foreach (BlogCategoryModel model in mylist)
            {
                if (model.BlogCategoryID == id)
                {
                    return model;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取所有博文分类
        /// </summary>
        ///
        public IList<BlogCategoryModel> FetchBlogStoryCategory()
        {
            IRepository<BlogStoryCategory> storyCategoryRep = Factory.Factory<IRepository<BlogStoryCategory>>.GetConcrete<BlogStoryCategory>();
            IList<BlogCategoryModel> mylist = new List<BlogCategoryModel>();
            foreach (BlogStoryCategory model in storyCategoryRep.FindAll())
            {
                mylist.Add(new BlogCategoryModel() { BlogCategoryID=model.Id, CategoryName=model.CategoryName, Description=model.Description, OrderPriority=model.OrderPriority, IconName=model.IconName  });
            }
            return mylist;
        }
        /// <summary>
        /// 获取一个博文分类
        /// </summary>
        ///
        public BlogCategoryModel   GetBlogCategoryModelContentByID(Guid aid)
        {
            IRepository<BlogStoryCategory> storyCategoryRep = Factory.Factory<IRepository<BlogStoryCategory>>.GetConcrete<BlogStoryCategory>();
            BlogStoryCategory blogstory= storyCategoryRep.GetByKey(aid);
            return new BlogCategoryModel() { BlogCategoryID=aid, CategoryName=blogstory.CategoryName, IconName=blogstory.IconName, Description=blogstory.Description, OrderPriority=blogstory.OrderPriority };
        }

        /// <summary>
        /// 修改博文分类
        /// </summary>
        ///
        public void UpdateBlogCategoryModelContent(BlogCategoryModel model)
        {
            IRepository<BlogStoryCategory> storyCategoryRep = Factory.Factory<IRepository<BlogStoryCategory>>.GetConcrete<BlogStoryCategory>();
            BlogStoryCategory mymodel= storyCategoryRep.GetByKey(model.BlogCategoryID);
            mymodel.CategoryName = model.CategoryName;
            mymodel.Description = model.Description;
            mymodel.IconName = model.IconName;
            mymodel.OrderPriority = model.OrderPriority;
            storyCategoryRep.Update(mymodel);
            storyCategoryRep.PersistAll();
        }

        /// <summary>
        /// 获取博文分类
        /// </summary>
        /// <param name="name">博客分类名</param>
        /// <returns></returns>
        public BlogCategoryModel GetBlogCategoryModelContentByName(string name)
        {
            
            IRepository<BlogStoryCategory> storyCategoryRep = Factory.Factory<IRepository<BlogStoryCategory>>.GetConcrete<BlogStoryCategory>();
            BlogStoryCategory blogstory = storyCategoryRep.Find(new Specification<BlogStoryCategory>(a => a.CategoryName == name));
            BlogCategoryModel k =new BlogCategoryModel();
            if (blogstory != null)
            {
              k  = new BlogCategoryModel() { BlogCategoryID = blogstory.Id, CategoryName = blogstory.CategoryName, IconName = blogstory.IconName, Description = blogstory.Description, OrderPriority = blogstory.OrderPriority };
            }
            return k;
        }

        /// <summary>
        /// 删除博文分类
        /// </summary>
        /// <param name="cid">分类ID</param>
        public void DeleteBlogStoryCategory(Guid cid)
        {
            IRepository<BlogStoryCategory> storyCategoryRep = Factory.Factory<IRepository<BlogStoryCategory>>.GetConcrete<BlogStoryCategory>();
            BlogService myservice = new BlogService();
            try
            {
                foreach (BlogStoryDspModel mo in myservice.GetAllBlogStorysSummaryOfCategory(cid))
                {
                    myservice.RemoveBlogStory(mo.StoryID);
                }
                storyCategoryRep.RemoveByKey(cid);
                storyCategoryRep.PersistAll();
            }
            catch { }
        }

        #endregion
    }
}
