using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class SitePage : IAggregateRoot
    {
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

        public void AlterToRow(System.Data.DataTable table)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>站点实例</returns>
        public static SitePage CreateFromReader(IDataReader rd)
        {
            SitePage instance = new SitePage();

            instance._id = new Guid(rd["PageID"].ToString());
            //s._accountMessageVO = new AccountMessageVO() { Id = new Guid(rd["UserID"].ToString()), UserName = rd["UserName"].ToString() };
            instance._name = rd["PageName"].ToString();
            instance._description = rd["PageDescription"].ToString();
            instance._createdDate = Convert.ToDateTime(rd["CreatedDate"].ToString());
            instance._pageContent = rd["PageContent"].ToString();
            return instance;
        }

        public string Name { get { return this._name; } set { } }

        private Guid _id;
        private string _name;//可以使名字与标题一样
        private string _description;
        private string _pageContent;
        private string _title;
        private string _keyword;
        private DateTime _createdDate;
                
//        private Dictionary<Guid, SiteModule> _spModules;
        
    }
}
