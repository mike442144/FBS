using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;
using FBS.Domain.Aggregate.ValueObject;
using FBS.Utils;

namespace FBS.Service
{
    public class CMSService
    {
        #region 文章相关

        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="model">新建文章模型</param>
        public void CreateArticle(NewArticleModel model)
        {
            IRepository<Article> rep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();
            Article a = new Article(model.Title,model.BriefTitle, model.Body, model.UserID, model.UserName, model.CategoryID, model.CategoryName, model.SourceUrl, model.SourceSite, model.ImgName);
            try
            {
                rep.Add(a);
                rep.PersistAll();
            }
            catch { }
        }

        public string GetArticleImageTiny(string ImgName)
        {
            PanoramaCutting ir = new PanoramaCutting("~/FLYUpload/Images/", "~/FLYUpload/Images/TinyImages/");
            return ir.GetImage(ImgName, 50, 50);
        }

        /// <summary>
        /// 获取文章详细内容
        /// </summary>
        /// <param name="aid">文章详细模型</param>
        public ArticleDetailsModel GetOneArticleContentByID(Guid aid)
        {
            IRepository<Article> rep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();


            Article article = null;
            ArticleDetailsModel target = null;
            IList<CommentDspModel> comments = null;
            try
            {
                article = rep.GetByKey(aid);
                if (article != null)
                {
                    IList<BlogComment> list = commentRep.FindAll(new Specification<BlogComment>(c => c.TargetId == aid).OrderBy(c => c.CreationDate).Skip(0).Take(10000));
                    if (list != null)
                    {
                        comments = new List<CommentDspModel>();
                        foreach (BlogComment c in list)
                        {
                            CommentDspModel tmp = new CommentDspModel() { AccountID = c.AccountInfo.Id, TargetID = c.TargetId, UserName = c.AccountInfo.UserName, CommentContent = c.Body, CreatedOn = c.CreationDate, CommentID = c.Id };
                            comments.Add(tmp);
                        }
                    }

                    target = new ArticleDetailsModel()
                    {
                        Body = article.ArticleVO.Body,
                        Title = article.ArticleVO.Title,
                        CategoryID = article.CategoryID,
                        CategoryName = article.CategoryName,
                        ClickCount = article.ArticleVO.ClickCount,
                        CommentCount = article.ArticleVO.CommentCount,
                        CreationDate = article.CreationDate,
                        SourceSite = article.ArticleVO.SourceSite,
                        SourceUrl = article.ArticleVO.SourceUrl,
                        UserID = article.UserID,
                        BriefTitle = article.ArticleVO.BriefTitle,
                        UserName=article.UserName,
                        Comments=comments
                    };
                }
                else
                {

                    return target;
                }
                
            }

            catch(Exception error) 
            {
                throw new Exception(error.Message);
            }

            return target;
        }

        /// <summary>
        /// 加点击率
        /// </summary>
        /// <param name="articleId"></param>
        public void AddArticleClickCountByID(Guid articleId)
        {
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();
            Article a=  articleRep.GetByKey(articleId);
            a.AddClickCount();
            articleRep.Update(a);
            articleRep.PersistAll();
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId"></param>
        public void RemoveArticleByID(Guid articleId)
        {
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();

            BlogService myservice = new BlogService();
            IList<CommentDspModel> list = myservice.FetchAllCommentsByTargetID(articleId);
            foreach (CommentDspModel model in list)
            {
                //myservice.DeleteComment(model.CommentID);
                this.DeleteCommentByID(model.CommentID);
            }
            articleRep.RemoveByKey(articleId);
            articleRep.PersistAll();
        }

        /// <summary>
        /// 获取最新文章
        /// </summary>
        /// <param name="model">文章类别名称</param>
        public ArticleDspModel GetLatestArticle(string categoryname)
        {
            ISpecification<Article> spec = null;
            if (string.IsNullOrEmpty(categoryname))
            {
                spec = new Specification<Article>(a => a.Id != Guid.Empty);
            }
            else
            {
                Guid aid = new FBS.Service.CategoryService().GetArticleCategoryByName(categoryname).CategoryID;
                spec = new Specification<Article>(a => a.CategoryID == aid);
            }

            spec = spec.Skip(0).Take(1).OrderByDescending(a => a.CreationDate);

            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();

            Article myarticle = articleRep.Find(spec);
            
            ArticleDspModel mymodel = new ArticleDspModel();
            if(myarticle!=null)
            {
                mymodel = new ArticleDspModel() { BriefTitle=myarticle.ArticleVO.BriefTitle, ArticleID = myarticle.Id, ClickCount = myarticle.ArticleVO.ClickCount, CreatedOn = myarticle.CreationDate, ImgName = myarticle.ImgName, Title = myarticle.ArticleVO.Title , SourceUrl=myarticle.ArticleVO.SourceUrl}; 
            }

            return mymodel;
        }

        /// <summary>
        /// 获取最新图片文章列表
        /// </summary>
        /// <param name="model">文章类别名称</param>
        public IList<ArticleDspModel> GetLatestImgArticle(int count)
        {
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();
            
            IList<ArticleDspModel> mylist = new List<ArticleDspModel>();
            foreach(Article myarticle in articleRep.FindAll(new Specification<Article>(p=>p.ImgName!=string.Empty).Skip(0).Take(count).OrderByDescending(a => a.CreationDate)))
            {
                mylist.Add(new ArticleDspModel() { BriefTitle=myarticle.ArticleVO.BriefTitle, ArticleID = myarticle.Id, ClickCount = myarticle.ArticleVO.ClickCount, CreatedOn = myarticle.CreationDate, ImgName = myarticle.ImgName, Title = myarticle.ArticleVO.Title });
            }
            return mylist;
        }
        /// <summary>
        /// 获取指定分类下的最新的图片新闻
        /// </summary>
        /// <param name="CategoryName">分类名称</param>
        /// <param name="count">取得数目</param>
        /// <returns></returns>
        public IList<ArticleDspModel> GetLatestImgArticleByCategoryName(string CategoryName,int count)
        {
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();

            IList<ArticleDspModel> mylist = new List<ArticleDspModel>();
            foreach (Article myarticle in articleRep.FindAll(new Specification<Article>(p => p.ImgName != string.Empty&&p.CategoryName==CategoryName).Skip(0).Take(count).OrderByDescending(a => a.CreationDate)))
            {
                mylist.Add(new ArticleDspModel() { BriefTitle=myarticle.ArticleVO.BriefTitle, ArticleID = myarticle.Id, ClickCount = myarticle.ArticleVO.ClickCount, CreatedOn = myarticle.CreationDate, ImgName = myarticle.ImgName, Title = myarticle.ArticleVO.Title });
            }
            return mylist;
        }
        /// <summary>
        /// 修改文章
        /// </summary>
        /// <param name="model">修改文章模型</param>
        public void UpdateArticle(ModifiedArticleModel model)
        {
            IRepository<Article> rep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();

            Article a = null;
            try
            {
                a = rep.GetByKey(model.ArticleID);
                a.ArticleVO = new ArticleVO(model.Title,model.BriefTitle,model.Body,a.ArticleVO.ClickCount,a.ArticleVO.CommentCount,model.SourceUrl,model.SourceSite);
                a.CategoryID = model.CategoryID;
                a.CategoryName = model.CategoryName;
                a.ImgName = model.ImgName;
                a.CreationDate = DateTime.Now;
                rep.Update(a);
                rep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 获取某分类下的文章列表模型
        /// </summary>
        /// <param name="cid">分类编号</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">获取数目</param>
        /// <returns>文章模型列表</returns>
        public IList<ArticleDspModel> FetchArticleByCategoryID(Guid cid,int startIndex,int count)
        {
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();
            IList<Article> alist = null;
            IList<ArticleDspModel> target = new List<ArticleDspModel>();
            try
            {
                alist = articleRep.FindAll(new Specification<Article>(a => a.CategoryID == cid).Skip(startIndex).Take(count).OrderByDescending(a=>a.CreationDate));
                foreach (Article a in alist)
                {
                    ArticleDspModel tmp = new ArticleDspModel() { SourceUrl=a.ArticleVO.SourceUrl, BriefTitle=a.ArticleVO.BriefTitle, ArticleID = a.Id, Title = a.ArticleVO.Title, ClickCount=a.ArticleVO.ClickCount, CreatedOn=a.CreationDate, ImgName=a.ImgName};
                    target.Add(tmp);
                }
            }
            catch { }

            return target;
        }


        public IList<ArticleDspModel> FetchAllArticleByCategoryID(Guid cid)
        {
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();
            IList<Article> alist = null;
            IList<ArticleDspModel> target = new List<ArticleDspModel>();
            try
            {
                alist = articleRep.FindAll(new Specification<Article>(a => a.CategoryID == cid));
                foreach (Article a in alist)
                {
                    ArticleDspModel tmp = new ArticleDspModel() { BriefTitle = a.ArticleVO.BriefTitle, ArticleID = a.Id, Title = a.ArticleVO.Title, ClickCount = a.ArticleVO.ClickCount, CreatedOn = a.CreationDate, ImgName = a.ImgName };
                    target.Add(tmp);
                }
            }
            catch { }

            return target;
        }

        /// <summary>
        /// 分享新闻
        /// </summary>
        /// <param name="model"></param>
        public void ShareOneNews(SiteShareModel model)
        {
            IRepository<Feed> feedRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();

            Feed feed = feedRep.GetByKey(model.FeedID);

            string subject = feed.Subject;
            subject = subject.Substring(subject.LastIndexOf("<a"));


            NewFeedModel newFeed = new NewFeedModel() { SourceUser = model.SourceUser, Sharer = model.Sharer, Subject = subject, Content = feed.Content, Type = FeedType.ShareStory };

            new BlogService().CreateFeed(newFeed);
        }

        /// <summary>
        /// 获取新闻分类
        /// </summary>
        /// <returns> 新闻分类列表</returns>
        public IList<ArticleCategoryModel> FetchArticleCategory()
        {
            IRepository<ArticleCategory> articleRep = Factory.Factory<IRepository<ArticleCategory>>.GetConcrete<ArticleCategory>();
            IList<ArticleCategory> alist =articleRep.FindAll();
           IList<ArticleCategoryModel> target = new List<ArticleCategoryModel>();

            foreach (ArticleCategory story in alist)
            {
                ArticleCategoryModel temp = new ArticleCategoryModel()
                {
                    CategoryID = story.Id,
                    CategoryName = story.Name,
                    IconName = story.Icon, 
                    Deepth = story.Deepth, 
                    Description = story.Description, 
                    ParentID = story.ParentID, 
                    Priority = story.Priority
                };
                target.Add(temp);
 
            }
            return target;
        }


        /// <summary>
        /// 获取分类下的文章总数
        /// </summary>
        /// <param name="cid">分类编号,若为Guid.Empty则对所有分类计数</param>
        /// <returns>文章数</returns>
        public int GetArticleCountOfCategory(Guid cid)
        {
            ISpecification<Article> spec = null;
            if (Guid.Empty.Equals(cid))
                spec = new Specification<Article>(a => a.Id != Guid.Empty);
            else
                spec = new Specification<Article>(a => a.CategoryID == cid);

            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();
            int target = 0;
                
            //target = articleRep.Count(new Specification<Article>(a => a.CategoryID == cid));
            target = articleRep.Count(spec);

            return target;
        }


        #endregion

        #region 评论
        /// <summary>
        /// 给文章添加评论
        /// </summary>
        /// <param name="model">评论模型</param>
        public void AddCommentToArticle(CommentModel model)
        {
            IRepository<BlogComment> rep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();

            BlogComment comment = new BlogComment(model.TargetID, model.AccountID, model.UserName, model.CommentContent);
            rep.Add(comment);
            rep.PersistAll();

            Article article = articleRep.GetByKey(model.TargetID);
            article.AddCommentCount();
            articleRep.Update(article);
            articleRep.PersistAll();
        }

        /// <summary>
        /// 删除文章评论
        /// </summary>
        /// <param name="commentId">评论编号</param>
        public void DeleteCommentByID(Guid commentId)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();

            BlogComment comment = commentRep.GetByKey(commentId);
            Article article = articleRep.GetByKey(comment.TargetId);

            commentRep.Remove(comment);
            commentRep.PersistAll();

            article.ReduceCommentCount();
            articleRep.Update(article);
            articleRep.PersistAll();
        }


        /// <summary>
        /// 通过分类名称获取分类模型
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <returns></returns>
        public ArticleCategoryModel GetArticleCategoryModelByCategoryName(string name)
        {
            IRepository<ArticleCategory> articleRep = Factory.Factory<IRepository<ArticleCategory>>.GetConcrete<ArticleCategory>();
            ArticleCategory ac= articleRep.Find(new Specification<ArticleCategory>(a => a.Name == name));
            ArticleCategoryModel model=new ArticleCategoryModel();
            if (ac != null)
            {
               model  = new ArticleCategoryModel() { CategoryID = ac.Id, CategoryName = ac.Name, Deepth = ac.Deepth, Description = ac.Description, ParentID = ac.ParentID, IconName = ac.Icon, Priority = ac.Priority };
            }
            return model;
        }

       /// <summary>
       /// 获取最新文章
       /// </summary>
       /// <param name="index">最新文章的起始点</param>
       /// <param name="count">获取的总条数</param>
       /// <returns></returns>
        public IList<ArticleDspModel> GetLatestArticles(int index, int count)
        {
            IRepository<Article> articleRep = Factory.Factory<IRepository<Article>>.GetConcrete<Article>();
            IList<Article> list = articleRep.FindAll(new Specification<Article>(aa=>aa.Id!=Guid.Empty).OrderByDescending(a => a.ClickCount).Skip(index).Take(count));
            IList<ArticleDspModel> mylist = new List<ArticleDspModel>();
            foreach(Article a in list)
            {
                ArticleDspModel model = new ArticleDspModel() { ArticleID=a.Id, BriefTitle=a.ArticleVO.BriefTitle, ImgName=a.ImgName, Title=a.ArticleVO.Title, ClickCount=a.ArticleVO.ClickCount, CreatedOn=a.CreationDate };
                mylist.Add(model);
            }
            return mylist;
        }
        #endregion
    }
}
