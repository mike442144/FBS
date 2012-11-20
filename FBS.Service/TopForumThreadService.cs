using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;
using FBS.Domain.Specifications;

namespace FBS.Service
{
    public class TopForumThreadService
    {
        /// <summary>
        /// 创建置顶帖
        /// </summary>
        /// <param name="model">新建置顶帖模型</param>
        public void CreateTopForumThread(Guid id,string type)
        {
            IRepository<TopForumThread> rep = Factory.Factory<IRepository<TopForumThread>>.GetConcrete<TopForumThread>();
            //IRepository<TopForumThread> readRep = Factory.Factory<IRepository<TopForumThread>>.GetConcrete();
            IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
           // ForumThread t = forumThreadRep.Find(new Specification<ForumThread>(s=>s.Id==id));
            ForumThread t = null;
            t = forumThreadRep.GetByKey(id);
            //NewTopForumThreadModel model = new NewTopForumThreadModel() { CreatTime = DateTime.Now, TopForumID = t.ForumID, TopForumThreadID = t.Id };
            if (type == "static")
            {
                TopForumThread tf = new TopForumThread(t.Id);
                try
                {
                    //rep.Add(new TopForumThread(Guid.NewGuid(),model.TopForumThreadID,model.TopForumID));
                    rep.Add(tf);
                    rep.PersistAll();
                }
                catch { }
            }
            else if(type=="onlyforum")
            {
                TopForumThread tf = new TopForumThread(t.Id, t.ForumID);
                try
                {
                    //rep.Add(new TopForumThread(Guid.NewGuid(),model.TopForumThreadID,model.TopForumID));
                    rep.Add(tf);
                    rep.PersistAll();
                }
                catch { }
            }
            else if (type == "cancel")
            {
                TopForumThread tf = rep.Find(new Specification<TopForumThread>(tft => tft.TopForumThreadID == id));
                try
                {
                    //rep.Add(new TopForumThread(Guid.NewGuid(),model.TopForumThreadID,model.TopForumID));
                    rep.Remove(tf);
                    rep.PersistAll();
                }
                catch { }
            }
        }
        /// <summary>
        /// 分贴吧获取置顶帖
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<ThreadsDspModel> FetchTopForumThreadDspModel(Guid aid,int count)
        {
            IRepository<TopForumThread> rep = Factory.Factory<IRepository<TopForumThread>>.GetConcrete<TopForumThread>();
            IList<ThreadsDspModel> mylist = new List<ThreadsDspModel>();
            IList<TopForumThread> target = new List<TopForumThread>();
            //创建主题仓储
            IRepository<ForumThread> forumThreadRep = FBS.Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();

            //IAccountRepository accRep = FBS.Factory.Factory<IAccountRepository>.GetConcrete();
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            target = rep.FindAll(new Specification<TopForumThread>(c => c.TopForumID == aid).OrderByDescending(c => c.CreatTime).Skip(0).Take(count));
            foreach (TopForumThread model in target)
            {
                ForumThread k=  forumThreadRep.GetByKey(model.TopForumThreadID);
                mylist.Add(new ThreadsDspModel() { Body=k.RootMessage.MessageVO.Body, ClickCount=k.State.ClickCount, CreationDate=k.CreationDate,ID=k.Id, LastModified=k.ModifiedDate, MessageCount=k.State.MessageCount, Title=k.RootMessage.MessageVO.Subject, UserID=k.RootMessage.Account, UserName=accRep.GetByKey(k.RootMessage.Account).UserName });
            }
            return mylist;
        }

        /// <summary>
        /// 分吧获取置顶帖
        /// </summary>
        /// <param name="fid"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<ThreadsDspModel> FetchTopForumThreadDspModelByForumID(Guid fid, int count)
        {
            IRepository<TopForumThread> rep = Factory.Factory<IRepository<TopForumThread>>.GetConcrete<TopForumThread>();
            IRepository<ThreadRootMessage> msgRep = Factory.Factory<IRepository<ThreadRootMessage>>.GetConcrete<ThreadRootMessage>();

            IList<ThreadsDspModel> mylist = new List<ThreadsDspModel>();
            IList<TopForumThread> target = new List<TopForumThread>();
            //创建主题仓储
            //IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            IRepository<ForumThread> forumThreadRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();

            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            target = rep.FindAll(new Specification<TopForumThread>(c => c.TopForumID == fid).OrderByDescending(c => c.CreatTime).Skip(0).Take(count));
            foreach (TopForumThread model in target)
            {

                ForumThread k = forumThreadRep.GetByKey(model.TopForumThreadID);
                if (k != null)
                {
                    k.RootMessage = msgRep.GetByKey(k.RootMessage.Id);

                    mylist.Add(new ThreadsDspModel() { Body = k.RootMessage.MessageVO.Body, ClickCount = k.State.ClickCount, CreationDate = k.CreationDate, ID = k.Id, LastModified = k.ModifiedDate, MessageCount = k.State.MessageCount, Title = k.RootMessage.MessageVO.Subject, UserID = k.RootMessage.Account, UserName = accRep.GetByKey(k.RootMessage.Account).UserName, ForumID = k.ForumID });

                }
            }
            return mylist;
 
        }
        /// <summary>
        /// 获取所有置顶帖(全局&分吧)
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<ThreadsDspModel> FetchTopForumThreadDspModel(int count)
        {
           IRepository<TopForumThread> rep = Factory.Factory<IRepository<TopForumThread>>.GetConcrete<TopForumThread>();
           IRepository<ThreadRootMessage> msgRep = Factory.Factory<IRepository<ThreadRootMessage>>.GetConcrete<ThreadRootMessage>();

            IList<ThreadsDspModel> mylist = new List<ThreadsDspModel>();
            IList<TopForumThread> target = new List<TopForumThread>();
            //创建主题仓储
            //IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            IRepository<ForumThread> forumThreadRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();

            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            target = rep.FindAll(new Specification<TopForumThread>(c => c.Id!=Guid.Empty).OrderByDescending(c => c.CreatTime).Skip(0).Take(count));
            foreach (TopForumThread model in target)
            {
                
                ForumThread k=  forumThreadRep.GetByKey(model.TopForumThreadID);
                if (k != null)
                {
                    k.RootMessage = msgRep.GetByKey(k.RootMessage.Id);

                    mylist.Add(new ThreadsDspModel() { Body = k.RootMessage.MessageVO.Body, ClickCount = k.State.ClickCount, CreationDate = k.CreationDate, ID = k.Id, LastModified = k.ModifiedDate, MessageCount = k.State.MessageCount, Title = k.RootMessage.MessageVO.Subject, UserID = k.RootMessage.Account, UserName = accRep.GetByKey(k.RootMessage.Account).UserName, ForumID = k.ForumID });

                }
            }
            return mylist;
        }

        /// <summary>
        /// 获取所有置顶帖(全局)
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<ThreadsDspModel> FetchTopForumThreadsDspModel(int count)
        {
            IRepository<TopForumThread> rep = Factory.Factory<IRepository<TopForumThread>>.GetConcrete<TopForumThread>();
            IRepository<ThreadRootMessage> msgRep = Factory.Factory<IRepository<ThreadRootMessage>>.GetConcrete<ThreadRootMessage>();

            IList<ThreadsDspModel> mylist = new List<ThreadsDspModel>();
            IList<TopForumThread> target = new List<TopForumThread>();
            //创建主题仓储
            //IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            IRepository<ForumThread> forumThreadRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();

            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            target = rep.FindAll(new Specification<TopForumThread>(c => c.TopForumID == Guid.Empty).OrderByDescending(c => c.CreatTime).Skip(0).Take(count));
            foreach (TopForumThread model in target)
            {

                ForumThread k = forumThreadRep.GetByKey(model.TopForumThreadID);
                if (k != null)
                {
                    k.RootMessage = msgRep.GetByKey(k.RootMessage.Id);

                    mylist.Add(new ThreadsDspModel() { Body = k.RootMessage.MessageVO.Body, ClickCount = k.State.ClickCount, CreationDate = k.CreationDate, ID = k.Id, LastModified = k.ModifiedDate, MessageCount = k.State.MessageCount, Title = k.RootMessage.MessageVO.Subject, UserID = k.RootMessage.Account, UserName = accRep.GetByKey(k.RootMessage.Account).UserName, ForumID = k.ForumID });

                }
            }
            return mylist;
        }


        public bool CheckTopForumThread(Guid aid)
        {
            IRepository<TopForumThread> rep = Factory.Factory<IRepository<TopForumThread>>.GetConcrete<TopForumThread>();
            IList<ThreadsDspModel> mylist = new List<ThreadsDspModel>();
            if (rep.FindAll(new Specification<TopForumThread>(c => c.TopForumThreadID == aid)).Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
