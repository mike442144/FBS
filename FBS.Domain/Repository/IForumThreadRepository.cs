using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Repository
{
    public interface IForumThreadRepository:IRepository<ForumThread>
    {
        //分页获取板块下的所有贴子
        IList<ForumThread> GetThreadsInOneForum(Guid forumId, int startIndex, int count);
        //不分页获取板块下所有的贴子
        IList<ForumThread> GetAllThreadsInOneForum(Guid forumId);

        //获取板块置顶帖
        IList<ForumThread> FetchTopThreadsInOneForum(Guid forumId, int startIndex, int count);

        //获取全站置顶帖
        IList<ForumThread> FetchAllTopThreads(int startIndex,int count);

        //获取全站精华帖
        IList<ForumThread> FetchAllEssence();

        //获取板块精华帖
        IList<ForumThread> FetchEssenceInOneForum(Guid forumId, int startIndex, int count);

        IList<ForumThread> GetThread(string cmdText);
    }
}
