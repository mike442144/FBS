using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class NewForumModel
    {
        
        public string ForumName { get; set; }

       
        public string ForumDsp { get; set; }

        public DateTime CreationTime { get; set; }

      
        public DateTime ModifiedTime { get; set; }

       
        public int ThreadCount { get; set; }

        public int Priority { get; set;}
    }
}
