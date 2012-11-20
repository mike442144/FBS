using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("回答问题模型")]
    public class AnswerToQuestionModel
    {
        [DisplayName("用户编号")]
        public Guid UserID { get; set; }

        [DisplayName("用户名")]
        public string UserName { get; set; }

        public string UserTiny { get; set; }

        [DisplayName("内容")]
        public string Body { get; set; }

        [DisplayName("问题编号")]
        public Guid QuestionID { get; set; }
    }

    /// <summary>
    /// 答案列表模型
    /// </summary>
    public class AnswerDspModel
    {
        public Guid AnswerID { get; set; }
        public Guid UserID { get; set;}
        public string UserName { get; set; }
        public string UserTiny{get;set;}
        public int UserPoints { get; set; }
        public string Body { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
