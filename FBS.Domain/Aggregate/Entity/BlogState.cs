using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class BlogState:IAggregateRoot
    {
        public BlogState(string body, AccountMessageVO msgVO)
        {
            this._id = Guid.NewGuid();
            this._pubDate = DateTime.Now;

            this._body = body;
            this._accountMessageVO = msgVO;
        }

        #region 数据库操作命令

        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            strSql.Append("INSERT INTO fbs_BlogState(");
            strSql.Append("BlogStateID, Body, CreatedOn, UserID, UserName, UserHead)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_BlogStateID, @in_Body, @in_CreatedOn, @in_UserID, @in_UserName, @in_UserHead)");

            cmdParms.Add("@in_BlogStateID", DbType.Guid);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserHead", DbType.String);
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

            strSql.Append("UPDATE fbs_BlogState SET ");
            strSql.Append("Body=@in_Body,");
            strSql.Append("CreatedOn=@in_CreatedOn,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("UserHead=@in_UserHead");
            strSql.Append(" WHERE BlogStateID=@in_BlogStateID");
            
            cmdParms.Add("@in_BlogStateID", DbType.Guid);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserHead", DbType.String);


        }

        #endregion

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

        public void AlterToRow(System.Data.DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("BlogStateID", typeof(Guid));
                t.Columns.Add("Body", typeof(string));
                t.Columns.Add("CreatedOn", typeof(DateTime));
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("UserName", typeof(string));
                t.Columns.Add("UserHead", typeof(string));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["BlogStateID"] = this._id ;
            row["Body"] = this._body;
            row["CreatedOn"] = this._pubDate;
            row["UserID"] = this._accountMessageVO.Id;
            row["UserName"] = this._accountMessageVO.UserName;
            row["UserHead"] = this._accountMessageVO.Head;
            //添加
            t.Rows.Add(row);
        }

        #endregion

        private DateTime _pubDate;
        private Guid _id;
        private string _body;
        private AccountMessageVO _accountMessageVO;
    }
}
