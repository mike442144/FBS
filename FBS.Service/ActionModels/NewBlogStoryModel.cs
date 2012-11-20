using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("新建博文模型")]
    public class NewBlogStoryModel
    {
        [DisplayName("博文标题")]
        public string Title { get; set; }

        [DisplayName("博文内容")]
        public string Body { get; set; }

        [DisplayName("创建人ID")]
        public Guid AccountID { get; set; }

        [DisplayName("创建人昵称")]
        public string UserName { get; set; }

        [DisplayName("创建人头像")]
        public string UserTiny { get; set; }

        [DisplayName("是否申请精华")]
        public bool IsRequireDigest { get; set; }

        [DisplayName("是否发到首页")]
        public bool IsPublishedToHomepage { get; set; }

        [DisplayName("文章分类ID")]
        public Guid CategoryID  { get; set; }

        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("文章标签")]
        public IList<string> Tags { get; set; }

        [DisplayName("图片名称")]
        public string ImgName { set; get; }
    }
}
