using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;
using FBS.Domain.Specifications;
using System.Data;

namespace FBS.Repository
{
    public class SiteRepository : ISiteRepository
    {
        private Site _currentSite;
        string _op = string.Empty;
        AviatorDb<Site> _db = null;
        public SiteRepository()
        {
            this._db = new AviatorDb<Site>();
            this._currentSite = null;
        }

        #region IRepository<Site> 成员

        public void Add(Site entity)
        {
            if (this._currentSite == null)
                this._currentSite = entity;
            else
                throw new Exception("网站已经安装，不可重复操作");
            this._op = "add";
        }

        public bool Exists(ISpecification<Site> specification)
        {
            bool isExists = false;
            var a = this._db.TEntitys.Where(specification.GetExpression().Compile());
            if (a != null)
            {
                isExists = !(a.ToList().Count() == 0);
            }

            return isExists;
        }

        public int Count(ISpecification<Site> specification)
        {
            throw new NotImplementedException();
        }

        public Site Find(ISpecification<Site> specification)
        {
            Site entity = null;

            var x= this._db.TEntitys.Where(specification.GetExpression().Compile());
            if (null != x && x.Count() != 0)
                entity = x.ToList()[0];

            return entity;
        }

        public Site GetByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public IList<Site> FindAll()
        {
            throw new NotImplementedException();
        }

        public IList<Site> FindAll(ISpecification<Site> specification)
        {
            throw new NotImplementedException();
        }

        public void Remove(Site entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Site entity)
        {
            this._currentSite = entity;
            this._op = "up";
        }

        public void PersistAll()
        {
            DataTable table=new DataTable();
            this._currentSite.AlterToRow(table);
            if (this._op == "up")
                Persistence.SitePersist.PersistAll(table, Persistence.DbOperation.Update);
            else if (this._op == "add")
                Persistence.SitePersist.PersistAll(table, Persistence.DbOperation.Insert);

            table.Clear();
        }

        #endregion
    }
}
