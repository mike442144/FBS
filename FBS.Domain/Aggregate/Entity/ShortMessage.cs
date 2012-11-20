using System;
using System.Collections.Generic;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class ShortMessage:IAggregateRoot
    {
        #region 新消息

        /// <summary>
        /// 新消息
        /// </summary>
        /// <param name="uid">发送用户编号</param>
        /// <param name="uname">发送用户昵称</param>
        /// <param name="uhead">发送用户头像</param>
        /// <param name="sendToId">接收用户编号</param>
        /// <param name="sendToName">接收用户昵称</param>
        /// <param name="sendToHead">接收用户头像</param>
        /// <param name="title">消息标题</param>
        /// <param name="body">消息内容</param>
        public ShortMessage(Guid uid,string uname,string uhead,Guid sendToId,string sendToName,string sendToHead,string title,string body)
        {
            this._id = Guid.NewGuid();

            this._sender = new AccountMessageVO(uid,uname,uhead );
            this._sendTo = new AccountMessageVO(sendToId,sendToName,sendToHead );

            this._title = title;
            this._body = body;

            this._sentOn = DateTime.Now;
        }
        public ShortMessage(Guid uid, string uname, string uhead, Guid sendToId, string sendToName, string sendToHead, string title)
        {
            this._id = Guid.NewGuid();

            this._sender = new AccountMessageVO( uid, uname,uhead );
            this._sendTo = new AccountMessageVO(sendToId, sendToName, sendToHead );

            this._title = title;
            //this._body = body;

            this._sentOn = DateTime.Now;
        }
        #endregion

        public void SetReadTag()
        {
            this._hasRead = true;
        }

        #region 从持久化创建
        private ShortMessage() { }

        /// <summary>
        /// 从持久化创建对象
        /// </summary>
        /// <param name="dr">数据阅读器</param>
        /// <returns>短消息</returns>
        public static ShortMessage CreateFromReader(IDataReader dr)
        {
            ShortMessage sm = new ShortMessage();
            sm._id = new Guid(dr["ShortMessageID"].ToString());
            sm._title =Utils.Utils.HtmlDecode(dr["MessageTitle"].ToString());
            sm._body = Utils.Utils.HtmlDecode(dr["MessageBody"].ToString());
            sm._sentOn = Convert.ToDateTime(dr["SentOn"]);
            sm._sender = new AccountMessageVO(new Guid(dr["SenderID"].ToString()),  dr["SenderName"].ToString(),  dr["SenderHead"].ToString());
            sm._sendTo = new AccountMessageVO(new Guid(dr["SendToID"].ToString()),  dr["SendToName"].ToString(),  dr["SendToHead"].ToString());
            sm._hasRead =Convert.ToBoolean( dr["HasRead"]);
            return sm;
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

            strSql.Append("INSERT INTO fbs_ShortMessage(");
            strSql.Append("ShortMessageID, SenderID, SenderName, SenderHead, SendToID, SendToName, SendToHead, MessageTitle, MessageBody, SentOn)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ShortMessageID, @in_SenderID, @in_SenderName, @in_SenderHead, @in_SendToID, @in_SendToName, @in_SendToHead, @in_MessageTitle, @in_MessageBody, @in_SentOn)");

            cmdParms.Add("@in_ShortMessageID", DbType.Guid);
            cmdParms.Add("@in_SenderID", DbType.Guid);
            cmdParms.Add("@in_SenderName", DbType.String);
            cmdParms.Add("@in_SenderHead", DbType.String);
            cmdParms.Add("@in_SendToID", DbType.Guid);
            cmdParms.Add("@in_SendToName", DbType.String);
            cmdParms.Add("@in_SendToHead", DbType.String);
            cmdParms.Add("@in_MessageTitle", DbType.String);
            cmdParms.Add("@in_MessageBody", DbType.String);
            cmdParms.Add("@in_SentOn", DbType.DateTime);
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

            strSql.Append("UPDATE fbs_ShortMessage SET ");
            strSql.Append("SenderID=@in_SenderID,");
            strSql.Append("SenderName=@in_SenderName,");
            strSql.Append("SenderHead=@in_SenderHead,");
            strSql.Append("SendToID=@in_SendToID,");
            strSql.Append("SendToName=@in_SendToName,");
            strSql.Append("SendToHead=@in_SendToHead,");
            strSql.Append("MessageTitle=@in_MessageTitle,");
            strSql.Append("MessageBody=@in_MessageBody,");
            strSql.Append("SentOn=@in_SentOn,");
            strSql.Append("HasRead=@in_HasRead");
            strSql.Append(" WHERE ShortMessageID=@in_ShortMessageID");

            cmdParms.Add("@in_ShortMessageID", DbType.Guid);
            cmdParms.Add("@in_SenderID", DbType.Guid);
            cmdParms.Add("@in_SenderName", DbType.String);
            cmdParms.Add("@in_SenderHead", DbType.String);
            cmdParms.Add("@in_SendToID", DbType.Guid);
            cmdParms.Add("@in_SendToName", DbType.String);
            cmdParms.Add("@in_SendToHead", DbType.String);
            cmdParms.Add("@in_MessageTitle", DbType.String);
            cmdParms.Add("@in_MessageBody", DbType.String);
            cmdParms.Add("@in_SentOn", DbType.DateTime);
            cmdParms.Add("@in_HasRead", DbType.Boolean);
    
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

            strSql.Append("DELETE FROM fbs_ShortMessage");
            strSql.Append(" WHERE ShortMessageID=@in_ShortMessageID");

            cmdParms.Add("@in_ShortMessageID", DbType.Guid);
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

        /// <summary>
        /// 转换为数据行
        /// </summary>
        /// <param name="t">数据表</param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("ShortMessageID", typeof(Guid));
                t.Columns.Add("SenderID", typeof(Guid));
                t.Columns.Add("SenderName", typeof(string));
                t.Columns.Add("SenderHead", typeof(string));
                t.Columns.Add("SendToID", typeof(Guid));
                t.Columns.Add("SendToName", typeof(string));
                t.Columns.Add("SendToHead", typeof(string));
                t.Columns.Add("MessageTitle",typeof(string));
                t.Columns.Add("MessageBody",typeof(string));
                t.Columns.Add("SentOn",typeof(DateTime));
                t.Columns.Add("HasRead",typeof(bool));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["ShortMessageID"] = this._id;
            row["SenderID"] = this._sender.Id;
            row["SenderName"] = this._sender.UserName;
            row["SenderHead"] = this._sender.Head;
            row["SendToID"] = this._sendTo.Id;
            row["SendToName"] = this._sendTo.UserName;
            row["SendToHead"] = this._sendTo.Head;
            row["MessageTitle"] = Utils.Utils.HtmlEncode(this._title);
            row["MessageBody"] = Utils.Utils.HtmlEncode(this._body);
            row["SentOn"] = this._sentOn;
            row["HasRead"]=this._hasRead;
            //添加
            t.Rows.Add(row);
        }

        #endregion

        #region 属性

        public Guid ReciverID
        {
            get { return this._sendTo.Id; }
        }
        public Guid SenderID
        {
            get { return this._sender.Id; }
        }
        public string SenderName
        {
            get { return this._sender.UserName; }
        }
        public string SenderHead
        {
            get { return this._sender.Head; }
        }

        public DateTime SentOn
        {
            get { return this._sentOn; }
        }
        public string Title
        {
            get { return this._title; }
        }
        public string Body
        {
            get { return this._body; }
            set { this._body = value; }
        }
        public bool HasRead
        {
            get { return this._hasRead; }
        }

        private Guid _id;
        private AccountMessageVO _sender;
        private AccountMessageVO _sendTo;
        private string _title;
        private string _body;
        private DateTime _sentOn;
        private bool _hasRead;

        #endregion
    }
}
