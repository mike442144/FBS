using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;
using FBS.Domain.Aggregate.ValueObject;

namespace FBS.Domain.Aggregate.Entity
{
    public class Site : IAggregateRoot
    {

        public Site(string name,string desc,string url,string cpy,string ver)
        {
            this._siteId = Guid.NewGuid();
            this._siteName = name;
            this._siteDescription = desc;
            this._siteurl = url;
            this._copyright = cpy;
            this._version = ver;
            this._createdDate = DateTime.Now;
        }

        public Site(Site entity)
        {
            
        }

        private Site()
        { }

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._siteId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同

            if (t.Columns.Count == 0)
            {
                t.Columns.Add("SiteID", typeof(Guid));
                t.Columns.Add("SiteName", typeof(string));
                t.Columns.Add("SiteDescription", typeof(string));
                t.Columns.Add("SiteUrl", typeof(string));
                t.Columns.Add("CopyRight", typeof(string));
                t.Columns.Add("Version", typeof(string));
                t.Columns.Add("CreatedDate", typeof(DateTime));
            }

            DataRow row = t.NewRow();
            row["SiteID"] = this._siteId;
            row["SiteName"] = this._siteName;
            row["SiteDescription"] = this._siteDescription;
            row["SiteUrl"] = Utils.Utils.HtmlEncode(this._siteurl);
            row["CopyRight"] = this._copyright;
            row["Version"] = this._version;
            row["CreatedDate"] = this._createdDate;
            t.Rows.Add(row);
        }

        #endregion

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

            strSql.Append("INSERT INTO fbs_SiteInfo(");
            strSql.Append("SiteID, SiteName, SiteDescription, SiteUrl, CopyRight, Version, CreatedDate)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_SiteID, @in_SiteName, @in_SiteDescription, @in_SiteUrl, @in_CopyRight, @in_Version, @in_CreatedDate)");

            cmdParms.Add("@in_SiteID", DbType.Guid);
            cmdParms.Add("@in_SiteName", DbType.String);
            cmdParms.Add("@in_SiteDescription", DbType.String);
            cmdParms.Add("@in_SiteUrl", DbType.String);
            cmdParms.Add("@in_CopyRight", DbType.String);
            cmdParms.Add("@in_Version", DbType.String);
            cmdParms.Add("@in_CreatedDate", DbType.DateTime);
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

            strSql.Append("UPDATE fbs_SiteInfo SET ");
            strSql.Append("SiteName=@in_SiteName,");
            strSql.Append("SiteDescription=@in_SiteDescription,");
            strSql.Append("SiteUrl=@in_SiteUrl,");
            strSql.Append("CopyRight=@in_CopyRight,");
            strSql.Append("Version=@in_Version,");
            strSql.Append("CreatedDate=@in_CreatedDate");
            strSql.Append(" WHERE SiteID=@in_SiteID");

            cmdParms.Add("@in_SiteID", DbType.Guid);
            cmdParms.Add("@in_SiteName", DbType.String);
            cmdParms.Add("@in_SiteDescription", DbType.String);
            cmdParms.Add("@in_SiteUrl", DbType.String);
            cmdParms.Add("@in_CopyRight", DbType.String);
            cmdParms.Add("@in_Version", DbType.String);
            cmdParms.Add("@in_CreatedDate", DbType.DateTime);
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

            strSql.Append("DELETE FROM fbs_SiteInfo");
            strSql.Append(" WHERE SiteID=@in_SiteID");

            cmdParms.Add("@in_SiteID", DbType.Guid);
        }

        #endregion

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>站点实例</returns>
        public static Site CreateFromReader(IDataReader rd)
        {
            Site s = new Site();

            s._siteId = new Guid(rd["SiteID"].ToString());
            //s._accountMessageVO = new AccountMessageVO() { Id = new Guid(rd["UserID"].ToString()), UserName = rd["UserName"].ToString() };
            s._siteName = rd["SiteName"].ToString();
            s._siteDescription = rd["SiteDescription"].ToString();
            s._siteurl = rd["SiteUrl"].ToString();
            s._copyright = rd["CopyRight"].ToString();
            s._version = rd["Version"].ToString();
            s._createdDate = Convert.ToDateTime(rd["CreatedDate"].ToString());

            return s;
        }

        private Guid _siteId;
        private string _siteName;

        public string SiteName
        {
            get { return _siteName; }
            set { this._siteName = value; }
        }
        private string _siteDescription;

        public string SiteDescription
        {
            get { return _siteDescription; }
            set { this._siteDescription = value; }
        }
        private string _siteurl;

        public string Siteurl
        {
            get { return _siteurl; }
            set { this._siteurl = value; }
        }
        private string _copyright;

        public string Copyright
        {
            get { return _copyright; }
            set { this._copyright = value; }
        }
        private string _version;

        public string Version
        {
            get { return _version; }
        }
        private AccountMessageVO _founder;
        private DateTime _createdDate;
        public SiteSettings Settings { get { return this._settings; } set { this._settings = value; } }
        private SiteSettings _settings;
    }
}
