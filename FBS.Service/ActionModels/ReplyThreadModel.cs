using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("回复帖子模型")]
    public class ReplyThreadModel
    {
        [DisplayName("主题ID")]
        public Guid ThreadID { get; set; }

        [DisplayName("内容")]
        public string Body { get; set; }

        [DisplayName("用户ID")]
        public Guid AccountID { get; set; }

        [DisplayName("是否匿名发表")]
        public bool IsAnonymous { get; set; }

        public Guid ForumID { get; set; }
    }

    public class ReplyRepliedMsgModel
    {
        public Guid ThreadID { get; set; }
        public Guid ReadyToReplyMessageID { get; set; }
        public string Body { get; set; }
        public Guid UserID { get; set; }
        public ReplyType ReplyType { get; set; }
        public Guid ForumID { get; set; }
        public UserStateModel User { get; set; }
    }

    public enum ReplyType
    {
        //支持
        Support,
        //反对
        Oppose,
        //回复内容
        SubContent,
        //引用
        Quote
    }
}
