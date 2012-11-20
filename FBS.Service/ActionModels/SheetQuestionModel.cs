using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class SheetQuestionModel
    {
        [DisplayName("问题编号")]
        public Guid QuestionID { set; get; }

        [DisplayName("问卷编号")]
        public Guid FormID { set; get; }

        [DisplayName("问题名称")]
        public string QuestionName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public IList<SheetAnswerModel> Answers { get; set; }
    }
}
