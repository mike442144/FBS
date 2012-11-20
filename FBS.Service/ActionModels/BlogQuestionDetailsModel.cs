using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("问题详细模型")]
    public class BlogQuestionDetailsModel 
    {
        [DisplayName("问题编号")]
        public Guid QuestionID { get; set; }

        [DisplayName("")]
        public string Subject { get; set; }

        [DisplayName("")]
        public string Body { get; set; }

        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string UserTiny { get; set; }
        public int UserPoints { get; set; }

        [DisplayName("")]
        public Guid CategoryID { get; set; }

        [DisplayName()]
        public string CategoryName { get; set; }

        [DisplayName()]
        public int ClickCount { get; set; }

        public int RewardPoints { get; set; }

        public int AnswerCount { get; set; }

        public DateTime CreationDate { get; set; }

        public Guid BestAnswerID { get; set; }

        public IList<AnswerDspModel> Answers { get; set; }


     
    }
}
