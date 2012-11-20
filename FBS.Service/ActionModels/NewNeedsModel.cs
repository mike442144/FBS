using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class NewNeedsModel
    {
        public Guid NeedsID
        {
            set;
            get;
        }

        public string NeedsContent
        {
            set;
            get;
        }
    }
}
