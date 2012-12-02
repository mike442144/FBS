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
    public class SiteInfoService
    {
        public SiteInfoService()
        {
            this._rep = Factory.Factory<ISiteRepository>.GetConcrete();
        }

        /// <summary>
        /// 初始化网站信息
        /// </summary>
        /// <param name="cnf">初始化网站信息</param>
        public void InitSiteInfo(StepOfSiteCnf cnf)
        {
            
            if (!this._rep.Exists(new Specification<Site>(s=>s.Id!=Guid.Empty)))
            {
                Site site = new Site(cnf.SiteName, cnf.SiteDesc, cnf.SiteUrl, cnf.CopyRight, cnf.Version.ToString());
                this._rep.Add(site);
                this._rep.PersistAll();
            }
            else
                throw new Exception("网站已安装，请勿重复操作！");
        }

        /// <summary>
        /// 获取网站基本信息
        /// </summary>
        /// <returns></returns>
        public StepOfSiteCnf GetSiteInfo()
        {
            StepOfSiteCnf info = new StepOfSiteCnf();

            var site = this._rep.Find(new Specification<Site>(s => s.Id != Guid.Empty));
            if (site != null)
            {
                info.SiteName = site.SiteName;
                info.SiteDesc = site.SiteDescription;
                info.SiteUrl = site.Siteurl;
                info.Version = new Version(site.Version);
            }

            return info;

        }

        private ISiteRepository _rep;

    }
}
