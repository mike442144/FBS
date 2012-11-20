using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace FBS.Service.ActionModels
{
    public class ArticleCategoryModel
    {
        [DisplayName("新闻分类ID")]
        public Guid CategoryID { set; get; }

        [DisplayName("新闻分类名称")]
        public string CategoryName{set;get;}

        [DisplayName("父分类的ID")]
        public Guid ParentID { set;get; }

        [DisplayName("分类描述")]
        public string Description { set; get; }

        [DisplayName("分类图像")]
        public string IconName { set; get; }

        [DisplayName("分类树的深度")]
        public uint Deepth { set; get; }

        [DisplayName("分类显示的优先级")]
        public uint Priority { set; get; }
    }
}
