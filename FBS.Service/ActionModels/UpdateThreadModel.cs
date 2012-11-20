using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class UpdateThreadModel
    {
        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("内容")]
        public string Body { get; set; }

        [DisplayName("修改者ID")]
        public Guid AccountID { get; set; }

        [DisplayName("板块ID")]
        public Guid ForumID { get; set; }
    }
}
