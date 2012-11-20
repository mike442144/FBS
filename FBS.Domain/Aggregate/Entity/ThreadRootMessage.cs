using System;
using System.Collections.Generic;
using System.Text;
using FBS.Domain.Aggregate.ValueObject;
using System.Data;
using System.Web;

namespace FBS.Domain.Aggregate.Entity
{
    [Serializable]
    public class ThreadRootMessage:ForumMessage
    {
        public static ThreadRootMessage CreateFromPersist(Guid id, DateTime creationDate, DateTime modifiedDate,Guid account ,Guid forumId,MessageVO msgVO)
        {
            ThreadRootMessage trm = new ThreadRootMessage();
            trm._messageId = id;
            trm._creationDate = creationDate;
            trm._modifiedDate = modifiedDate;
            trm._account = account;
            trm._forum = forumId;
            trm._messageVO = msgVO;

            return trm;
        }

        public ThreadRootMessage(MessageVO messageVO,Guid account,Guid forumId):base(messageVO,account,forumId)
        {

        }

        public ThreadRootMessage()
        { }

        public static ThreadRootMessage CreateFromReader(IDataReader r)
        {
            ThreadRootMessage msg = new ThreadRootMessage();

            msg._messageId = new Guid(r["MessageID"].ToString());
            msg._creationDate =Convert.ToDateTime(r["CreationDate"]);
            msg._modifiedDate = Convert.ToDateTime(r["ModifiedDate"]);
            msg._forumThreadId = new Guid(r["ThreadID"].ToString());
            msg._forum = new Guid(r["ForumID"].ToString());
            msg._account = new Guid(r["AccountID"].ToString());
            MessageVO msgVO = new MessageVO();
            msgVO.Body = HttpUtility.HtmlDecode(r["Body"].ToString());
            msgVO.RewardPoints = Convert.ToInt32(r["RewardPoints"]??0);
            msgVO.Subject = r["Subject"].ToString();

            msg._messageVO = msgVO;

            return msg;
        }

        public override void AlterToRow(DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("MessageID",typeof(Guid));
                t.Columns.Add("ParentMessageID", typeof(Guid));
                t.Columns.Add("ThreadID", typeof(Guid));
                t.Columns.Add("ForumID", typeof(Guid));
                t.Columns.Add("AccountID", typeof(Guid));
                t.Columns.Add("Subject",typeof(string));
                t.Columns.Add("Body",typeof(string));
                t.Columns.Add("RewardPoints",typeof(int));
                t.Columns.Add("CreationDate",typeof(DateTime));
                t.Columns.Add("ModifiedDate",typeof(DateTime));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();

            row["MessageID"] = this._messageId;
            row["ParentMessageID"] = Guid.Empty;//DBNull.Value;//null;
            row["ThreadID"] = this._forumThreadId;
            row["ForumID"] = this._forum;
            row["AccountID"] = this.Account;
            row["Subject"]=this._messageVO.Subject;
            row["Body"] = Utils.Utils.HtmlEncode(this._messageVO.Body);
            row["RewardPoints"] = this._messageVO.RewardPoints;
            row["CreationDate"] = this._creationDate;
            row["ModifiedDate"] = this.ModifiedDate;

            //添加
            t.Rows.Add(row);
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


            strSql.Append("INSERT INTO fbs_Message(");
            strSql.Append("MessageID, ParentMessageID, ThreadID, ForumID, AccountID, Subject, Body, RewardPoints, CreationDate, ModifiedDate)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_MessageID, @in_ParentMessageID, @in_ThreadID, @in_ForumID, @in_AccountID, @in_Subject, @in_Body, @in_RewardPoints, @in_CreationDate, @in_ModifiedDate)");

            cmdParms.Add("@in_MessageID", DbType.Guid);
            cmdParms.Add("@in_ParentMessageID", DbType.Guid);
            cmdParms.Add("@in_ThreadID", DbType.Guid);
            cmdParms.Add("@in_ForumID", DbType.Guid);
            cmdParms.Add("@in_AccountID", DbType.Guid);
            cmdParms.Add("@in_Subject", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_RewardPoints", DbType.Int32);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_ModifiedDate", DbType.DateTime);
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

            strSql.Append("UPDATE fbs_Message SET ");
            strSql.Append("ParentMessageID=@in_ParentMessageID,");
            strSql.Append("ThreadID=@in_ThreadID,");
            strSql.Append("ForumID=@in_ForumID,");
            strSql.Append("AccountID=@in_AccountID,");
            strSql.Append("Subject=@in_Subject,");
            strSql.Append("Body=@in_Body,");
            strSql.Append("RewardPoints=@in_RewardPoints,");
            strSql.Append("CreationDate=@in_CreationDate,");
            strSql.Append("ModifiedDate=@in_ModifiedDate");
            strSql.Append(" WHERE MessageID=@in_MessageID");

            cmdParms.Add("@in_ParentMessageID", DbType.Guid);
            cmdParms.Add("@in_ThreadID", DbType.Guid);
            cmdParms.Add("@in_ForumID", DbType.Guid);
            cmdParms.Add("@in_AccountID", DbType.Guid);
            cmdParms.Add("@in_Subject", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_RewardPoints", DbType.Int32);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_ModifiedDate", DbType.DateTime);
            cmdParms.Add("@in_MessageID", DbType.Guid);
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

            strSql.Append("DELETE FROM fbs_Message");
            strSql.Append(" WHERE MessageID=@in_MessageID");

            cmdParms.Add("@in_MessageID", DbType.Guid);
        }

        #endregion
    }
}
