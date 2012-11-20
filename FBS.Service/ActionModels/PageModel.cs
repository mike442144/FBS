using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public enum PageType
    {
        Home,
        Article,
        Archive,
        Single
    }

    class PageModel
    {
        public PageModel(PageType type)
        {

        }

        private PageType Type;
        public string Name;
        public string Description;
        public string Title;
        public string Keywords;
    }

    class PageHeader
    { }

    class PageMain
    { }

    class PageFooter
    { }
}
