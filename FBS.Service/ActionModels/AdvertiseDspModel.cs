using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Service.ActionModels
{
    

    public class AdvertiseDspModel
    {
        public Guid ID
        {
            get;
            set;
        }

        public string AdvertiseName
        {
            get;
            set;
        }


        public string Location
        {
            get;
            set;
        }


        public AdvertiseType ContentType
        {
            get;
            set;
        }


        public string Path
        {
            get;
            set;
        }


        public int Width
        {
            get;
            set;
        }


        public int Height
        {
            get;
            set;
        }


        public string Url
        {
            get;
            set;
        }


        public int Priority
        {
            get;
            set;
        }


        public DateTime BeginTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        public AdvertiseDspModel()
        {
            this.ID = Guid.NewGuid();
            this.BeginTime = DateTime.Now;
        }
    }
}
