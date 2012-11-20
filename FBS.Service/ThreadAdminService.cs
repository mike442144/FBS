using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Security;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Service
{
    
    public class ThreadAdminService
    {
        //将主题置顶或取消置顶
        public void SetThreadTop()
        {
        }

        //将主题设为精华或取消精华
        public void SetThreadDigest()
        {
        }

        //删除主题
        public void DeleteThread(Guid tId)
        {
            IRepository<ForumThread> threadRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();

            threadRep.RemoveByKey(tId);

            threadRep.PersistAll();
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="mId"></param>
        public void DeleteMessage(Guid mId)
        {
            IRepository<ForumMessageReply> msgRep = Factory.Factory<IRepository<ForumMessageReply>>.GetConcrete<ForumMessageReply>();
            IRepository<ForumThread> threadRep = Factory.Factory<IRepository<ForumThread>>.GetConcrete<ForumThread>();
            //IRepository<Forum> forumRep = Factory.Factory<IRepository<Forum>>.GetConcrete<Forum>();

            

            ForumMessageReply re= msgRep.GetByKey(mId);
            ForumThread thread= threadRep.GetByKey(re.ForumThreadID);
            msgRep.RemoveByKey(mId);
            msgRep.PersistAll();
            thread.reduceMessageCount();
            threadRep.Update(thread);
            threadRep.PersistAll();
            //forumRep.Update(forum);
            //forumRep.PersistAll();
        }

        //移动主题到指定板块
        public void MoveThread()
        {
        }

        public bool IsTiebaAdmin(Guid id)
        {
            bool flag = false;

            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account acc = null;
            acc = accRep.GetByKey(id);
            if (acc == null)
                throw new Exception("用户不存在");//账户不存在
            if(acc.Roles!=null&&acc.Roles.Length>0)
                if (acc.Roles == "ForumAdmin" || acc.Roles == "MasterAdmin")
                    flag = true;

            return flag;
        }
    }
}
