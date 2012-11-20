using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    /// <summary>
    /// 站内分享模型
    /// </summary>
    public class SiteShareModel
    {
        /// <summary>
        /// 新鲜事编号
        /// </summary>
        public Guid FeedID { get; set; }

        /// <summary>
        /// 分享者
        /// </summary>
        public UserStateModel Sharer { get; set; }

        /// <summary>
        /// 分享的原始创建者
        /// </summary>
        public UserStateModel SourceUser { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }
    }
}
