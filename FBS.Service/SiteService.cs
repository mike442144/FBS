using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;

namespace FBS.Service
{
    public class SiteService
    {
        public SiteService()
        {
            this._rep = Factory.Factory<ISiteRepository>.GetConcrete();
        }

        /// <summary>
        /// 设置网站信息
        /// </summary>
        /// <param name="cnf">设置网站信息</param>
        public void SetSiteInfo(SiteCnf cnf)
        {
            this._site = this._rep.Find(new Specification<Site>(s => s.Id != Guid.Empty));
            if (this._site == null)
            {
                this._site = new Site(cnf.SiteName, cnf.SiteDesc, cnf.SiteUrl, cnf.CopyRight, cnf.Version.ToString());
                this._rep.Add(this._site);
            }
            else
            {
                this._site.SiteName = cnf.SiteName;
                this._site.SiteDescription = cnf.SiteDesc;
                this._site.Siteurl = cnf.SiteUrl;
                this._site.Copyright = cnf.CopyRight;

                this._rep.Update(this._site);
            }
            this._rep.PersistAll();
        }

        /// <summary>
        /// 获取网站基本信息
        /// </summary>
        /// <returns></returns>
        public SiteCnf GetSiteInfo()
        {
            SiteCnf info = new SiteCnf();

            this._site = this._rep.Find(new Specification<Site>(s => s.Id != Guid.Empty));
            if (this._site != null)
            {
                info.SiteName = this._site.SiteName;
                info.SiteDesc = this._site.SiteDescription;
                info.SiteUrl = this._site.Siteurl;
                info.CopyRight = this._site.Copyright;
                info.Version = new Version(this._site.Version);
            }

            return info;

        }
        public SitePage PageByName(string pageName) 
        {
            loadSite();
            return this._rep.PageByName(this._site, pageName);
        }
        private void loadSite() 
        {
            if(this._site==null)
                this._site=this._rep.Find(new Specification<Site>(s => s.Id != Guid.Empty));
        }

        public void ChangeTheme(string name)
        {
            loadSite();
            if(this._site.Settings == null)
                this._site.Settings = this._rep.SiteSettings(this._site);
            if (this._site.Settings != null)
                this._site.Settings.ThemeName = name;
            this._rep.Update(this._site);
            this._rep.PersistAll();
        }
        public string ThemeName()
        {
            loadSite();
            return this._rep.SiteTheme(this._site);
        }
        private ISiteRepository _rep;
        private Site _site;
    }
}
