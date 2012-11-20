using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("广告匹配展示模型")]
    public class AdvertiseFillingDetailsModel
    {
        [DisplayName("广告页面编号")]
        public Guid PageID { get; set; }
        [DisplayName("广告编号")]
        public Guid AdvertisementID { get; set; }
        [DisplayName("广告占位符名称")]
        public string PositionName { get; set; }
    }
}
