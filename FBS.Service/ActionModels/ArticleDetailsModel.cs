using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("文章详细模型")]
    public class ArticleDetailsModel
    {
        [DisplayName("文章标题")]
        public string Title { get; set; }

        [DisplayName("文章标题")]
        public string BriefTitle { get; set; }

        [DisplayName("文章内容")]
        public string Body { get; set; }

        [DisplayName("来源网址")]
        public string SourceUrl { get; set;}

        [DisplayName("来源网站")]
        public string SourceSite { get; set; }

        [DisplayName("点击数")]
        public int ClickCount { get; set;}

        [DisplayName("评论数")]
        public int CommentCount { get; set; }

        [DisplayName("创建日期")]
        public DateTime CreationDate { get; set; }

        [DisplayName("分类编号")]
        public Guid CategoryID { get; set; }

        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("发布者编号")]
        public Guid UserID { get; set; }

        [DisplayName("发布者昵称")]
        public string UserName { get; set; }

        [DisplayName("文章评论")]
        public IList<CommentDspModel> Comments { get; set; }
    }
}
