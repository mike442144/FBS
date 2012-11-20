using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("新建广告匹配模型")]
    public class NewAdvertiseFillingModel
    {
        [DisplayName("编号")]
        public Guid ID{get;set;}

        [DisplayName("页面编号")]
        public Guid PageID { get; set; }

        [DisplayName("广告编号")]
        public Guid AdvertisementID { get; set; }

        [DisplayName("广告占位符名称")]
        public string PositionName { get; set; }
    }
}
