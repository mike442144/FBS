using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Aggregate.Entity
{
    //聚合根的属性类
    public class AggregateRoot:System.Attribute
    {
        string _name;

        #region 构造函数
        public AggregateRoot(string str)
        {
            this._name = str;
        }

        public AggregateRoot()
        { }
        #endregion

        public override string ToString()
        {
            return this._name;
        }
    }
}
