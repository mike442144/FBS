using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;

namespace FBS.Domain.Aggregate.Entity
{
    public class SiteModule : IAggregateRoot
    {
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

        private Guid _moduleId;
        private string _moduleName;
        private string _moduleDescription;
        //private string 
    }
}
