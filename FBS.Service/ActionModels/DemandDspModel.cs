using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class DemandDspModel
    {
        public Guid DemandID
        {
            get;
            set;
        }

        public string CustomerName
        {
            set;
            get;
        }

        public string CustomerPhoneNum
        {
            set;
            get;
        }

        public string CustomerOtherConnect
        {
            set;
            get;
        }

        public string DemandCity
        {
            set;
            get;
        }

        public string BusinessmanName
        {
            set;
            get;
        }

        public string GroupOnType
        {
            set;
            get;
        }

        public string DemandContent
        {
            set;
            get;
        }
    }
}
