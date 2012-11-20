using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Service.ActionModels
{
    [DisplayName("新鲜事显示模型")]
    public class FeedDspModel
    {
        public Guid FeedID { get; set; }
        public string UserHead { get; set; }
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public FeedType FeedType { get; set; }
        public int ReplayCount { get; set; }
    }
}
