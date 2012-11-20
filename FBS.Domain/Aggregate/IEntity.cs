using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FBS.Domain.Entity
{
    public interface IEntity
    {
        Guid Id{get;set;}
        void AlterToRow(DataTable table);
    }
}
