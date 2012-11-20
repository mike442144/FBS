using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Web.Share
{
    public interface ISiteHtmlParser
    {
        void ParseHtml(string url, ref ShareThread shareThread);
    }
}
