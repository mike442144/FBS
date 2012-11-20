using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class NewShareThreadModel
    {
        public string ThumbnailUrl { set; get; }
        public string Body { set; get; }
        public string Subject { set; get; }
        public string PlayUrl { set; get; }
        //public Guid VideoID { set; get; }
        public string RawUrl { set; get; }
        public DateTime ShareTime { set; get; }
        public string Source { set; get; }
    }
}
