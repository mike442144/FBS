using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class AdvertiseFillingDspModel
    {
        [DisplayName("页面匹配编号")]
        public Guid ID { get; set; }
        [DisplayName("广告页面编号")]
        public Guid PageID { get; set; }
        [DisplayName("广告编号")]
        public Guid AdvertisementID { get; set; }
    }
}
