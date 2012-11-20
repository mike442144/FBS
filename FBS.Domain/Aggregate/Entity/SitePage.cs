using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;

namespace FBS.Domain.Aggregate.Entity
{
    public class SitePage : IAggregateRoot
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

        private Guid _spId;
        private string _spName;//可以使名字与标题一样
        private string _spDescription;
        private string _title;
        private string _keyword;

        
        
        private Dictionary<Guid, SiteModule> _spModules;
        
    }
}
