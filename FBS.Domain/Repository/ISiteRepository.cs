using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Repository
{
    public interface ISiteRepository:IRepository<Site>
    {
        SitePage PageByName(Site site,string name);
        string SiteTheme(Site site);
        SiteSettings SiteSettings(Site site);
    }
}
