using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("帖子详细模型")]
    public class ThreadDetailsModel
    {
        public Guid RootMessageID { get;set; }

        [DisplayName("内容")]
        public string Body { get; set; }

        [DisplayName("点击")]
        public int ClickCount { get; set; }

        [DisplayName("回复")]
        public int MessageCount { get; set; }

        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("作者")]
        public string UserName { get { return name; } set { if (string.IsNullOrEmpty(value)) name = "还没想好"; else name = value; } }
        private string name;
        
        [DisplayName("最后更新")]
        public DateTime LastModified { get; set; }

        [DisplayName("发表日期")]
        public DateTime CreationDate { get; set; }

        [DisplayName("回复列表")]
        public IList<ThreadsDspModel> ReplyList { get; set; }

        [DisplayName("发帖人编号")]
        public Guid UserId { get; set; }

        [DisplayName("帖子板块号")]
        public Guid ForumID { set; get; }

        [DisplayName("帖子板块名")]
        public string ForumName { set; get; }

        public int ForumMessageSum { get; set; }
    }
}
