using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FBS.Domain.Aggregate.ValueObject;
using FBS.Domain.Entity;
using System.Data;
using System.Web;

namespace FBS.Domain.Aggregate.Entity
{
    [Serializable]
    public class Forum:IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public Forum(Guid id)
        {
            this._forumState = new ForumState(this);
            this._formId = id;
        }

        public Forum(string name, string description, int Priority)
        {
            this._name = name;
            this._description = description;
            this._formId = Guid.NewGuid();
            this._creationDate = DateTime.Now;
            this._modifiedDate = this._creationDate;
            this._forumState = new ForumState(this);
            this._priority = Priority;
        }


        public string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string Dsp
        {
            get { return this._description; }
            set { this._description=value; }
        }

        public DateTime CreationDate
        {
            get { return this._creationDate; }
            set { this._creationDate=value; }
        }

        public DateTime ModifiedDate
        {
            get { return this._modifiedDate; }
            set { this._modifiedDate = value; }
        }

        public int ThreadCount
        {
            get { return this._forumState.ThreadCount; }
            set { this._forumState.ThreadCount=value; }
        }

        public int Priority
        {
            get { return this._priority; }
            set { this._priority = value; }
        }
        /// <summary>
        /// 新回复
        /// </summary>
        /// <param name="forumMessageReply">回复消息ForumMessageReply</param>
        public void AddNewMessage(ForumMessageReply forumMessageReply)
        {
            this._forumState.AddMessageCount();//添加回复数
            this._forumState.LastPost = forumMessageReply;//最后回复
            
        }

        /// <summary>
        /// 最近更新
        /// </summary>
        /// <param name="forumMessage">更新的消息,ForumMessage</param>
        public void UpdateNewMessage(ForumMessage forumMessage)
        {
            this._forumState.LastPost = forumMessage;//最后更新
        }

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._formId;
            }
            set
            {
                
            }
        }

        #endregion

        private Guid _formId;

        /// <summary>
        /// 板块名称
        /// </summary>
        private string _name;

        /// <summary>
        /// 板块描述
        /// </summary>
        private string _description;

        /// <summary>
        /// 创建日期
        /// </summary>
        private DateTime _creationDate;

        /// <summary>
        /// 修改日期
        /// </summary>
        private DateTime _modifiedDate;

        /// <summary>
        /// 附加属性
        /// </summary>
        private Property _property;

        /// <summary>
        /// 板块信息,值对象
        /// </summary>
        private ForumState _forumState;
        //热门关键字

        private int _priority;

        


        #region IEntity 成员


        public void AlterToRow(DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("ForumID",typeof(Guid));
                t.Columns.Add("Name",typeof(string));
                t.Columns.Add("Description", typeof(string));
                t.Columns.Add("ModifiedDate",typeof(DateTime));
                t.Columns.Add("CreationDate", typeof(DateTime));
                t.Columns.Add("MessageCount",typeof(int));
                t.Columns.Add("ThreadCount", typeof(int));
                t.Columns.Add("Priority",typeof(int));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["ForumID"] = this._formId;
            row["Name"] = this._name;
            row["Description"] = HttpUtility.HtmlEncode(this._description);
            row["ModifiedDate"] = this._modifiedDate;
            row["CreationDate"] = this._creationDate;
            row["MessageCount"] = this._forumState.MessageCount;
            row["ThreadCount"]=this._forumState.ThreadCount;
            row["Priority"] = this._priority;
            //添加
            t.Rows.Add(row);
        }

        public static Forum CreateFromReader(IDataReader r)
        {
            Forum f = new Forum(new Guid(r["ForumID"].ToString()));
            f._name = r["Name"].ToString();
            f._description = HttpUtility.HtmlDecode(r["Description"].ToString());
            f._modifiedDate = Convert.ToDateTime(r["ModifiedDate"]);
            f._creationDate = Convert.ToDateTime(r["CreationDate"]);
            f._forumState = new ForumState() { MessageCount=Convert.ToInt32(r["MessageCount"]), ThreadCount=Convert.ToInt32(r["ThreadCount"]) };
            f._priority = Int32.Parse( r["Priority"].ToString());
            return f;
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

            strSql.Append("INSERT INTO fbs_Forum(");
            strSql.Append("ForumID, Name, Description, ModifiedDate, CreationDate, MessageCount, ThreadCount, Priority)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ForumID, @in_Name, @in_Description, @in_ModifiedDate, @in_CreationDate, @in_MessageCount, @in_ThreadCount, @in_Priority)");

            cmdParms.Add("@in_ForumID", DbType.Guid);
            cmdParms.Add("@in_Name", DbType.String);
            cmdParms.Add("@in_Description", DbType.StringFixedLength);
            cmdParms.Add("@in_ModifiedDate", DbType.DateTime);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_MessageCount", DbType.Int32);
            cmdParms.Add("@in_ThreadCount", DbType.Int32);
            cmdParms.Add("@in_Priority", DbType.Int32);
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

            strSql.Append("UPDATE fbs_Forum SET ");
            strSql.Append("Name=@in_Name,");
            strSql.Append("Description=@in_Description,");
            strSql.Append("ModifiedDate=@in_ModifiedDate,");
            strSql.Append("CreationDate=@in_CreationDate,");
            strSql.Append("MessageCount=@in_MessageCount,");
            strSql.Append("ThreadCount=@in_ThreadCount,");
            strSql.Append("Priority=@in_Priority");
            strSql.Append(" WHERE ForumID=@in_ForumID");

            cmdParms.Add("@in_ForumID", DbType.Guid);
            cmdParms.Add("@in_Name", DbType.String);
            cmdParms.Add("@in_Description", DbType.StringFixedLength);
            cmdParms.Add("@in_ModifiedDate", DbType.DateTime);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_MessageCount", DbType.Int32);
            cmdParms.Add("@in_ThreadCount", DbType.Int32);
            cmdParms.Add("@in_Priority", DbType.Int32);

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

            strSql.Append("DELETE FROM fbs_Forum");
            strSql.Append(" WHERE ForumID=@in_ForumID");

            cmdParms.Add("@in_ForumID", DbType.Guid);
        }

        #endregion
    }
}
