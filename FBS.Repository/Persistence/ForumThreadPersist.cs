using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.ValueObject;
using System.Collections;
using FBS.Utils;
using FBS.Domain.Log;

namespace FBS.Repository.Persistence
{
    /// <summary>
    /// 
    /// </summary>
    internal class ForumMessagePersist :DBUtility.DALHelper
    {
        public static DbConnection conn = null;
        public static DbTransaction trans = null;

        /// <summary>
        /// 添加所有消息
        /// </summary>
        /// <param name="Province">数据表</param>
        public static void AddAll(DataTable table)
        {
            
            using (conn = DataHelper.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                foreach (DataRow row in table.Rows)
                    PersistMessage(row);
                trans.Commit();
            }
            table.Clear();
            table = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Province"></param>
        public static void UpdateAll(DataTable Province)
        {
 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Province"></param>
        public static void RemoveAll(DataTable Province)
        {
 
        }

        /// <summary>
        /// 获取回复列表，实现分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="startIndex"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static IList<ForumMessageReply> GetFromDbByRootMsgId(Guid id,int startIndex,int len)
        {
            IList<ForumMessageReply> list = new List<ForumMessageReply>();

            using (DbDataReader db = DataHelper.GetPageList("fbs_Message", "CreationDate", "ParentMessageID='" + id.ToString()+"'", startIndex, len))
            {
                while (db.Read())
                {
                    list.Add(ForumMessageReply.CreateFromReader(db));
                }
            }


            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("SELECT top ("+len.ToString()+") * FROM fbs_Message ");
            //strSql.Append(" WHERE ParentMessageID=@in_ParentMessageID ORDER BY CreationDate ASC");
            //DbParameter[] cmdParms = new DbParameter[]{
            //    DataHelper.CreateInDbParameter("@in_ParentMessageID", DbType.Guid, id)};

            
            //using (DbDataReader dr=DataHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            //{
            //    while (dr.Read())
            //    {
            //        list.Add(ForumMessageReply.CreateFromReader(dr));
            //    }
            //}

            return list;
        }

        /// <summary>
        /// 持久化ForumMessage
        /// </summary>
        /// <param name="r">数据行</param>
        private static void PersistMessage(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_Message(");
            strSql.Append("MessageID,ParentMessageID,ThreadID,ForumID,AccountID,Subject,Body,RewardPoints,CreationDate,ModifiedDate)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ID,@in_ParentMessageID,@in_ThreadID,@in_ForumID,@in_AccountID,@in_Subject,@in_Body,@in_RewardPoints,@in_CreationDate,@in_ModifiedDate)");
            DbParameter[] cmdParms = new DbParameter[]{
				DataHelper.CreateInDbParameter("@in_ID", DbType.Guid,r["MessageID"]),
				DataHelper.CreateInDbParameter("@in_ParentMessageID", DbType.Guid,r["ParentMessageID"]),
				DataHelper.CreateInDbParameter("@in_ThreadID", DbType.Guid, r["ThreadID"]),
				DataHelper.CreateInDbParameter("@in_ForumID", DbType.Guid, r["ForumID"]),
				DataHelper.CreateInDbParameter("@in_AccountID", DbType.Guid, r["AccountID"]),
				DataHelper.CreateInDbParameter("@in_Subject", DbType.String, r["Subject"]),
				DataHelper.CreateInDbParameter("@in_Body", DbType.String, r["Body"]),
				DataHelper.CreateInDbParameter("@in_RewardPoints", DbType.Int32, r["RewardPoints"]),
				DataHelper.CreateInDbParameter("@in_CreationDate", DbType.DateTime, r["CreationDate"]),
				DataHelper.CreateInDbParameter("@in_ModifiedDate", DbType.DateTime, r["ModifiedDate"])};

            //DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
            DataHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), cmdParms);
        }

        internal static ForumMessageReply GetByKey(Guid key)
        {
            ForumMessageReply target = null;
            using (DbDataReader db = DataHelper.ExecuteReader(CommandType.Text,"select * from fbs_Message where MessageID='"+key.ToString()+"'"))
            {
                if (db.Read())
                {
                    target = ForumMessageReply.CreateFromReader(db);
                }
            }

            return target;
        }
    }
    internal class ForumThreadBuilder
    {
        internal ForumThreadBuilder()
        {
        }

        ~ForumThreadBuilder()
        {
 
        }

        /// <summary>
        /// 从数据库构造根帖
        /// </summary>
        /// <param name="reader">数据读取器</param>
        /// <returns>根帖</returns>
        internal ThreadRootMessage BuildRootMessage(IDataReader reader)
        {
            //新实例
            //ThreadRootMessage rootmsg = ThreadRootMessage.CreateFromPersist(
            //    new Guid(reader["MessageID"].ToString()), 
            //    Convert.ToDateTime(reader["CreationDate"]), 
            //    Convert.ToDateTime(reader["ModifiedDate"]), 
            //    new Guid(reader["AccountID"].ToString()),
            //    new Guid(reader["ForumID"].ToString()),
            //    new MessageVO() 
            //    { 
            //        Body = Utils.Utils.HtmlDecode(reader["Body"].ToString()), 
            //        Subject = reader["Subject"].ToString(), 
            //        RewardPoints = Convert.ToInt32(reader["RewardPoints"]) 
            //    });

            ThreadRootMessage rootmsg = ThreadRootMessage.CreateFromReader(reader);

            return rootmsg;
        }
    }

    internal class ForumThreadPersist:DBUtility.DALHelper
    {
        /// <summary>
        /// 辅助构造对象
        /// </summary>
        static ForumThreadBuilder builder = new ForumThreadBuilder();

        public static HashSet<ForumThread> GetThread(string cmdText)
        {
            HashSet<ForumThread> thread = new HashSet<ForumThread>();
            using(IDataReader reader = DataHelper.ExecuteReader(CommandType.Text,cmdText ,null))
            {
                while (reader.Read())
                {
                    ForumThread tmp = ForumThread.CreateFromReader(reader);
                    tmp.RootMessage = builder.BuildRootMessage(reader);
                    thread.Add(tmp);
                }
            }
            return thread;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static HashSet<ForumThread> GetAllForumThread(Guid forumId,int startIndex,int count)
        {
            HashSet<ForumThread> thread = new HashSet<ForumThread>();
            string feild = " fbs_ForumThread.ThreadID, fbs_ForumThread.ForumID, fbs_ForumThread.RootMessageID, fbs_ForumThread.RewardPoints, fbs_ForumThread.ModifiedDate, fbs_ForumThread.CreationDate, fbs_ForumThread.ClickCount, fbs_ForumThread.MessageCount, fbs_ForumThread.MessageSubject, fbs_ForumThread.UserID, fbs_ForumThread.UserName, fbs_Message.MessageID, fbs_Message.ParentMessageID, fbs_Message.AccountID, fbs_Message.Subject, fbs_Message.Body";
            //string feild = "*";
            string sql = "SELECT TOP " + count + "* FROM (SELECT ROW_NUMBER() OVER (ORDER BY fbs_ForumThread.ModifiedDate DESC) AS RowNumber," + feild + " FROM fbs_ForumThread inner join fbs_Message on fbs_ForumThread.RootMessageID=fbs_Message.MessageID AND fbs_Message.ThreadID=fbs_ForumThread.ThreadID WHERE fbs_ForumThread.ForumID='" + forumId + "') A WHERE RowNumber > " + startIndex;

            //string sql = "select * from fbs_ForumThread inner join fbs_Message on fbs_ForumThread.RootMessageID=fbs_Message.MessageID";

            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                {
                    ForumThread tmp = ForumThread.CreateFromReader(reader);

                    tmp.RootMessage = builder.BuildRootMessage(reader);
                    //tmp.RootMessage.ForumThread = tmp;

                    thread.Add(tmp);
                }
            }

            return thread;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Province"></param>
        public static void AddAll(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                Persist(row);
            }
            table.Clear();
            table = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Province"></param>
        public static void UpdateAll(DataTable Province) 
        {
            foreach (DataRow row in Province.Rows)
            {
                Update(row);
            }
            Province.Clear();
            Province = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Province"></param>
        public static void RemoveAll(DataTable table)
        {
            foreach (DataRow row in table.Rows)
                Remove(row);
            table.Clear();
            table = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        private static void Remove(DataRow r)
        {

        }

        /// <summary>
        /// 更新ForumThread
        /// </summary>
        /// <param name="r"></param>
        private static void Update(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE fbs_ForumThread SET ");
            strSql.Append("ForumID=@in_ForumID,");
            strSql.Append("RootMessageID=@in_RootMessageID,");
            strSql.Append("RewardPoints=@in_RewardPoints,");
            strSql.Append("ModifiedDate=@in_ModifiedDate,");
            strSql.Append("CreationDate=@in_CreationDate,");
            strSql.Append("ClickCount=@in_ClickCount,");
            strSql.Append("MessageCount=@in_MessageCount");
            strSql.Append(" WHERE ThreadID=@in_ThreadID");
            DbParameter[] cmdParms = new DbParameter[]{
				DataHelper.CreateInDbParameter("@in_ForumID", DbType.Guid, r["ForumID"]),
				DataHelper.CreateInDbParameter("@in_RootMessageID", DbType.Guid, r["RootMessageID"]),
				DataHelper.CreateInDbParameter("@in_RewardPoints", DbType.Int32, r["RewardPoints"]),
				DataHelper.CreateInDbParameter("@in_ModifiedDate", DbType.DateTime, r["CreationDate"]),
				DataHelper.CreateInDbParameter("@in_CreationDate", DbType.DateTime,  r["CreationDate"]),
				DataHelper.CreateInDbParameter("@in_ClickCount", DbType.Int32, r["ClickCount"]),
				DataHelper.CreateInDbParameter("@in_MessageCount", DbType.Int32, r["MessageCount"]),
				DataHelper.CreateInDbParameter("@in_ThreadID", DbType.Guid,r["ID"])
            };

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        /// <summary>
        /// 持久化ForumThread
        /// </summary>
        /// <param name="r">数据行</param>
        private static void Persist(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_ForumThread(");
            strSql.Append("ThreadID,ForumID,RootMessageID,RewardPoints,ModifiedDate,CreationDate,ClickCount,MessageCount)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ID,@in_ForumID,@in_RootMessageID,@in_RewardPoints,@in_ModifiedDate,@in_CreationDate,@in_ClickCount,@in_MessageCount)");
            DbParameter[] cmdParms = new DbParameter[]{
				DataHelper.CreateInDbParameter("@in_ID", DbType.Guid,r["ID"]),
				DataHelper.CreateInDbParameter("@in_ForumID", DbType.Guid, r["ForumID"]),
				DataHelper.CreateInDbParameter("@in_RootMessageID", DbType.Guid, r["RootMessageID"]),
				DataHelper.CreateInDbParameter("@in_RewardPoints", DbType.Int32, r["RewardPoints"]),
				DataHelper.CreateInDbParameter("@in_ModifiedDate", DbType.DateTime, r["ModifiedDate"]),
				DataHelper.CreateInDbParameter("@in_CreationDate", DbType.DateTime, r["CreationDate"]),
                DataHelper.CreateInDbParameter("@in_ClickCount",DbType.Int32,r["ClickCount"]),
                DataHelper.CreateInDbParameter("@in_MessageCount",DbType.Int32,r["MessageCount"])
            };

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
    }
}
