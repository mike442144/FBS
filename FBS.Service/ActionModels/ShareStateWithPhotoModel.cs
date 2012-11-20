using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    /// <summary>
    /// 分享照片模型
    /// </summary>
    public class ShareModel
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
        /// 分享来源者
        /// </summary>
        public UserStateModel SourceUser { get; set; }

        /// <summary>
        /// 分享标题
        /// </summary>
        public string Subject { get; set; }
    }
}
