using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("广告详细模型")]
    public class AdvertisementDetailsModel
    {
        [DisplayName("广告类型")]
        public string AdvertisementType { get; set; }

        [DisplayName("广告内容URL")]
        public string AdvertisementContentURL { get; set; }

        [DisplayName("广告优先级")]
        public int AdvertisementPriority { get; set; }

        [DisplayName("广告开始时间")]
        public DateTime AdvertisementBeginTime { get; set; }

        [DisplayName("广告结束时间")]
        public DateTime AdvertisementEndTime { get; set; }

        [DisplayName("广告URL")]
        public string AdvertisementURL { get; set; }
    }
}
