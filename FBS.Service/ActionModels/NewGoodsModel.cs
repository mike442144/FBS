using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class NewGoodsModel
    {

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
