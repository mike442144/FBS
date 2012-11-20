using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FBS.Service.ActionModels
{
    //[DisplayName("评论模型")]
    
    public class CommentModel
    {
        [DisplayName("博文id")]
        public Guid TargetID { get; set; }

        [DisplayName("评论者id")]
        public Guid AccountID { get; set; }

        [DisplayName("评论者昵称")]
        public string UserName { get; set; }

        [DisplayName("评论内容")]
        public string CommentContent { get; set; }

        [DisplayName("评论时间")]
        public DateTime CreatedOn { get; set; }

        [DisplayName("评论者头像")]
        public string Head { get; set; }
    }

    //[DataContract()]
    public class CommentDspModel
    {
        [DisplayName("评论编号")]
        //[DataMember()]
        public Guid CommentID { get; set; }

        [DisplayName("博文id")]
        //[DataMember()]
        public Guid TargetID { get; set; }

        [DisplayName("评论者id")]
        //[DataMember()]
        public Guid AccountID { get; set; }

        [DisplayName("评论者昵称")]
       // [DataMember()]
        public string UserName { get; set; }

        [DisplayName("评论内容")]
        //[DataMember()]
        public string CommentContent { get; set; }

        [DisplayName("评论时间")]
        //[DataMember()]
        public DateTime CreatedOn { get; set; }

        [DisplayName("评论者头像")]
       // [DataMember()]
        public string Head{get;set;}
    }
}
