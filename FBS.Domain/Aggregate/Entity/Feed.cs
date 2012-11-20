using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;
using System.Web;

namespace FBS.Domain.Aggregate.Entity
{
    /// <summary>
    /// 新鲜事类型
    /// </summary>
    public enum FeedType
    {
        /// <summary>
        /// 新发状态
        /// </summary>
        NewState,

        /// <summary>
        /// 转发状态
        /// </summary>
        ForwardState,

        /// <summary>
        /// 转发新闻系统中的新闻到博客
        /// </summary>
        ForwardNews,

        /// <summary>
        /// 写博文
        /// </summary>
        NewStory,

        /// <summary>
        /// 上传照片
        /// </summary>
        NewPhoto,

        /// <summary>
        /// 分享博文
        /// </summary>
        ShareStory,

        /// <summary>
        /// 分享照片
        /// </summary>
        SharePhoto,
        /// <summary>
        /// 分享视频.视频来自各大视频网站
        /// </summary>
        ShareVideo,

        /// <summary>
        /// 分享新视频
        /// </summary>
        NewVideo,
        /// <summary>
        /// 分享非新鲜事博文
        /// </summary>
        ShareBlogDetails,


        /// <summary>
        /// 加关注
        /// </summary>
        Support,

        /// <summary>
        /// 取消关注
        /// </summary>
        CancelSupport,
        /// <summary>
        /// 添加好友
        /// </summary>
        BuildFriendRelation,
        /// <summary>
        /// 解除好友
        /// </summary>
        BrokeUpFriendRelation
       
    }

    public class Feed:IAggregateRoot
    {
        #region 创建新鲜事
        /// <summary>
        /// 新鲜事
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="uname">用户昵称</param>
        /// <param name="uhead">头像</param>
        /// <param name="subject">标题</param>
        /// <param name="content">内容</param>
        public Feed(Guid uid,string uname,string uhead,string subject,string content,FeedType ftype)
        {
            this._feedId = Guid.NewGuid();
            this._createdOn = DateTime.Now;

            this._uid = uid;
            this._uname = uname;
            this._uhead = uhead;
            this._subject = subject;
            this._content = content;
            this._ftype = ftype;
            this._replayCount = 0;
        }

        #endregion

        #region 从持久化创建
        private Feed() { }

        /// <summary>
        /// 从持久化创建对象
        /// </summary>
        /// <param name="dr">数据阅读器</param>
        /// <returns>Feed对象</returns>
        public static Feed CreateFromReader(IDataReader dr)
        {
            Feed newFeed = new Feed();
            newFeed._feedId=new Guid(dr["FeedID"].ToString());
            newFeed._uid = new Guid(dr["UserID"].ToString());
            newFeed._uname = dr["UserName"].ToString();
            newFeed._uhead = dr["UserHead"].ToString();
            newFeed._subject = HttpUtility.HtmlDecode(dr["Subject"].ToString());
            newFeed._content = HttpUtility.HtmlDecode(dr["Content"].ToString());
            newFeed._createdOn = Convert.ToDateTime(dr["CreatedOn"]);
            newFeed._ftype = (FeedType)Enum.Parse(typeof(FeedType),dr["FeedType"].ToString());
            newFeed._replayCount =int.Parse(dr["ReplayCount"].ToString());
            return newFeed;
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

            strSql.Append("INSERT INTO fbs_Feed(");
            strSql.Append("FeedID, UserID, UserName, UserHead, Subject, Content, CreatedOn, FeedType, ReplayCount)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_FeedID, @in_UserID, @in_UserName, @in_UserHead, @in_Subject, @in_Content, @in_CreatedOn, @in_FeedType, @in_ReplayCount)");

            cmdParms.Add("@in_FeedID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserHead", DbType.String);
            cmdParms.Add("@in_Subject", DbType.String);
            cmdParms.Add("@in_Content", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_FeedType", DbType.String);
            cmdParms.Add("@in_ReplayCount", DbType.Int32);
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

            strSql.Append("UPDATE fbs_Feed SET ");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("UserHead=@in_UserHead,");
            strSql.Append("Subject=@in_Subject,");
            strSql.Append("Content=@in_Content,");
            strSql.Append("CreatedOn=@in_CreatedOn,");
            strSql.Append("FeedType=@in_FeedType,");
            strSql.Append("ReplayCount=@in_ReplayCount");
            strSql.Append(" WHERE FeedID=@in_FeedID");

            cmdParms.Add("@in_FeedID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserHead", DbType.String);
            cmdParms.Add("@in_Subject", DbType.String);
            cmdParms.Add("@in_Content", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_FeedType", DbType.String);
            cmdParms.Add("@in_ReplayCount", DbType.Int32);
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

            strSql.Append("DELETE FROM fbs_Feed");
            strSql.Append(" WHERE FeedID=@in_FeedID");

            cmdParms.Add("@in_FeedID", DbType.Guid);
        }

        #endregion

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._feedId;
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
                t.Columns.Add("FeedID", typeof(Guid));
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("UserName", typeof(string));
                t.Columns.Add("UserHead", typeof(string));
                t.Columns.Add("Subject", typeof(string));
                t.Columns.Add("Content", typeof(string));
                t.Columns.Add("CreatedOn", typeof(DateTime));
                t.Columns.Add("FeedType",typeof(string));
                t.Columns.Add("ReplayCount", typeof(int));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["FeedID"] = this._feedId;
            row["UserID"] = this._uid;
            row["UserName"] = this._uname;
            row["UserHead"] = this._uhead;
            row["Subject"] = HttpUtility.HtmlEncode(this._subject);
            row["Content"] = HttpUtility.HtmlEncode(this._content);
            row["CreatedOn"] = this._createdOn;
            row["FeedType"] = this._ftype.ToString();
            row["ReplayCount"] = this._replayCount;
            //添加
            t.Rows.Add(row);
        }

        #endregion

        #region 属性
        private Guid _feedId;
        private Guid _uid;
        private string _uname;
        private string _uhead;
        private FeedType _ftype;
        private int _replayCount;

        public int ReplayCount
        {
            get { return this._replayCount; }
            set { this._replayCount = value; }
        }


        public FeedType FeedType
        {
            get { return this._ftype; }
        }

        public Guid UserID
        {
            get { return this._uid; }
        }

        public string UserName
        {
            get { return this._uname; }
        }

        public string UserHead
        {
            get { return _uhead; }
        }
        private string _subject;

        public string Subject
        {
            get { return _subject; }
        }
        private string _content;

        public string Content
        {
            get { return _content; }
        }
        private DateTime _createdOn;

        public DateTime CreatedOn
        {
            get { return _createdOn; }
        }


        #endregion

        public void AddReplayCount()
        {
            this._replayCount++;
        }

        public void ReduceReplayCount()
        {
            this._replayCount--;
        }
    }
}
