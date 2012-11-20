using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("新建状态模型")]
    public class NewBlogStateModel
    {
        [DisplayName("状态内容")]
        public string Body { get; set; }

        [DisplayName("用户编号")]
        public Guid UserID { get; set; }

        [DisplayName("用户名称")]
        public string UserName { get; set; }

        [DisplayName("用户头像")]
        public string UserHead { get; set; }
    }

    /// <summary>
    /// 转发状态模型
    /// </summary>
    public class ForwardStateModel
    {
        /// <summary>
        /// 转发者
        /// </summary>
        public UserStateModel Sharer { get; set; }

        /// <summary>
        /// 发布原始状态者
        /// </summary>
        public UserStateModel SourceUser { get; set; }

        public Guid FeedID { get; set; }
    }
}
