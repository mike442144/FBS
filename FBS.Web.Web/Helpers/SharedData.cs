using System;
using FBS.Service;
using FBS.Service.ActionModels;

namespace Helpers
{
    /// <summary>
    /// 站点共享信息
    /// </summary>
    public static class SharedData
    {
        static SiteService siteInfoService;
        static SiteSettings siteSettings;
        static SiteCnf sc;
        public static string SiteName { get { return getSiteInfo().SiteName; } }
        public static string CopyRight { get { return getSiteInfo().CopyRight; } }
        public static string Desc { get { return getSiteInfo().SiteDesc; } }
        public static string ThemeName
        {
            get
            {
                if (siteInfoService == null) siteInfoService = new SiteService();
                return siteInfoService.ThemeName();
            }
        }
        static SiteCnf getSiteInfo()
        {
            if (sc == null)
            {
                if (siteInfoService == null)
                    siteInfoService = new SiteService();
                sc = siteInfoService.GetSiteInfo();
            }
            return sc;
        }
    }
}