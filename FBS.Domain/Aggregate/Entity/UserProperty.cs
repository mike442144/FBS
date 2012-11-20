using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class UserProperty:IAggregateRoot
    {
        #region 新实例
        public UserProperty()
        { }

        public UserProperty(Guid uid, string blogTheme, string blogName, string userType, string brief)
        {
            this._uid = uid;
            this._blogTheme = blogTheme;
            this._blogName = blogName;
            this._userType = userType;
            this._brief = brief;
            this._propertyId = Guid.NewGuid();
            this._followeesCount = 0;
            this._followersCount = 0;
            this._friendsCount = 0;
            this._display = false;
        }

        #endregion

        #region 从持久化创建

       
        public static UserProperty CreateFromReader(IDataReader dr)
        {
            UserProperty instance = new UserProperty();
            instance._propertyId=new Guid(dr["ID"].ToString());
            instance._uid = new Guid(dr["UserID"].ToString());            
            instance._blogTheme = dr["BlogTheme"].ToString();
            instance._blogName = dr["BlogName"].ToString();
            instance._userType = dr["UserType"].ToString();
            instance._brief = dr["Brief"].ToString();
            instance._followersCount = Convert.ToInt32(dr["FollowersCount"]);
            instance._followeesCount = Convert.ToInt32(dr["FolloweesCount"]);
            instance._friendsCount = Convert.ToInt32(dr["FriendsCount"]);
            instance._display = Convert.ToBoolean(dr["Display"]);
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

            strSql.Append("INSERT INTO fbs_UserProperty(");
            strSql.Append("ID, UserID, BlogTheme, BlogName, UserType, Brief, FollowersCount, FolloweesCount,FriendsCount,Display)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ID, @in_UserID, @in_BlogTheme, @in_BlogName, @in_UserType, @in_Brief, @in_FollowersCount, @in_FolloweesCount,@in_FriendsCount,@in_Display)");

            cmdParms.Add("@in_ID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_BlogTheme", DbType.String);
            cmdParms.Add("@in_BlogName", DbType.String);
            cmdParms.Add("@in_UserType", DbType.String);
            cmdParms.Add("@in_Brief", DbType.String);
            cmdParms.Add("@in_FollowersCount", DbType.Int32);
            cmdParms.Add("@in_FolloweesCount", DbType.Int32);
            cmdParms.Add("@in_FriendsCount",DbType.Int32);
            cmdParms.Add("@in_Display", DbType.Boolean);
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

            strSql.Append("UPDATE fbs_UserProperty SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("BlogTheme=@in_BlogTheme,");
            strSql.Append("BlogName=@in_BlogName,");
            strSql.Append("UserType=@in_UserType,");
            strSql.Append("Brief=@in_Brief,");
            strSql.Append("FollowersCount=@in_FollowersCount,");
            strSql.Append("FolloweesCount=@in_FolloweesCount,");
            strSql.Append("FriendsCount=@in_FriendsCount,");
            strSql.Append("Display=@in_Display");
            strSql.Append(" WHERE ID=@in_ID");

            cmdParms.Add("@in_ID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_BlogTheme", DbType.String);
            cmdParms.Add("@in_BlogName", DbType.String);
            cmdParms.Add("@in_UserType", DbType.String);
            cmdParms.Add("@in_Brief", DbType.String);
            cmdParms.Add("@in_FollowersCount", DbType.Int32);
            cmdParms.Add("@in_FolloweesCount", DbType.Int32);
            cmdParms.Add("@in_FriendsCount",DbType.Int32);
            cmdParms.Add("@in_Display", DbType.Boolean);
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

            strSql.Append("DELETE FROM fbs_UserProperty");
            strSql.Append(" WHERE ID=@in_ID");

            cmdParms.Add("@in_ID", DbType.Guid);
        }

        #endregion

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._propertyId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同

            if (t.Columns.Count == 0)
            {
                t.Columns.Add("ID", typeof(Guid));
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("BlogTheme", typeof(string));
                t.Columns.Add("BlogName", typeof(string));
                t.Columns.Add("UserType", typeof(string));
                t.Columns.Add("Brief", typeof(string));
                t.Columns.Add("FollowersCount", typeof(int));
                t.Columns.Add("FolloweesCount", typeof(int));
                t.Columns.Add("FriendsCount",typeof(int));
                t.Columns.Add("Display", typeof(bool));
            }

            System.Data.DataRow row = t.NewRow();
            row["ID"] = this._propertyId;
            row["UserID"] = this._uid;            
            row["BlogTheme"] = this._blogTheme;
            row["BlogName"] = this._blogName;
            row["UserType"] = this._userType;
            row["Brief"] = this._brief;
            row["FollowersCount"] = this._followersCount;
            row["FolloweesCount"] = this._followeesCount;
            row["FriendsCount"] = this._friendsCount;
            row["Display"] = this._display;

            t.Rows.Add(row);
        }

        #endregion

        #region 辅助方法(增加、减少粉丝、关注和好友等)

        /// <summary>
        /// 增加粉丝数
        /// </summary>
        public void AddFollowersCount()
        {
            this._followersCount++;
        }

        /// <summary>
        /// 增加关注数
        /// </summary>

        public void AddFolloweesCount()
        {
            this._followeesCount++;
        }

        /// <summary>
        /// 减少粉丝数
        /// </summary>
        public void ReduceFollowersCount()
        {
            this._followersCount--;
        }

        /// <summary>
        /// 减少关注数
        /// </summary>
        public void ReduceFolloweesCount()
        {
            this._followeesCount--;
        }

        /// <summary>
        /// 增加好数
        /// </summary>
        public void AddFriendsCount()
        {
            this._friendsCount++;
        }

        /// <summary>
        /// 减少好友数
        /// </summary>
        public void ReduceFriendsCount()
        {
            this._friendsCount--;
        }

        #endregion

        #region 属性

        private Guid _propertyId;
        /// <summary>
        /// 用户编号
        /// </summary>
        private Guid _uid;

        /// <summary>
        /// 博客主题名称
        /// </summary>
        private string _blogTheme;

        private string _blogName;
        /// <summary>
        /// 博客名称
        /// </summary>
        private string _userType;

        /// <summary>
        /// 简介
        /// </summary>
        private string _brief;

        /// <summary>
        /// 粉丝数
        /// </summary>
        private int _followersCount;

        /// <summary>
        /// 关注数
        /// </summary>
        private int _followeesCount;

        /// <summary>
        /// 好友数
        /// </summary>
        private int _friendsCount;

        private bool _display;

        public string BlogName
        {
            get { return this._blogName; }
            set { this._blogName = value; }
        }
        public string Type
        {
            get { return this._userType; }
            set { this._userType = value; }
        }

        public Guid UserID
        {
            get { return _uid; }
            set { this._uid = value; }
        }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FollowersCount 
        {
            get { return this._followersCount; }
            set { this._followersCount = value; }
        }

        /// <summary>
        /// 关注数
        /// </summary>
        public int FolloweesCount 
        {
            get { return this._followeesCount; }
            set { this._followeesCount = value; }
        }
        /// <summary>
        /// 显示
        /// </summary>
        public bool Display
        {
            get { return this._display; }
            set { this._display = value; }
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief
        {
            get { return this._brief; }
            set { this._brief = value; }
        }

        
        /// <summary>
        /// 博客主题
        /// </summary>
        public string BlogTheme
        {
            get { return this._blogTheme; }
            set { this._blogTheme = value; }
        }
        #endregion
    }
}
