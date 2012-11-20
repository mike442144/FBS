using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class AdvertiseFilling : IAggregateRoot
    {
        #region IEntity 成员

        /// <summary>
        /// 广告编号
        /// </summary>
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
                t.Columns.Add("ID", typeof(Guid));
                t.Columns.Add("PageID", typeof(Guid));
                t.Columns.Add("AdvertisementID", typeof(Guid));
                t.Columns.Add("PositionName", typeof(string));
            }

            DataRow row = t.NewRow();
            row["ID"] = this._id;
            row["PageID"] = this._pageID;
            row["AdvertisementID"] = this._advertisementID;
            row["PositionName"] = this._positionName;
      
            t.Rows.Add(row);
        }

        #endregion


        #region 属性
        /// <summary>
        /// 匹配编号
        /// </summary>
        private Guid _id;
        /// <summary>
        /// 页面编号
        /// </summary>
        private Guid _pageID;
        /// <summary>
        /// 广告编号
        /// </summary>
        private Guid _advertisementID;
        /// <summary>
        /// 广告位置
        /// </summary>
        private string _positionName;

        //public Guid ID
        //{
        //    set { this._id = value; }
        //    get { return this._id; }
        //}

        public Guid PageID
        {
            set { this._pageID = value; }
            get { return this._pageID; }
        }

        public Guid AdvertisementID
        {
            set { this._advertisementID = value; }
            get { return this._advertisementID; }
        }

        public string PositionName
        {
            set { this._positionName = value; }
            get { return this._positionName; }
        }
        #endregion

        public AdvertiseFilling(Guid pageid,Guid adverid,string positionname)
        {
            this._id = Guid.NewGuid();
            this._advertisementID = adverid;
            this._pageID = pageid;
            this._positionName = positionname;
        }


                #region 生成数据库命令

        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql,Dictionary<string,DbType> cmdParms)
        {
            if (strSql == null || cmdParms == null)
                return;

            strSql.Append("INSERT INTO fbs_AdvertiseFilling(");
            strSql.Append("ID, PageID, AdvertisementID, PositionName)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ID, @in_PageID, @in_AdvertisementID, @in_PositionName)");

            cmdParms.Add("@in_ID", DbType.Guid);
            cmdParms.Add("@in_PageID", DbType.Guid);
            cmdParms.Add("@in_AdvertisementID", DbType.Guid);
            cmdParms.Add("@in_PositionName", DbType.StringFixedLength);
        }

        /// <summary>
        /// 生成更新命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareUpdateCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (null==strSql||null==cmdParms)
                return;

            strSql.Append("UPDATE fbs_AdvertiseFilling SET ");
            strSql.Append("PageID=@in_PageID,");
            strSql.Append("AdvertisementID=@in_AdvertisementID,");
            strSql.Append("PositionName=@in_PositionName");
            strSql.Append(" WHERE ID=@in_ID");

            cmdParms.Add("@in_ID", DbType.Guid);
            cmdParms.Add("@in_PageID", DbType.Guid);
            cmdParms.Add("@in_AdvertisementID", DbType.Guid);
            cmdParms.Add("@in_PositionName", DbType.StringFixedLength);
          
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

            strSql.Append("DELETE FROM fbs_AdvertiseFilling");
            strSql.Append(" WHERE ID=@in_ID");

            cmdParms.Add("@in_ID", DbType.Guid);
        }

        #endregion



        #region 从持久化创建

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>文章实例</returns>
        public static AdvertiseFilling CreateFromReader(IDataReader rd)
        {
            AdvertiseFilling a = new AdvertiseFilling();

            a._id = new Guid(rd["ID"].ToString());
            a._advertisementID = new Guid(rd["AdvertisementID"].ToString());
            a._pageID = new Guid(rd["PageID"].ToString());
            a._positionName = rd["PositionName"].ToString();

            return a;
        }

    
        #endregion
        public AdvertiseFilling()
        {

        }
    }
}
