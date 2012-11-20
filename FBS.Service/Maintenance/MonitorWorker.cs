using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using System.Threading;
using FBS.Utils;

namespace FBS.Service.Maintenance
{
    public class MonitorWorker
    {
        public void Moniter()
        {
            while (true)
            {
                LoggerHelper.Info("Monitor thread starting...");

                IAccountRepository accountRep = Factory.Factory<IAccountRepository>.GetConcrete();
                IForumThreadRepository threadRep = Factory.Factory<IForumThreadRepository>.GetConcrete();
                IForumMessageRepository msgRep = Factory.Factory<IForumMessageRepository>.GetConcrete();
                IBlogStoryRepository blogRep = Factory.Factory<IBlogStoryRepository>.GetConcrete();
                IForumsRepository forumRep = Factory.Factory<IForumsRepository>.GetConcrete();

                accountRep.PersistAll();
                threadRep.PersistAll();
                msgRep.PersistAll();
                blogRep.PersistAll();
                forumRep.PersistAll();

                LoggerHelper.Info("Monitor thread completing persist...");
                Thread.Sleep(6000000);
            }
        }
    }
}
