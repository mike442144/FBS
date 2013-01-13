using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class SiteSettings
    {
        public SiteSettings()
        {

        }
        public void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同

            if (t.Columns.Count == 0)
            {
                t.Columns.Add("SiteID", typeof(Guid));
                t.Columns.Add("ThemeName", typeof(string));
                t.Columns.Add("LastModify", typeof(DateTime));
            }

            System.Data.DataRow row = t.NewRow();
            row["SiteID"] = this.SiteID;
            row["ThemeName"] = this.ThemeName;
            row["LastModify"] = this._lastModify;
            t.Rows.Add(row);
        }

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>站点设置</returns>
        public static SiteSettings CreateFromReader(IDataReader rd)
        {
            var instance = new SiteSettings();

            instance._siteId = new Guid(rd["SiteID"].ToString());
            instance._themeName = rd["ThemeName"].ToString();
            instance._lastModify = Convert.ToDateTime(rd["LastModify"].ToString());

            return instance;
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

            strSql.Append("INSERT INTO fbs_SiteSettings(");
            strSql.Append("SiteID, ThemeName, LastModify)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_SiteID, @in_ThemeName, @in_LastModify)");

            cmdParms.Add("@in_SiteID", DbType.Guid);
            cmdParms.Add("@in_ThemeName", DbType.String);
            cmdParms.Add("@in_LastModify", DbType.DateTime);
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

            strSql.Append("UPDATE fbs_SiteSettings SET ");
            strSql.Append("ThemeName=@in_ThemeName,");
            strSql.Append("LastModify=@in_LastModify");
            strSql.Append(" WHERE SiteID=@in_SiteID");

            cmdParms.Add("@in_SiteID", DbType.Guid);
            cmdParms.Add("@in_ThemeName", DbType.String);
            cmdParms.Add("@in_LastModify", DbType.DateTime);
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

            strSql.Append("DELETE FROM fbs_SiteSettings");
            strSql.Append(" WHERE SiteID=@in_SiteID");

            cmdParms.Add("@in_SiteID", DbType.Guid);
        }

        #endregion

        private Guid _siteId;
        private string _themeName;
        private DateTime _lastModify;
        public Guid SiteID { get { return this._siteId; } set { this._siteId = value; } }
        public String ThemeName { get { return this._themeName; } set { this._themeName = value; } }
        public DateTime LastModify { get { return this._lastModify; } set { this._lastModify = value; } }
    }
}
