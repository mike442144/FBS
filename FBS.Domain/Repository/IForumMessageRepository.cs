using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Repository
{
    public interface IForumMessageRepository
    {
        void AddReply(Guid threadId,ForumMessageReply reply);
        void RemoveReply(Guid threadId, ForumMessageReply reply);
        void ModifyReply(Guid threadId, ForumMessageReply reply);
        IList<ForumMessageReply> GetReplyByRootMsgID(Guid rootMsg, int startIndex, int count);
        IList<ForumMessageReply> GetReplyByThreadID(Guid threadId, int startIndex, int count);
        void PersistAll();
        ForumMessageReply GetByKey(Guid key);
    }
}
