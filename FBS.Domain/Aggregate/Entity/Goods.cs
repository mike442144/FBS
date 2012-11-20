using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class Goods : IAggregateRoot
    {
        #region IEntity 成员

        /// <summary>
        /// 广告编号
        /// </summary>
        public Guid Id
        {
            get
            {
                return this._goodsID;
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        #region 从持久化创建

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>文章实例</returns>
        public static Goods CreateFromReader(IDataReader rd)
        {
            Goods a = new Goods();

            a._goodsID = new Guid(rd["GoodsID"].ToString());
            a._goodsBeginTime = Convert.ToDateTime(rd["GoodsBeginTime"]);
            a._goodsBuyCount = System.Int16.Parse(rd["GoodsBuyCount"].ToString());
            a._goodsDetailsContent = rd["GoodsDetailsContent"].ToString();
            a._goodsEndTime = Convert.ToDateTime(rd["GoodsEndTime"]);
            a._goodsIsOn = (Boolean)rd["GoodsIsOn"];
            a._goodsName = rd["GoodsName"].ToString();
            a._goodsNowPrice = float.Parse(rd["GoodsNowPrice"].ToString());
            a._goodsOldPrice = float.Parse(rd["GoodsOldPrice"].ToString());
            a._goodsPicURL = rd["GoodsPicURL"].ToString();
            return a;
        }

        #endregion


        public Goods()
        { 
        
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
                t.Columns.Add("GoodsID", typeof(Guid));
                t.Columns.Add("GoodsName", typeof(string));
                t.Columns.Add("GoodsNowPrice", typeof(float));
                t.Columns.Add("GoodsPicURL", typeof(string));
                t.Columns.Add("GoodsOldPrice", typeof(float));
                t.Columns.Add("GoodsBuyCount", typeof(int));
                t.Columns.Add("GoodsBeginTime", typeof(DateTime));
                t.Columns.Add("GoodsEndTime",typeof(DateTime));
                t.Columns.Add("GoodsDetailsContent",typeof(string));
                t.Columns.Add("GoodsIsOn",typeof(bool));
            }

            DataRow row = t.NewRow();
            row["GoodsID"] = this._goodsID;
            row["GoodsName"] = this._goodsName;
            row["GoodsNowPrice"] = this._goodsNowPrice;
            row["GoodsPicURL"] = this._goodsPicURL;
            row["GoodsOldPrice"] = this._goodsOldPrice;
            row["GoodsBuyCount"] = this._goodsBuyCount;
            row["GoodsBeginTime"] = this._goodsBeginTime;
            row["GoodsEndTime"]=this._goodsEndTime;
            row["GoodsDetailsContent"]=this._goodsDetailsContent;
            row["GoodsIsOn"] = this._goodsIsOn;
            t.Rows.Add(row);
        }

        #endregion

        #region 属性
        private Guid _goodsID;
        private string _goodsName;
        private float _goodsNowPrice;
        private string _goodsPicURL;
        private float _goodsOldPrice;
        private int _goodsBuyCount;
        private DateTime _goodsBeginTime;
        private DateTime _goodsEndTime;
        private string _goodsDetailsContent;
        private bool _goodsIsOn;
        #endregion

        public Goods(Guid aid,string goodsname, float goodsnowprice, string goodspicurl, float goodsoldprice, int goodsbuycount, DateTime goodsbegintime, DateTime goodsendtime, string goodsdetailscontent, bool goodsison)
        {
            this._goodsID = aid;
            this._goodsBeginTime = goodsbegintime;
            this._goodsBuyCount = goodsbuycount;
            this._goodsDetailsContent = goodsdetailscontent;
            this._goodsEndTime = goodsendtime;
            this._goodsIsOn = goodsison;
            this._goodsName = goodsname;
            this._goodsNowPrice = goodsnowprice;
            this._goodsOldPrice = goodsoldprice;
            this._goodsPicURL = goodspicurl;
        }


        public Goods(string goodsname,float goodsnowprice,string goodspicurl,float goodsoldprice,int goodsbuycount,DateTime goodsbegintime,DateTime goodsendtime,string goodsdetailscontent,bool goodsison)
        {
            this._goodsID = Guid.NewGuid();
            this._goodsBeginTime = goodsbegintime;
            this._goodsBuyCount = goodsbuycount;
            this._goodsDetailsContent = goodsdetailscontent;
            this._goodsEndTime = goodsendtime;
            this._goodsIsOn = goodsison;
            this._goodsName = goodsname;
            this._goodsNowPrice = goodsnowprice;
            this._goodsOldPrice = goodsoldprice;
            this._goodsPicURL = goodspicurl;
        }

        public Guid GoodsID 
        {
            set { this._goodsID = value; }
            get { return this._goodsID; }
        }

        public string GoodsName
        {
            set { this._goodsName = value; }
            get { return this._goodsName; }
        }

        public float GoodsNowPrice
        {
            set { this._goodsNowPrice = value; }
            get { return this._goodsNowPrice; }
        }

        public string GoodsPicURL
        {
            set { this._goodsPicURL = value; }
            get { return this._goodsPicURL; }
        }

        public float GoodsOldPrice
        {
            set { this._goodsOldPrice = value; }
            get { return this._goodsOldPrice; }
        }

        public int GoodsBuyCount 
        {
            set { this._goodsBuyCount = value; }
            get { return this._goodsBuyCount; }
        }

        public DateTime GoodsBeginTime 
        {
            set { this._goodsBeginTime = value; }
            get { return this._goodsBeginTime; }
        }

        public DateTime GoodsEndTime
        {
            set { this._goodsEndTime = value; }
            get { return this._goodsEndTime; }
        }

        public string GoodsDetailsContent
        {
            set { this._goodsDetailsContent = value; }
            get { return this._goodsDetailsContent; }
        }

        public bool GoodsIsOn 
        {
            set { this._goodsIsOn = value; }
            get { return this._goodsIsOn; }
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

            strSql.Append("INSERT INTO fbs_Goods(");
            strSql.Append("GoodsID, GoodsName, GoodsNowPrice, GoodsPicURL, GoodsOldPrice, GoodsBuyCount, GoodsBeginTime, GoodsEndTime, GoodsDetailsContent, GoodsIsOn)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_GoodsID, @in_GoodsName, @in_GoodsNowPrice, @in_GoodsPicURL, @in_GoodsOldPrice, @in_GoodsBuyCount, @in_GoodsBeginTime, @in_GoodsEndTime, @in_GoodsDetailsContent, @in_GoodsIsOn)");

            cmdParms.Add("@in_GoodsID", DbType.Guid);
            cmdParms.Add("@in_GoodsName", DbType.String);
            cmdParms.Add("@in_GoodsNowPrice", DbType.Double);
            cmdParms.Add("@in_GoodsPicURL", DbType.String);
            cmdParms.Add("@in_GoodsOldPrice", DbType.Double);
            cmdParms.Add("@in_GoodsBuyCount", DbType.Int32);
            cmdParms.Add("@in_GoodsBeginTime", DbType.DateTime);
            cmdParms.Add("@in_GoodsEndTime", DbType.DateTime);
            cmdParms.Add("@in_GoodsDetailsContent", DbType.String);
            cmdParms.Add("@in_GoodsIsOn", DbType.Boolean);

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

            strSql.Append("UPDATE fbs_Goods SET ");
            strSql.Append("GoodsName=@in_GoodsName,");
            strSql.Append("GoodsNowPrice=@in_GoodsNowPrice,");
            strSql.Append("GoodsPicURL=@in_GoodsPicURL,");
            strSql.Append("GoodsOldPrice=@in_GoodsOldPrice,");
            strSql.Append("GoodsBuyCount=@in_GoodsBuyCount,");
            strSql.Append("GoodsBeginTime=@in_GoodsBeginTime,");
            strSql.Append("GoodsEndTime=@in_GoodsEndTime,");
            strSql.Append("GoodsDetailsContent=@in_GoodsDetailsContent,");
            strSql.Append("GoodsIsOn=@in_GoodsIsOn");
            strSql.Append(" WHERE GoodsID=@in_GoodsID");

            cmdParms.Add("@in_GoodsID", DbType.Guid);
            cmdParms.Add("@in_GoodsName", DbType.String);
            cmdParms.Add("@in_GoodsNowPrice", DbType.Double);
            cmdParms.Add("@in_GoodsPicURL", DbType.String);
            cmdParms.Add("@in_GoodsOldPrice", DbType.Double);
            cmdParms.Add("@in_GoodsBuyCount", DbType.Int32);
            cmdParms.Add("@in_GoodsBeginTime", DbType.DateTime);
            cmdParms.Add("@in_GoodsEndTime", DbType.DateTime);
            cmdParms.Add("@in_GoodsDetailsContent", DbType.String);
            cmdParms.Add("@in_GoodsIsOn", DbType.Boolean);

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

            strSql.Append("DELETE FROM fbs_Goods");
            strSql.Append(" WHERE GoodsID=@in_GoodsID");

            cmdParms.Add("@in_GoodsID", DbType.Guid);
            
        }

        #endregion


    }
}
