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
        Persistence.DbOperation _op;
        AviatorDb<Site> _db = null;
        AviatorDb<SitePage> _pageDb = null;
        AviatorDb<SiteSettings> _settingsDb = null;
        public SiteRepository()
        {
            this._db = new AviatorDb<Site>();
            this._pageDb = new AviatorDb<SitePage>();
            this._settingsDb = new AviatorDb<SiteSettings>();
            this._currentSite = null;
        }

        #region IRepository<Site> 成员

        public void Add(Site entity)
        {
            if (this._currentSite == null)
                this._currentSite = entity;
            else
                throw new Exception("网站已经安装，不可重复操作");
            this._op = Persistence.DbOperation.Insert;
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
            this._op = Persistence.DbOperation.Update;
        }

        public void PersistAll()
        {
            var siteTable=new DataTable();
            
            this._currentSite.AlterToRow(siteTable);
            
            Persistence.TPersist<Site>.PersistAll(siteTable, this._op);
            
            siteTable.Clear();
            siteTable.Dispose();
            siteTable = null;
            if (this._currentSite.Settings != null)
            {
                var settingsTable = new DataTable();
                this._currentSite.Settings.AlterToRow(settingsTable);
                Persistence.TPersist<SiteSettings>.PersistAll(settingsTable, this._op);
                settingsTable.Clear();
                settingsTable.Dispose();
                settingsTable = null;
            }
        }

        #endregion

        public SitePage PageByName(Site site, string name)
        {
            SitePage result = null;
            var sps = this._pageDb.TEntitys.Where(sp => sp.Name == name);
            if (sps != null)
                result = sps.ToList().FirstOrDefault();
            return result;
        }

        public string SiteTheme(Site site)
        {
            var themeName = string.Empty;
            var settings = this._settingsDb.TEntitys.Where(set => set.SiteID == site.Id);
            if (settings != null)
                themeName = settings.ToList().FirstOrDefault().ThemeName;
            return themeName;
        }

        public SiteSettings SiteSettings(Site site)
        {
            var settings = this._settingsDb.TEntitys.Where(set => set.SiteID == site.Id);
            if (settings != null)
                return settings.ToList().FirstOrDefault();
            return null;
        }
    }
}
