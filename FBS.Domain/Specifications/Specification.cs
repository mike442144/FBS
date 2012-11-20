using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using FBS.Domain.Entity;

namespace FBS.Domain.Specifications
{
    public class Specification<TEntity>:ISpecification<TEntity> where TEntity:class,IEntity
    {

        private string _name;
        private int _skip=0;
        private int _take=0;
        private Expression<Func<TEntity, bool>> _expression;
        private Expression<Func<TEntity, object>> _orderbyDescExpression;
        private Expression<Func<TEntity, object>> _orderbyExpression;

        public Specification(string name)
        {
            this._name = name;
        }

        public Specification(Expression<Func<TEntity, bool>> expression)
        {
            this._expression = expression;
        }


        #region ISpecification 成员

        public ISpecification<TEntity> And(ISpecification<TEntity> other)
        {
            //ISpecification<TEntity> spec = new Specification<TEntity>(Expression<Func<TEntity,bool>>.And(this._expression, other.GetExpression()));
            
            //BinaryExpression be = Expression<Func<TEntity, bool>>.And(this._expression, other.GetExpression());
            //ISpecification<TEntity> sp = new Specification<TEntity>(be);
            return null;
        }

        public ISpecification<TEntity> AndNot(ISpecification<TEntity> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSatisfiedBy(object obj)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<TEntity, bool>> GetExpression()
        {
            return this._expression;
        }

        public Expression<Func<TEntity, object>> GetOrderByExpression()
        {
            return this._orderbyExpression;
        }

        public ISpecification<TEntity> Not()
        {
            throw new NotImplementedException();
        }

        public ISpecification<TEntity> Or(ISpecification<TEntity> other)
        {
            throw new NotImplementedException();
        }

        public ISpecification<TEntity> Skip(int skip)
        {
            //throw new NotImplementedException();
            this._skip = skip;
            return this;
        }

        public ISpecification<TEntity> Take(int take)
        {
            this._take = take;
            return this;
        }

        public int Skip()
        {
            return this._skip;
        }

        public int Take()
        {
            return this._take;
        }

        public ISpecification<TEntity> OrderByDescending(Expression<Func<TEntity, object>> keySelector)
        {
            this._orderbyDescExpression = keySelector;
            return this;
        }

        public Expression<Func<TEntity, object>> GetOrderByDescExpression()
        {
            return this._orderbyDescExpression;
        }

        public ISpecification<TEntity> OrderBy(Expression<Func<TEntity, object>> keySelector)
        {
            this._orderbyExpression = keySelector;
            return this;
        }
        #endregion
    }
}
