using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("新建广告页面模型")]
    public  class NewAdvertisePageModel
    {
            [DisplayName("广告页面编号")]
            public Guid PageID { get; set; }

            [DisplayName("广告页面URL")]
            public string  PageURL { get; set; }

            [DisplayName("广告页面简介")]
            public string PageDescription { get; set; }

    }
}
