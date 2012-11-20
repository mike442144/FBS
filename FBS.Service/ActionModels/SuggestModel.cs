using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class SuggestModel
    {
        public Guid SuggestID { set; get; }

        public string Body { set; get; }

        public Guid UserID { set; get; }

        public string UserName { set; get; }

        public DateTime CreationDate { set; get; }

        public string Reply { set; get; }

        public string Type { set; get; }
    }
}
