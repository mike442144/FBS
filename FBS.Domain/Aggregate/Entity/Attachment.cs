using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Aggregate.Entity
{
    [Serializable]
    public class Attachment
    {
        private ForumMessage _forumMessage;
        public Attachment(ForumMessage forumMessage)
        {
            this._forumMessage = forumMessage;
        }
    }
}
