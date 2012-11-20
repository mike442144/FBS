using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Utils.TreeModel;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class BlogCategoryTree : ComplexTreeNode<BlogCategoryTree>,IEntity
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">名称</param>
        public BlogCategoryTree(string name)
        {
            this._name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public BlogCategoryTree(Guid id)
        {
            this._id = id;
        }

        private Guid _id;

        /// <summary>
        /// 名称
        /// </summary>
        private string _name;

        public string Name
        {
            get { return _name; }
        }

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._id;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AlterToRow(DataTable t)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
