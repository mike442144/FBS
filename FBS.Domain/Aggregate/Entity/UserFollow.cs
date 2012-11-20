using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class UserFollow:IAggregateRoot
    {
        #region 新建实例

        /// <summary>
        /// 关注某人
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="followee">关注对象</param>
        public UserFollow(Guid uid,string uname,string uhead, Guid followee,string fname,string fhead)
        {
            this._uid = uid;
            this._uname = uname;
            this._uhead = uhead;

            this._followee = followee;
            this._followeeHead = fhead;
            this._followeeName = fname;

            this._id = Guid.NewGuid();
        }

        #endregion

        #region 从持久化创建

        private UserFollow() { }
        public static UserFollow CreateFromReader(IDataReader dr)
        {
            UserFollow instance = new UserFollow();
            instance._id = new Guid(dr["ID"].ToString());
            
            instance._uid = new Guid(dr["UserID"].ToString());
            instance._uname = dr["UserName"].ToString();
            instance._uhead = dr["UserHead"].ToString();

            instance._followee = new Guid(dr["FolloweeID"].ToString());
            instance._followeeName = dr["FolloweeName"].ToString();
            instance._followeeHead = dr["FolloweeHead"].ToString();

            return instance;
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

            strSql.Append("INSERT INTO fbs_UserFollow(");
            strSql.Append("ID, UserID, UserName, UserHead, FolloweeID, FolloweeName, FolloweeHead)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ID, @in_UserID, @in_UserName, @in_UserHead, @in_FolloweeID, @in_FolloweeName, @in_FolloweeHead)");

            cmdParms.Add("@in_ID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserHead", DbType.String);
            cmdParms.Add("@in_FolloweeID", DbType.Guid);
            cmdParms.Add("@in_FolloweeName", DbType.String);
            cmdParms.Add("@in_FolloweeHead", DbType.String);
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

            strSql.Append("UPDATE fbs_UserFollow SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("UserHead=@in_UserHead,");
            strSql.Append("FolloweeID=@in_FolloweeID,");
            strSql.Append("FolloweeName=@in_FolloweeName,");
            strSql.Append("FolloweeHead=@in_FolloweeHead");
            strSql.Append(" WHERE ID=@in_ID");

            cmdParms.Add("@in_ID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserHead", DbType.String);
            cmdParms.Add("@in_FolloweeID", DbType.Guid);
            cmdParms.Add("@in_FolloweeName", DbType.String);
            cmdParms.Add("@in_FolloweeHead", DbType.String);
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

            strSql.Append("DELETE FROM fbs_UserFollow");
            strSql.Append(" WHERE ID=@in_ID");

            cmdParms.Add("@in_ID", DbType.Guid);
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

        public void AlterToRow(System.Data.DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("ID", typeof(Guid));
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("UserName",typeof(string));
                t.Columns.Add("UserHead",typeof(string));
                t.Columns.Add("FolloweeID", typeof(Guid));
                t.Columns.Add("FolloweeName", typeof(string));
                t.Columns.Add("FolloweeHead",typeof(string));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["ID"] = this._id;
            row["UserID"] = this._uid;
            row["UserName"]=this._uname;
            row["UserHead"]=this._uhead;
            row["FolloweeID"] = this._followee;
            row["FolloweeName"] = this._followeeName;
            row["FolloweeHead"] = this._followeeHead;
            //添加
            t.Rows.Add(row);
        }

        #endregion
        
        #region 属性
        private Guid _id;

        private Guid _uid;
        private string _uname;
        private string _uhead;

        private Guid _followee;
        private string _followeeName;
        private string _followeeHead;

        public Guid UserID
        {
            get { return this._uid; }
        }
        /// <summary>
        /// 关注者的id
        /// </summary>
        public Guid Followee
        {
            get { return this._followee; }
        }

        public string UserName 
        {
            get { return this._uname; }
        }

        public string UserHead
        {
            get { return this._uhead; }
        }

        public string FolloweeName
        {
            get { return this._followeeName; }
        }

        public string FolloweeHead
        {
            get { return this._followeeHead; }
        }
        #endregion
    }
}
