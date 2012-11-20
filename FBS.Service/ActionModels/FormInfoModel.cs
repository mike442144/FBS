using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class FormInfoModel
    {
        [DisplayName("问卷名")]
        public Guid FormID { set; get; }

        [DisplayName("问卷主题")]
        public string Title { set; get; }

        [DisplayName("问卷描述")]
        public string Description { set; get; }

        [DisplayName("显示问卷")]
        public bool Display { set; get; }
    }
}
