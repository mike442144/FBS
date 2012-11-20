using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Repository
{
    public interface IFeedRepository:IRepository<Feed>
    {
        IList<Feed> FetchFeedsByUserID(Guid uid,int startIndex,int count);
        IList<Feed> FetchFeedsByUserID(Guid uid, int startIndex, int count,string type);
        int GetFriendsFeedsCountByUserID(Guid uid, string type);
    }
}
