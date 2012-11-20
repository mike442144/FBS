using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class ForbiddenAccount : IAggregateRoot
    {
        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._forbiddenID;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ForbiddenAccount()
        { 
        
        }

        public ForbiddenAccount(Guid AccountID,string UserName,string IP,DateTime forbiddentime,DateTime refreshtime,string state,string forbiddentype)
        {
            this._forbiddenID = Guid.NewGuid();
            this._accountID = AccountID;
            this._iP = IP;
            this._forbiddenTime = forbiddentime;
            this._refreshTime = refreshtime;
            this._state = state;
            this._forbiddenType = forbiddentype;
            this._userName = UserName;
        }

        /// <summary>
        /// 转化成数据行
        /// </summary>
        /// <param name="t"></param>
        public void AlterToRow(DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("ForbiddenID", typeof(Guid));
                t.Columns.Add("AccountID", typeof(Guid));
                t.Columns.Add("UserName", typeof(string));
                t.Columns.Add("IP", typeof(string));
                t.Columns.Add("ForbiddenTime", typeof(DateTime));
                t.Columns.Add("RefreshTime", typeof(DateTime));
                t.Columns.Add("State", typeof(string));
                t.Columns.Add("ForbiddenType", typeof(string));
            }
            //新建行
            System.Data.DataRow row = t.NewRow();
            row["ForbiddenID"] = this._forbiddenID;
            row["AccountID"] = this._accountID;
            row["UserName"] = this._userName;
            row["IP"] = this._iP;
            row["ForbiddenTime"] = this._forbiddenTime;
            row["RefreshTime"] = this._refreshTime;
            row["State"] = this._state;
            row["ForbiddenType"] = this._forbiddenType;
            //添加
            t.Rows.Add(row);
        }

        #endregion


        #region 属性

        private Guid _forbiddenID;


        public Guid ForbiddenId 
        {
            set { throw new NotImplementedException(); }
            get { return this._forbiddenID; }
        }

        private Guid _accountID;

        public Guid AccountID
        {
            set { this._accountID = value; }
            get { return this._accountID; }
        }

        private string _userName;

        public string UserName
        {
            set { this._userName = value; }
            get { return this._userName; }
        }

        private string _iP;

        public string IP
        {
            set { this._iP = value; }
            get { return this._iP; }
        }

        private DateTime _forbiddenTime;

        public DateTime ForbiddenTime
        {
            set { this._forbiddenTime = value; }
            get { return this._forbiddenTime; }
        }


        private DateTime _refreshTime;

        public DateTime RefreshTime
        {
            set { this._refreshTime = value; }
            get { return this._refreshTime; }
        }

        private string _state;

        public string State
        {
            get { return this._state; }
            set { this._state = value; }
        }

        private string _forbiddenType;

        public string ForbiddenType
        {
            get { return this._forbiddenType; }
            set { this._forbiddenType = value; }
        }

        #endregion


        #region 从持久化创建

        /// <summary>
        /// 从数据阅读器创建
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static ForbiddenAccount CreateFromReader(IDataReader rd)
        {
            ForbiddenAccount a = new ForbiddenAccount();
            a._forbiddenID = new Guid(rd["ForbiddenID"].ToString());
            a._accountID = new Guid(rd["AccountID"].ToString());
            a._userName = rd["UserName"].ToString();
            a._iP = rd["IP"].ToString();
            a._forbiddenTime = DateTime.Parse(rd["ForbiddenTime"].ToString());
            a._refreshTime = DateTime.Parse(rd["RefreshTime"].ToString());
            a._forbiddenType = rd["ForbiddenType"].ToString();
            a._state = rd["State"].ToString();
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

            strSql.Append("INSERT INTO fbs_ForbiddenAccount(");
            strSql.Append("ForbiddenID, AccountID, UserName, IP, ForbiddenTime, RefreshTime, State, ForbiddenType)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ForbiddenID, @in_AccountID, @in_UserName, @in_IP, @in_ForbiddenTime, @in_RefreshTime, @in_State, @in_ForbiddenType)");

            cmdParms.Add("@in_ForbiddenID", DbType.Guid);
            cmdParms.Add("@in_AccountID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_IP", DbType.StringFixedLength);
            cmdParms.Add("@in_ForbiddenTime", DbType.DateTime);
            cmdParms.Add("@in_RefreshTime", DbType.DateTime);
            cmdParms.Add("@in_State", DbType.StringFixedLength);
            cmdParms.Add("@in_ForbiddenType", DbType.StringFixedLength);
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

            strSql.Append("UPDATE fbs_ForbiddenAccount SET ");
            strSql.Append("AccountID=@in_AccountID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("IP=@in_IP,");
            strSql.Append("ForbiddenTime=@in_ForbiddenTime,");
            strSql.Append("RefreshTime=@in_RefreshTime,");
            strSql.Append("State=@in_State,");
            strSql.Append("ForbiddenType=@in_ForbiddenType");
            strSql.Append(" WHERE ForbiddenID=@in_ForbiddenID");

            cmdParms.Add("@in_ForbiddenID", DbType.Guid);
            cmdParms.Add("@in_AccountID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_IP", DbType.StringFixedLength);
            cmdParms.Add("@in_ForbiddenTime", DbType.DateTime);
            cmdParms.Add("@in_RefreshTime", DbType.DateTime);
            cmdParms.Add("@in_State", DbType.StringFixedLength);
            cmdParms.Add("@in_ForbiddenType", DbType.StringFixedLength);
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

            strSql.Append("DELETE FROM fbs_ForbiddenAccount");
            strSql.Append(" WHERE ForbiddenID=@in_ForbiddenID");

            cmdParms.Add("@in_ForbiddenID", DbType.Guid);
        }

        #endregion



    }
}
