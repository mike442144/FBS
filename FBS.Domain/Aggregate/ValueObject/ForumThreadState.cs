using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Aggregate.ValueObject
{
    [Serializable]
    public class ForumThreadState
    {
        public ForumThreadState(ForumThread forumThread)
        {
            this._forumThread = forumThread;
        }

        private int _clickCount;

        public int ClickCount
        {
            get { return _clickCount; }
            set { _clickCount = value; }
        }
        private int _messageCount;

        public int MessageCount
        {
            get { return _messageCount; }
            set { _messageCount = value; }
        }
        private ForumThread _forumThread;

        public ForumThread ForumThread
        {
            get { return _forumThread; }
            set { _forumThread = value; }
        }
        private Guid _lastPost;

        public Guid LastPost
        {
            get { return _lastPost; }
            set { _lastPost = value; }
        }
        private DateTime _modifiedDate;

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { _modifiedDate = value; }
        }
    }
}
