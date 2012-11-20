using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    public class GoodsDetailsModel
    {
        [DisplayName("商品名称")]
        public string GoodsName
        {
            set;
            get;
        }

        public float GoodsNowPrice
        {
            set;
            get;
        }

        public string GoodsPicURL
        {
            set;
            get;
        }

        public float GoodsOldPrice
        {
            set;
            get;
        }

        public int GoodsBuyCount
        {
            set;
            get;
        }

        public DateTime GoodsBeginTime
        {
            set;
            get;
        }

        public DateTime GoodsEndTime
        {
            set;
            get;
        }

        public string GoodsDetailsContent
        {
            set;
            get;
        }

        public bool GoodsIsOn
        {
            set;
            get;
        }
    }
}
