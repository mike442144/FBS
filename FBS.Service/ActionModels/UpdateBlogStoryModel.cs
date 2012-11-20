using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("更新博文模型")]
    class UpdateBlogStoryModel
    {
        [DisplayName("博文标题")]
        public string Title { get; set; }

        [DisplayName("博文内容")]
        public string Body { get; set; }

        [DisplayName("修改人ID")]
        public Guid AccountID { get; set; }

        [DisplayName("是否申请精华")]
        public bool IsRequireDigest { get; set; }

        [DisplayName("文章分类")]
        public List<string> Categories { get; set; }

        [DisplayName("文章标签")]
        public List<string> Tags { get; set; }
    }
}
