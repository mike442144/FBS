using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("广告页面模型")]
    public class AdvertisePageDspModel
    {
        [DisplayName("广告页面编号")]
        public Guid PageID { get; set; }

        [DisplayName("广告页面URL")]
        public string PageURL { get; set; }

        [DisplayName("广告页面简介")]
        public string PageDescription { get; set; }
    }
}
