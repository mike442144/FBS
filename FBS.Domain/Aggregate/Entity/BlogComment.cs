using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;
using FBS.Domain.Aggregate.ValueObject;

namespace FBS.Domain.Aggregate.Entity
{   
    public class BlogComment:IAggregateRoot
    {
        #region 构造函数

        /// <summary>
        /// 新建评论
        /// </summary>
        /// <param name="storyId"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="body"></param>
        public BlogComment(Guid targetId,Guid userId,string name,string body)
        {
            this._targetId = targetId;
            this._accountInfo = new AccountMessageVO(userId, name);
            this._body = body;

            this._commentId = Guid.NewGuid();
            this._creationDate = DateTime.Now;
        }

        public BlogComment(Guid targetId, Guid userId, string name, string body,string userHead)
        {
            this._targetId = targetId;
            this._accountInfo = new AccountMessageVO( userId, name, userHead );
            this._body = body;

            this._commentId = Guid.NewGuid();
            this._creationDate = DateTime.Now;
        }
        private BlogComment() { }

        #endregion

        #region 从持久化创建

        public static BlogComment CreateFromReader(IDataReader dr)
        {
            BlogComment instance = new BlogComment();
            instance._commentId=new Guid(dr["CommentID"].ToString());
            instance._creationDate = Convert.ToDateTime(dr["CreatedOn"]);
            instance._targetId = new Guid(dr["TargetID"].ToString());
            instance._body = dr["Body"].ToString();
            instance._accountInfo = new AccountMessageVO(new Guid(dr["UserID"].ToString()),dr["UserName"].ToString(),dr["UserTiny"].ToString());

            return instance;
        }
        #endregion

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._commentId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AlterToRow(DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("CommentID",typeof(Guid));
                t.Columns.Add("TargetID", typeof(Guid));
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("UserName",typeof(string));
                t.Columns.Add("UserTiny",typeof(string));
                t.Columns.Add("Body",typeof(string));
                t.Columns.Add("CreatedOn",typeof(DateTime));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["CommentID"] = this._commentId;
            row["TargetID"] = this._targetId;
            row["UserID"] = this._accountInfo.Id;
            row["UserName"] = this._accountInfo.UserName;
            row["UserTiny"] = this._accountInfo.Tiny;
            row["Body"] = this._body;
            row["CreatedOn"] = this._creationDate;
            //添加
            t.Rows.Add(row);
        }
        #endregion

        #region 数据库命令生成

        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (strSql == null || cmdParms == null)
                return;

            strSql.Append("INSERT INTO fbs_Comment(");
            strSql.Append("CommentID, TargetID, UserID, UserName,UserTiny, Body, CreatedOn)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_CommentID, @in_TargetID, @in_UserID, @in_UserName,@in_UserTiny, @in_Body, @in_CreatedOn)");

            cmdParms.Add("@in_CommentID", DbType.Guid);
            cmdParms.Add("@in_TargetID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserTiny",DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
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

            strSql.Append("UPDATE fbs_Comment SET ");
            strSql.Append("TargetID=@in_TargetID,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("UserTiny=@in_UserTiny,");
            strSql.Append("Body=@in_Body,");
            strSql.Append("CreatedOn=@in_CreatedOn");
            strSql.Append(" WHERE CommentID=@in_CommentID");

            cmdParms.Add("@in_CommentID", DbType.Guid);
            cmdParms.Add("@in_TargetID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
        }

        public static void PrepareDeleteCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (null == strSql || null == cmdParms)
                return;

            strSql.Append("DELETE FROM fbs_Comment");
            strSql.Append(" WHERE CommentID=@in_CommentID");

            cmdParms.Add("@in_CommentID", DbType.Guid);
        }

        #endregion

        #region 属性

        private Guid _commentId;

        private Guid _targetId;

        public Guid TargetId
        {
            get { return _targetId; }
        }

        private AccountMessageVO _accountInfo;

        public AccountMessageVO AccountInfo
        {
            get { return _accountInfo; }
        }

        private string _body;

        public string Body
        {
            get { return _body; }
        }

        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get { return _creationDate; }
        }

        #endregion

    }
}
