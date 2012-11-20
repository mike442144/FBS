using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Specifications;
using FBS.Domain.Repository;
using FBS.Domain.Entity;
using System.Data;
using FBS.Repository.Persistence;

namespace FBS.Repository
{
    public class TRepository<TEntity>:IRepository<TEntity> where TEntity :class,IAggregateRoot
    {
        HashSet<TEntity> _list = null;
        HashSet<TEntity> _newList = null;
        HashSet<TEntity> _modifiedList = null; 
        HashSet<TEntity> _removedList = null;
        AviatorDb<TEntity> _db = null;

        public TRepository()
        {
            this._list = new HashSet<TEntity>();
            this._newList = new HashSet<TEntity>();
            this._modifiedList = new HashSet<TEntity>();
            this._removedList = new HashSet<TEntity>();
            this._db = new AviatorDb<TEntity>();
        }



        #region IRepository<TEntity> 成员

        public void Add(TEntity entity)
        {
            this._list.Add(entity);
            this._newList.Add(entity);
        }

        /// <summary>
        /// 判断是否存在满足条件的对象
        /// </summary>
        /// <param name="specification">规约</param>
        /// <returns></returns>
        public bool Exists(ISpecification<TEntity> specification)
        {
            //return this._list.Any(specification.GetExpression().Compile());
            bool isExists = false;
            var a=this._db.TEntitys.Where(specification.GetExpression().Compile());
            if (a != null)
            {
                isExists = !(a.ToList().Count() == 0);
            }

            return isExists;
        }

        public TEntity Find(ISpecification<TEntity> specification)
        {
            TEntity entity = null;
            //try
            //{
            //    entity = this._list.SingleOrDefault(specification.GetExpression().Compile());
            //}
            //catch (InvalidOperationException) { }

            //return entity;

            var x = this._db.TEntitys.Where(specification.GetExpression());
            if (null != x && x.Count() != 0)
                entity = x.ToList()[0];

            return entity;
        }

        /// <summary>
        /// 按键值获取
        /// </summary>
        /// <param name="id">键值</param>
        /// <returns>实例</returns>
        /// 若找不到指定键值的实例则返回null
        public TEntity GetByKey(Guid id)
        {
            TEntity entity = null;
            try
            {
                //entity = this._list.Single(t => t.Id == id);
                var l = this._db.TEntitys.Where(t => t.Id == id);
                var x=l.ToList();
                if (x.Count != 0)
                    entity = x[0];
            }
            catch (InvalidOperationException) { }

            return entity;
        }

        public IList<TEntity> FindAll()
        {
            return this._db.TEntitys.ToList();
        }

        public IList<TEntity> FindAll(ISpecification<TEntity> specification)
        {
            //return this._list.TakeWhile(specification.GetExpression().Compile()).ToList();
            IList<TEntity> target = null;
            if (specification.Skip() != 0 || specification.Take() != 0)
            {
                if (specification.GetOrderByDescExpression() != null)
                    target = this._db.TEntitys.Where(specification.GetExpression()).Skip(specification.Skip()).OrderByDescending(specification.GetOrderByDescExpression()).Take(specification.Take()).ToList();
                else
                    target = this._db.TEntitys.Where(specification.GetExpression()).Skip(specification.Skip()).OrderBy(specification.GetOrderByExpression()).Take(specification.Take()).ToList();
            }
            else
                target = this._db.TEntitys.Where(specification.GetExpression()).ToList();

            return target;
        }

        public void Remove(TEntity entity)
        {
            //从集合中删除实例
            this._list.Remove(entity);

            //添加到已删除列表
            this._removedList.Add(entity);
        }

        public void Update(TEntity entity)
        {
            //更新集合中实例
            this._list.Remove(entity);
            this._list.Add(entity);

            //添加到已更新列表
            this._modifiedList.Add(entity);
        }

        public void PersistAll()
        {
            //新实例数据表
            DataTable newTable = new DataTable("NewTable");

            //修改的实例数据表
            DataTable modifiedTable = new DataTable("ModifiedTable");

            //删除的实例数据表
            DataTable removedTable = new DataTable("removedTable");

            foreach (TEntity entity in this._newList)
                entity.AlterToRow(newTable);

            foreach (TEntity entity in this._modifiedList)
                entity.AlterToRow(modifiedTable);

            foreach (TEntity entity in this._removedList)
                entity.AlterToRow(removedTable);


            if(newTable.Rows.Count!=0)
                TPersist<TEntity>.PersistAll(newTable,DbOperation.Insert);

            if(modifiedTable.Rows.Count!=0)
                TPersist<TEntity>.PersistAll(modifiedTable, DbOperation.Update);

            if(removedTable.Rows.Count!=0)
                TPersist<TEntity>.PersistAll(removedTable, DbOperation.Delete);


            this._newList.Clear();
            this._modifiedList.Clear();
            this._removedList.Clear();
            
        }


        public void RemoveByKey(Guid id)
        {
            TEntity tmp=null;
            
            foreach (TEntity entity in this._list)
                if (entity.Id == id)
                {
                    tmp = entity;
                    break;
                }
            if (tmp == null)
            {
                var x = this._db.TEntitys.Where(t => t.Id == id).ToList();
                if(x.Count!=0)
                    tmp = x[0];
            }

            if (tmp != null)
            {
                this._list.Remove(tmp);
                this._removedList.Add(tmp);
            }
        }

        public int Count(ISpecification<TEntity> specification)
        {
            return this._db.TEntitys.Where(specification.GetExpression()).Count();
        }

        #endregion
    }
}
