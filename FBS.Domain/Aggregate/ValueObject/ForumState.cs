using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Aggregate.ValueObject
{
    [Serializable]
    public class ForumState
    {
        public ForumState()
        { }

        public ForumState(Forum forum)
        {
            this._forum = forum;
            this._lastPost = new ForumMessage();

            //还需要一些初始化工作
            //....
        }

        /// <summary>
        /// 增加帖子数量
        /// </summary>

        public void AddMessageCount()
        {
            this._messageCount++;
        }

        /// <summary>
        /// 增加主题数量
        /// </summary>

        public void AddThreadCount()
        {
            this._threadCount++;
        }

        /// <summary>
        /// 主题数
        /// </summary>
        public int ThreadCount
        {
            get { return this._threadCount; }
            set { this._threadCount = value; }
        }

        /// <summary>
        /// 帖子数
        /// </summary>
        public int MessageCount
        {
            get { return this._messageCount; }
            set { this._messageCount = value; }
        }

        /// <summary>
        /// 最新回复
        /// </summary>
        public ForumMessage LastPost
        {
            get { return this._lastPost; }
            set { this._lastPost = value; }
        }

        //主题中的消息数量包括了主题在里面，因此回复数是消息数减1。
        private int _threadCount = 0;

        private int _messageCount = 0;

        private ForumMessage _lastPost;

        private Forum _forum;
    }
}
