using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;

namespace FBS.Domain.Repository
{
    public interface IXmlRepository:IRepository<Province>
    {
        IList<City> Select(string attr, string value);
    }
}
