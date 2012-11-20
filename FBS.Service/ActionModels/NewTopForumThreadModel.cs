using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public  class NewTopForumThreadModel
    {
       public  Guid TopForumThreadID
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
           get; set;
       }
    }
}
