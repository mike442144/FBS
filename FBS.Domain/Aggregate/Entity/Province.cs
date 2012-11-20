using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;

namespace FBS.Domain.Aggregate.Entity
{
    public class Province:IAggregateRoot
    {
        public Province(int id,string name)
        {
            this._id = id;
            this._name = name;
            this._cities = new HashSet<City>();
        }

        #region 城市
        private int _id;
        private string _name;
        private HashSet<City> _cities;

        public string Name
        {
            get { return this._name; }
        }

        public Int32 ID
        {
            get { return this._id; }
        }
        #endregion


        #region IEntity 成员

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

        public void AlterToRow(System.Data.DataTable table)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
