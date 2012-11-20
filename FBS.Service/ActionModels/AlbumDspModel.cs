using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    public class AlbumDspModel
    {
        public Guid AlbumID { set; get; }

        public Guid UserID { set; get; }

        public string UserName { set; get; }

        public string PhotoName { set; get; }

        public string Description { set; get; }

        public DateTime Time { set; get; }

    }
}
