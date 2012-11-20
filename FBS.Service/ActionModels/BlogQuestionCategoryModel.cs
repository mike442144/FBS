using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class BlogQuestionCategoryModel
    {
        [DisplayName("博客问题分类ID")]
        public Guid QuestionCategory { set; get; }

        [DisplayName("博客问题分类名称")]
        public string CategoryName { set; get; }

        [DisplayName("博客问题分类描述")]
        public string Description { set; get; }

        [DisplayName("博客问题分类图像")]
        public string IconName { set; get; }

        [DisplayName("博客问题分类排序优先级")]
        public uint OrderPriority { set; get; }


    }
}
