using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;

namespace FBS.Domain.Aggregate.Entity
{
    #region [Entity and Enums]
    /// <summary>
    /// Location Enumeration Definition
    /// Describes where the advertise will appear
    /// </summary>

    public enum AdvertiseType
    {
        AnimatedGif,
        FlashMovie
    }

    /// <summary>
    /// MediaType Enumeration Definition
    /// Type of media
    /// </summary>
    
    /// <summary>
    /// Entity of Advertise
    /// </summary>
    public class AdvertiseObject : IAggregateRoot
    {
        private Guid _ID = Guid.NewGuid();
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private DateTime _BeginTime;
        public DateTime BeginTime
        {
            get { return this._BeginTime; }
            set { this._BeginTime = value; }
        }

        private DateTime _EndTime;
        public DateTime EndTime
        {
            get { return this._EndTime; }
            set { this._EndTime = value; }
        }

        public bool IsOverdue()
        {
            if (this._EndTime < DateTime.Now)
            {
                return true;
            }
            else return false;
        }

        public bool IsPutin()
        {
            if (this._BeginTime <= DateTime.Now)
            {
                return true;
            }
            else
                return false;
        }

        private string _AdvertiseName;
        public string AdvertiseName
        {
            get { return this._AdvertiseName; }
            set { this._AdvertiseName = value; }
        }

        public Guid Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        private string _Location;
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        private AdvertiseType _ContentType;
        public AdvertiseType ContentType
        {
            get { return _ContentType; }
            set { _ContentType = value; }
        }

        private string _Path;
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        private int _Width;
        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        private int _Height;
        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        private string _Url;
        public string Url
        {
            get { return _Url; }
            set { _Url = value; }
        }

        private int _Priority;
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }


        public void AlterToRow(System.Data.DataTable table)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
