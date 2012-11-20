using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace FBS.Service.ActionModels
{
    public class ArticleDspModel:Utils.ISortEntity
    {
        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("简要标题")]
        public string BriefTitle { get; set; }

        [DisplayName("编号")]
        public Guid ArticleID { get; set; }
        
        [DisplayName("发布日期")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("点击")]
        public int ClickCount { get; set; }

        [DisplayName("图片名称")]
        public string ImgName { get; set; }

        public string SourceUrl { get; set; }
        #region ISortEntity 成员
        public int CompareTo(object obj)
        {
            int selfLength = Utils.Utils.GetStringLength(this.BriefTitle);
            int objLength = Utils.Utils.GetStringLength(((ArticleDspModel)obj).BriefTitle);

            return Convert.ToInt32(selfLength>=objLength);
        }
        //排序属性的长度
        public int SortFieldLength()
        {
            return Utils.Utils.GetStringLength(this.BriefTitle);
        }

        string FBS.Utils.ISortEntity.Title
        {
            get
            {
                return this.BriefTitle;
            }
            set
            {
                this.BriefTitle = value;
            }
        }

        string FBS.Utils.ISortEntity.ImageName
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
        //Guid FBS.Utils.ISortEntity.UserId
        //{
        //    get
        //    {
        //        return Guid.Empty;
        //    }
        //    set { 
            
        //    }
        //}
        #endregion
    }
}
