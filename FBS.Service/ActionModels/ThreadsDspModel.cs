using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FBS.Utils;

namespace FBS.Service.ActionModels
{
    [DisplayName("贴吧显示的模型")]
    public class ThreadsDspModel:ISortEntity
    {
        public Guid MessageID { get; set; }

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

        [DisplayName("内容")]
        public string Body { get; set; }

        [DisplayName("帖子ID")]
        public Guid ID { get; set; }

        [DisplayName("论坛ID")]
        public Guid ForumID { get; set; }

        [DisplayName("发帖人编号")]
        public Guid UserID{get;set;}

        #region ISortEntity 成员

        public int SortFieldLength()
        {
            return Utils.Utils.GetStringLength(this.Title);
        }

        public string ImageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            int selfLength = Utils.Utils.GetStringLength(this.Title);
            int objLength = Utils.Utils.GetStringLength(((ThreadsDspModel)obj).Title);

            return Convert.ToInt32(selfLength >= objLength);
        }

        #endregion
    }
}
