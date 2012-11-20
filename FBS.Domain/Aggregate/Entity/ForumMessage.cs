using System;
using System.Collections.Generic;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using System.Collections;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    [Serializable]
    public class ForumMessage:IAggregateRoot
    {
        public ForumMessage()
        {
            this._messageVO = new MessageVO();
            //this._attachment = new Attachment(this);
            this._messageId = Guid.NewGuid();
        }

        
        public ForumMessage(MessageVO messageVO,Guid account,Guid forumId)
        {
            this._messageVO = messageVO;
            this._account = account;
            this._creationDate = DateTime.Now;
            this._modifiedDate = this._creationDate;
            this._forum = forumId;
            //this._forumThreadId = threadId;
            this._messageId = Guid.NewGuid();
        }

        protected Guid _messageId;
        protected DateTime _creationDate;
        public DateTime CreationDate
        {
            get
            {
                return _creationDate;
            }
            set { this._creationDate = value; }
        }

        protected DateTime _modifiedDate;

        public DateTime ModifiedDate
        {
            get { return _modifiedDate; }
            set { this._modifiedDate = value; }
        }

        protected Guid _account;

        public Guid Account
        {
            get { return _account; }
            set { _account = value; }
        }
        protected Guid _operator;
        protected Guid _forumThreadId;

        public Guid ForumThreadID
        {
            get { return _forumThreadId; }
            set { _forumThreadId = value; }
        }
        protected Guid _forum;

        public Guid Forum
        {
            get { return _forum; }
            set { _forum = value; }
        }
        protected MessageVO _messageVO;
        protected bool _replyIsNotified;
        public MessageVO MessageVO
        {
            get { return _messageVO; }
        }
        protected IList _outFilters;
        //附件
        protected Attachment _attachment;

        public bool IsMasked()
        {
            throw new NotImplementedException();
        }

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._messageId;
            }
            set
            {
                this._messageId = value;
            }
        }

        public virtual void AlterToRow(DataTable table) { throw new NotImplementedException(); }

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
