using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;
using FBS.Repository.Persistence;

namespace FBS.Repository
{
    public class FeedRepository:IFeedRepository
    {

        #region IRepository<Feed> 成员

        public void Add(Feed entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(ISpecification<Feed> specification)
        {
            throw new NotImplementedException();
        }

        public int Count(ISpecification<Feed> specification)
        {
            throw new NotImplementedException();
        }

        public Feed Find(ISpecification<Feed> specification)
        {
            throw new NotImplementedException();
        }

        public Feed GetByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<Feed> FindAll()
        {
            throw new NotImplementedException();
        }

        public IList<Feed> FindAll(ISpecification<Feed> specification)
        {
            throw new NotImplementedException();
        }

        public void Remove(Feed entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Feed entity)
        {
            throw new NotImplementedException();
        }

        public void PersistAll()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IFeedRepository 成员

        public IList<Feed> FetchFeedsByUserID(Guid uid, int startIndex, int count)
        {
            return FeedPersist.FetchFeeds(uid, startIndex, count);
        }


        public IList<Feed> FetchFeedsByUserID(Guid uid, int startIndex, int count,string type)
        {
            return FeedPersist.FetchFeeds(uid, startIndex, count,type);
        }

        public int GetFriendsFeedsCountByUserID(Guid uid,string type)
        {
          return FeedPersist.GetFriendsFeedsCountByUserID(uid,type);
        }
        #endregion
    }
}
