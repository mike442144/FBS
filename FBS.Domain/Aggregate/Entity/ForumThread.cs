using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    [AggregateRoot("ForumThread")]
    [Serializable]
    public class ForumThread:IAggregateRoot
    {
        #region 成员变量
        /// <summary>
        /// 帖子ID
        /// </summary>
        private Guid _threadId;

        /// <summary>
        /// 板块
        /// </summary>
        private Guid _forumId;

        public Guid ForumID
        {
            get { return _forumId; }
            set { _forumId = value; }
        }

        /// <summary>
        /// 主题的根帖
        /// </summary>
        private ThreadRootMessage _rootMessage;

        public ThreadRootMessage RootMessage
        {
            get { return _rootMessage; }
            set { this._rootMessage = value; }
        }

        private IList<ForumMessageReply> _messageReply;

        public IList<ForumMessageReply> MessageReply
        {
            get { return this._messageReply; }
            set { this._messageReply = value; }
        }

        /// <summary>
        /// 值对象
        /// </summary>
        private ForumThreadState _state;

        public ForumThreadState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// ForumThread在各个状态下的附加属性
        /// </summary>
        private Property _property;

        /// <summary>
        /// 值对象
        /// </summary>
        private ThreadTagsVO _threadTagsVO;

        #endregion
        
        /// <summary>
        /// 普通对象，可以被缓存并复用
        /// </summary>
        /// <param name="rootMessage">主题的根:帖子</param>
        public ForumThread(ThreadRootMessage rootMessage)
        {
            this._rootMessage = rootMessage;
        }

        /// <summary>
        /// 新建主题构造函数
        /// </summary>
        /// <param name="rootMessage"></param>
        /// <param name="forum"></param>
        public ForumThread(ThreadRootMessage rootMessage,Guid forumId)
        {
            this._rootMessage = rootMessage;
            
            this._forumId = forumId;
            this._state = new ForumThreadState(this) { ClickCount=0, MessageCount=0, ModifiedDate=rootMessage.CreationDate };
            this._threadId = Guid.NewGuid();
            this._rootMessage.ForumThreadID = this._threadId;
        }

        public ForumThread(Guid forumId)
        {
            this._forumId = forumId;
            this._threadId = Guid.NewGuid();
        }

        /// <summary>
        /// 增加帖子点击数
        /// </summary>
        public void AddClickCount()
        {
            this._state.ClickCount++;
        }

        /// <summary>
        /// 增加帖子消息数
        /// </summary>
        public void AddMessageCount()
        {
            this._state.MessageCount++;
        }

        /// <summary>
        /// 减少帖子消息数
        /// </summary>
        public void reduceMessageCount()
        {
            this._state.MessageCount--;
        }
        /// <summary>
        /// 主题创建日期
        /// </summary>
        public DateTime CreationDate
        {
            get
            {
                return this._rootMessage.CreationDate;
            }
            set 
            {
                this._rootMessage.CreationDate = value; 
            }
        }

        public DateTime ModifiedDate
        {
            get { return this._state.ModifiedDate; }
            set { this._state.ModifiedDate = value; }
        }

        private bool _isDigest;

        public bool IsDigest
        {
            get
            {
                return _isDigest;
            }
        }

        //临时对象
        public ForumThread()
        {
            
        }

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._threadId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IEntity 成员


        public void AlterToRow(DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("ID", typeof(Guid));
                t.Columns.Add("ForumID", typeof(Guid));
                t.Columns.Add("RootMessageID", typeof(Guid));
                t.Columns.Add("RewardPoints",typeof(int));
                t.Columns.Add("ModifiedDate",typeof(DateTime));
                t.Columns.Add("CreationDate",typeof(DateTime));
                t.Columns.Add("ClickCount",typeof(int));
                t.Columns.Add("MessageCount",typeof(int));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["ID"] = this._threadId;
            row["ForumID"] = this.ForumID;
            row["RootMessageID"] = this.RootMessage.Id;
            row["RewardPoints"] = this._rootMessage.MessageVO.RewardPoints;
            row["ModifiedDate"] = this.ModifiedDate;
            row["CreationDate"] = this.CreationDate;    
            row["ClickCount"] = this._state.ClickCount;
            row["MessageCount"] = this._state.MessageCount;
            //添加
            t.Rows.Add(row);
        }

        public static ForumThread CreateFromReader(IDataReader r)
        {
            ForumThread a = new ForumThread();
            a._threadId = new Guid(r["ThreadID"].ToString());
            a._state = new ForumThreadState(a) { ModifiedDate=Convert.ToDateTime(r["ModifiedDate"]), ClickCount=Convert.ToInt32(r["ClickCount"]), MessageCount=Convert.ToInt32(r["MessageCount"])};
            a._forumId = new Guid(r["ForumID"].ToString());
            a._rootMessage = new ThreadRootMessage();
            a.CreationDate = Convert.ToDateTime(r["CreationDate"]);
            //仅有Message编号，并无完整对象
            a._rootMessage.Id = new Guid(r["RootMessageID"].ToString());

            return a;
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

            strSql.Append("INSERT INTO fbs_ForumThread(");
            strSql.Append("ThreadID, ForumID, RootMessageID, RewardPoints, ModifiedDate, CreationDate, ClickCount, MessageCount)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ThreadID, @in_ForumID, @in_RootMessageID, @in_RewardPoints, @in_ModifiedDate, @in_CreationDate, @in_ClickCount, @in_MessageCount)");

            cmdParms.Add("@in_ThreadID", DbType.Guid);
            cmdParms.Add("@in_ForumID", DbType.Guid);
            cmdParms.Add("@in_RootMessageID", DbType.Guid);
            cmdParms.Add("@in_RewardPoints", DbType.Int32);
            cmdParms.Add("@in_ModifiedDate", DbType.DateTime);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_ClickCount", DbType.Int32);
            cmdParms.Add("@in_MessageCount", DbType.Int32);
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

            strSql.Append("UPDATE fbs_ForumThread SET ");
            strSql.Append("ForumID=@in_ForumID,");
            strSql.Append("RootMessageID=@in_RootMessageID,");
            strSql.Append("RewardPoints=@in_RewardPoints,");
            strSql.Append("ModifiedDate=@in_ModifiedDate,");
            strSql.Append("CreationDate=@in_CreationDate,");
            strSql.Append("ClickCount=@in_ClickCount,");
            strSql.Append("MessageCount=@in_MessageCount");
            strSql.Append(" WHERE ThreadID=@in_ThreadID");

            cmdParms.Add("@in_ThreadID", DbType.Guid);
            cmdParms.Add("@in_ForumID", DbType.Guid);
            cmdParms.Add("@in_RootMessageID", DbType.Guid);
            cmdParms.Add("@in_RewardPoints", DbType.Int32);
            cmdParms.Add("@in_ModifiedDate", DbType.DateTime);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_ClickCount", DbType.Int32);
            cmdParms.Add("@in_MessageCount", DbType.Int32);

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

            strSql.Append("DELETE FROM fbs_ForumThread");
            strSql.Append(" WHERE ThreadID=@in_ThreadID");

            cmdParms.Add("@in_ThreadID", DbType.Guid);
        }

        #endregion
    }
}
