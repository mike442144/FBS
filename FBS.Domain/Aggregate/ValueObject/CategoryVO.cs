using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Aggregate.ValueObject
{
    public class CategoryVO
    {
        public CategoryVO(Guid id,string name,string iconname)
        {
            this._name = name;
            
            this._iconName = iconname;
            this._categoryId = id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="iconName"></param>
        /// <param name="priority"></param>
        public CategoryVO(string name, string description, string iconName, uint priority)
        {
            this.CategoryId = Guid.NewGuid();
            this._name = name;
            this._description = description;
            this._iconName = iconName;
            this._priority = priority;
        }

        public CategoryVO(string name, string description, string iconName, uint priority, Guid parentId)
        {
            this.CategoryId = Guid.NewGuid();
            this._name = name;
            this._description = description;
            this._iconName = iconName;
            this._priority = priority;
            this._parentId = parentId;
        }

        public CategoryVO() { }


        /// <summary>
        /// 名称
        /// </summary>
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string _description;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        private Guid _categoryId;

        public Guid CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        /// <summary>
        /// 图标名
        /// </summary>
        private string _iconName;

        public string IconName
        {
            get { return _iconName; }
            set { _iconName = value; }
        }

        /// <summary>
        /// 优先级
        /// </summary>
        private uint _priority;

        public uint Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }


        private Guid _parentId;

        public Guid ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        private uint _deepth;

        public uint Deepth
        {
            get { return _deepth; }
            set { _deepth = value; }
        }

    }
}
