using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    /// <summary>
    /// 上传照片模型
    /// </summary>
    public class NewPhotoModel
    {
        public UserStateModel User { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgPath { get; set; }
    }
}
