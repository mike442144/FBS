using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class SheetAnswerModel
    {
        public Guid AnswerID { set; get; }

        public Guid QuestionID { set; get; }

        public string AnswerName { set; get; }

        public Guid FormID { get; set; }

        public float Percent { get; set; }

        public int Count { get; set; }
    }
}
