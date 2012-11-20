using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.QueryObject
{
    public interface IQuery
    {
        void AddCriterion(string field,object value);
    }
}
