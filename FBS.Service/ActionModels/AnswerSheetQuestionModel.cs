using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class AnswerSheetQuestionModel
    {
        public Guid QuestionID { set; get; }

        public Guid FormID { set; get; }

        public string QuestionName { set; get; }
    }
}
