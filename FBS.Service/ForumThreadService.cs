using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Utils;
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.ValueObject;
using FBS.Domain.Log;
using FBS.Domain.Specifications;

namespace FBS.Service
{
    /// <summary>
    /// 论坛主题操作类
    /// </summary>
    [Logging()]
    public class ForumThreadService:ContextBoundObject
    {
        /// <summary>
        /// 创建一个主题
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void CreateThread(NewThreadModel model)
        {
            //创建主题仓储
            //IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            //IAccountRepository accountRep = FBS.Factory.Factory<IAccountRepository>.GetConcrete();
            //IForumsRepository forumsRep = FBS.Factory.Factory<IForumsRepository>.GetConcrete();

            IRepository<ForumThread> threadRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();
            IRepository<ThreadRootMessage> msgRep = Factory.Factory<IRepository<ThreadRootMessage>>.GetConcrete<ThreadRootMessage>();

            //Account creater = null;
            ForumThread topic = null;
            ThreadRootMessage rootMsg = null;

            //帖子内容对象
            MessageVO messageVO = new MessageVO();
            messageVO.Body = model.Body;
            messageVO.Subject = model.Title;
            
            try
            {
                //creater = accountRep.GetByKey(model.AccountID);

                rootMsg = new ThreadRootMessage(messageVO, model.AccountID, model.ForumID);
                topic = new ForumThread(rootMsg, model.ForumID);
                

                //topic.RootMessage = rootMsg;

                threadRep.Add(topic);
                msgRep.Add(rootMsg);

                threadRep.PersistAll();
                msgRep.PersistAll();

                //forumThreadRep.Add(topic);
                //forumThreadRep.PersistAll();
            }
            catch
            {
                throw new AddForumThreadException("添加主题至仓储时异常");
            }

        }

        
        /// <summary>
        /// 编辑一个主题的信息
        /// </summary>
        /// <param name="entity">待更新的主题实体</param>
        /// <returns>是否更新成功</returns>
        public void EditThread(UpdateThreadModel entity)
        {
            //创建主题仓储
            IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();

            try
            {
                
            }
            catch
            {
                throw new UpdateForumThreadException("更新主题至仓储时异常");
            }
        }

        /// <summary>
        /// 对主题进行回复
        /// </summary>
        /// <param name="forumThread">主题实体</param>
        /// <param name="forumMessageReply">回复消息实体</param>
        public void ReplyThread(ReplyThreadModel model)
        {
            //IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            //IAccountRepository accountRep = FBS.Factory.Factory<IAccountRepository>.GetConcrete();
            //IForumsRepository forumsRep = FBS.Factory.Factory<IForumsRepository>.GetConcrete();
            //IForumMessageRepository msgRep = FBS.Factory.Factory<IForumMessageRepository>.GetConcrete();

            IRepository<ForumMessage> msgRep = Factory.Factory<IRepository<ForumMessage>>.GetConcrete<ForumMessage>();
            IForumThreadRepository threadRep = Factory.Factory<IForumThreadRepository>.GetConcrete();
            IRepository<Forum> forumRep = Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();
            IRepository<ForumThread> threadWriteRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();
            ForumMessageReply reply = null;
            ThreadRootMessage rootmsg = null;
            Forum forum = null;
            ForumThread thread = null;

            MessageVO mVO = new MessageVO();
            mVO.Body = model.Body;
            

            try
            {
                thread = threadRep.GetByKey(model.ThreadID);
                rootmsg = thread.RootMessage;
                forum = forumRep.GetByKey(model.ForumID);

                
                //主题回复数增加
                thread.AddMessageCount();
                //forum = forumsRep.GetByKey(rootmsg.Forum);
                //poster = accountRep.GetByKey(model.AccountID);


                //回复
                reply = new ForumMessageReply(mVO, model.AccountID, model.ForumID, rootmsg.Id, thread.Id);

                //加入板块的最后发表并对板块回复计数
                forum.AddNewMessage(reply);

                //更新帖子
                thread.ModifiedDate = reply.CreationDate;
                //thread.CreationDate = reply.CreationDate;
                //给主题增加回复
                msgRep.Add(reply);
                msgRep.PersistAll();

                threadWriteRep.Update(thread);
                threadWriteRep.PersistAll();

                forumRep.Update(forum);
                forumRep.PersistAll();

                
                //forumThreadRep.Update(thread);
                //forumThreadRep.PersistAll();
            }
            catch(Exception error) { throw new ReplyForumThreadException("回复失败，原因为：",error);}
        }

        /// <summary>
        /// 回复帖子的回复
        /// </summary>
        /// <param name="model">回复模型</param>
        public void ReplyToRepliedMessage(ReplyRepliedMsgModel model)
        {
            //IForumThreadRepository forumThreadRep = FBS.Factory.Factory<IForumThreadRepository>.GetConcrete();
            //IAccountRepository accountRep = FBS.Factory.Factory<IAccountRepository>.GetConcrete();
            //IForumsRepository forumsRep = FBS.Factory.Factory<IForumsRepository>.GetConcrete();
            //IForumMessageRepository msgRep = FBS.Factory.Factory<IForumMessageRepository>.GetConcrete();

            IForumThreadRepository threadRep = Factory.Factory<IForumThreadRepository>.GetConcrete();
            IRepository<ForumThread> threadWriteRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();

            IRepository<ForumMessageReply> msgRep = Factory.Factory<IRepository<ForumMessageReply>>.GetConcrete<ForumMessageReply>();
            IRepository<Forum> forumRep = Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();
            IRepository<Account> accountRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            //获取欲回复的回复
            ForumMessageReply readyToReply=msgRep.GetByKey(model.ReadyToReplyMessageID);
            ForumThread thread = threadRep.GetByKey(model.ThreadID);
            Forum forum = forumRep.GetByKey(thread.ForumID);
            //板块
            //Forum forum = forumsRep.GetByKey(readyToReply.Forum);

            //消息对象
            MessageVO mVO = new MessageVO();

            switch (model.ReplyType)
            {
                
                case ReplyType.Quote:
                    mVO.Body = "<div class=\"quote\">" + "@<a href='" + FBS.Utils.UrlFactory.CreateUrl(UrlFactory.PageName.UserHome, readyToReply.Account.ToString()) + "' target='_blank'>" + accountRep.GetByKey(readyToReply.Account).UserName + "</a><br />" + readyToReply.MessageVO.Body + "</div>" + model.Body;
                    break;
                default:
                    mVO.Body = "<div class=\"reply\">" + "@<a href='" + FBS.Utils.UrlFactory.CreateUrl(UrlFactory.PageName.UserHome, readyToReply.Account.ToString()) + "' target='_blank'>" + accountRep.GetByKey(readyToReply.Account).UserName + "</a><br />" + readyToReply.MessageVO.Body + "</div>" + model.Body;
                    break;
            }

            ForumMessageReply newReply = new ForumMessageReply(mVO, model.User.UserID, forum.Id, model.ReadyToReplyMessageID, model.ThreadID);

            //更新"吧"的帖子数
            forum.AddNewMessage(newReply);
            forumRep.Update(forum);
            forumRep.PersistAll();

            //更新主题的最后更新时间
            thread.ModifiedDate = newReply.CreationDate;
            thread.AddMessageCount();
            threadWriteRep.Update(thread);
            threadWriteRep.PersistAll();

            //向库中增加回帖
            msgRep.Add(newReply);
            msgRep.PersistAll();
        }

        //将主题置顶
        public void SetThreadTop()
        {

        }

        /// <summary>
        /// 读取主题所有内容（主贴+回复）
        /// </summary>
        /// <param name="id">主题编号</param>
        public ThreadDetailsModel GetThreadAllContent(Guid id, int pageIndex)
        {
            //IForumThreadRepository threadsRep = Factory.Factory<IForumThreadRepository>.GetConcrete();
            //IAccountRepository accountRep = Factory.Factory<IAccountRepository>.GetConcrete();
            //IForumMessageRepository msgRep = Factory.Factory<IForumMessageRepository>.GetConcrete();
            //IForumsRepository forumsRep = Factory.Factory<IForumsRepository>.GetConcrete();

            //主题读取仓储
            IForumThreadRepository threadRep = Factory.Factory<IForumThreadRepository>.GetConcrete();
            //主题更改仓储
            IRepository<ForumThread> threadWriteRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();

            IRepository<ForumMessageReply> msgRep = Factory.Factory<IRepository<ForumMessageReply>>.GetConcrete<ForumMessageReply>();
            IRepository<Forum> forumRep = Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();
            IRepository<Account> accountRep=Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            //IRepository<ForumMessageReply> replyRep = Factory.Factory<IRepository<ForumMessageReply>>.GetConcrete<ForumMessageReply>();

            ForumThread thread = null;
            thread = threadRep.GetByKey(id);
            //thread.RootMessage = msgRep.GetByKey(thread.RootMessage.Id);

            //不存在该主题
            if (thread == null)
                return null;

            Forum forum = forumRep.GetByKey(thread.ForumID);

            //要返回的对象，包括根帖及回复
            ThreadDetailsModel target = new ThreadDetailsModel();

            target.ClickCount = thread.State.ClickCount;
            target.Body = thread.RootMessage.MessageVO.Body;
            target.CreationDate = thread.CreationDate;
            target.MessageCount = thread.State.MessageCount;
            target.Title = thread.RootMessage.MessageVO.Subject;
            target.UserId = thread.RootMessage.Account;
            target.RootMessageID = thread.RootMessage.Id;
            target.ForumID = thread.ForumID;
            target.ForumName = forum.Name;
            target.UserName = accountRep.GetByKey(thread.RootMessage.Account).UserName;
            target.LastModified = thread.State.ModifiedDate;
            target.ForumMessageSum = forum.ThreadCount;

            //创建回复列表
            IList<ThreadsDspModel> list = new List<ThreadsDspModel>();

            //从仓储中取出
            IList<ForumMessageReply> replylist = msgRep.FindAll(new Specification<ForumMessageReply>(m => m.ForumThreadID == thread.Id).OrderBy(m => m.CreationDate).Skip((pageIndex - 1) * 15).Take(15));
            //IList<ForumMessageReply> replylist = msgRep.GetReplyByRootMsgID(thread.RootMessage.Id, (pageIndex-1)*15, 15);
            
            //转换回复
            foreach (ForumMessage msg in replylist)
            {
                if (msg.Id == thread.RootMessage.Id)
                    continue;
                ThreadsDspModel model = new ThreadsDspModel();
                model.Body = msg.MessageVO.Body;
                model.CreationDate = msg.CreationDate;
                model.UserID = msg.Account;
                model.UserName = accountRep.GetByKey(msg.Account).UserName;
                model.MessageID = msg.Id;
                list.Add(model);
            }

            //加到帖子详细模型中
            target.ReplyList = list;


            //点击增加计数
            thread.AddClickCount();
            //更新thread
            threadWriteRep.Update(thread);
            //持久化
            threadWriteRep.PersistAll();

            
            return target;
        }

         /// <summary>
        /// 获取所有精华帖
        /// </summary>
        /// <param name="id">主题编号</param>
        public IList<ForumThread> GetHotThreadContent()
        {
            //IForumThreadRepository threadsRep = Factory.Factory<IForumThreadRepository>.GetConcrete();
            //return threadsRep.FetchAllEssence();
            throw new NotImplementedException("获取精华帖方法未实现");
        }

    }
}
