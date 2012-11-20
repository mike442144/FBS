using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class SheetResultModel
    {
        public Dictionary<SheetQuestionModel, IList<SheetAnswerModel>> QA { get; set; }
    }
}
