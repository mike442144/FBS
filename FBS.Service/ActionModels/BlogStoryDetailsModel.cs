using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("博文详细模型")]
    public class BlogStoryDetailsModel
    {
        [DisplayName("博文编号")]
        public Guid StoryID { get; set; }

        [DisplayName("博文分类编号")]
        public Guid CategoryID { get;set; }
        [DisplayName("博文分类名")]
        public string CategoryName { get; set; }

    
        [DisplayName("博文标题")]
        public string Title{get;set;}

        [DisplayName("博文内容")]
        public string Content{get;set;}

        [DisplayName("博文标签")]
        public IList<string> Tags{get;set;}

        [DisplayName("博主名字")]
        public string WriterName { set; get; }

        [DisplayName("博主编号")]
        public Guid AccountID { get; set; }

        [DisplayName("发表时间")]
        public DateTime PublishTime { set; get; }

        [DisplayName("评论次数")]
        public int CommentsCount { set; get; }

        [DisplayName("阅读次数")]
        public int ReadCount { set; get; }

        [DisplayName("图片名称")]
        public string ImgName { set; get; }

        [DisplayName("博文评论")]
        public IList<CommentDspModel> Comments { get; set; }
    }
}
