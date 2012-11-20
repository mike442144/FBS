using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;
using FBS.Domain.Repository;
using FBS.Service.ActionModels;
using FBS.Domain.Log;

namespace FBS.Service
{
    /// <summary>
    /// 论坛板块操作类
    /// </summary>
    [Logging()]
    public class ForumService:ContextBoundObject
    {
        //获得板块简介
        public IList<ForumDspModel> GetForumsIntroduce()
        {
            IList<ForumDspModel> list = new List<ForumDspModel>();
            IRepository<Forum> forumRep = Factory.Factory<IRepository<Forum>>.GetConcrete <Forum>();
            int Count = forumRep.FindAll().Count;
            IList<Forum> forumList = forumRep.FindAll(new Specification<Forum>(s => s.Id != Guid.Empty).Skip(0).Take(Count).OrderBy(st => st.Priority));

            foreach (Forum forum in forumList)
            {
                ForumDspModel temp = new ForumDspModel();
                temp.ID = forum.Id;
                temp.ForumName = forum.Name;
                temp.CreationTime = forum.CreationDate;
                temp.ModifiedTime = forum.ModifiedDate;
                temp.ThreadCount = forum.ThreadCount;
                temp.Priority = forum.Priority;
                list.Add(temp);
            }
            return list;
            //TODO
            //获得所有的Forum实体
        }
        /// <summary>
        /// 根据板块ID获得板块中所有的主题列表
        /// </summary>
        /// <param name="forumID"></param>
        /// <returns></returns>
        public IList<ThreadsDspModel> GetAllForumThreadListByForumID(Guid forumID)
        {
            IList<ThreadsDspModel> list = new List<ThreadsDspModel>();
            //创建主题仓储
            //IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();

            IForumThreadRepository forumThreadRep = Factory.Factory<IForumThreadRepository>.GetConcrete();
            IRepository<ForumMessage> msgRep = Factory.Factory<IRepository<ForumMessage>>.GetConcrete<ForumMessage>();

            //IList<ForumThread> forumThreadList = forumThreadRep.GetAllThreadsInOneForum(forumID);
            //IList<ForumThread> forumThreadList = forumThreadRep.FindAll(new Specification<ForumThread>(t => t.ForumID == forumID).OrderByDescending(t => t.CreationDate).Skip(0).Take(100));
            IList<ForumThread> forumThreadList = forumThreadRep.GetThreadsInOneForum(forumID, 0, 100);
            IRepository<Account> accRep = FBS.Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
           
            foreach (ForumThread ft in forumThreadList)
            {
                ThreadsDspModel temp = new ThreadsDspModel();
                temp.ID = ft.Id;
                temp.ClickCount = ft.State.ClickCount;
                temp.MessageCount = ft.State.MessageCount;
                temp.Title = ft.RootMessage.MessageVO.Subject;
                temp.UserID = ft.RootMessage.Account;
                temp.ForumID = ft.ForumID;
                try
                {
                    temp.UserName = accRep.GetByKey(ft.RootMessage.Account).UserName;
                }
                catch (InvalidOperationException)
                {
                    temp.UserName = "查无此人";
                }
                temp.LastModified = ft.State.ModifiedDate;
                temp.CreationDate = ft.CreationDate;
                list.Add(temp);
            }
            return list;
 
        }
        /// <summary>
        /// 根据板块ID获得板块中的主题列表(分页)
        /// </summary>
        /// <param name="forumID">板块ID</param>
        /// <returns>主题列表</returns>
        public IList<ThreadsDspModel> GetForumThreadListByForumID(Guid forumID,int startIndex,int count)
        {
            IList<ThreadsDspModel> list = new List<ThreadsDspModel>();
            //创建主题仓储
            IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();

            IList<ForumThread> forumThreadList = forumThreadRep.GetThreadsInOneForum(forumID, startIndex, count);
            IAccountRepository accRep = FBS.Factory.Factory<IAccountRepository>.GetConcrete();
      //      return forumThreadRep.FindAll();

            foreach (ForumThread ft in forumThreadList)
            {
                ThreadsDspModel temp = new ThreadsDspModel();
                temp.ID = ft.Id;
                temp.ClickCount = ft.State.ClickCount;
                temp.MessageCount = ft.State.MessageCount;
                temp.Title = ft.RootMessage.MessageVO.Subject;
                temp.UserID = ft.RootMessage.Account;
                try
                {
                    temp.UserName = accRep.GetByKey(ft.RootMessage.Account).UserName;
                }
                catch (InvalidOperationException)
                {
                    temp.UserName = "查无此人";
                }
                temp.LastModified = ft.State.ModifiedDate;
                temp.CreationDate = ft.CreationDate;
                temp.ForumID = ft.ForumID;
                list.Add(temp);
            }
            return list;
        }
        /// <summary>
        /// 创建贴吧
        /// </summary>
        /// <param name="model"></param>
        public void CreateForum(NewForumModel model)
        {
            IRepository<Forum> forumRep = Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();

            //IForumsRepository forumRep = FBS.Factory.Factory<IForumsRepository>.GetConcrete();

            Forum fo = new Forum(model.ForumName,model.ForumDsp,model.Priority);
            forumRep.Add(fo);
            forumRep.PersistAll();
        }

        /// <summary>
        /// 修改贴吧板块
        /// </summary>
        /// <param name="model"></param>
        public void ModifyForum(ForumDspModel model)
        {
            IRepository<Forum> forumRep = FBS.Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();
            Forum fo = null;
            fo= forumRep.GetByKey(model.ID);
            fo.CreationDate = DateTime.Now;
            fo.Dsp = model.ForumDsp;
            fo.Name = model.ForumName;
            fo.ThreadCount = model.ThreadCount;
            fo.CreationDate = model.CreationTime;
            fo.Priority = model.Priority;
            forumRep.Update(fo);
            forumRep.PersistAll();
        }

        /// <summary>
        /// 此函数未使用
        /// 获取贴吧含分页
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<ForumDspModel> FetchForumDspModel(int index,int count)
        {
            IList<ForumDspModel> mylist = new List<ForumDspModel>();
            IForumsRepository forumRep = FBS.Factory.Factory<IForumsRepository>.GetConcrete();
            foreach(Forum forum in forumRep.FindAll(new Specification<Forum>(c => c.Id != null).OrderBy(c => c.CreationDate).Skip(index).Take(count)))
            {
                ForumDspModel model = new ForumDspModel() { CreationTime=forum.CreationDate, ForumDsp=forum.Dsp, ForumName=forum.Name, ModifiedTime=forum.ModifiedDate, ID=forum.Id, ThreadCount=forum.ThreadCount};
                mylist.Add(model);
            }
            return mylist;
        }
        /// <summary>
        /// 获取所有贴吧吧
        /// </summary>
        /// <returns></returns>
        public IList<ForumDspModel> FetchForumDspModel()
        {
            IList<ForumDspModel> mylist = new List<ForumDspModel>();
            IRepository<Forum> forumRep = FBS.Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();
            foreach (Forum forum in forumRep.FindAll())
            {
                ForumDspModel model = new ForumDspModel() { Priority=forum.Priority, CreationTime = forum.CreationDate, ForumDsp = forum.Dsp, ForumName = forum.Name, ModifiedTime = forum.ModifiedDate, ID = forum.Id, ThreadCount = forum.ThreadCount };
                mylist.Add(model);
            }
            return mylist;
        }
        /// <summary>
        /// 删除贴吧
        /// </summary>
        /// <param name="aid">贴吧编号</param>
        public void RemoveForumByKey(Guid aid)
        {
            IRepository<TopForumThread> tftRep = Factory.Factory<IRepository<TopForumThread>>.GetConcrete<TopForumThread>();
            IRepository<Forum> forumRep = FBS.Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();
           IRepository<ForumThread> threadRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();
           // IForumThreadRepository threadRep = Factory.Factory<IForumThreadRepository>.GetConcrete<ForumThread>();
           
            //IRepository<ForumMessageReply> msgrRep = Factory.Factory<IRepository<ForumMessageReply>>.GetConcrete<ForumMessageReply>();
           IRepository<ThreadRootMessage> trmRep = Factory.Factory<IRepository<ThreadRootMessage>>.GetConcrete<ThreadRootMessage>();
           IList<ThreadRootMessage> trmList = trmRep.FindAll(new Specification<ThreadRootMessage>(t => t.Forum == aid));
            if (trmList != null)
            {
                foreach (ThreadRootMessage trm in trmList)
                {
                    trmRep.Remove(trm);
                }
                trmRep.PersistAll();
            }            IRepository<ForumMessageReply> fmrRep = Factory.Factory<IRepository<ForumMessageReply>>.GetConcrete<ForumMessageReply>();
            IList<ForumMessageReply> fmrList = fmrRep.FindAll(new Specification<ForumMessageReply>(f => f.Forum == aid));
            if (fmrList != null)
            {
                foreach (ForumMessageReply fmr in fmrList)
                {
                    fmrRep.Remove(fmr);
                }
                fmrRep.PersistAll();
            }
   
            IList<ForumThread> fThreadList = threadRep.FindAll(new Specification<ForumThread>(ft => ft.ForumID == aid));
            if(fThreadList!=null)
            {
                foreach (ForumThread ft in fThreadList)
                {
                    threadRep.RemoveByKey(ft.Id);
                    TopForumThread tft = tftRep.Find(new Specification<TopForumThread>(tftha => tftha.TopForumThreadID == ft.Id));
                    if (null != tft)
                    {
                        tftRep.Remove(tft);
                        tftRep.PersistAll();
                    }

                }
                threadRep.PersistAll();
            }


            
            
            
            
            
            forumRep.Remove(forumRep.GetByKey(aid));
            forumRep.PersistAll();
        }
        /// <summary>
        /// 通过编号获取贴吧
        /// </summary>
        /// <param name="id">贴吧编号</param>
        /// <returns></returns>
        public ForumDspModel GetForumDspModelByID(Guid id)
        {
            IRepository<Forum> forumRep = FBS.Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();
            Forum fo= forumRep.GetByKey(id);
            ForumDspModel model = new ForumDspModel();
            if (fo != null)
            {
                model.ForumDsp = fo.Dsp;
                model.ForumName = fo.Name;
                model.ID = fo.Id;
                model.ModifiedTime = fo.ModifiedDate;
                model.ThreadCount = fo.ThreadCount;
                model.CreationTime = fo.CreationDate;
                model.Priority = fo.Priority;
            }
            return model;
        }
    }
}
