using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FBS.Utils;

namespace FBS.Service.ActionModels
{
    /// <summary>
    /// 博文列表显示模型
    /// </summary>
    [DisplayName("博文列表显示模型")]
    public class BlogStoryDspModel : ISortEntity
    {
        [DisplayName("博文编号")]
        public Guid StoryID { get; set; }

        [DisplayName("博主的id")]
        public Guid UserID { get; set; }

        [DisplayName("博文标题")]
        public string Title { set; get; }

        [DisplayName("博文简介")]
        public string Description { set; get; }

        [DisplayName("博主名字")]
        public string WriterName { set; get; }

        [DisplayName("发表时间")]
        public DateTime PublishTime { set; get; }

        [DisplayName("评论次数")]
        public int CommentsCount { set; get; }

        [DisplayName("阅读次数")]
        public int ReadCount { set; get; }

        [DisplayName("图片名称")]
        public string ImgName { set; get; }

        #region ISortEntity 成员

        public int SortFieldLength()
        {
            return Utils.Utils.GetStringLength(this.Title);
        }

        public string ImageName
        {
            get
            {
                return this.ImgName;
            }
            set
            {
                this.ImgName = value;
            }
        }

       // public Guid UserId
        //{
          //  get { return this.UserID; }
            //set { }
        //}
        #endregion

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            int selfLength = Utils.Utils.GetStringLength(this.Title);
            int objLength = Utils.Utils.GetStringLength(((BlogStoryDspModel)obj).Title);

            return Convert.ToInt32(selfLength >= objLength);
        }

        #endregion
    }
}
