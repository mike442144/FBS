using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class AdvertisePage : IAggregateRoot
    {
        /// <summary>
        /// 页面编号
        /// </summary>
        private Guid _pageID;
        /// <summary>
        /// 页面内容URL
        /// </summary>
        private string _pageURL;
        /// <summary>
        /// 页面描述
        /// </summary>
        private string _pageDescription;

        public Guid PageID
        {
            set { this._pageID = value; }
            get { return this._pageID; }
        }

        public string PageURL
        {
            set { this._pageURL = value; }
            get { return this._pageURL; }
        }

        public string PageDescription
        {
            set { this._pageDescription = value; }
            get { return this._pageDescription; }
        }
        #region IEntity 成员

        /// <summary>
        ///  页面编号
        /// </summary>
        public Guid Id
        {
            get
            {
                return this._pageID;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 转化为数据行
        /// </summary>
        /// <param name="t">数据表</param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同

            if (t.Columns.Count == 0)
            {
                t.Columns.Add("PageID", typeof(Guid));
                t.Columns.Add("PageURL", typeof(string));
                t.Columns.Add("PageDescription", typeof(string));
            }

            DataRow row = t.NewRow();
            row["PageID"] = this._pageID;
            row["PageURL"] = this._pageURL;
            row["PageDescription"] = this._pageDescription;

            t.Rows.Add(row);
        }

        #endregion

        public AdvertisePage(string pageURL,string pageDescri)
        {
            this._pageID = Guid.NewGuid();
            this._pageURL = pageURL;
            this._pageDescription = pageDescri;
        }

        public AdvertisePage()
        { 
        
        }
        #region 生成数据库命令

        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (strSql == null || cmdParms == null)
                return;

            strSql.Append("INSERT INTO fbs_AdvertisePage(");
            strSql.Append("PageID, PageURL, PageDescription)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_PageID, @in_PageURL, @in_PageDescription)");

            cmdParms.Add("@in_PageID", DbType.Guid);
            cmdParms.Add("@in_PageURL", DbType.StringFixedLength);
            cmdParms.Add("@in_PageDescription", DbType.StringFixedLength);
            
        }

        /// <summary>
        /// 生成更新命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareUpdateCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (null == strSql || null == cmdParms)
                return;

            strSql.Append("UPDATE fbs_AdvertisePage SET ");
            strSql.Append("PageURL=@in_PageURL,");
            strSql.Append("PageDescription=@in_PageDescription");
            strSql.Append(" WHERE PageID=@in_PageID");

            cmdParms.Add("@in_PageID", DbType.Guid);
            cmdParms.Add("@in_PageURL", DbType.StringFixedLength);
            cmdParms.Add("@in_PageDescription", DbType.StringFixedLength);
           
        }

        /// <summary>
        /// 生成删除命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareDeleteCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (null == strSql || null == cmdParms)
                return;

            strSql.Append(" WHERE PageID=@in_PageID");

            cmdParms.Add("@in_PageID", DbType.Guid);

        }

        #endregion


        #region 从持久化创建

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>文章实例</returns>
        public static AdvertisePage CreateFromReader(IDataReader rd)
        {
            AdvertisePage a = new AdvertisePage();

            a._pageID = new Guid(rd["PageID"].ToString());
            a._pageURL = rd["PageURL"].ToString();
            a._pageDescription = rd["PageDescription"].ToString();
            return a;
        }


        #endregion
    }
}
