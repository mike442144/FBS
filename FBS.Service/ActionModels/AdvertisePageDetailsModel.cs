using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("广告页面显示模型")]
    public class AdvertisePageDetailsModel
    {

        [DisplayName("广告页面URL")]
        public string PageURL { get; set; }

        [DisplayName("广告页面简介")]
        public string PageDescription { get; set; }
    }
}
