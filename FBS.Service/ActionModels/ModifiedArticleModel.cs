using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class ModifiedArticleModel
    {
        [DisplayName("文章编号")]
        public Guid ArticleID { get; set; }

        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("简要标题")]
        public string BriefTitle { get; set; }

        [DisplayName("内容")]
        public string Body { get; set; }

        [DisplayName("用户编号")]
        public Guid UserID { get; set; }

        [DisplayName("用户名称")]
        public string UserName { get; set; }

        [DisplayName("分类编号")]
        public Guid CategoryID { get; set; }

        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("原文链接")]
        public string SourceUrl { get; set; }

        [DisplayName("来源网站")]
        public string SourceSite { get; set; }

        [DisplayName("图片名称")]
        public string ImgName { get; set; }
    }
}
