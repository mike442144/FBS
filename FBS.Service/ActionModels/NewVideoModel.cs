using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    /// <summary>
    /// 新视频
    /// </summary>
    public class NewVideoModel
    {
        /// <summary>
        /// 视频原始地址
        /// </summary>
        public string RawUrl { get; set; }

        /// <summary>
        /// 分享者
        /// </summary>
        public UserStateModel Sharer { get; set; }

        /// <summary>
        /// 视频评论
        /// </summary>
        public string Comment { get; set; }
    }
}
