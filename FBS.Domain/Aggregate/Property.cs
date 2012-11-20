using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace FBS.Domain.Aggregate
{
    /// <summary>
    /// Property对象是ForumThread的附加属性，它会随着ForumThread的不同状态改变
    /// </summary>
    [Serializable]
    public class Property
    {
        private NameValueCollection properties;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Property()
        {
            this.properties = new NameValueCollection();
        }

        /// <summary>
        /// 复制构造函数
        /// </summary>
        /// <param name="oldCollection">要复制的键值对集合</param>
        public Property(NameValueCollection oldCollection)
        {
            this.properties = new NameValueCollection(oldCollection);
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        public void AddNameValue(string name, string value)
        {
            this.properties.Add(name, value);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="name">键名</param>
        public string GetValue(string name)
        {
            return this.properties.Get(name);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="index">索引</param>
        public string GetValue(int index)
        {
            return this.properties.Get(index);    
        }

        /// <summary>
        /// 根据键名设置键值
        /// </summary>
        /// <param name="name">键名</param>
        /// <param name="value">键值</param>
        public void SetValueByName(string name,string value)
        {
            this.properties.Set(name, value);
        }

        /// <summary>
        /// 根据索引获取键名
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>键值</returns>
        public string GetNameByIndex(int index)
        {
            return this.properties.GetKey(index);
        }

        /// <summary>
        /// 以键名删除键值对
        /// </summary>
        /// <param name="name">键名</param>
        public void Remove(string name)
        {
            this.properties.Remove(name);
        }
    }
}
