using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Web;
using System.Data;
using FBS.Domain.Aggregate.ValueObject;

namespace FBS.Domain.Aggregate.Entity
{
    public class Suggest :IAggregateRoot
    {
        #region 新建建议

        public Suggest(string body, Guid accountID, string accountName, string reply, string type)
        {
            this._id = Guid.NewGuid();
            this._accountMessageVO = new AccountMessageVO(accountID,accountName);
            this._creationDate = DateTime.Now;
            this._body = body;
            this._reply = reply;
            this._type = type;
        }

        private Suggest() { }
        #endregion

        #region IEntity成员
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
        /// 转换到数据行
        /// </summary>
        /// <param name="t"></param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("SuggestID",typeof(Guid));
                
                t.Columns.Add("Body",typeof(string));
                t.Columns.Add("UserID",typeof(Guid));
                t.Columns.Add("UserName",typeof(string));
               
             
                t.Columns.Add("CreationDate",typeof(DateTime));
                t.Columns.Add("Reply",typeof(string));
                t.Columns.Add("Type", typeof(string));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["SuggestID"] = this._id;
            
            row["Body"] = this._body;
            row["UserID"] = this._accountMessageVO.Id;
            row["UserName"] = this._accountMessageVO.UserName;
          
          
            row["CreationDate"] = this._creationDate;
            row["Reply"] =this._reply;
            row["Type"] = this._type;

            //添加
            t.Rows.Add(row);
        }

        #endregion
     
        #region 从持久化创建
        
        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static Suggest CreateFromReader(IDataReader rd)
        {
            Suggest s = new Suggest();
            s._id=new Guid(rd["SuggestID"].ToString());
            s._creationDate = Convert.ToDateTime(rd["CreationDate"]);
            s._accountMessageVO = new AccountMessageVO(new Guid(rd["UserID"].ToString()), rd["UserName"].ToString());
            s._body = rd["Body"].ToString();
            s._reply =rd["Reply"].ToString();
            s._type = rd["Type"].ToString();
 
            return s;
        }
        #endregion

        #region 数据库命令
        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (strSql == null || cmdParms == null)
                return;

            strSql.Append("INSERT INTO fbs_Suggest(");
            strSql.Append("SuggestID, Body, UserID, UserName, CreationDate, Reply, Type)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_SuggestID, @in_Body, @in_UserID, @in_UserName, @in_CreationDate, @in_Reply,@in_Type)");

            cmdParms.Add("@in_SuggestID", DbType.Guid);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_Reply", DbType.String);
            cmdParms.Add("@in_Type", DbType.String);
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
            strSql.Append("UPDATE fbs_Suggest SET ");
            strSql.Append("Body=@in_Body,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("CreationDate=@in_CreationDate,");
            strSql.Append("Reply=@in_Reply,");
            strSql.Append("Type=@in_Type");
            strSql.Append(" WHERE SuggestID=@in_SuggestID");


            cmdParms.Add("@in_SuggestID", DbType.Guid);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_Reply", DbType.String);
            cmdParms.Add("@in_Type", DbType.String);
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
            
            strSql.Append("DELETE FROM fbs_Suggest");
            strSql.Append(" WHERE SuggestID=@in_SuggestID");

            
                cmdParms.Add("@in_SuggestID", DbType.Guid);
        }

        #endregion

        #region 属性
        private Guid _id;

        private string _type;
        public string Type
        {
            get { return this._type; }
            set { this._type = value; }
        }
        private string _body;

        public string Body
        {
            get { return this._body; }
            set { this._body = value; }
        }


        private string _reply;

        public string Reply
        {
            get { return this._reply; }
            set { this._reply = value; }
        }

        private AccountMessageVO _accountMessageVO;

        public Guid UserID
        {
            get { return this._accountMessageVO.Id; }
        }
        public string UserName
        {
            get { return this._accountMessageVO.UserName; }
        }

        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get { return _creationDate; }
        }
        #endregion
    }
}
