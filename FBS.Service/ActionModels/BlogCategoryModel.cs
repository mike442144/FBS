using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class BlogCategoryModel
    {
        [DisplayName("博文分类ID")]
        public Guid BlogCategoryID { set; get; }

        [DisplayName("博文分类名称")]
        public string CategoryName { set; get; }

        [DisplayName("分类图标的名称")]
        public string IconName { set; get; }

        [DisplayName("分类的描述")]
        public string Description { set; get; }

        [DisplayName("分类排序的优先级")]
        public uint OrderPriority { set; get; }
    }
}
