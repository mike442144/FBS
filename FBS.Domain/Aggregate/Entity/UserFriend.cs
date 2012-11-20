using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class UserFriend:IAggregateRoot
    {
        #region 建立好友关系

        /// <summary>
        /// 创建好友关系
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="fid">好友编号</param>
        /// <param name="fname">好友姓名</param>
        /// <param name="fhead">好友头像</param>
        public UserFriend(Guid uid,Guid fid,string fname,string fhead)
        {
            this._userId = uid;
            this._friendId = fid;
            this._friendName = fname;
            this._friendHead = fhead;
            this._createdOn = DateTime.Now.Date;
        }


        #endregion

        #region 从持久化创建
        private UserFriend() { }

        /// <summary>
        /// 持久化创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>UserFriend模型</returns>
        public static UserFriend CreateFromReader(IDataReader rd)
        {
            UserFriend uf = new UserFriend();
            uf._userId =new Guid( rd["UserID"].ToString());
            uf._friendId =new Guid( rd["FriendID"].ToString());
            uf._friendName=rd["FriendName"].ToString();
            uf._friendHead = rd["FriendHead"].ToString();

            return uf;
        }

        #endregion

        #region 持久化命令

        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (strSql == null || cmdParms == null)
                return;

            strSql.Append("INSERT INTO fbs_UserFriend(");
            strSql.Append("UserID, FriendID, CreatedOn, FriendHead, FriendName)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserID, @in_FriendID, @in_CreatedOn, @in_FriendHead, @in_FriendName)");

            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_FriendID", DbType.Guid);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_FriendHead", DbType.String);
            cmdParms.Add("@in_FriendName", DbType.String);
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

            strSql.Append("UPDATE fbs_UserFriend SET ");
            strSql.Append("CreatedOn=@in_CreatedOn,");
            strSql.Append("FriendHead=@in_FriendHead,");
            strSql.Append("FriendName=@in_FriendName");
            strSql.Append(" WHERE UserID=@in_UserID AND FriendID=@in_FriendID");

            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_FriendID", DbType.Guid);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_FriendHead", DbType.String);
            cmdParms.Add("@in_FriendName", DbType.String);

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

            strSql.Append("SELECT COUNT(1) FROM fbs_UserFriend");
            strSql.Append(" WHERE UserID=@in_UserID AND FriendID=@in_FriendID");

            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_FriendID", DbType.Guid);
        }

        #endregion

        #region 属性
        private DateTime _createdOn;

        public DateTime CreatedOn
        {
            get { return this._createdOn; }
        }

        private string _friendName;

        public string FriendName
        {
            get { return _friendName; }
        }
        private string _friendHead;

        public string FriendHead
        {
            get { return _friendHead; }
        }
        private Guid _userId;

        private Guid _friendId;

        public Guid FriendId
        {
            get { return _friendId; }
        }
        #endregion

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._userId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AlterToRow(DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("FriendID", typeof(Guid));
                t.Columns.Add("CreatedOn", typeof(DateTime));
                t.Columns.Add("FriendHead", typeof(string));
                t.Columns.Add("FriendName", typeof(string));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["UserID"] = this._userId;
            row["FriendID"] = this._friendId;
            row["CreatedOn"] = this._createdOn;
            row["FriendHead"] = this._friendHead;
            row["FriendName"] = this._friendName;
            //添加
            t.Rows.Add(row);
        }

        #endregion
    }
}
