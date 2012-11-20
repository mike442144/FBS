using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using FBS.Service.ActionModels;
using FBS.Domain.Repository;
using FBS.Web.Share;
using FBS.Domain.Specifications;

namespace FBS.Service
{
    public class ShareThreadService
    {
        /// <summary>
        /// 创建分享视频
        /// </summary>
        /// <param name="model">新建分享视频模型</param>
        public void CreateShareThread(NewShareThreadModel model)
        {
            IRepository<ShareThread> rep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();

            try
            {
                //public ShareThread(string rawUrl,string playUrl,MediaType mtype,string subject,string body,string thumbnailUrl)
                rep.Add(new ShareThread(model.RawUrl, model.PlayUrl, MediaType.Video, model.Subject, model.Body, model.ThumbnailUrl, DateTime.Now,model.Source));
                rep.PersistAll();
            }
            catch {
            
            }
        }

        /// <summary>
        /// 创建分享视频
        /// </summary>
        /// <param name="model">新建分享视频模型</param>
        public void CreateShareThread(NewVideoModel model,string url)
        { 
            ShareThread st=null;
            VideoShareProvider.ParseHtml(model.RawUrl, ref st);
            ShareThreadService mservice = new ShareThreadService();
            NewShareThreadModel newmodel = new NewShareThreadModel() { Body=st.Body, PlayUrl=st.PlayUrl, RawUrl=url, ShareTime=DateTime.Now, Subject=st.Subject, ThumbnailUrl=st.ThumbnailUrl};
            this.CreateShareThread(newmodel);
        }

        public void CreateShareThread(NewVideoModel model, string url,string source)
        {
            ShareThread st = null;
            VideoShareProvider.ParseHtml(model.RawUrl, ref st);
            ShareThreadService mservice = new ShareThreadService();
            NewShareThreadModel newmodel = new NewShareThreadModel() { Body = st.Body, PlayUrl = st.PlayUrl, RawUrl = url, ShareTime = DateTime.Now, Subject = st.Subject, ThumbnailUrl = st.ThumbnailUrl , Source=source};
            this.CreateShareThread(newmodel);
        }

        public void CreateShareThread(NewVideoModel model, string url, string source,string title)
        {
            ShareThread st = null;
            VideoShareProvider.ParseHtml(model.RawUrl, ref st);
            string subject = string.Empty;
            string toDelete=" - 视频 - 优酷视频 - 在线观看";
            if (string.IsNullOrEmpty(title))
            {
                if (st.Subject.IndexOf(toDelete) >= 0)
                    subject = st.Subject.Substring(0, st.Subject.IndexOf(toDelete));//并不是每一个网站都使用此过滤标记，暂时使用
                else
                    subject = st.Subject;
            }
            else
            {
                subject = title;
            }
            ShareThreadService mservice = new ShareThreadService();
            NewShareThreadModel newmodel = new NewShareThreadModel() { Body = st.Body, PlayUrl = st.PlayUrl, RawUrl = url, ShareTime = DateTime.Now, Subject = subject, ThumbnailUrl = st.ThumbnailUrl, Source = source };
            this.CreateShareThread(newmodel);
        }


        /// <summary>
        /// 获取分享视频详细内容
        /// </summary>
        /// <param name="aid">分享视频编号</param>
        public ShareThreadDetailsModel GetOneShareThreadContentByID(Guid aid)
        {
            IRepository<ShareThread> rep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();

            ShareThread sharethread = null;
            ShareThreadDetailsModel target = null;

            var comments = commentRep.FindAll(new Specification<BlogComment>(c => c.TargetId == aid).OrderBy(c=>c.CreationDate).Skip(0).Take(10000));
            IList<CommentDspModel> cmodels = new List<CommentDspModel>();

            foreach (BlogComment c in comments)
            {
                CommentDspModel tmp = new CommentDspModel() { AccountID=c.AccountInfo.Id, CommentContent=c.Body, CommentID=c.Id, CreatedOn=c.CreationDate, TargetID=c.TargetId, UserName=c.AccountInfo.UserName };
                cmodels.Add(tmp);
            }

            try
            {
                sharethread = rep.GetByKey(aid);

                target = new ShareThreadDetailsModel()
                {
                     RawUrl=sharethread.RawUrl,
                    Body=sharethread.Body,
                     PlayUrl=sharethread.PlayUrl,
                      Subject=sharethread.Subject,
                       ThumbnailUrl=sharethread.ThumbnailUrl,
                        ShareTime=sharethread.ShareTime,
                         Comments=cmodels
                };
            }

            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

            return target;
        }


        /// <summary>
        /// 删除视频，同时删除评论
        /// </summary>
        /// <param name="tid"></param>
        public void RemoveShareThreadByKey(Guid tid)
        {
            IRepository<ShareThread> rep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();
            //删除之前先删评论
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IList<BlogComment> list= commentRep.FindAll(new Specification<BlogComment>(c => c.TargetId == tid));
            foreach(BlogComment co in list)
            {
                commentRep.RemoveByKey(co.Id);
                commentRep.PersistAll();
            }

            rep.RemoveByKey(tid);
            rep.PersistAll();
        }



        /// <summary>
        /// 按时间最新的获取所有分享视频详细内容
        /// </summary> 
        public IList<ShareThreadDspModel> FetchShareThreadDspModel(int index,int count)
        {
            IRepository<ShareThread> articleRep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();
            IList<ShareThread> alist = articleRep.FindAll(new Specification<ShareThread>(st=>st.PlayUrl!=string.Empty).Take(count).Skip(index).OrderByDescending(st=>st.ShareTime));
            IList<ShareThreadDspModel> mylist = new List<ShareThreadDspModel>();
            foreach(ShareThread model in alist)
            {
                mylist.Add(new ShareThreadDspModel() { RawUrl=model.RawUrl, Body = model.Body, PlayUrl = model.PlayUrl, ShareTime = model.ShareTime, Subject = model.Subject, ThumbnailUrl = model.ThumbnailUrl, VideoID = model.Id });
            }
            //for (int i = 0; i < mylist.Count; i++)
            //{
            //    for (int j = i; j < mylist.Count; j++)
            //    {
            //        if (mylist[i].ShareTime <= mylist[j].ShareTime)
            //        {
            //            endlist.Add(mylist[i]);
            //        }
            //    }
            //}
            return mylist;
        }
        /// <summary>
        /// 获取视频根据类型
        /// </summary>
        /// <param name="index">起始</param>
        /// <param name="count">总数</param>
        /// <param name="Source">类型</param>
        /// <returns></returns>
        public IList<ShareThreadDspModel> FetchShareThreadDspModel(int index, int count,string Source)
        {
            IRepository<ShareThread> articleRep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();
            IList<ShareThread> alist = articleRep.FindAll(new Specification<ShareThread>(st => st.PlayUrl != string.Empty&&st.Source==Source).Take(count).Skip(index).OrderByDescending(st => st.ShareTime));
            IList<ShareThreadDspModel> mylist = new List<ShareThreadDspModel>();
            foreach (ShareThread model in alist)
            {
                mylist.Add(new ShareThreadDspModel() { Body = model.Body, PlayUrl = model.PlayUrl, ShareTime = model.ShareTime, Subject = model.Subject, ThumbnailUrl = model.ThumbnailUrl, VideoID = model.Id });
            }
            //for (int i = 0; i < mylist.Count; i++)
            //{
            //    for (int j = i; j < mylist.Count; j++)
            //    {
            //        if (mylist[i].ShareTime <= mylist[j].ShareTime)
            //        {
            //            endlist.Add(mylist[i]);
            //        }
            //    }
            //}
            return mylist;
        }

        /// <summary>
        /// 获取视频总数
        /// </summary>
        /// <returns></returns>
        public int GetShareThreadCount()
        { 
            IRepository<ShareThread> articleRep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();
            IList<ShareThread> alist = articleRep.FindAll();
            return alist.Count;
        }

        /// <summary>
        /// 根据视频类型获取该类型视频总数
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetShareThreadCountByType(string type)
        {
            IRepository<ShareThread> articleRep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();
            IList<ShareThread> alist = articleRep.FindAll(new Specification<ShareThread>(st => st.Source==type));
            return alist.Count;
        }

        //public IList<ShareThreadDspModel> FetchShareThreadDspModelOrderByclick(int index, int count)
        //{
        //    IRepository<ShareThread> articleRep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();
        //    IList<ShareThread> alist = articleRep.FindAll(new Specification<ShareThread>(st => st.PlayUrl != string.Empty).Take(count).Skip(index).OrderByDescending(st => st));
        //    IList<ShareThreadDspModel> mylist = new List<ShareThreadDspModel>();
        //    foreach (ShareThread model in alist)
        //    {
        //        mylist.Add(new ShareThreadDspModel() { Body = model.Body, PlayUrl = model.PlayUrl, ShareTime = model.ShareTime, Subject = model.Subject, ThumbnailUrl = model.ThumbnailUrl, VideoID = model.Id });
        //    }
        //    return mylist;
        //}

        /// <summary>
        /// 给视频添加评论
        /// </summary>
        /// <param name="model">评论模型</param>
        public void AddCommentToShareThread(CommentModel model)
        {
            IRepository<BlogComment> rep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IRepository<ShareThread> articleRep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();

            BlogComment comment = new BlogComment(model.TargetID, model.AccountID, model.UserName, model.CommentContent);
            rep.Add(comment);
            rep.PersistAll();

            //Article article = articleRep.GetByKey(model.TargetID);
            //article.AddCommentCount();
            //articleRep.Update(article);
            //articleRep.PersistAll();
        }
        /// <summary>
        /// 删除评论模型
        /// </summary>
        /// <param name="commentId">评论模型</param>
        public void DeleteCommentByID(Guid commentId)
        {
            IRepository<BlogComment> commentRep = Factory.Factory<IRepository<BlogComment>>.GetConcrete<BlogComment>();
            IRepository<ShareThread> articleRep = Factory.Factory<IRepository<ShareThread>>.GetConcrete<ShareThread>();

            BlogComment comment = commentRep.GetByKey(commentId);
            //Article article = articleRep.GetByKey(comment.TargetId);

            commentRep.Remove(comment);
            commentRep.PersistAll();

            //article.ReduceCommentCount();
            //articleRep.Update(article);
            //articleRep.PersistAll();
        }


    }
}
