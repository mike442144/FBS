using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class TopForumThreadDspModel
    {
        public Guid ID
        {
            get;
            set;
        }

        public Guid TopForumThread
        {
            get;
            set;
        }
        public Guid TopForumID
        {
            get;
            set;
        }
        public DateTime CreatTime
        {
            get;
            set;
        }
    }
}
