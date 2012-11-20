using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;
using FBS.Domain.Specifications;
using FBS.Service.ActionModels;
using FBS.Domain.Security;

namespace FBS.Service
{
    /// <summary>
    /// 焦点数据操作类
    /// </summary>
    //[Security()]
    public class FocuseService : ContextBoundObject
    {
        /// <summary>
        /// 获得精华主题列表
        /// </summary>
        /// <returns></returns>
        public IList<ForumThread> GetDigestThreadList()
        {
            IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            ISpecification<ForumThread> nameSpec = new Specification<ForumThread>(o => o.IsDigest == true);
            return forumThreadRep.FindAll(nameSpec);
        }

        /// <summary>
        /// 获得热门主题列表
        /// </summary>
        /// <returns></returns>
        //[Task("ListForwardItem", "获取列表")]
        public IList<ThreadsDspModel> GetHotThreadList()
        {
            IList<ThreadsDspModel> list = new List<ThreadsDspModel>();
            IForumThreadRepository forumThreadRep = Factory.Factory<IForumThreadRepository>.GetConcrete();
            IAccountRepository accountRep=Factory.Factory<IAccountRepository>.GetConcrete();

            //转换为显示模型
            foreach (ForumThread th in forumThreadRep.FindAll(new Specification<ForumThread>(s=>s.ForumID!=Guid.Empty).Skip(0).Take(5).OrderByDescending(sf=>sf.State.MessageCount)))
            {
                ThreadsDspModel tmp = new ThreadsDspModel();
                tmp.ID = th.Id;
                tmp.ClickCount = th.State.ClickCount;
                tmp.MessageCount = th.State.MessageCount;
                tmp.Title = th.RootMessage.MessageVO.Subject;
                tmp.UserID = th.RootMessage.Account;
                try
                {
                    tmp.UserName = accountRep.GetByKey(th.RootMessage.Account).UserName;
                }
                catch (InvalidOperationException) 
                {

                    ///账户不存在
                    tmp.UserName = "查无此人";
                }

                tmp.LastModified = th.State.ModifiedDate;
                tmp.CreationDate = th.CreationDate;

                list.Add(tmp);
            }
            return list;
        }

        /// <summary>
        /// 获得最近10天内发表的主题列表
        /// </summary>
        /// <returns></returns>
        //[Task("ListForwardItem", "获取列表")]
        public IList<ThreadsDspModel> GetRecentThreadList(int days)
        {
            IList<ThreadsDspModel> list = new List<ThreadsDspModel>();
            IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            //ISpecification<ForumThread> nameSpec = new Specification<ForumThread>(o => o.CreationDate.CompareTo(DateTime.Now.AddDays(-10)) >= 0);
            DateTime formal = DateTime.Now.AddDays(days * (-1));
            IList<ForumThread> forumThreadList = forumThreadRep.GetThread("select * from fbs_ForumThread inner join fbs_Message on fbs_ForumThread.RootMessageID=fbs_Message.MessageID where fbs_ForumThread.CreationDate > '"+formal+"' order by fbs_ForumThread.CreationDate DESC");
            IAccountRepository accRep = FBS.Factory.Factory<IAccountRepository>.GetConcrete();

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
        /// 获得最近10天内发表的主题列表
        /// </summary>
        /// <returns></returns>
        //[Task("ListForwardItem", "获取列表")]
        public IList<ThreadsDspModel> GetRecentThreadList(Guid aid)
        {
            IList<ThreadsDspModel> list = new List<ThreadsDspModel>();
            IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            ISpecification<ForumThread> nameSpec = new Specification<ForumThread>(o => o.CreationDate.CompareTo(DateTime.Now.AddDays(-10)) >= 0&&o.ForumID==aid);
            IList<ForumThread> forumThreadList = forumThreadRep.FindAll(nameSpec);
            IAccountRepository accRep = FBS.Factory.Factory<IAccountRepository>.GetConcrete();

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

                list.Add(temp);
            }
            return list;
        }
    }
}
