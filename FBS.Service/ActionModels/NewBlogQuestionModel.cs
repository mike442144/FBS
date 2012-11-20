using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("提问模型")]
    public class NewBlogQuestionModel
    {
        [DisplayName("标题")]
        public string Subject { get; set; }

        [DisplayName("内容")]
        public string Body { get; set; }

        [DisplayName("分类编号")]
        public Guid CategoryId { get; set; }

        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("悬赏分")]
        public int RewardPoints { get; set; }

        [DisplayName("用户编号")]
        public Guid UserID { get; set; }

        [DisplayName("用户昵称")]
        public string UserName { get; set; }

        [DisplayName("用户头像")]
        public string UserTiny { get; set; }
    }
}
