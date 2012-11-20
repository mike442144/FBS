using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Aggregate.Entity
{
    /// <summary>
    /// 城市
    /// </summary>
    public class City
    {
        public City(int id,string name,string postcode)
        {
            this._id = id;
            this._name = name;
            this._postCode = postcode;
        }


        public City()
        { 
        
        }
        #region 属性

        /// <summary>
        /// 编号
        /// </summary>
        private int _id;

        /// <summary>
        /// 城市名称
        /// </summary>
        private string _name;

        /// <summary>
        /// 邮编
        /// </summary>
        private string _postCode;

        public string Name
        {
            set { this.Name = value; }
            get { return this._name; }
        }

        #endregion
    }
}
