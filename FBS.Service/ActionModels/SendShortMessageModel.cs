using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("发送短消息模型")]
    public class SendShortMessageModel
    {
        [DisplayName("发送者")]
        public FriendModel Sender { get; set; }

        [DisplayName("接收者")]
        public FriendModel Reciver { get; set; }

        [DisplayName("消息标题")]
        public string Title { get; set; }

        [DisplayName("消息内容")]
        public string Body { get; set; }
    }

    [DisplayName("短消息列表模型")]
    public class ShortMessageDspModel
    {
        [DisplayName("编号")]
        public Guid ShortMsgID { get; set; }

        [DisplayName("发送者")]
        public UserStateModel Sender { get; set; }

        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("内容")]
        public string Body { get; set; }

        [DisplayName("发送时间")]
        public DateTime SentOn { get; set; }
    }
}
