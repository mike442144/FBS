using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Aggregate.ValueObject
{
    public class QuestionState
    {
        public QuestionState()
        { }

        /// <summary>
        /// 标题
        /// </summary>
        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        private string _body;

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }


        /// <summary>
        /// 浏览
        /// </summary>
        private int _clickCount;

        public int ClickCount
        {
            get { return _clickCount; }
            set { _clickCount = value; }
        }

        /// <summary>
        /// 回答数
        /// </summary>
        private int _answerCount;

        public int AnswerCount
        {
            get { return _answerCount; }
            set { _answerCount = value; }
        }

        /// <summary>
        /// 悬赏分
        /// </summary>
        private int _rewardPoints;

        public int RewardPoints
        {
            get { return _rewardPoints; }
            set { _rewardPoints = value; }
        }

        /// <summary>
        /// 最佳答案编号
        /// </summary>
        private Guid _bestAnswerId;

        public Guid BestAnswerId
        {
            get { return _bestAnswerId; }
            set { _bestAnswerId = value; }
        }

    }
}
