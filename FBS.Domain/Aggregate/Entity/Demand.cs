using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class Demand : IAggregateRoot
    {
        #region IEntity 成员

        /// <summary>
        /// 需求编号
        /// </summary>
        public Guid Id
        {
            get
            {
                return this._demandID;
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
        public static Demand CreateFromReader(IDataReader rd)
        {
            Demand a = new Demand();

            a._businessmanName = rd["BusinessmanName"].ToString();
            a._customerName = rd["CustomerName"].ToString();
            a._customerOtherConnect = rd["CustomerOtherConnect"].ToString();
            a._customerPhoneNum = rd["CustomerPhoneNum"].ToString();
            a._demandCity = rd["DemandCity"].ToString();
            a._demandContent = rd["DemandContent"].ToString();
            a._demandID = new Guid(rd["DemandID"].ToString());
            a._groupOnType = rd["GroupOnType"].ToString();
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
                t.Columns.Add("DemandID", typeof(Guid));
                t.Columns.Add("CustomerName", typeof(string));
                t.Columns.Add("CustomerPhoneNum", typeof(string));
                t.Columns.Add("CustomerOtherConnect", typeof(string));
                t.Columns.Add("DemandCity", typeof(string));
                t.Columns.Add("BusinessmanName", typeof(string));
                t.Columns.Add("GroupOnType", typeof(string));
                t.Columns.Add("DemandContent",typeof(string));
            }

            DataRow row = t.NewRow();
            row["DemandID"] = this._demandID;
            row["CustomerName"] = this._customerName;
            row["CustomerPhoneNum"] = this._customerPhoneNum;
            row["CustomerOtherConnect"] = this._customerOtherConnect;
            row["DemandCity"] = this._demandCity;
            row["BusinessmanName"] = this._businessmanName;
            row["GroupOnType"] = this._groupOnType;
            row["DemandContent"] = this._demandContent;
            t.Rows.Add(row);
        }
        #endregion



        #region 属性
        private Guid _demandID;
        private string _customerName;
        private string _customerPhoneNum;
        private string _customerOtherConnect;
        private string _demandCity;
        private string _businessmanName;
        private string _groupOnType;
        private string _demandContent;
        #endregion


        public Guid DemandID 
        { 
            get { return this._demandID;}
            set { this._demandID = value; }
        }

        public string CustomerName
        {
            set { this._customerName = value; }
            get { return this._customerName; }
        }

        public string CustomerPhoneNum
        {
            set { this._customerPhoneNum = value; }
            get { return this._customerPhoneNum; }
        }

        public string CustomerOtherConnect
        {
            set { this._customerOtherConnect = value; }
            get { return this._customerOtherConnect; }
        }

        public string DemandCity
        {
            set { this._demandCity = value; }
            get { return this._demandCity; }
        }

        public string BusinessmanName
        {
            set { this._businessmanName=value; }
            get { return this._businessmanName; }
        }

        public string GroupOnType
        {
            set { this._groupOnType = value; }
            get { return this._groupOnType; }
        }

        public string DemandContent
        {
            set { this._demandContent = value; }
            get { return this._demandContent; }
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

            strSql.Append("INSERT INTO fbs_Demand(");
            strSql.Append("DemandID, CustomerName, CustomerPhoneNum, CustomerOtherConnect, DemandCity, BusinessmanName, GroupOnType, DemandContent)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_DemandID, @in_CustomerName, @in_CustomerPhoneNum, @in_CustomerOtherConnect, @in_DemandCity, @in_BusinessmanName, @in_GroupOnType, @in_DemandContent)");


            cmdParms.Add("@in_DemandID", DbType.Guid);
            cmdParms.Add("@in_CustomerName", DbType.String);
            cmdParms.Add("@in_CustomerPhoneNum", DbType.String);
            cmdParms.Add("@in_CustomerOtherConnect", DbType.String);
            cmdParms.Add("@in_DemandCity", DbType.String);
            cmdParms.Add("@in_BusinessmanName", DbType.String);
            cmdParms.Add("@in_GroupOnType", DbType.String);
            cmdParms.Add("@in_DemandContent", DbType.String);
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

            strSql.Append("UPDATE fbs_Demand SET ");
            strSql.Append("CustomerName=@in_CustomerName,");
            strSql.Append("CustomerPhoneNum=@in_CustomerPhoneNum,");
            strSql.Append("CustomerOtherConnect=@in_CustomerOtherConnect,");
            strSql.Append("DemandCity=@in_DemandCity,");
            strSql.Append("BusinessmanName=@in_BusinessmanName,");
            strSql.Append("GroupOnType=@in_GroupOnType,");
            strSql.Append("DemandContent=@in_DemandContent");
            strSql.Append(" WHERE DemandID=@in_DemandID");

            cmdParms.Add("@in_DemandID", DbType.Guid);
            cmdParms.Add("@in_CustomerName", DbType.String);
            cmdParms.Add("@in_CustomerPhoneNum", DbType.String);
            cmdParms.Add("@in_CustomerOtherConnect", DbType.String);
            cmdParms.Add("@in_DemandCity", DbType.String);
            cmdParms.Add("@in_BusinessmanName", DbType.String);
            cmdParms.Add("@in_GroupOnType", DbType.String);
            cmdParms.Add("@in_DemandContent", DbType.String);
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

            strSql.Append("DELETE FROM fbs_Demand");
            strSql.Append(" WHERE DemandID=@in_DemandID");

            cmdParms.Add("@in_DemandID", DbType.Guid);
        }

        #endregion


        public Demand()
        { 
        
        }

        public Demand(Guid aid, string cunstomername, string PhoneNum, string OtherConnect, string City, string manName, string Type, string Content)
        {
            this._businessmanName = manName;
            this._customerName = cunstomername;
            this._customerOtherConnect = OtherConnect;
            this._customerPhoneNum = PhoneNum;
            this._demandCity = City;
            this._demandContent = Content;
            this._demandID = aid;
            this._groupOnType = Type;
        }

        public Demand(string cunstomername, string PhoneNum, string OtherConnect, string City, string manName, string Type, string Content)
        {
            this._businessmanName = manName;
            this._customerName = cunstomername;
            this._customerOtherConnect = OtherConnect;
            this._customerPhoneNum = PhoneNum;
            this._demandCity = City;
            this._demandContent = Content;
            this._demandID = Guid.NewGuid();
            this._groupOnType = Type;
        }
    }
}