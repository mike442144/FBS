using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class AddFolloweeModel
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string UserHead { get; set; }

        public Guid FolloweeID { get; set; }
        public string FolloweeName { get; set; }
        public string FolloweeHead { get; set; }
    }
}
