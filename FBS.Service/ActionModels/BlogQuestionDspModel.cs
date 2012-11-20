using System;
using System.ComponentModel;
using FBS.Utils;

namespace FBS.Service.ActionModels
{
    [DisplayName("问题列表模型")]
    public class BlogQuestionDspModel: ISortEntity
    {
        [DisplayName("问题编号")]
        public Guid QuestionID { get; set; }

        [DisplayName("标题")]
        public string Subject { get; set; }

        [DisplayName("分类名称")]
        public string CategoryName { get; set; }

        [DisplayName("分类编号")]
        public Guid CategoryID { get; set; }

        [DisplayName("悬赏分")]
        public int RewardPoints { get; set; }

        [DisplayName("提问人昵称")]
        public string UserName { get; set; }

        [DisplayName("提问人编号")]
        public Guid UserID { get; set; }

        [DisplayName("回答数")]
        public int AnswerCount { get; set; }

        [DisplayName("")]
        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        #region ISortEntity 成员

        public int SortFieldLength()
        {
            return Utils.Utils.GetStringLength(this.Subject);
        }

        public string ImageName
        {
            get
            {
                return string.Empty;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get { return this.Subject; }
            set { this.Subject = value; }
        }

        #endregion

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            int selfLength = Utils.Utils.GetStringLength(this.Subject);
            int objLength = Utils.Utils.GetStringLength(((BlogQuestionDspModel)obj).Subject);

            return Convert.ToInt32(selfLength >= objLength);
        }

        #endregion
    }
}
