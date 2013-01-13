using System;
using System.Collections.Generic;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;
using FBS.Domain.Specifications;
using FBS.Service.ActionModels;
using FBS.Utils;
using System.Web;
using System.Text.RegularExpressions;
using System.Linq;
using System.Configuration;
using FBS.Domain.Log;

namespace FBS.Service
{
    [Logging()]
    public class BlogService:ContextBoundObject
    {
        #region 博文操作

        /// <summary>
        /// 删除博文
        /// </summary>
        /// <param name="id">博文编号</param>
        public void RemoveBlogStory(Guid id )
        {
            IBlogStoryRepository storyRep = Factory.Factory<IBlogStoryRepository>.GetConcrete();

            IList<CommentDspModel> list = this.FetchAllCommentsByTargetID(id);
            foreach(CommentDspModel model in list)
            {
                this.DeleteComment(model.CommentID);
            }
            storyRep.RemoveByKey(id);
            storyRep.PersistAll();
        }
        /// <summary>
        /// 修改博文
        /// </summary>
        /// <param name="model"></param>
        public void UpdateBlogArticle(BlogStoryDetailsModel model)
        {
            IBlogStoryRepository storyRep = Factory.Factory<IBlogStoryRepository>.GetConcrete();
            BlogStory b = storyRep.GetByKey(model.StoryID);
            //if (model.ImgName != "")
            //{
            //    string subDir = System.DateTime.Now.ToString("yyyyMMdd");

            //    //生成两种尺寸头像
            //    ImageResizer ir = new ImageResizer("~/FLYUpload/Images/" + subDir + "/", "~/FLYUpload/Images/" + subDir + "/");
            //    string str = model.ImgName;
            //    string ss = str.Substring(str.LastIndexOf("/") + 1);
            //    string head = ir.GetImage(ss, 110, 90);




            //    model.ImgName = subDir + "/" + head;

            //}
            if (b != null)
            {
                b.Description = model.Content;
                b.ImgName = model.ImgName;
                b.CategoryID = model.CategoryID;
                b.CategoryName = model.CategoryName;
                //b.UserID = model.AccountID;
                b.Title = model.Title;
                b.CreationDate = DateTime.Now;
                storyRep.Update(b);
                storyRep.PersistAll();
            }

         
 
        }


        /// <summary>
        /// 创建一篇博文
        /// </summary>
        /// <param name="model"></param>
        public void CreateBlogStory(NewBlogStoryModel model)
        {
            IBlogStoryRepository storyRep = Factory.Factory<IBlogStoryRepository>.GetConcrete();
            
            BlogStory story = null;
            //if (model.ImgName != "")
            //{
            //    string subDir = System.DateTime.Now.ToString("yyyyMMdd");

            //    //生成两种尺寸头像
            //    ImageResizer ir = new ImageResizer("~/FLYUpload/Images/" + subDir + "/", "~/FLYUpload/Images/" + subDir + "/");
            //    string str = model.ImgName;
            //     string ss  = str.Substring(str.LastIndexOf("/")+1);
            //    string head = ir.GetImage(ss, 110, 90);
               
                

                
            //    model.ImgName = subDir + "/" + head;
               
            //}
            //新建实例
            story = new BlogStory(model.CategoryID, model.CategoryName, model.AccountID, model.UserName,model.UserTiny, model.Title, model.Body, model.IsPublishedToHomepage,model.ImgName);

            //文章添加到仓储
            storyRep.Add(story);

            //持久化
            storyRep.PersistAll();

            //新鲜事模型
            NewFeedModel feedModel = new NewFeedModel() { Sharer = new UserStateModel() { UserID=model.AccountID,UserName=model.UserName,UserTiny=model.UserTiny }, Subject = model.Title, Content = model.Body, TargetID = story.Id, Type = FeedType.NewStory };
            //添加到新鲜事
            this.CreateFeed(feedModel);
        }
        
        /// <summary>
        /// 获得指定分类的博客
        /// </summary>
        /// <param name="categoryname">分类名</param>
        /// <returns></returns>
        public BlogStoryDspModel GetLeastBlogStoryContentByCategoryName(string categoryname)
        {
            CategoryService myservice = new CategoryService();
            IList<BlogCategoryModel> list = myservice.FetchBlogStoryCategory();
            Guid BlogCategoryID = Guid.Empty;
            foreach (BlogCategoryModel model in list)
            {
                if (model.CategoryName == categoryname)
                {
                    BlogCategoryID = model.BlogCategoryID;
                    break;
                }
            }
            BlogStoryDspModel model1 = null;
            IList<BlogStoryDspModel> k = new BlogService().GetAllBlogStorysSummaryOfCategory(BlogCategoryID);
            if (k.Count>0)
            {
                model1 = k[0];
            }
            return model1;
        }
        /// <summary>
        /// 获得一篇博文的详细信息
        /// </summary>
        /// <param name="storyId">博文编号</param>
        /// <returns>博文详细模型</returns>
        public BlogStoryDetailsModel GetOneBlogStoryContentByID(Guid storyId)
        {
            //IBlogStoryRepository storyRep = Factory.Factory<IBlogStoryRepository>.GetConcrete();
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            CategoryService cateservice = new CategoryService();
            BlogStoryDetailsModel target = null;
            BlogStory story = storyRep.GetByKey(storyId);
            if (story != null)
            {
                string name = cateservice.GetCategoryNameById(story.CategoryID);
                target = new BlogStoryDetailsModel() { CategoryName = name, CategoryID = story.CategoryID, StoryID = story.Id, AccountID = story.UserID, WriterName = story.UserName, Title = story.State.Title, Content = story.State.Description, PublishTime = story.CreationDate, ReadCount = story.State.ClickCount, CommentsCount = story.State.CommentCount };
                target.Comments = new List<CommentDspModel>();
                int commentCount = commentRep.FindAll(new Specification<BlogComment>(c => c.TargetId == storyId)).Count;
                foreach (BlogComment c in commentRep.FindAll(new Specification<BlogComment>(c => c.TargetId == storyId).Skip(0).Take(commentCount).OrderBy(c => c.CreationDate)))
                {
                    CommentDspModel tmp = new CommentDspModel() { CreatedOn = c.CreationDate, CommentID = c.Id, AccountID = c.AccountInfo.Id, UserName = c.AccountInfo.UserName, CommentContent = c.Body, TargetID = c.TargetId };
                    target.Comments.Add(tmp);
                }

                //博文增加点击数
                story.AddClickCount();

                //更新博文
                storyRep.Update(story);

                //持久化
                storyRep.PersistAll();

            }
            return target;
        }

        /// <summary>
        /// 获取指定用户的博文
        /// </summary>
        /// <param name="userId">用户编号</param>
        public IList<BlogStoryDspModel> GetAllBlogStorysSummaryOfUser(Guid userId, int startIndex, int count)
        {
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();

            IList<BlogStory> blist = storyRep.FindAll(new Specification<BlogStory>(s => s.UserID == userId).Skip(startIndex).Take(count).OrderByDescending(b => b.CreationDate));
            IList<BlogStoryDspModel> targets = new List<BlogStoryDspModel>();

            foreach (BlogStory story in blist)
            {
                BlogStoryDspModel tmp = new BlogStoryDspModel() { UserID=userId, StoryID=story.Id, WriterName = story.UserName, Title = story.State.Title, CommentsCount = story.State.CommentCount, ReadCount = story.State.ClickCount, Description = story.State.GetShortBody(50), PublishTime = story.CreationDate};
                targets.Add(tmp);
            }

            return targets;
        }
        /// <summary>
        /// 某个用户博文的数量
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public int GetCountOfStorysByUserID(Guid userid)
        {
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            int i = storyRep.FindAll(new Specification<BlogStory>(s => s.UserID == userid)).Count;
            return i;
 
        }

        /// <summary>
        /// 获取最新的博文通过博文分类名称
        /// </summary>
        /// <param name="categoryname"></param>
        /// <returns></returns>
        public BlogStoryDspModel GetLatesetStoryByCategoryName(string categoryname)
        {
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            CategoryService cateservice = new CategoryService();
            BlogCategoryModel model = cateservice.GetBlogCategoryModelByCategoryName(categoryname);
            Guid categoryId=Guid.Empty;
            if(model!=null)
            {
                categoryId = model.BlogCategoryID;
            }
            IList<BlogStory> list = storyRep.FindAll(new Specification<BlogStory>(s => s.CategoryID == categoryId).Skip(0).Take(1).OrderByDescending(k=>k.CreationDate));
            BlogStoryDspModel tmp = null;
           if(list.Count>0)
           {
            BlogStory story = list[0];
            tmp = new BlogStoryDspModel()
            {
                WriterName = story.UserName,
                Title = story.State.Title,
                CommentsCount = story.State.CommentCount,
                ReadCount = story.State.ClickCount,
                Description = story.State.GetShortBody(50),
                PublishTime = story.CreationDate,
                StoryID = story.Id,
                UserID = story.UserID
            };
           }

            return tmp;
        }
        /// <summary>
        /// 获取某个分类的博文列表
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        public IList<BlogStoryDspModel> GetAllBlogStorysSummaryOfCategory(Guid categoryId)
        {
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();

            IList<BlogStory> storys = storyRep.FindAll(new Specification<BlogStory>(s => s.CategoryID == categoryId));
            IList<BlogStoryDspModel> targets = new List<BlogStoryDspModel>();

            foreach (BlogStory story in storys)
            {
                BlogStoryDspModel tmp = new BlogStoryDspModel()
                {
                    WriterName = story.UserName,
                    Title = story.State.Title,
                    CommentsCount = story.State.CommentCount,
                    ReadCount = story.State.ClickCount,
                    Description = story.State.GetShortBody(50),
                    PublishTime = story.CreationDate,
                     StoryID=story.Id,
                    UserID = story.UserID
                };
                targets.Add(tmp);
            }

            return targets;
        }
        /// <summary>
        /// (分页)获取某个分类的博文列表
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        public IList<BlogStoryDspModel> GetAllBlogStorysByCategory(Guid categoryId,int startIndex,int count)
        {
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();

            IList<BlogStory> storys = storyRep.FindAll(new Specification<BlogStory>(s => s.CategoryID == categoryId).Skip(startIndex).Take(count).OrderByDescending(bs=>bs.CreationDate));
            IList<BlogStoryDspModel> targets = new List<BlogStoryDspModel>();

            foreach (BlogStory story in storys)
            {
                BlogStoryDspModel tmp = new BlogStoryDspModel()
                {
                    WriterName = story.UserName,
                    Title = story.State.Title,
                    CommentsCount = story.State.CommentCount,
                    ReadCount = story.State.ClickCount,
                    Description = story.State.GetShortBody(200),
                    PublishTime = story.CreationDate,
                    StoryID = story.Id,
                     UserID = story.UserID, ImgName=story.ImgName

                };
                targets.Add(tmp);
            }

            return targets;
        }


        /// <summary>
        /// 获取某个分类的博文的数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetCountByBlogCategory(Guid id)
        {
            int count = 0;
            IRepository<BlogStory> sRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            count = sRep.FindAll(new Specification<BlogStory>( s => s.CategoryID == id)).Count;
            return count;
        }
        
        /// <summary>
        /// 分享新鲜事（博文）
        /// </summary>
        /// <param name="model">站内分享模型</param>
        public void ShareOneBlogStory(SiteShareModel model)
        {
            IRepository<Feed> feedRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();

            Feed feed = feedRep.GetByKey(model.FeedID);

            string subject = feed.Subject;
            subject = subject.Substring(subject.LastIndexOf("<a"));


            NewFeedModel newFeed = new NewFeedModel() { SourceUser = model.SourceUser, Sharer = model.Sharer,Subject=subject,Content=feed.Content,Type=FeedType.ShareStory };

            this.CreateFeed(newFeed);
        }
        /// <summary>
        /// 分享博文
        /// </summary>
        /// <param name="model">站内分享模型</param>
        public void ShareOneNotFeedsBlogStory(BlogStoryDetailsModel p,SiteShareModel model,Guid targetid )
        {
            NewFeedModel newFeed = new NewFeedModel() { TargetID=targetid, SourceUser = model.SourceUser, Sharer = model.Sharer, Subject = p.Title, Content = p.Content, Type = FeedType.ShareBlogDetails};
            this.CreateFeed(newFeed);
        }

       
        /// <summary>
        /// 获取最近发表的N篇博文简介
        /// </summary>
        /// <param name="n">获取数量</param>
        /// <returns></returns>
        public IList<BlogStoryDspModel> GetRecentBlogStorysSummary(int n)
        {
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();

            IList<BlogStory> storys = storyRep.FindAll(new Specification<BlogStory>(post => post.Id != Guid.Empty).Take(n).OrderByDescending(p => p.CreationDate));
            IList<BlogStoryDspModel> targets = new List<BlogStoryDspModel>();

            foreach (BlogStory story in storys)
            {
                BlogStoryDspModel tmp = new BlogStoryDspModel()
                {
                    WriterName = story.UserName,
                    Title = story.State.Title,
                    CommentsCount = story.State.CommentCount,
                    ReadCount = story.State.ClickCount,
                    Description = story.State.GetShortBody(200),
                    PublishTime = story.CreationDate,
                    StoryID = story.Id,
                    UserID = story.UserID
                };
                targets.Add(tmp);
            }

            return targets;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<BlogStoryDspModel> GetHighClickStrorys(int index, int count)
        {
            IRepository<BlogStory> bsRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            IList<BlogStory> list = new List<BlogStory>();
            IList<BlogStoryDspModel> temp = new List<BlogStoryDspModel>();

            int clickCount = Convert.ToInt32(ConfigurationManager.AppSettings["StoryNumOnIndex"]);

            list = bsRep.FindAll(new Specification<BlogStory>(b => b.ClickCount > clickCount).OrderByDescending(a => a.CreationDate).Skip(index).Take(count));
            foreach (BlogStory b in list)
            {
                BlogStoryDspModel model = new BlogStoryDspModel();
                model.StoryID = b.Id;
                model.Title = b.State.Title;
                model.Description = b.State.Description;
                model.CommentsCount = b.State.CommentCount;
                model.PublishTime = b.CreationDate;
                model.ReadCount = b.ClickCount;
                model.WriterName = b.UserName;
                model.UserID = b.UserID;
                temp.Add(model);

            }
            return temp;

        }

        public IList<BlogStoryDspModel> GetNewHotBlogStory(int index, int count)
        {
            IRepository<BlogStory> bsRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            IList<BlogStory> list = null;
            IList<BlogStory> list2 = null;
            IOrderedEnumerable<BlogStory> tmpList = null;
            IOrderedEnumerable<BlogStory> tmpList2 = null;
            DateTime k = DateTime.Now.AddDays(-7);
            ///原创文章
            ///
            CategoryService cateservice = new CategoryService();
            BlogCategoryModel cmodel1 = cateservice.GetBlogCategoryModelByCategoryName("原创文章");
            if (cmodel1 != null)
            {
                list = bsRep.FindAll(new Specification<BlogStory>(b => b.CreationDate > k && b.CategoryID == cmodel1.BlogCategoryID).OrderByDescending(a => a.CreationDate).Skip(index).Take(count));
                tmpList = list.OrderByDescending(b => b.ClickCount);
            }

            int shenyu = count - tmpList.Count();
            ///看我发明
            ///
            BlogCategoryModel cmodel2 = cateservice.GetBlogCategoryModelByCategoryName("看我发明");
            if (cmodel2 != null)
            {
                list2 = bsRep.FindAll(new Specification<BlogStory>(b => b.CreationDate > k && b.CategoryID == cmodel2.BlogCategoryID).OrderByDescending(a => a.CreationDate).Skip(index).Take(shenyu));

                tmpList2 = list2.OrderByDescending(b => b.ClickCount);
            }
            IList<BlogStoryDspModel> target = new List<BlogStoryDspModel>();

            foreach (BlogStory b in tmpList)
            {
                BlogStoryDspModel model = new BlogStoryDspModel();
                model.StoryID = b.Id;
                model.Title = b.State.Title;
                model.Description = b.State.Description;
                model.CommentsCount = b.State.CommentCount;
                model.PublishTime = b.CreationDate;
                model.ReadCount = b.ClickCount;
                model.WriterName = b.UserName;
                model.UserID = b.UserID;
                model.ImgName = b.ImgName;
                target.Add(model);
            }

            foreach (BlogStory b in tmpList2)
            {
                BlogStoryDspModel model = new BlogStoryDspModel();
                model.StoryID = b.Id;
                model.Title = b.State.Title;
                model.Description = b.State.Description;
                model.CommentsCount = b.State.CommentCount;
                model.PublishTime = b.CreationDate;
                model.ReadCount = b.ClickCount;
                model.WriterName = b.UserName;
                model.UserID = b.UserID;
                model.ImgName = b.ImgName;
                target.Add(model);
            }
            return target;
        }


        //获得所有精华博文简介
        public void GetAllDigestBlogStorysSummary()
        {


        }

        //获取所有新闻博文简介
        public void GetAllNewsBlogStorysSummary()
        {
        }

        //获取关注博客
        public void GetFollowedBlogStorySummary()
        {
        }

        //获取观赏体验区的博文
        public IList<BlogStoryDspModel> GetBlogStroyUseInViewing(int index, int count)
        {
            IRepository<BlogStory> bsRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            IList<BlogStory> list = new List<BlogStory>();
            CategoryService cateservice = new CategoryService();
            BlogCategoryModel catemodel = cateservice.GetBlogCategoryModelByCategoryName("原创文章");
            IList<BlogStoryDspModel> temp = new List<BlogStoryDspModel>();
            if (catemodel != null)
            {

                list = bsRep.FindAll(new Specification<BlogStory>(b => b.CategoryID == catemodel.BlogCategoryID).OrderByDescending(a => a.CreationDate).Skip(index).Take(count));
                foreach (BlogStory b in list)
                {
                    BlogStoryDspModel model = new BlogStoryDspModel();
                    model.StoryID = b.Id;
                    model.Title = b.State.Title;
                    model.Description = b.State.Description;
                    model.CommentsCount = b.State.CommentCount;
                    model.PublishTime = b.CreationDate;
                    model.ReadCount = b.ClickCount;
                    model.WriterName = b.UserName;
                    model.UserID = b.UserID;
                    model.WriterName = b.UserName;
                    model.ImageName = b.ImgName;
                    temp.Add(model);
                }
            }
            return temp;
        }

        /// <summary>
        /// 获取最新图片博文
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<BlogStoryDspModel> GetLastestImgBlogStoryList(int count)
        {
            IRepository<BlogStory> bsRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            IList<BlogStory> list = new List<BlogStory>();
            list = bsRep.FindAll(new Specification<BlogStory>(b => b.ImgName != string.Empty).OrderByDescending(a => a.CreationDate).Skip(0).Take(count));
            IList<BlogStoryDspModel> mlist = new List<BlogStoryDspModel>();
            foreach (BlogStory b in list)
            {
                BlogStoryDspModel model = new BlogStoryDspModel() { StoryID = b.Id, ImgName = b.ImgName, Title = b.Title };
            }
            return mlist;
        }



        public BlogStoryDspModel GetViewingImgStory(int index)
        {
            //BlogCategoryModel cate1 = GetBlogCategoryModelByCategoryName("原创文章");
            IRepository<BlogStory> bsRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            IList<BlogStory> list = new List<BlogStory>();
            list = bsRep.FindAll(new Specification<BlogStory>(b => b.ImgName != string.Empty).OrderByDescending(a => a.CreationDate).Skip(index).Take(1));
            BlogStoryDspModel mo = null;
            foreach (BlogStory st in list)
            {
                if (!string.IsNullOrEmpty(st.ImgName))
                {
                    mo = new BlogStoryDspModel() { ImgName = st.ImgName, Title = st.Title, StoryID = st.Id, UserID = st.UserID };
                    return mo;
                }
            }
            return null;
        }


        #endregion

        #region 评论
        /// <summary>
        /// 对博文进行评论
        /// </summary>
        /// <param name="model"></param>
        public void AddCommentToBlogStory(CommentModel model)
        {
            //IBlogStoryRepository storyRep = Factory.Factory<IBlogStoryRepository>.GetConcrete();
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();

            BlogStory story = storyRep.GetByKey(model.TargetID);
            BlogComment comment=null;

            try
            {
                comment = new BlogComment(model.TargetID, model.AccountID, model.UserName, model.CommentContent);
                
                //回复加入仓库
                commentRep.Add(comment);

                //增加回复数目
                story.AddCommentCount();

                //更新博文
                storyRep.Update(story);

                //持久化
                storyRep.PersistAll();

                commentRep.PersistAll();
            }
            catch (Exception error) 
            {
                throw new Exception("评论失败，原因为：" + error.Message);
            }

            //是否在service层进行权限判定？？
            //为文章添加一个评论
            //对评论用户进行积分处理
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="id"></param>
        public void DeleteComment(Guid id)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();

            BlogComment currentComment = commentRep.GetByKey(id);
            BlogStory currentStory = storyRep.GetByKey(currentComment.TargetId);
            currentStory.ReduceCommentCount();

            //删除评论并持久化
            commentRep.Remove(currentComment);
            commentRep.PersistAll();

            //更新博文的评论数并持久化
            storyRep.Update(currentStory);
            storyRep.PersistAll();
        }
        /// <summary>
        /// 给新鲜事评论
        /// </summary>
        /// <param name="model">评论模型</param>
        public CommentDspModel AddCommentToFeedsByFeedsID(CommentModel model)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            BlogComment com = new BlogComment(model.TargetID, model.AccountID, model.UserName, model.CommentContent,model.Head);

            commentRep.Add(com);
            commentRep.PersistAll();

            CommentDspModel target = new CommentDspModel() { AccountID=model.AccountID, CommentContent=model.CommentContent, CommentID=com.Id, CreatedOn=model.CreatedOn, Head=model.Head, TargetID=model.TargetID, UserName=model.UserName };
            return target;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Targetid"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<CommentDspModel> FetchCommentsByTargetID(Guid Targetid, int index, int count)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IList<BlogComment> klist = commentRep.FindAll(new Specification<BlogComment>(p => p.TargetId == Targetid).Skip(index).Take(count).OrderBy(k => k.CreationDate));
            IList<CommentDspModel> mylist = new List<CommentDspModel>();
            foreach (BlogComment com in klist)
            {
                CommentDspModel model = new CommentDspModel() { AccountID = com.AccountInfo.Id, CommentContent = com.Body, CommentID = com.Id, CreatedOn = com.CreationDate, TargetID = com.TargetId, UserName = com.AccountInfo.UserName, Head = com.AccountInfo.Tiny };
                mylist.Add(model);
            }
            return mylist;
        }

        public IList<CommentDspModel> FetchAllCommentsByTargetID(Guid Targetid)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IList<BlogComment> klist = commentRep.FindAll(new Specification<BlogComment>(p => p.TargetId == Targetid));
            IList<CommentDspModel> mylist = new List<CommentDspModel>();
            foreach (BlogComment com in klist)
            {
                CommentDspModel model = new CommentDspModel() { AccountID = com.AccountInfo.Id, CommentContent = com.Body, CommentID = com.Id, CreatedOn = com.CreationDate, TargetID = com.TargetId, UserName = com.AccountInfo.UserName, Head = com.AccountInfo.Tiny };
                mylist.Add(model);
            }
            return mylist;
        }

        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <param name="Feedsid">新鲜事编号</param>
        /// <param name="index">获取的起始位置</param>
        /// <param name="count">获取的条数</param>
        /// <returns></returns>
        public IList<CommentDspModel> FetchCommentsByFeedsID(Guid Feedsid,int index,int count)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IList<BlogComment> klist= commentRep.FindAll(new Specification<BlogComment>(p => p.TargetId == Feedsid).Skip(index).Take(count).OrderBy(k => k.CreationDate));
            IList<CommentDspModel> mylist = new List<CommentDspModel>();
            foreach(BlogComment com in klist)
            {
                CommentDspModel model = new CommentDspModel() { AccountID = com.AccountInfo.Id, CommentContent = com.Body, CommentID = com.Id, CreatedOn = com.CreationDate, TargetID = com.TargetId, UserName = com.AccountInfo.UserName, Head = com.AccountInfo.Tiny };
                mylist.Add(model);
            }
            return mylist;
        }

        /// <summary>
        /// 获取所有新鲜事评论
        /// </summary>
        /// <param name="Feedsid">新鲜事编号</param>
        /// <returns></returns>
        public IList<CommentDspModel> FetchAllCommentsByFeedID(Guid Feedsid)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            int kk=  commentRep.Count(new Specification<BlogComment>(pp => pp.TargetId == Feedsid));
            IList<BlogComment> klist = commentRep.FindAll(new Specification<BlogComment>(p => p.TargetId == Feedsid).Skip(0).Take(kk).OrderBy(k => k.CreationDate));
            IList<CommentDspModel> mylist = new List<CommentDspModel>();
            foreach (BlogComment com in klist)
            {
                CommentDspModel model = new CommentDspModel() { AccountID = com.AccountInfo.Id, CommentContent = com.Body, CommentID = com.Id, CreatedOn = com.CreationDate, TargetID = com.TargetId, UserName = com.AccountInfo.UserName, Head = com.AccountInfo.Tiny};
                mylist.Add(model);
            }
            return mylist;
        }

        
        
        /// <summary>
        /// 通过评论编号获取评论内容
        /// </summary>
        /// <param name="commnetid">评论编号</param>
        /// <returns></returns>
        public CommentDspModel GetOneCommentDspModelByID(Guid commnetid)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            BlogComment com = commentRep.GetByKey(commnetid);
            CommentDspModel model = new CommentDspModel() { AccountID = com.AccountInfo.Id, CommentContent = com.Body, CommentID = com.Id, CreatedOn = com.CreationDate, TargetID = com.TargetId, UserName = com.AccountInfo.UserName };
            return model;
        }
        /// <summary>
        /// 通过评论编号删除评论
        /// </summary>
        /// <param name="id">评论编号</param>
        public void DeleteFeedComment(Guid id)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IRepository<BlogStory> storyRep = Factory.Factory<IRepository<BlogStory>>.GetConcrete<BlogStory>();

            BlogComment currentComment = commentRep.GetByKey(id);
            //减少评论数
            this.ReduceCommentCount(currentComment.TargetId);
            //删除评论并持久化
            commentRep.Remove(currentComment);
            commentRep.PersistAll();
        }

        #endregion

        #region 短消息

        /// <summary>
        /// 发送短消息
        /// </summary>
        /// <param name="model">发送短消息模型</param>
        public void SendShortMessage(SendShortMessageModel model)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();

            try
            {
                //新建实例
                ShortMessage newSM = new ShortMessage(model.Sender.UserID, model.Sender.UserName, model.Sender.Head, model.Reciver.UserID, model.Reciver.UserName, model.Reciver.Head, model.Title, model.Body);
                
                //添加到仓库
                smRep.Add(newSM);

                //待久化
                smRep.PersistAll();
            }
            catch { throw; }
        }

        /// <summary>
        /// 通过用户编号获取短消息
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>短消息列表</returns>
        public IList<ShortMessageDspModel> FetchShortMessagesByUserID(Guid uid,int index,int count)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();
            IList<ShortMessageDspModel> targets = new List<ShortMessageDspModel>();
            IList<ShortMessage> tmpList = null;
            try
            {
                //获取所有短消息
                //tmpList = smRep.FindAll(new Specification<ShortMessage>(sm => sm.ReciverID == uid));
                tmpList = smRep.FindAll(new Specification<ShortMessage>(b => b.ReciverID==uid).OrderByDescending(a => a.SentOn).Skip(index).Take(count));
                //转化为服务层调用模型
                foreach (ShortMessage sm in tmpList)
                {
                    ShortMessageDspModel m = new ShortMessageDspModel() { Sender = new UserStateModel() { UserID=sm.SenderID,UserName=sm.SenderName,UserTiny=sm.SenderHead },SentOn=sm.SentOn, Title=sm.Title,Body=sm.Body, ShortMsgID=sm.Id };
                    targets.Add(m);
                }
            }
            catch{}

            return targets;
        }

        /// <summary>
        /// 查看短消息
        /// </summary>
        /// <param name="sm_id">短消息编号</param>
        /// <returns>短消息模型</returns>
        public ShortMessageDspModel ViewShortMessage(Guid sm_id)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();

            ShortMessageDspModel target = null;
            try
            {
                //获取短消息
                ShortMessage tmp = smRep.GetByKey(sm_id);
                if (tmp != null)
                {
                    //转换到ActionModel给服务层使用
                    target = new ShortMessageDspModel() { Sender = new UserStateModel() {UserID=tmp.SenderID,UserName=tmp.SenderName,UserTiny=tmp.SenderHead },SentOn=tmp.SentOn,Title=tmp.Title,Body=tmp.Body, ShortMsgID=sm_id };

                    //标记为已读
                    tmp.SetReadTag();

                    //更新
                    smRep.Update(tmp);
                    //持久化
                    smRep.PersistAll();
                }
            }
            catch { }

            return target;
        }



        /// <summary>
        /// 删除指定短消息
        /// </summary>
        /// <param name="msgid">短消息编号</param>
        public void DeleteShortMessage(Guid aid)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();
            smRep.RemoveByKey(aid);
            smRep.PersistAll();
        }


        /// <summary>
        /// 根据用户编号未读短消息个数
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>未读短消息个数</returns>
        public int GetShortMessagesCountByUserID(Guid uid)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();
            return  smRep.Count(new Specification<ShortMessage>(sm => sm.ReciverID == uid&sm.HasRead==false));
        }


        public int GetAllShortMessagesCountByUserID(Guid uid)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();
            return smRep.Count(new Specification<ShortMessage>(sm => sm.ReciverID == uid));
        }
        
        
        /// <summary>
        /// 修改短消息
        /// </summary>
        /// <param name="model"></param>
        public void UpdateShortMessage(ShortMessageDspModel model,string type)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();
            ShortMessage tmp = smRep.GetByKey(model.ShortMsgID);
            if (type == "accept")
            {
                tmp.Body = "您成功添加" + model.Sender.UserName + "为好友。";
            }
            else
            {
                tmp.Body = "您拒绝了" + model.Sender.UserName + "的好友请求。";
            }
            smRep.Update(tmp);
            smRep.PersistAll();
        }
        #endregion
        
        #region 问答操作

        /// <summary>
        /// 新建问题
        /// </summary>
        /// <param name="model">新建问题模型</param>
        public void CreateQuestion(NewBlogQuestionModel model)
        {
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            BlogQuestion newQues = new BlogQuestion(model.CategoryId, model.CategoryName, model.UserID, model.UserName,model.UserTiny, model.Subject, model.Body,model.RewardPoints);

            Account a = accRep.Find(new Specification<Account>(acc => acc.Id == model.UserID));
            a.ReducePoints(model.RewardPoints);

            //向仓储添加问题
            questionRep.Add(newQues);
            accRep.Update(a);

            //增加点击数 
            //newQues.AddClickCount();

            //更新问题
            //questionRep.Update(newQues);

            //持久化
            questionRep.PersistAll();
            accRep.PersistAll();
            
            
        }

        /// <summary>
        /// 回答问题
        /// </summary>
        /// <param name="model">模型</param>
        public void AnswerToQuestion(AnswerToQuestionModel model)
        {
            //IBlogQuestionRepository questionRep = Factory.Factory<IBlogQuestionRepository>.GetConcrete();
            IRepository<BlogQuestion> quesRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();
            IRepository<BlogAnswer> answerRep = Factory.Factory<IRepository<BlogAnswer>>.GetConcrete<BlogAnswer>();

            BlogQuestion question = null;
            BlogAnswer a = new BlogAnswer(model.Body, model.UserID, model.UserName,model.UserTiny, model.QuestionID);

            //向仓储增加答案
            answerRep.Add(a);
            answerRep.PersistAll();
            //获取当前问题
            question = quesRep.GetByKey(model.QuestionID);

            //增加答案数
            question.AddAnswerCount();
            //更新
            quesRep.Update(question);

            //将所有持久化
            quesRep.PersistAll();

            
        }

        /// <summary>
        /// 设置最佳答案
        /// </summary>
        /// <param name="model"></param>
        public void SettingBestAnswer(SettingBestAnswerModel model)
        {
            //IBlogQuestionRepository questionRep = Factory.Factory<IBlogQuestionRepository>.GetConcrete();
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();
            IRepository<Account> accountRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            BlogQuestion q = questionRep.GetByKey(model.QuestionID);
            

            //给问题设置最佳答案
            q.SettingBestAnswer(model.BestAnswerID);
            //更新问题
            questionRep.Update(q);
            //持久化
            questionRep.PersistAll();


            //处理各用户的积分增减
            //提问者
            Account questionUser = accountRep.GetByKey(model.QuestionUserID);
            //散分
            questionUser.ReducePoints(q.RewardPoints);
            //更新
            accountRep.Update(questionUser);

            //将分数散给回答者
            foreach (Guid key in model.AnswersDic.Keys)
            {
                Account tmp = accountRep.GetByKey(key);
                tmp.AddPoints(model.AnswersDic[key]);
                accountRep.Update(tmp);
            }

            accountRep.PersistAll();
        }

        /// <summary>
        /// 查看问题详细
        /// </summary>
        /// <param name="qid">问题编号</param>
        /// <returns></returns>
        public BlogQuestionDetailsModel ViewBlogQuestionByID(Guid qid)
        {
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();
            IRepository<BlogAnswer> answerRep = Factory.Factory<IRepository<BlogAnswer>>.GetConcrete<BlogAnswer>();

            BlogQuestionDetailsModel target = null;
            int anserCount = answerRep.FindAll(new Specification<BlogAnswer>(a => a.QuestionId == qid)).Count;
            //获取答案集合并转换到ActionModel的集合，最后作为答案详细模型的属性
            var answers = answerRep.FindAll(new Specification<BlogAnswer>(a => a.QuestionId == qid).Skip(0).Take(anserCount).OrderByDescending(a => a.CreationDate));
            IList<AnswerDspModel> answerList = new List<AnswerDspModel>();
            foreach (BlogAnswer answer in answers)
            {
                AnswerDspModel model = new AnswerDspModel() { AnswerID=answer.Id,UserID=answer.UserID,UserName=answer.UserName, UserPoints=answer.UserPoints,UserTiny=answer.UserTiny,CreatedOn=answer.CreationDate,Body=answer.Body };
                answerList.Add(model);
            }
            
            BlogQuestion tmp=questionRep.GetByKey(qid);
            if (tmp != null)
            {
                target = new BlogQuestionDetailsModel() { QuestionID = tmp.Id, Subject = tmp.State.Subject, Body = tmp.State.Body, AnswerCount = tmp.State.AnswerCount, BestAnswerID = tmp.State.BestAnswerId, ClickCount = tmp.State.ClickCount, RewardPoints = tmp.State.RewardPoints, CreationDate = tmp.CreationDate, CategoryID = tmp.CategoryID, CategoryName = tmp.CategoryName, UserID=tmp.UserID,UserName=tmp.UserName, UserTiny=tmp.UserTiny, UserPoints=tmp.UserPoints, Answers=answerList };
            }

            //增加点击数
            tmp.AddClickCount();
            //更新
            questionRep.Update(tmp);
            //持久化
            questionRep.PersistAll();

            return target;
        }

        /// <summary>
        /// 获取所有问题数量
        /// </summary>
        /// <returns>问题数量</returns>
        public int GetAllQuestionCount()
        {
            return this.GetQuestionCountOfCategory(Guid.Empty);
        }

        /// <summary>
        /// 获取高分问题总数量
        /// </summary>
        /// <param name="boundary">分数边界</param>
        /// <returns>问题数量</returns>
        public int GetQuestionCountOfHighPoint(int boundary)
        {
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();

            int target = 0;

            if (boundary >= 0)//合法
            {
                target = questionRep.Count(new Specification<BlogQuestion>(q => q.RewardPoints >= boundary));
            }

            return target;
        }

        /// <summary>
        /// 获取高分问题列表
        /// </summary>
        /// <param name="boundary">分数</param>
        /// <returns>问题列表</returns>
        public IList<BlogQuestionDspModel> FetchQuestionListOfHighPoint(int boundary,int startIndex,int count)
        {
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();

            IList<BlogQuestionDspModel> targets = new List<BlogQuestionDspModel>();

            var qs = questionRep.FindAll(new Specification<BlogQuestion>(q => q.RewardPoints >= boundary).Skip(startIndex).Take(count).OrderByDescending(q=>q.CreationDate));
            if(qs!=null)
                foreach (BlogQuestion q in qs)
                {
                    BlogQuestionDspModel tmp = new BlogQuestionDspModel() { Subject = q.State.Subject, CategoryID = q.CategoryID, CategoryName = q.CategoryName, QuestionID = q.Id, UserID = q.UserID, UserName = q.UserName, AnswerCount = q.State.AnswerCount, RewardPoints = q.State.RewardPoints };
                    targets.Add(tmp);
                }

            return targets;
        }

        /// <summary>
        /// 获取用户的问答列表
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>问题列表</returns>
        public IList<BlogQuestionDspModel> FetchQuestionListByUserID(Guid uid)
        {
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();

            IList<BlogQuestionDspModel> targets = new List<BlogQuestionDspModel>();

            var qs = questionRep.FindAll(new Specification<BlogQuestion>(q => q.UserID == uid));
            if (qs != null)
            {
                foreach (BlogQuestion q in qs)
                {
                    BlogQuestionDspModel tmp = new BlogQuestionDspModel() {Subject=q.State.Subject,CategoryID=q.CategoryID,CategoryName=q.CategoryName,QuestionID=q.Id,UserID=q.UserID,UserName=q.UserName,AnswerCount=q.State.AnswerCount,RewardPoints=q.State.RewardPoints };
                    targets.Add(tmp);
                }
            }

            return targets;
        }

        /// <summary>
        /// 获取用户的问答列表
        /// 分页实现
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<BlogQuestionDspModel> FetchQuestionListByUserID(Guid uid,int startIndex,int count)
        {
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();
            IList<BlogQuestionDspModel> targets = new List<BlogQuestionDspModel>();

            var qs = questionRep.FindAll(new Specification<BlogQuestion>(q => q.UserID == uid).Skip(startIndex).Take(count).OrderByDescending(q => q.CreationDate));

            if (qs != null)
            {
                foreach (BlogQuestion q in qs)
                {
                    BlogQuestionDspModel tmp = new BlogQuestionDspModel() { Subject = q.State.Subject, CategoryID = q.CategoryID, CategoryName = q.CategoryName, QuestionID = q.Id, UserID = q.UserID, UserName = q.UserName, AnswerCount = q.State.AnswerCount, RewardPoints = q.State.RewardPoints };
                    targets.Add(tmp);
                }
            }

            return targets;
        }

        /// <summary>
        /// 获取分类下问题总数量
        /// </summary>
        /// <param name="cid">分类编号</param>
        /// <returns>问题总数</returns>
        public int GetQuestionCountOfCategory(Guid cid)
        {
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();
            int target = 0;
            if(cid.Equals(Guid.Empty))
                target = questionRep.Count(new Specification<BlogQuestion>(q => q.BestAnswerID == Guid.Empty));
            else
                target = questionRep.Count(new Specification<BlogQuestion>(q => q.BestAnswerID == Guid.Empty && q.CategoryID == cid));

            return target;
        }

        /// <summary>
        /// 获取问答首页问题列表
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">数目</param>
        /// <returns>问题集合</returns>
        public IList<BlogQuestionDspModel> FetchQuestionListOnIndex(int startIndex,int count)
        {
            return FetchQuestionsListByCategoryID(startIndex, count, Guid.Empty);
        }

        /// <summary>
        /// 获取分类中问题列表
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">数目</param>
        /// <param name="categoryId">分类编号</param>
        /// <returns>问题集合</returns>
        public IList<BlogQuestionDspModel> FetchQuestionsListByCategoryID(int startIndex, int count, Guid categoryId)
        {
            IList<BlogQuestionDspModel> targets = new List<BlogQuestionDspModel>();
            
            IRepository<BlogQuestion> questionRep = Factory.Factory<IRepository<BlogQuestion>>.GetConcrete<BlogQuestion>();
            IList<BlogQuestion> source = null;
            

            if (categoryId.Equals(Guid.Empty))
                source = questionRep.FindAll(new Specification<BlogQuestion>(q => q.BestAnswerID==Guid.Empty).Skip(startIndex).Take(count).OrderByDescending(q => q.CreationDate));
            else
                source = questionRep.FindAll(new Specification<BlogQuestion>(q => q.BestAnswerID == Guid.Empty && q.CategoryID == categoryId).Skip(startIndex).Take(count).OrderByDescending(q => q.CreationDate));
            foreach (BlogQuestion q in source)
            {
                BlogQuestionDspModel model = new BlogQuestionDspModel()
                {
                    QuestionID = q.Id,
                    UserID = q.UserID,
                    UserName = q.UserName,
                    AnswerCount = q.State.AnswerCount,
                    RewardPoints = q.State.RewardPoints,
                    Subject = q.State.Subject,
                    CategoryID = q.CategoryID,
                    CategoryName = q.CategoryName
                };
                targets.Add(model);

            }

            return targets;
        }

        /// <summary>
        /// 获取博客问题分类
        /// </summary>
        /// <returns>博客问题分类</returns>
        public IList<BlogQuestionCategoryModel> FetchQuestionCategory()
        {
            IRepository<BlogQuestionCategory> articleRep = Factory.Factory<IRepository<BlogQuestionCategory>>.GetConcrete<BlogQuestionCategory>();
            IList<BlogQuestionCategory> alist = articleRep.FindAll();
            IList<BlogQuestionCategoryModel> target = new List<BlogQuestionCategoryModel>();

            foreach (BlogQuestionCategory story in alist)
            {
                BlogQuestionCategoryModel temp = new BlogQuestionCategoryModel()
                {
                     QuestionCategory = story.Id,
                     CategoryName = story.CategoryName,
                    IconName = story.Icon,
                    
                     OrderPriority = story.Priority
                };
                target.Add(temp);

            }
            return target;
 
        }

        

        #endregion

        #region 博客状态

        /// <summary>
        /// 发状态
        /// </summary>
        /// <param name="model">新建状态模型</param>
        public void CreateBlogState(NewBlogStateModel model)
        {
            IRepository<BlogState> rep = Factory.Factory<IRepository<BlogState>>.GetConcrete<BlogState>();
            

            BlogState state = new 
                BlogState(model.Body, 
                new FBS.Domain.Aggregate.ValueObject.AccountMessageVO(
                    model.UserID, 
                    model.UserName, 
                    model.UserHead)
                    );

            try
            {
                //添加到状态
                rep.Add(state);
                rep.PersistAll();

                //加入新鲜事
                NewFeedModel feedModel = new NewFeedModel() { Sharer = new UserStateModel() {UserName=model.UserName,UserID=model.UserID,UserTiny=model.UserHead }, Subject = model.Body, Content = string.Empty, TargetID = Guid.Empty, Type = FeedType.NewState };
                this.CreateFeed(feedModel);
            }
            catch { }
        }

        /// <summary>
        /// 转发状态
        /// </summary>
        /// <param name="model">转发模型</param>
        public void ForwardBlogState(SiteShareModel model)
        {
            IRepository<Feed> feedRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();

            Feed f = feedRep.GetByKey(model.FeedID);

            NewFeedModel newModel = null;

            //准备转发的状态没有被转发过
            if (f.FeedType.Equals(FeedType.NewState))
            {
                newModel = new NewFeedModel(){Sharer=model.Sharer,SourceUser=model.SourceUser,Type = FeedType.ForwardState,Subject = model.Subject,Content = "<div class='original-stauts'>" + f.Subject+ "</div>"};
            }
                //转发已经被转发过的状态
            else if(f.FeedType.Equals(FeedType.ForwardState))
            {
                newModel = new NewFeedModel(){Sharer=model.Sharer,SourceUser=model.SourceUser,Type = FeedType.ForwardState,Subject = model.Subject+"@"+f.Subject,Content = f.Content };
            }

            this.CreateFeed(newModel);
        }

        
        
        /// <summary>
        /// 发照片
        /// </summary>
        /// <param name="model">发照片模型</param>
        public void NewStateWithPhoto(NewPhotoModel model)
        {
            ImageResizer ir = new ImageResizer("~/usrimg/", "~/BlogImg/");
            string tinyimg = ir.GetImage(model.ImgPath, 110, 110);
            string dispimg = ir.GetImage(model.ImgPath, 380, 600);

            NewFeedModel feedModel = new NewFeedModel() {Sharer=model.User, Subject=model.Subject,Content=tinyimg+"|"+dispimg,Type=FeedType.NewPhoto };

            this.CreateFeed(feedModel);
        }

        /// <summary>
        /// 分享照片
        /// </summary>
        /// <param name="model">分享照片模型</param>
        public void ShareStateWithPhoto(SiteShareModel model)
        {
            IRepository<Feed> feedRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();
            

            Feed feedToShare = feedRep.GetByKey(model.FeedID);
            NewFeedModel feedModel = null;
            

            //分享新发的照片
            if(feedToShare.FeedType.Equals(FeedType.NewPhoto))
            {
                feedModel = new NewFeedModel() { Sharer = model.Sharer,SourceUser=model.SourceUser, Subject = model.Subject, Content = "<div class='original-stauts'>" + feedToShare.Subject + feedToShare.Content + "</div>", Type=FeedType.SharePhoto};
            }
            //分享已经被分享过的照片
            else if (feedToShare.FeedType.Equals(FeedType.SharePhoto))
            {
                feedModel = new NewFeedModel() {Sharer=model.Sharer,SourceUser=model.SourceUser,Subject=model.Subject+"@"+feedToShare.Subject,Content=feedToShare.Content,Type=FeedType.SharePhoto };
            }

            this.CreateFeed(feedModel);
        }
        #endregion

        #region 新鲜事


        /// <summary>
        /// 获取新鲜事评论总数
        /// </summary>
        /// <param name="feedsid">新鲜事编号</param>
        /// <returns></returns>
        public int GetFeedsCommentsCountByFeedsID(Guid feedsid)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            int k = commentRep.Count(new Specification<BlogComment>(p => p.TargetId == feedsid));
            return k;
        }


        /// <summary>
        /// 创建新鲜事
        /// </summary>
        /// <param name="model">新鲜事模型</param>
        public void CreateFeed(NewFeedModel model)
        {
            IRepository<Feed> feedRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();

            string subject = string.Empty;
            string content = string.Empty;

            switch (model.Type)
            {
                case FeedType.NewStory: 
                    {
                        //拼得标题
                        subject = "<a href='"+Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome,model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;发表文章&nbsp;<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.ViewStory, model.Sharer.UserID.ToString(), model.TargetID.ToString());
                        subject += "' target='_blank'>";
                        subject += model.Subject + "</a>";

                        //获得文章简介
                        content = Utils.Utils.RemoveHtml(model.Content);
                        content = content.Length > 100 ? content.Substring(0, 100)+"..." : content;
                        break; 
                    }
                case FeedType.NewState:
                    {
                        //拼接html
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a> : " + model.Subject;
                        
                        break;
                    }
                case FeedType.ForwardState:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>:";
                        subject += model.Subject;

                        //subject += "@";
                        //subject += "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.SourceUser.UserID.ToString()) + "' target='_blank'>" + model.SourceUser.UserName + "</a>";
                        
                        content = model.Content;
                        break; 
                    }
                case FeedType.ShareStory:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;分享&nbsp;<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.SourceUser.UserID.ToString()) + "' target='_blank'>" + model.SourceUser.UserName + "</a>";
                        subject += "&nbsp;的文章&nbsp;";
                        subject += model.Subject;
                        content = model.Content;
                        break; 
                    }
                case FeedType.SharePhoto:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a> : " + model.Subject;
                        //subject += "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.SourceUser.UserID.ToString()) + "' target='_blank'>" + model.SourceUser.UserName + "</a> : " + model.Subject;
                        
                        Regex r = new Regex(@"\d{18}");
                        content=r.Replace(model.Content,DateTime.Now.Ticks.ToString());
                        break;
                    }
                case FeedType.ShareVideo:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;分享的视频&nbsp;";

                        Regex r = new Regex(@"\d{18}");
                        string identify=DateTime.Now.Ticks.ToString();

                        subject += r.Replace(model.Subject,identify);
                        content = r.Replace(model.Content,identify);
                        break;
                    }
                case FeedType.ForwardNews:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a> : ";
                        subject += "<a href='"+Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.ViewNews,model.TargetID.ToString())+"'>"+model.Subject+"</a>";
                        break;
                    }
                case FeedType.NewPhoto:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a> : " + model.Subject;
                        string[] img=model.Content.Split('|');
                        string identify = DateTime.Now.Ticks.ToString();

                        content = "<div class='figure' id='prev_" + identify + "'><a href='###nogo' onclick=\"displayBigImg('" + identify + "')\"><img class='photo' src='../../BlogImg/" + img[0] + "' alt='照片' /></a></div>";
                        content += "<div style='display:none;' class='disp-img' id='disp_" + identify + "'><a href='###nogo' onclick=\"displaySmallImg('" + identify + "')\"><img class='bigimg' src='../../BlogImg/" + img[1] + "' /></a></div>";
                        break;
                    }
                case FeedType.NewVideo:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;分享的视频&nbsp;";
                        subject += model.Subject;
                        content = model.Content;
                        break;
                    }
                case FeedType.ShareBlogDetails:
                    { 
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;分享&nbsp;<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.SourceUser.UserID.ToString()) + "' target='_blank'>" + model.SourceUser.UserName + "</a>";
                        subject += "&nbsp;的文章&nbsp;";
                        subject += "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.ViewStory, model.SourceUser.UserID.ToString(), model.TargetID.ToString()) + "'>" + model.Subject + "</a>";
                        content = Utils.Utils.RemoveHtml(model.Content);
                        content = content.Length > 100 ? content.Substring(0, 100) + "..." : content;
                        break; 
                    }
                case FeedType.Support:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;关注了&nbsp;<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.SourceUser.UserID.ToString()) + "' target='_blank'>" + model.SourceUser.UserName + "</a>";
                        content = Utils.Utils.RemoveHtml(model.Content);
                        content = content.Length > 100 ? content.Substring(0, 100) + "..." : content;
                        break; 
                    }
                case FeedType.CancelSupport: 
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;取消了对&nbsp;<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.SourceUser.UserID.ToString()) + "' target='_blank'>" + model.SourceUser.UserName + "</a>的关注";
                        content = Utils.Utils.RemoveHtml(model.Content);
                        content = content.Length > 100 ? content.Substring(0, 100) + "..." : content;
                        break; 
                    }
                case FeedType.BuildFriendRelation:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;和&nbsp;<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.SourceUser.UserID.ToString()) + "' target='_blank'>" + model.SourceUser.UserName + "</a>建立了好友关系";
                        
                        content = Utils.Utils.RemoveHtml(model.Content);
                        content = content.Length > 100 ? content.Substring(0, 100) + "..." : content;
                        break; 
                    }
                case FeedType.BrokeUpFriendRelation:
                    {
                        subject = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.Sharer.UserID.ToString()) + "' target='_blank'>" + model.Sharer.UserName + "</a>";
                        subject += "&nbsp;和&nbsp;<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.SourceUser.UserID.ToString()) + "' target='_blank'>" + model.SourceUser.UserName + "</a>解除了好友关系";

                        content = Utils.Utils.RemoveHtml(model.Content);
                        content = content.Length > 100 ? content.Substring(0, 100) + "..." : content;
                        break; 
                    }
                default: break;
            }
            
            
            //创建新鲜事
            Feed feed = new Feed(model.Sharer.UserID, model.Sharer.UserName, model.Sharer.UserTiny, subject, content,model.Type);

            feedRep.Add(feed);
            feedRep.PersistAll();
        }

        /// <summary>
        /// 根据用户编号获得新鲜事列表
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>新鲜事列表</returns>
        public IList<FeedDspModel> FetchFeedsByUserID(Guid uid,int startIndex,int count)
        {
            IFeedRepository feedRep = Factory.Factory<IFeedRepository>.GetConcrete();

            IList<FeedDspModel> targets = new List<FeedDspModel>();

            //获取好友新鲜事
            var list=feedRep.FetchFeedsByUserID(uid, startIndex, count);

            foreach (Feed f in list)
            {
                FeedDspModel tmp = new FeedDspModel() {UserHead=f.UserHead,Subject=f.Subject,Content=f.Content,CreatedOn=f.CreatedOn,UserID=f.UserID,UserName=f.UserName,FeedType=f.FeedType,FeedID=f.Id };
                targets.Add(tmp);
            }

            return targets;
        }


        /// <summary>
        /// 根据用户编号获取新鲜事
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public IList<FeedDspModel> FetchFeedsByUserID(Guid uid, int startIndex, int count,string type)
        {
            IFeedRepository feedRep = Factory.Factory<IFeedRepository>.GetConcrete();

            IList<FeedDspModel> targets = new List<FeedDspModel>();

            //获取好友新鲜事
            var list = feedRep.FetchFeedsByUserID(uid, startIndex, count,type);

            foreach (Feed f in list)
            {
                FeedDspModel tmp = new FeedDspModel() { ReplayCount=f.ReplayCount, UserHead = f.UserHead, Subject = f.Subject, Content = f.Content, CreatedOn = f.CreatedOn, UserID = f.UserID, UserName = f.UserName, FeedType = f.FeedType, FeedID = f.Id };
                targets.Add(tmp);
            }

            return targets;
        }

        /// <summary>
        /// 获取用户新鲜事总数
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>新鲜事总数</returns>
        public int GetFeedsCountByUserID(Guid uid)
        {
            IRepository<Feed> articleRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();
            int target = 0;
            try
            {
                target = articleRep.Count((new Specification<Feed>(a => a.UserID == uid)));
            }
            catch
            { }

            return target;
        }

        /// <summary>
        /// 按照微语，博文获取新鲜事的总数
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetFeedsCountByUserID(Guid uid,string type)
        {
            IFeedRepository feedRep = Factory.Factory<IFeedRepository>.GetConcrete();
            int target = 0;
            try
            {

                target = feedRep.GetFriendsFeedsCountByUserID(uid, type);
                
            }
            catch
            { }

            return target;
        }

        /// <summary>
        /// 删除评论时减少评论数
        /// </summary>
        /// <param name="uid"></param>
        public void ReduceCommentCount(Guid uid)
        {
            IRepository<Feed> feedRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();
            Feed fe = feedRep.GetByKey(uid);
            fe.ReduceReplayCount();
            feedRep.Update(fe);
            feedRep.PersistAll();
        }
        /// <summary>
        /// 增加评论时增加评论数
        /// </summary>
        /// <param name="uid"></param>
        public void AddCommentCount(Guid uid)
        {
            IRepository<Feed> feedRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();
            Feed fe = feedRep.GetByKey(uid);
            fe.AddReplayCount();
            feedRep.Update(fe);
            feedRep.PersistAll();
        }

       
        #endregion

        #region 问卷操作
        /// <summary>
        /// 获取所有问卷信息
        /// </summary>
        /// <returns></returns>
        public IList<FormInfoModel> FetchFormInfo()
        {
            IRepository<FormInfo> fiRep = Factory.Factory<IRepository<FormInfo>>.GetConcrete<FormInfo>();
            IList<FormInfoModel> targets = new List<FormInfoModel>();
            IList<FormInfo> temList = null;

            temList = fiRep.FindAll();
            foreach (FormInfo fi in temList)
            {
                FormInfoModel m = new FormInfoModel(){FormID=fi.Id, Title=fi.Title, Description=fi.Description, Display=fi.Display};
                targets.Add(m);
            }

            return targets;
        }

        /// <summary>
        /// 根据FormID首页显示某问卷
        /// </summary>
        /// <param name="fid"></param>
        public void DisPlayForm(Guid fid)
        {
            IRepository<FormInfo> formRep = Factory.Factory<IRepository<FormInfo>>.GetConcrete<FormInfo>();

            FormInfo Selected = null;
            FormInfo ReadyToSelect = null;

            Selected = formRep.Find(new Specification<FormInfo>(f => f.Display==true));

            if (Selected != null)
            {
                //取消原先显示
                Selected.SettingOnIndex(false);
                formRep.Update(Selected);
            }

            //设置新问卷
            ReadyToSelect = formRep.GetByKey(fid);
            ReadyToSelect.SettingOnIndex(true);

            //更新
            formRep.Update(ReadyToSelect);
            //持久化
            formRep.PersistAll();
        }

        /// <summary>
        /// 判断显示的问卷
        /// </summary>
        /// <returns></returns>
        public bool DisplayForm()
        {
            IRepository<FormInfo> formRep = Factory.Factory<IRepository<FormInfo>>.GetConcrete<FormInfo>();
            FormInfo f = formRep.Find(new Specification<FormInfo>(q => q.Display == true));
            if (f != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到首页显示的问卷
        /// </summary>
        /// <returns></returns>
        public FormInfoModel GetDisplayForm()
        {
            IRepository<FormInfo> formRep = Factory.Factory<IRepository<FormInfo>>.GetConcrete<FormInfo>();
            FormInfo current = formRep.Find(new Specification<FormInfo>(q => q.Display == true));
            FormInfoModel target=null;

            if (null != current)
            {
                target= new FormInfoModel() { FormID = current.Id, Display = current.Display, Description = current.Description, Title = current.Title };
            }
            return target;
        }

        /// <summary>
        /// 添加问卷信息
        /// </summary>
        /// <param name="model"></param>
        public void CreateFormInfo(FormInfoModel model)
        {
            IRepository<FormInfo> fiRep = Factory.Factory<IRepository<FormInfo>>.GetConcrete<FormInfo>();
            try
            {
                fiRep.Add(new FormInfo(model.Title, model.Description,false));
                fiRep.PersistAll();
            }
            catch{}
        }
       

        /// <summary>
        /// 删除问卷信息
        /// </summary>
        /// <param name="cid">编号</param>
        public void DeleteFormInfo(Guid cid)
        {
            IRepository<FormInfo> categoryRep = Factory.Factory<IRepository<FormInfo>>.GetConcrete<FormInfo>();

            try
            {
                categoryRep.RemoveByKey(cid);
                categoryRep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 修改问卷
        /// </summary>
        /// <param name="model">修改问卷模型</param>
        public void UpdateFormInfo(FormInfoModel model)
        {
            IRepository<FormInfo> rep = Factory.Factory<IRepository<FormInfo>>.GetConcrete<FormInfo>();

            FormInfo f = null;
            try
            {
                f = rep.GetByKey(model.FormID);
                

                f.Title= model.Title;
                f.Description = model.Description;
             
                rep.Update(f);
                rep.PersistAll();
            }
            catch { }
        }
        /// <summary>
        /// 判断问卷下问题的数量
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public int IsHaveTwoQuestion(Guid fid)
        {
            IRepository<SheetQuestion> rep = Factory.Factory<IRepository<SheetQuestion>>.GetConcrete<SheetQuestion>();
            int count = rep.FindAll(new Specification<SheetQuestion>(s => s.FormID == fid)).Count;
            return count;

 
        }
        /// <summary>
        /// 根据问题ID找到FormID
        /// </summary>
        /// <param name="qid"></param>
        /// <returns></returns>
        public Guid GetFormIDByQuestionID(Guid qid)
        {
            Guid formid = Guid.Empty;
            IRepository<SheetQuestion> rep = Factory.Factory<IRepository<SheetQuestion>>.GetConcrete<SheetQuestion>();
            SheetQuestion s = null;
            try
            {
                s = rep.GetByKey(qid);
                formid = s.FormID;

            }
            catch
            { }
            return formid;

 
        }
        /// <summary>
        /// 查看问卷详细
        /// </summary>
        /// <param name="qid">问卷编号</param>
        /// <returns></returns>
        public FormInfoModel ViewFormByID(Guid fid)
        {
            IRepository<FormInfo> questionRep = Factory.Factory<IRepository<FormInfo>>.GetConcrete<FormInfo>();
          FormInfoModel target = null;

          FormInfo tmp = questionRep.GetByKey(fid);
            if (tmp != null)
            {
                target = new FormInfoModel() { FormID=tmp.Id, Title=tmp.Title, Description=tmp.Description  };
            }

            return target;
        }
        #endregion

        #region 问卷下问题和答案
        /// <summary>
        /// 获取问卷下所有问题
        /// </summary>
        /// <returns></returns>
        public IList<SheetQuestionModel> FetchQuestionByFormID(Guid fid)
        {
            IRepository<SheetQuestion> fiRep = Factory.Factory<IRepository<SheetQuestion>>.GetConcrete<SheetQuestion>();
            IList<SheetQuestionModel> targets = new List<SheetQuestionModel>();
            IList<SheetQuestion> temList = null;

            temList = fiRep.FindAll(new Specification<SheetQuestion>(q => q.FormID == fid));

            foreach (SheetQuestion q in temList)
            {
                SheetQuestionModel m = new SheetQuestionModel() { QuestionID = q.Id, FormID = q.FormID, QuestionName = q.QuestionName,Answers=this.FetchAnswerByQuestionID(q.Id) };
                
                targets.Add(m);
            }

            return targets;
        }
      
        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="model"></param>
        public void CreateQuestion(AnswerSheetQuestionModel model)
        {
            IRepository<SheetQuestion> fiRep = Factory.Factory<IRepository<SheetQuestion>>.GetConcrete<SheetQuestion>();

            fiRep.Add(new SheetQuestion(model.FormID, model.QuestionName));
            fiRep.PersistAll();
        }


        /// <summary>
        /// 删除问题信息
        /// </summary>
        /// <param name="cid">编号</param>
        public void DeleteQuestion(Guid qid)
        {
            IRepository<SheetQuestion> categoryRep = Factory.Factory<IRepository<SheetQuestion>>.GetConcrete<SheetQuestion>();

            try
            {
                categoryRep.RemoveByKey(qid);
                categoryRep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 修改问题
        /// </summary>
        /// <param name="model">修改问卷模型</param>
        public void UpdateSheetQuestion(AnswerSheetQuestionModel model)
        {
            IRepository<SheetQuestion> rep = Factory.Factory<IRepository<SheetQuestion>>.GetConcrete<SheetQuestion>();

            SheetQuestion s = null;
            try
            {
                s = rep.GetByKey(model.QuestionID);    
                s.QuestionName= model.QuestionName;
                rep.Update(s);
                rep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 获取某个问题
        /// </summary>
        /// <param name="qid">问题</param>
        /// <returns></returns>
        public AnswerSheetQuestionModel GetOneQuestionByID(Guid qid)
        {
            IRepository<SheetQuestion> questionRep = Factory.Factory<IRepository<SheetQuestion>>.GetConcrete<SheetQuestion>();
            AnswerSheetQuestionModel target = null;

            SheetQuestion tmp = questionRep.GetByKey(qid);
            if (tmp != null)
            {
                target = new AnswerSheetQuestionModel() { QuestionID = tmp.Id, FormID = tmp.FormID, QuestionName = tmp.QuestionName };
            }

            return target;
        }



        #endregion

        #region 问题答案
        /// <summary>
        /// 获取问题答案
        /// </summary>
        /// <returns></returns>
        public IList<SheetAnswerModel> FetchAnswerByQuestionID(Guid qid)
        {
            IRepository<SheetAnswer> fiRep = Factory.Factory<IRepository<SheetAnswer>>.GetConcrete<SheetAnswer>();
            IList<SheetAnswerModel> targets = new List<SheetAnswerModel>();
            IList<SheetAnswer> temList = null;
       
                temList = fiRep.FindAll(new Specification<SheetAnswer>(q => q.QuestionID == qid));
                foreach (SheetAnswer fi in temList)
                {
                    SheetAnswerModel m = new SheetAnswerModel() {  AnswerID= fi.Id, QuestionID=fi.QuestionID, AnswerName= fi.AnswerName, Count=fi.Count};
                    targets.Add(m);
                }
         
            return targets;
        }

        /// <summary>
        /// 获取某答案
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public SheetAnswerModel FetchOneAnserByAnswerID(Guid aid)
        {
            IRepository<SheetAnswer> fiRep = Factory.Factory<IRepository<SheetAnswer>>.GetConcrete<SheetAnswer>();
            SheetAnswerModel model = null;
            SheetAnswer sa = null;
            try
            {
                sa = fiRep.Find(new Specification<SheetAnswer>(s => s.Id == aid));
                if (sa != null)
                {
                    model = new SheetAnswerModel() { AnswerID = sa.Id, QuestionID = sa.QuestionID, AnswerName = sa.AnswerName };
                }
            }
            catch
            { }
            return model;
 
        }

       
        /// <summary>
        /// 添加问题选项
        /// </summary>
        /// <param name="model"></param>
        public void CreateQuestionAnswer(SheetAnswerModel model)
        {
            IRepository<SheetAnswer> fiRep = Factory.Factory<IRepository<SheetAnswer>>.GetConcrete<SheetAnswer>();
            try
            {
                fiRep.Add(new SheetAnswer(model.QuestionID,model.AnswerName,model.FormID));
                fiRep.PersistAll();
            }
            catch { }
        }


        /// <summary>
        /// 删除问题选项
        /// </summary>
        /// <param name="cid">编号</param>
        public void DeleteQuestionAnswer(Guid aid)
        {
            IRepository<SheetAnswer> categoryRep = Factory.Factory<IRepository<SheetAnswer>>.GetConcrete<SheetAnswer>();

            try
            {
                categoryRep.RemoveByKey(aid);
                categoryRep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 修改选项
        /// </summary>
        /// <param name="model">修改问卷模型</param>
        public void UpdateSheetAnswer(SheetAnswerModel model)
        {
            IRepository<SheetAnswer> rep = Factory.Factory<IRepository<SheetAnswer>>.GetConcrete<SheetAnswer>();

            SheetAnswer s = null;
            try
            {
                s = rep.GetByKey(model.AnswerID);


                s.QuestionID = model.QuestionID;
                s.AnswerName = model.AnswerName;
                

                rep.Update(s);
                rep.PersistAll();
            }
            catch { }
        }

      
        #endregion

        #region 写入用户选择的答案和结果
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aid"></param>
        public void AddSheetAnswerCount(Guid aid)
        {
            IRepository<SheetAnswer> rep = Factory.Factory<IRepository<SheetAnswer>>.GetConcrete<SheetAnswer>();
            SheetAnswer s = null;
            try
            {
                s = rep.GetByKey(aid);
                if (s != null)
                {
                    s.Count+=1;
                    rep.Update(s);
                }
                else
                {
                    s.Count = 1;
                    rep.Update(s);
                }
                rep.PersistAll();
            }
            catch
            { }
 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="qid"></param>
        /// <returns></returns>
        public int GetAllSheetAnswerCount(Guid qid)
        {
            IRepository<SheetAnswer> answerRep = Factory.Factory<IRepository<SheetAnswer>>.GetConcrete<SheetAnswer>();
            int count = 0;
            
            var answers = answerRep.FindAll(new Specification<SheetAnswer>(q => q.QuestionID == qid));

            foreach (SheetAnswer answer in answers)
            {
                count += answer.Count;
            }

            return count;
        }

        /// <summary>
        /// 获取某问题的答案选择的人数
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public int GetSheetAnswerCount(Guid aid)
        {
            IRepository<SheetAnswer> answerRep = Factory.Factory<IRepository<SheetAnswer>>.GetConcrete<SheetAnswer>();
            SheetAnswer answer = null;
            int count = 0;

            answer = answerRep.Find(new Specification<SheetAnswer>(q => q.Id == aid));
            count = answer.Count;

            return count;

        }

        public IList<SheetQuestionModel> ViewResultOfOneForm(Guid formId)
        {
            var questions = this.FetchQuestionByFormID(formId);
            //IList<SheetQuestionModel> target = new List<SheetQuestionModel>();

            //Dictionary<SheetQuestionModel, IList<SheetAnswerModel>> qa = new Dictionary<SheetQuestionModel, IList<SheetAnswerModel>>();

            foreach (SheetQuestionModel model in questions)
                foreach (SheetAnswerModel answer in model.Answers)
                {
                    int a=this.GetSheetAnswerCount(answer.AnswerID);
                    int b=this.GetAllSheetAnswerCount(answer.QuestionID);
                    if (b != 0)
                        answer.Percent = (float)a / b;
                    else answer.Percent = 0;
                }

            return questions;
        }

        #endregion

        #region 最受欢迎
        /// <summary>
        /// 最受欢迎人物
        /// </summary>
        /// <returns></returns>
        public IList< UserStateModel> GetPopularMen()
        {
            IRepository<UserProperty> uRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            IList<UserProperty> list = uRep.FindAll(new Specification<UserProperty>(u=>u.Type=="personal").Skip(0).Take(10).OrderByDescending(p => p.FollowersCount));
            IList<UserStateModel> umodel = new List<UserStateModel>();
            
            foreach (UserProperty up in list)
            {
                UserStateModel usmodel = new UserStateModel(); 
                Account a = aRep.Find(new Specification<Account>(s => s.Id == up.UserID));
                usmodel.UserID = a.Id;
                usmodel.UserName = a.UserName;
                usmodel.UserHead = a.UserHead;
                usmodel.UserTiny = a.Tiny;
                umodel.Add(usmodel);

 
            }
            return umodel;


        }
        /// <summary>
        /// 最受欢迎机构
        /// </summary>
        /// <returns></returns>
        public IList< UserStateModel> GetPopularCompany()
        {
            IRepository<UserProperty> uRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            IList<UserProperty> list = uRep.FindAll(new Specification<UserProperty>(u => u.Type == "company").Skip(0).Take(10).OrderByDescending(p => p.FollowersCount));
            IList<UserStateModel> umodel = new List<UserStateModel>();

            foreach (UserProperty up in list)
            {
                UserStateModel usmodel = new UserStateModel();
                Account a = aRep.Find(new Specification<Account>(q => q.Id == up.UserID));
                usmodel.UserID = a.Id;
                usmodel.UserName = a.UserName;
                usmodel.UserHead = a.UserHead;
                usmodel.UserTiny = a.Tiny;
                umodel.Add(usmodel);


            }
            return umodel;

        }
        

        /// <summary>
        /// 首页显示的机构
        /// </summary>
        /// <returns></returns>
        public IList<UserStateModel> GetDisplayCompany(int count)
        {
            IRepository<UserProperty> uRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            IList<UserProperty> list = uRep.FindAll(new Specification<UserProperty>(u => u.Display==true).Skip(0).Take(count).OrderBy(aa=>aa.FollowersCount));
            IList<UserStateModel> umodel = new List<UserStateModel>();

            foreach (UserProperty up in list)
            {
                UserStateModel usmodel = new UserStateModel();
                Account a = aRep.Find(new Specification<Account>(q => q.Id == up.UserID));
                usmodel.UserID = a.Id;
                usmodel.UserName = a.UserName;
                usmodel.UserHead = a.UserHead;
                usmodel.UserTiny = a.Tiny;
                usmodel.Display = up.Display;
                umodel.Add(usmodel);


            }
            return umodel;


        }
        /// <summary>
        ///  获取机构
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<UserStateModel> GetAllCompany(int startIndex, int count)
        {
            IRepository<UserProperty> suggestRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            IList<UserProperty> alist = null;
            IList<UserStateModel> target = new List<UserStateModel>();
            try
            {

                alist = suggestRep.FindAll(new Specification<UserProperty>(s => s.Type=="company").Skip(startIndex).Take(count).OrderByDescending(p => p.FollowersCount));
                foreach (UserProperty a in alist)
                {
                    Account ac = aRep.Find(new Specification<Account>(acc=>acc.Id==a.UserID));
                    UserStateModel tmp = new UserStateModel() { UserID=ac.Id, UserName=ac.UserName, UserHead=ac.UserHead, UserTiny=ac.Tiny, Display=a.Display };
                    target.Add(tmp);
                }
            }
            catch { }

            return target;
        }
        /// <summary>
        /// 显示所有推荐的机构
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<UserStateModel> GetAllRecommendCompany()
        {
            IRepository<UserProperty> suggestRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            IList<UserProperty> alist = null;
            IList<UserStateModel> target = new List<UserStateModel>();
            try
            {

                alist = suggestRep.FindAll(new Specification<UserProperty>(s => s.Type == "company"&s.Display==true));
                foreach (UserProperty a in alist)
                {
                    Account ac = aRep.Find(new Specification<Account>(acc => acc.Id == a.UserID));
                    UserStateModel tmp = new UserStateModel() { UserID = ac.Id, UserName = ac.UserName, UserHead = ac.UserHead, UserTiny = ac.Tiny, Display = a.Display };
                    target.Add(tmp);
                }
            }
            catch { }

            return target;
        }
        /// <summary>
        /// 推荐显示机构
        /// </summary>
        public void SetDisplyCompany(Guid id)
        {
            IRepository<UserProperty> upRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty up = upRep.Find(new Specification<UserProperty>(u => u.UserID == id));
            up.Display = true;
            upRep.Update(up);
            upRep.PersistAll();

 
        }
        /// <summary>
        /// 取消推荐
        /// </summary>
        /// <param name="id"></param>
        public void CancelDisplayCompany(Guid id)
        {

            IRepository<UserProperty> upRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty up = upRep.Find(new Specification<UserProperty>(u => u.UserID == id));
            up.Display = false;
            upRep.Update(up);
            upRep.PersistAll();

        }
        /// <summary>
        /// 所有机构的总数
        /// </summary>
        /// <returns></returns>
        public int AllCompanyCount()
        {
            IRepository<UserProperty> upRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            int i = 0;
            i = upRep.FindAll(new Specification<UserProperty>(u => u.Type == "company")).Count;
            return i;
 
        }
        /// <summary>
        /// 显示的机构数
        /// </summary>
        /// <returns></returns>
        public int AllDisplayCount()
        {
            IRepository<UserProperty> upRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            int i = 0;
            i = upRep.FindAll(new Specification<UserProperty>(u => u.Type == "company"&u.Display==true)).Count;
            return i;
        }
      
        #endregion

        #region 企业照片
        /// <summary>
        /// 查找所有企业照片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<AlbumDspModel> GetAllPicByUserID(Guid id,int index,int count)
        {
            IRepository<Album> aRep = Factory.Factory<IRepository<Album>>.GetConcrete<Album>();
            IList<AlbumDspModel> list = new List<AlbumDspModel>();
            IList<Album> target = aRep.FindAll(new Specification<Album>(a => a.UserID == id).OrderBy(al=>al.Time).Skip(index).Take(count));
            foreach (Album a in target)
            {
                AlbumDspModel model = new AlbumDspModel();
                model.AlbumID = a.Id;
                model.UserID = a.UserID;
                model.UserName = a.UserName;
                model.PhotoName = a.PhotoName;
                model.Description = a.Description;
                model.Time = a.Time;
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 获取企业用户的图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList<AlbumDspModel> GetAllPictByUserID(Guid id)
        {
            IRepository<Album> aRep = Factory.Factory<IRepository<Album>>.GetConcrete<Album>();
            IList<AlbumDspModel> list = new List<AlbumDspModel>();
            IList<Album> target = aRep.FindAll(new Specification<Album>(a => a.UserID == id).OrderByDescending(al => al.Time));
            foreach (Album a in target)
            {
                AlbumDspModel model = new AlbumDspModel();
                model.AlbumID = a.Id;
                model.UserID = a.UserID;
                model.UserName = a.UserName;
                model.PhotoName = a.PhotoName;
                model.Description = a.Description;
                model.Time = a.Time;
                list.Add(model);
            }
            return list;
        }
        /// <summary>
        /// 删除照片
        /// </summary>
        /// <param name="id"></param>
        public void RemovePhoto(Guid id)
        {
            IRepository<Album> aRep = Factory.Factory<IRepository<Album>>.GetConcrete<Album>();
            if (id != Guid.Empty)
            {
                aRep.RemoveByKey(id);
                aRep.PersistAll();
            }
 
        }

        public void RemovePhoto(string photoName)
        {
            IRepository<Album> rep = Factory.Factory<IRepository<Album>>.GetConcrete<Album>();
            Album To_Delete = rep.Find(new Specification<Album>(a => a.PhotoName == photoName));
            rep.Remove(To_Delete);
            rep.PersistAll();
        }

        /// <summary>
        /// 添加照片
        /// </summary>
        /// <param name="model"></param>
        public void InsertCompanyPhoto(AlbumDspModel model)
        {
            IRepository<Album> aRep = Factory.Factory<IRepository<Album>>.GetConcrete<Album>();
            Album A = new Album(model.UserID,model.UserName,model.PhotoName,model.Description,model.Time);
            aRep.Add(A);
            aRep.PersistAll();
 
        }

        /// <summary>
        /// 修改照片信息
        /// </summary>
        /// <param name="model"></param>
        public void UpdatePhoto(AlbumDspModel model)
        {
            IRepository<Album> aRep = Factory.Factory<IRepository<Album>>.GetConcrete<Album>();
            Album a = aRep.GetByKey(model.AlbumID);
            a.Description = model.Description;
            aRep.Update(a);
            aRep.PersistAll();
 
        }
        #endregion

    }
}
 