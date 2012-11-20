using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{   
    public enum MediaType
    {
        None = 0,
        Video = 1,
        Music = 2,
        Website = 9999,
    }
    public class ShareThread:IAggregateRoot
    {
        /// <summary>
        /// 构造分享实例
        /// </summary>
        /// <param name="playUrl">播放地址</param>
        /// <param name="mtype">媒体类型</param>
        /// <param name="subject">标题</param>
        /// <param name="body">简介</param>
        /// <param name="thumbnailUrl">缩略图地址</param>
        /// 
        public ShareThread(string rawUrl, string playUrl, MediaType mtype, string subject, string body, string thumbnailUrl, DateTime addtime)
        {
            this._rawUrl = rawUrl;
            this._playUrl = playUrl;
            this._mediaType = mtype;
            this._subject = subject;
            this._body = body;
            this._thumbnailUrl = thumbnailUrl;
            this._id = Guid.NewGuid();
            this._shareTime = addtime;
        }

        public ShareThread(string rawUrl,string playUrl,MediaType mtype,string subject,string body,string thumbnailUrl,DateTime addtime,string source)
        {
            this._rawUrl = rawUrl;
            this._playUrl = playUrl;
            this._mediaType = mtype;
            this._subject = subject;
            this._body = body;
            this._thumbnailUrl = thumbnailUrl;
            this._id = Guid.NewGuid();
            this._shareTime = addtime;
            this._source = source;
        }

        public override string ToString()
        {
            string str = this._playUrl+"       "+this._subject+"     "+this._body+"     ";
            return str;
        }

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

        /// <summary>
        /// 转化为数据行
        /// </summary>
        /// <param name="t">数据表</param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同

            if (t.Columns.Count == 0)
            {
                t.Columns.Add("VideoID", typeof(Guid));
                t.Columns.Add("PlayUrl", typeof(string));
                t.Columns.Add("Subject", typeof(string));
                t.Columns.Add("Body", typeof(string));
                t.Columns.Add("MediaType", typeof(int));
                t.Columns.Add("ThumbnailUrl", typeof(string));
                t.Columns.Add("ShareTime", typeof(DateTime));
                t.Columns.Add("Source",typeof(string));
                t.Columns.Add("RawUrl",typeof(string));
            }

            DataRow row = t.NewRow();
            row["VideoID"] = this._id;
            row["PlayUrl"] = this._playUrl;
            row["Subject"] = this._subject;
            row["Body"] = this._body;
            row["MediaType"] = this._mediaType;
            row["ThumbnailUrl"] = this._thumbnailUrl;
            row["ShareTime"] = this._shareTime;
            row["Source"] = this._source;
            row["RawUrl"] = this._rawUrl;
            t.Rows.Add(row);
        }

        #endregion

        #region 属性

        private Guid _id;
        private string _playUrl;
        private DateTime _shareTime;
        public string PlayUrl
        {
            get { return _playUrl; }
        }
        public DateTime ShareTime
        {
            get { return _shareTime; }
        }
        private MediaType _mediaType;
        private string _subject;
        private string _rawUrl;

        public string RawUrl
        {
            get { return _rawUrl; }
        }

        public string Subject
        {
            get { return _subject; }
            set { this._subject = value; }
        }
        private string _body;

        public string Body
        {
            get { return _body; }
        }
        private string _thumbnailUrl;

        public string ThumbnailUrl
        {
            get { return _thumbnailUrl; }
        }

        private string _source;

        public string Source
        {
            get { return this._source; }
            set { this._source = value; }
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

            strSql.Append("INSERT INTO fbs_ShareThread(");
            strSql.Append("VideoID, PlayUrl, Subject, Body, MediaType, ThumbnailUrl, ShareTime, Source,RawUrl)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_VideoID, @in_PlayUrl, @in_Subject, @in_Body, @in_MediaType, @in_ThumbnailUrl, @in_ShareTime, @in_Source,@in_RawUrl)");

            cmdParms.Add("@in_VideoID", DbType.Guid);
            cmdParms.Add("@in_PlayUrl", DbType.String);
            cmdParms.Add("@in_Subject", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_MediaType", DbType.Int32);
            cmdParms.Add("@in_ThumbnailUrl", DbType.String);
            cmdParms.Add("@in_ShareTime", DbType.DateTime);
            cmdParms.Add("@in_Source", DbType.String);
            cmdParms.Add("@in_RawUrl",DbType.String);
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

            strSql.Append("UPDATE fbs_ShareThread SET ");
            strSql.Append("PlayUrl=@in_PlayUrl,");
            strSql.Append("Subject=@in_Subject,");
            strSql.Append("Body=@in_Body,");
            strSql.Append("MediaType=@in_MediaType,");
            strSql.Append("ThumbnailUrl=@in_ThumbnailUrl,");
            strSql.Append("ShareTime=@in_ShareTime,");
            strSql.Append("Source=@in_Source");
            strSql.Append("RawUrl=@in_RawUrl");
            strSql.Append(" WHERE VideoID=@in_VideoID");

            cmdParms.Add("@in_VideoID", DbType.Guid);
            cmdParms.Add("@in_PlayUrl", DbType.String);
            cmdParms.Add("@in_Subject", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_MediaType", DbType.Int32);
            cmdParms.Add("@in_ThumbnailUrl", DbType.String);
            cmdParms.Add("@in_ShareTime", DbType.DateTime);
            cmdParms.Add("@in_Source", DbType.String);
            cmdParms.Add("@in_RawUrl",DbType.String);

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

            strSql.Append("DELETE FROM fbs_ShareThread");
            strSql.Append(" WHERE VideoID=@in_VideoID");

            cmdParms.Add("@in_VideoID", DbType.Guid);

        }

        #endregion

        #region 从持久化创建

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>文章实例</returns>
        public static ShareThread CreateFromReader(IDataReader rd)
        {
            ShareThread a = new ShareThread();
            a._id = new Guid(rd["VideoID"].ToString());
            a._body = rd["Body"].ToString();
            a._mediaType =MediaType.Video;
            a._playUrl = rd["PlayUrl"].ToString();
            a._rawUrl = rd["RawUrl"].ToString() ;
            a._shareTime = Convert.ToDateTime(rd["ShareTime"]);
            a._subject = rd["Subject"].ToString();
            a._thumbnailUrl = rd["ThumbnailUrl"].ToString();
            a._source = rd["Source"].ToString();
            return a;
        }

        #endregion

        public ShareThread()
        { 
        
        }
    }
}
