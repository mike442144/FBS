using System;
using System.Collections.Generic;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class Needs : IAggregateRoot
    {
        #region IEntity 成员

        /// <summary>
        /// 广告编号
        /// </summary>
        public Guid Id
        {
            get
            {
                return this._needsID;
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
                t.Columns.Add("NeedsID", typeof(Guid));
                t.Columns.Add("NeedsContent", typeof(string)); 
            }

            DataRow row = t.NewRow();
            row["NeedsID"] = this._needsID;
            row["NeedsContent"] = this._needsContent;
            t.Rows.Add(row);
        }

        #endregion

        #region 从持久化创建

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>文章实例</returns>
        public static Needs CreateFromReader(IDataReader rd)
        {
            Needs a = new Needs();

            a._needsContent = rd["NeedsContent"].ToString();
            a._needsID = new Guid(rd["NeedsID"].ToString());
            
            return a;
        }
        #endregion

        #region 属性
        private Guid _needsID;
        private string _needsContent;
        #endregion

        public Guid NeedsID
        {
            set { this._needsID = value; }
            get { return this._needsID; }
        }

        public string NeedsContent
        {
            set { this._needsContent = value; }
            get { return this._needsContent; }
        }

        public Needs()
        { 
        
        }

        public Needs(string content)
        {
            this._needsID = Guid.NewGuid();
            this._needsContent = content;
        }

        public Needs(Guid aid,string content)
        {
            this._needsID = aid;
            this._needsContent = content;
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

            strSql.Append("INSERT INTO fbs_Needs(");
            strSql.Append("NeedsID, NeedsContent)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_NeedsID, @in_NeedsContent)");

            cmdParms.Add("@in_NeedsID", DbType.Guid);
            cmdParms.Add("@in_NeedsContent", DbType.String);
            
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

            strSql.Append("UPDATE fbs_Needs SET ");
            strSql.Append("NeedsContent=@in_NeedsContent");
            strSql.Append(" WHERE NeedsID=@in_NeedsID");

            cmdParms.Add("@in_NeedsID", DbType.Guid);
            cmdParms.Add("@in_NeedsContent", DbType.String);
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

            strSql.Append("DELETE FROM fbs_Needs");
            strSql.Append(" WHERE NeedsID=@in_NeedsID");

            cmdParms.Add("@in_NeedsID", DbType.Guid);
        }

        #endregion
    }
}
