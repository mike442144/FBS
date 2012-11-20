using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("创建主题模型")]
    public class NewThreadModel
    {

        [DisplayName("主题标题")]
        public string Title { get; set; }

        [DisplayName("主题内容")]
        public string Body { get; set; }

        [DisplayName("创建人ID")]
        public Guid AccountID { get; set; }

        [DisplayName("是否匿名发表")]
        public bool IsAnonymous { get; set; }

        [DisplayName("板块ID")]
        public Guid ForumID { get; set; }
    }
}
