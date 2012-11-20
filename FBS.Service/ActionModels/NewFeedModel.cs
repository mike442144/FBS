using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Service.ActionModels
{
    [DisplayName("新建新鲜事")]
    public class NewFeedModel
    {
        [DisplayName("新鲜事产生者或分享者")]
        public UserStateModel Sharer { get; set; }

        [DisplayName("分享来源者")]
        public UserStateModel SourceUser { get; set; }

        [DisplayName("标题")]
        public string Subject { get; set; }

        [DisplayName("内容")]
        public string Content { get; set; }

        [DisplayName("目标新鲜事编号")]
        public Guid TargetID { get; set; }

        [DisplayName("类型")]
        public FeedType Type { get; set; }
    }

}
