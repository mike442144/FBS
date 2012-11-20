using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
namespace FBS.Service.ActionModels
{
    public class AdvertisementDspModel
    {

        [DisplayName("广告编号")]
        public Guid AdvertisementID { get; set; }

        [DisplayName("广告类型")]
        public string AdvertisementType { get; set; }

        [DisplayName("广告内容URL")]
        public string  AdvertisementContentURL { get; set; }

        [DisplayName("广告优先级")]
        public int  AdvertisementPriority { get; set; }

        [DisplayName("广告URL")]
        public string AdvertisementURL { get; set; }
    }
}
