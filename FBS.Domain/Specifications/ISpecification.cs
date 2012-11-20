using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using FBS.Domain.Entity;

namespace FBS.Domain.Specifications
{
    public interface ISpecification<TEntity> where TEntity:class,IEntity
    {
        ISpecification<TEntity> And(ISpecification<TEntity> other);
        ISpecification<TEntity> AndNot(ISpecification<TEntity> other);
        bool IsSatisfiedBy(object obj);
        Expression<Func<TEntity, bool>> GetExpression();
        Expression<Func<TEntity, object>> GetOrderByDescExpression();
        Expression<Func<TEntity, object>> GetOrderByExpression();
        ISpecification<TEntity> Not();
        ISpecification<TEntity> Or(ISpecification<TEntity> other);
        ISpecification<TEntity> Skip(int skip);
        ISpecification<TEntity> Take(int take);
        Int32 Skip();
        Int32 Take();
        ISpecification<TEntity> OrderByDescending(Expression<Func<TEntity, object>> keySelector);
        ISpecification<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector);
    }
}
