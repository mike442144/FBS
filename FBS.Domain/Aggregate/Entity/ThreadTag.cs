using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    [Serializable]
    public class ThreadTag:IEntity
    {
        private Guid _tagId;
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private int _assonum;

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._tagId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IEntity 成员


        public void AlterToRow(DataTable t)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
