using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class TopForumThread : IAggregateRoot
    {
        #region IEntity 成员

        /// <summary>
        /// 广告编号
        /// </summary>
        public Guid Id
        {
            get
            {
                return this._ID;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public TopForumThread()
        { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aid"></param>
        /// <param name="bid"></param>
        public TopForumThread(Guid aid,Guid bid)
        {
            this._TopForumThreadID = aid;
            this._ID= Guid.NewGuid();
            this._TopForumID = bid;
            this._CreatTime = DateTime.Now;
        }

        public TopForumThread(Guid aid)
        {
            this._TopForumThreadID = aid;
            this._ID = Guid.NewGuid();
            this.TopForumID = Guid.Empty;
            this._CreatTime = DateTime.Now;
 
        }

        public TopForumThread(Guid id, Guid aid, Guid bid, DateTime t)
        {
            this._TopForumThreadID = aid;
            this._ID = id;
            this._TopForumID = bid;
            this._CreatTime = t;
        }


        #region 从持久化创建

        /// <summary>
        /// 从数据阅读器创建
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static TopForumThread CreateFromReader(IDataReader rd)
        {
            TopForumThread a = new TopForumThread();
            a._ID = new Guid(rd["ID"].ToString());
            a._TopForumThreadID = new Guid(rd["TopForumThreadID"].ToString());
            a._TopForumID = new Guid(rd["TopForumID"].ToString());
            a._CreatTime = DateTime.Parse(rd["CreatTime"].ToString());
            return a;
        }
        #endregion



        /// <summary>
        /// 转化为数据行
        /// </summary>
        /// <param name="t">数据表</param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同

            if (t.Columns.Count == 0)
            {
                t.Columns.Add("ID", typeof(Guid));
                t.Columns.Add("TopForumThreadID", typeof(Guid));
                t.Columns.Add("TopForumID", typeof(Guid));
                t.Columns.Add("CreatTime", typeof(DateTime));
            }

            DataRow row = t.NewRow();
            row["ID"] = this._ID;
            row["TopForumThreadID"] = this._TopForumThreadID;
            row["TopForumID"] = this._TopForumID;
            row["CreatTime"] = this._CreatTime;
            t.Rows.Add(row);
        }

        #endregion


        #region 属性
        /// <summary>
        ///编号
        /// </summary>
        private Guid _ID;
        /// <summary>
        /// 主题编号 image ,flash
        /// </summary>
        /// 
        private Guid _TopForumThreadID;

        private Guid _TopForumID;

        private DateTime _CreatTime;
       

        public DateTime CreatTime
        {
            set { this._CreatTime = value; }
            get { return this._CreatTime;  }
        }
        public Guid TopForumThreadID
        {
            set { this._TopForumThreadID = value; }
            get { return this._TopForumThreadID; }
        }

        public Guid TopForumID
        {
            set { this._TopForumID = value; }
            get { return this._TopForumID; }
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

            strSql.Append("INSERT INTO fbs_TopForumThread(");
            strSql.Append(" ID, TopForumThreadID,TopForumID, CreatTime)");
            strSql.Append(" VALUES (");
            strSql.Append(" @in_ID,@in_TopForumThreadID, @in_TopForumID, @in_CreatTime)");

            cmdParms.Add("@in_ID", DbType.Guid);
            cmdParms.Add("@in_TopForumThreadID", DbType.Guid);       
            cmdParms.Add("@in_TopForumID", DbType.Guid);
            cmdParms.Add("@in_CreatTime", DbType.DateTime);
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

            strSql.Append("UPDATE fbs_TopForumThread SET ");
            strSql.Append("TopForumThreadID=@in_TopForumThreadID,");
            strSql.Append("TopForumID=@in_TopForumID,");
            strSql.Append("CreatTime=@in_CreatTime");
            strSql.Append(" WHERE ID=@in_ID");

            cmdParms.Add("@in_ID", DbType.Guid);
            cmdParms.Add("@in_TopForumThreadID", DbType.Guid);
            cmdParms.Add("@in_TopForumID", DbType.Guid);
            cmdParms.Add("@in_CreatTime", DbType.DateTime);
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

            strSql.Append("DELETE FROM fbs_TopForumThread");
            strSql.Append(" WHERE ID=@in_ID");

            cmdParms.Add("@in_ID", DbType.Guid);
        }

        #endregion

    }
}
