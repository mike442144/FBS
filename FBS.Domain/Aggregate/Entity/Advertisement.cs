using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class Advertisement : IAggregateRoot
    {
        #region IEntity 成员

        /// <summary>
        /// 广告编号
        /// </summary>
        public Guid Id
        {
            get
            {
                return this._advertisementID;
            }
            set
            {
                throw new NotImplementedException();
            }
        }



        #region 从持久化创建

        /// <summary>
        /// 从数据阅读器创建
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static Advertisement CreateFromReader(IDataReader rd)
        {
            Advertisement a = new Advertisement();
            a._advertisementBeginTime=DateTime.Parse(rd["AdvertisementBeginTime"].ToString());
            a._advertisementContentURL = rd["AdvertisementContentURL"].ToString();
            a._advertisementEndTime = DateTime.Parse(rd["AdvertisementEndTime"].ToString());
            a._advertisementID = new Guid(rd["AdvertisementID"].ToString());
            a._advertisementPriority =System.Int16.Parse( rd["AdvertisementPriority"].ToString());
            a._advertisementType = rd["AdvertisementType"].ToString();
            a._advertisementURL = rd["AdvertisementURL"].ToString();
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
                t.Columns.Add("AdvertisementID", typeof(Guid));
                t.Columns.Add("AdvertisementType", typeof(string));
                t.Columns.Add("AdvertisementContentURL", typeof(string));
                t.Columns.Add("AdvertisementPriority", typeof(int));
                t.Columns.Add("AdvertisementBeginTime", typeof(DateTime));
                t.Columns.Add("AdvertisementEndTime", typeof(DateTime));
                t.Columns.Add("AdvertisementURL", typeof(string));
            }

            DataRow row = t.NewRow();
            row["AdvertisementID"] = this._advertisementID;
            row["AdvertisementType"] = this.AdvertisementType;
            row["AdvertisementContentURL"]=this._advertisementContentURL;
            row["AdvertisementPriority"] = this._advertisementPriority;
            row["AdvertisementBeginTime"] = this._advertisementBeginTime;
            row["AdvertisementEndTime"] = this._advertisementEndTime;
            row["AdvertisementURL"] = this._advertisementURL;
            t.Rows.Add(row);
        }

        #endregion



        #region 属性
        /// <summary>
        /// 广告名称
        /// </summary>
        private Guid _advertisementID;
        /// <summary>
        /// 广告类型 image ,flash
        /// </summary>
        private string  _advertisementType;
        /// <summary>
        /// 广告内容URL
        /// </summary>
        private string _advertisementContentURL;
        /// <summary>
        /// 广告优先级
        /// </summary>
        private int _advertisementPriority;
        /// <summary>
        /// 广告开始时间
        /// </summary>
        private DateTime _advertisementBeginTime;
        /// <summary>
        /// 广告名称
        /// </summary>
        private DateTime _advertisementEndTime;
        /// <summary>
        /// 广告链接
        /// </summary>
        private string _advertisementURL;

        public Guid AdvertisementID
        {
            set { this._advertisementID = value; }
            get { return this._advertisementID; }
        }

        public string AdvertisementType
        {
            set { this._advertisementType = value; }
            get { return this._advertisementType; }
        }
        public string AdvertisementContentURL
        {
            set { this._advertisementContentURL = value; }
            get { return this._advertisementContentURL; }
        }
        public int AdvertisementPriority
        {
            set { this._advertisementPriority = value; }
            get { return this._advertisementPriority; }
        }

        public DateTime AdvertisementBeginTime
        {
            set { this._advertisementBeginTime = value; }
            get { return this._advertisementBeginTime; }
        }

        public DateTime AdvertisementEndTime
        {
            set { this._advertisementEndTime = value; }
            get { return this._advertisementEndTime; }
        }

        public string AdvertisementURL
        {
            set { this._advertisementURL = value; }
            get { return this._advertisementURL; }
        }
        #endregion


        public Advertisement(string  type,string contenturl,int priority,DateTime BeginTime,DateTime EndTime,string advertisementurl)
        {
           this.AdvertisementID = Guid.NewGuid();
           this._advertisementType = type;
           this._advertisementContentURL = contenturl;
           this._advertisementPriority = priority;
           this.AdvertisementBeginTime = BeginTime;
           this.AdvertisementEndTime = EndTime;
           this._advertisementURL = advertisementurl;
        }

        public Advertisement()
        { }


        public Advertisement(Guid aid, string type, string contenturl, int priority, DateTime BeginTime, DateTime EndTime, string advertisementurl)
        {
            this.AdvertisementID = aid;
            this._advertisementType = type;
            this._advertisementContentURL = contenturl;
            this._advertisementPriority = priority;
            this.AdvertisementBeginTime = BeginTime;
            this.AdvertisementEndTime = EndTime;
            this._advertisementURL = advertisementurl;
        }


        


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

            strSql.Append("INSERT INTO fbs_Advertisement(");
            strSql.Append("AdvertisementID, AdvertisementType, AdvertisementContentURL, AdvertisementPriority, AdvertisementBeginTime, AdvertisementEndTime, AdvertisementURL)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_AdvertisementID, @in_AdvertisementType, @in_AdvertisementContentURL, @in_AdvertisementPriority, @in_AdvertisementBeginTime, @in_AdvertisementEndTime, @in_AdvertisementURL)");


            cmdParms.Add("@in_AdvertisementID", DbType.Guid);
            cmdParms.Add("@in_AdvertisementType", DbType.StringFixedLength);
            cmdParms.Add("@in_AdvertisementContentURL", DbType.StringFixedLength);
            cmdParms.Add("@in_AdvertisementPriority", DbType.Int32);
            cmdParms.Add("@in_AdvertisementBeginTime", DbType.DateTime);
            cmdParms.Add("@in_AdvertisementEndTime", DbType.DateTime);
            cmdParms.Add("@in_AdvertisementURL", DbType.String);
           
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

            strSql.Append("UPDATE fbs_Advertisement SET ");
            strSql.Append("AdvertisementType=@in_AdvertisementType,");
            strSql.Append("AdvertisementContentURL=@in_AdvertisementContentURL,");
            strSql.Append("AdvertisementPriority=@in_AdvertisementPriority,");
            strSql.Append("AdvertisementBeginTime=@in_AdvertisementBeginTime,");
            strSql.Append("AdvertisementEndTime=@in_AdvertisementEndTime,");
            strSql.Append("AdvertisementURL=@in_AdvertisementURL");
            strSql.Append(" WHERE AdvertisementID=@in_AdvertisementID");

            cmdParms.Add("@in_AdvertisementID", DbType.Guid);
            cmdParms.Add("@in_AdvertisementType", DbType.StringFixedLength);
            cmdParms.Add("@in_AdvertisementContentURL", DbType.StringFixedLength);
            cmdParms.Add("@in_AdvertisementPriority", DbType.Int32);
            cmdParms.Add("@in_AdvertisementBeginTime", DbType.DateTime);
            cmdParms.Add("@in_AdvertisementEndTime", DbType.DateTime);
            cmdParms.Add("@in_AdvertisementURL", DbType.String);

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

            strSql.Append("DELETE FROM fbs_Advertisement");
            strSql.Append(" WHERE AdvertisementID=@in_AdvertisementID");

            cmdParms.Add("@in_AdvertisementID", DbType.Guid);
           
        }

        #endregion



    }
}
