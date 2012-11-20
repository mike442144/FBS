using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using System.Data;
using System.Web;

namespace FBS.Domain.Aggregate.Entity
{
    [Serializable]
    public class ForumMessageReply:ForumMessage
    {
        public ForumMessageReply(MessageVO messageVO,Guid account,Guid forumId,Guid parentMessageId,Guid threadId):base(messageVO,account,forumId)
        {
            this._parentMessageId = parentMessageId;
            this._forumThreadId = threadId;
        }

        public ForumMessageReply() { }

        private Guid _parentMessageId;

        public Guid ParentMessageID
        {
            get { return this._parentMessageId; }
            set { this._parentMessageId = value; }
        }

        public static ForumMessageReply CreateFromReader(IDataReader dr)
        {
            ForumMessageReply reply = new ForumMessageReply();
            reply._messageId = new Guid(dr["MessageID"].ToString());
            reply._forum = new Guid(dr["ForumID"].ToString());
            reply._account = new Guid(dr["AccountID"].ToString());
            reply._creationDate = Convert.ToDateTime(dr["CreationDate"]);
            reply._modifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
            reply._forumThreadId = new Guid(dr["ThreadID"].ToString());
            MessageVO vo = new MessageVO();
            vo.Body = Utils.Utils.HtmlDecode(dr["Body"].ToString());
            vo.Subject = dr["Subject"].ToString();
            vo.RewardPoints = Convert.ToInt32(dr["RewardPoints"]);

            reply._messageVO = vo;
            
            return reply;
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
            row["ParentMessageID"] = this._parentMessageId;
            row["ThreadID"] = this._forumThreadId;
            row["ForumID"] = this._forum;
            row["AccountID"] = this._account;
            row["Subject"] = null;
            row["Body"] = HttpUtility.HtmlEncode(this._messageVO.Body);
            row["RewardPoints"] = this._messageVO.RewardPoints;
            row["CreationDate"] = this._creationDate;
            row["ModifiedDate"] = this._modifiedDate;
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
