using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace FBS.Service.ActionModels
{
    [DisplayName("设置最佳答案模型")]
    public class SettingBestAnswerModel
    {
        [DisplayName("最佳答案编号")]
        public Guid BestAnswerID { get; set; }

        [DisplayName("提问用户编号")]
        public Guid QuestionUserID { get; set; }

        [DisplayName("问题编号")]
        public Guid QuestionID { get; set; }

        [DisplayName("所有获得分数的用户ID与对应的分数")]
        public Dictionary<Guid,int> AnswersDic { get; set; }
    }
}
