using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FBS.Domain.Aggregate.ValueObject
{
    [Serializable]
    public class MessageVO
    {
        public MessageVO()
        {
        }

        /// <summary>
        /// 去除html标签并提取消息概要
        /// </summary>
        /// <param name="len">摘要长度</param>
        /// <returns>摘要</returns>
        public string GetShortBody(int len)
        {
            string shortBody="";
            if (this._body != null)
            {
                shortBody = Utils.Utils.RemoveHtml(this._body);
                shortBody = shortBody.Substring(0, len);
            }

            return shortBody;
        }

        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        //private string _title;

        //public string Title
        //{
        //    get { return this._title; }
        //    set { this._title = value; }
        //}

        private string _body;

        public string Body
        {
            get { return _body; }
            set { _body = value; }
        }

        private string[] _tagTitle;

        public string[] TagTitle
        {
            get { return _tagTitle; }
            set { _tagTitle = value; }
        }
        private int _rewardPoints;

        public int RewardPoints
        {
            get { return _rewardPoints; }
            set { _rewardPoints = value; }
        }

        private bool _isFiltered;

        public bool IsFiltered
        {
            get { return _isFiltered; }
            set { _isFiltered = value; }
        }


    }
}
