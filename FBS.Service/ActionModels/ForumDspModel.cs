using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel; 

namespace FBS.Service.ActionModels
{
    [DisplayName("版块简介模型")]
    public class ForumDspModel
    {
        [DisplayName("版块名字")]
        public string ForumName { get; set; }

        [DisplayName("版块描述")]
        public string ForumDsp { get; set; }

        [DisplayName("创建日期")]
        public DateTime CreationTime { get; set; }

        [DisplayName("最后修改时间")]
        public DateTime ModifiedTime { get; set; }

        [DisplayName("版块帖子数")]
        public int ThreadCount { get; set; }

        [DisplayName("版块id")]
        public Guid ID { get; set; }

        [DisplayName("贴吧优先级")]
        public int Priority { get; set; }
    }
}
