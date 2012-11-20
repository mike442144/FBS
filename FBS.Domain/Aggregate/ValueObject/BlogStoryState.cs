using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Aggregate.ValueObject
{
    public class BlogStoryState
    {
        /// <summary>
        /// 去除html标签并提取消息概要
        /// </summary>
        /// <param name="len">摘要长度</param>
        /// <returns>摘要</returns>
        public string GetShortBody(int len)
        {
            string shortBody = "";
            if (this._description != null)
            {
                shortBody = Utils.Utils.RemoveHtml(this._description);
                if(shortBody.Length>len)
                    shortBody = shortBody.Substring(0, len);
            }

            return shortBody;
        }

        public int ClickCount
        {
            get { return _clickCount; }
            set { _clickCount = value; }
        }

        
        public int CommentCount
        {
            get { return _commentCount; }
            set { _commentCount = value; }
        }

        public string Title
        {
            get { return _title; }
            set { this._title = value; }
        }

        public string Description
        {
            get { return _description; }
            set { this._description = value; }
        }

        private int _clickCount=0;
        private int _commentCount=0;

        private string _title;
        private string _description;
        
    }
}
