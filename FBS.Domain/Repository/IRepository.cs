using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Specifications;

namespace FBS.Domain.Repository
{
    public interface IRepository<TEntity> where TEntity:class,IAggregateRoot
    {
        void Add(TEntity entity);
        bool Exists(ISpecification<TEntity> specification);
        int Count(ISpecification<TEntity> specification); 
        TEntity Find(ISpecification<TEntity> specification);
        TEntity GetByKey(Guid id);
        IList<TEntity> FindAll();
        IList<TEntity> FindAll(ISpecification<TEntity> specification);
        void Remove(TEntity entity);
        void RemoveByKey(Guid id);
        void Update(TEntity entity);
        void PersistAll();
    }
}