using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;

namespace FBS.Domain.Aggregate.Entity
{
    public abstract class CategoryBase : IAggregateRoot
    {
        protected Guid _categoryId;
        protected string _name;
        protected string _description;
        protected string _icon;
        protected uint _priority;

        #region IEntity 成员

        public abstract Guid Id { get; set; }

        public abstract void AlterToRow(System.Data.DataTable table);

        #endregion
    }
}
