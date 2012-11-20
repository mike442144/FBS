using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class NewArticleCategoryModel
    {
        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("分类描述")]
        public string Description { get; set; }

        [DisplayName("图标")]
        public string Icon { get; set; }

        [DisplayName("优先级")]
        public uint Priority { get; set; }

        [DisplayName("父分类ID")]
        public Guid ParentID { get; set; }

        [DisplayName("深度")]
        public uint Deepth { get; set; }
    }

    public class NewQuestionCategoryModel
    {
        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("分类描述")]
        public string Description { get; set; }

        [DisplayName("图标")]
        public string Icon { get; set; }

        [DisplayName("优先级")]
        public uint Priority { get; set; }
    }

    public class NewBlogStoryCategoryModel
    {
        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("分类描述")]
        public string Description { get; set; }

        [DisplayName("图标")]
        public string Icon { get; set; }

        [DisplayName("优先级")]
        public uint Priority { get; set; }
    }

    [DisplayName("文章分类详细模型")]
    public class ArticleCategoryDetailsModel
    {
        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("分类编号")]
        public Guid CategoryID { get; set; }

        [DisplayName("图标")]
        public string Icon { get; set; }

        [DisplayName("分类描述")]
        public string Description { get; set; }

        [DisplayName("优先级")]
        public uint Priority { get; set; }

        [DisplayName("父分类编号")]
        public Guid ParentID { get; set; }

        [DisplayName("分类深度")]
        public uint Deepth { get; set; }
    }
}
