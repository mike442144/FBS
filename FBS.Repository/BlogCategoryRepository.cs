using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;

namespace FBS.Repository
{
    public class BlogCategoryRepository:IBlogCategoryRepository
    {
        private AviatorDb<BlogStoryCategory> _db = null;
        public void Add(BlogStoryCategory entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(ISpecification<BlogStoryCategory> specification)
        {
            throw new NotImplementedException();
        }

        public int Count(ISpecification<BlogStoryCategory> specification)
        {
            throw new NotImplementedException();
        }

        public BlogStoryCategory Find(ISpecification<BlogStoryCategory> specification)
        {
            throw new NotImplementedException();
        }

        public BlogStoryCategory GetByKey(Guid id)
        {
            BlogStoryCategory target = null;
            var results = this._db.TEntitys.Where(bc => bc.Id == id);
            var x = results.ToList();
            if (x.Count > 0) 
            {
                target = x[0];
            }
            return target;
        }

        public IList<BlogStoryCategory> FindAll()
        {
            throw new NotImplementedException();
        }

        public IList<BlogStoryCategory> FindAll(ISpecification<BlogStoryCategory> specification)
        {
            throw new NotImplementedException();
        }

        public void Remove(BlogStoryCategory entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(BlogStoryCategory entity)
        {
            throw new NotImplementedException();
        }

        public void PersistAll()
        {
            throw new NotImplementedException();
        }
    }
}
