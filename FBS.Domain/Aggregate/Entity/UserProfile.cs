using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;
using System.Web;

namespace FBS.Domain.Aggregate.Entity
{
    public class UserProfile:IAggregateRoot
    {
        #region 创建用户资料
        /// <summary>
        /// 新建用户资料
        /// </summary>
        /// <param name="uid">用户编号</param>
      
        
        public UserProfile(Guid uid)
        {
            this._uid = uid;
            this._birthday = Convert.ToDateTime("1900-01-01 00:00:00");
            this._cellphone = string.Empty;
            this._sex = false;
            this._province = string.Empty;
            this._city = string.Empty;
            this._company = string.Empty;
            this._hobby = string.Empty;
            this._qq = string.Empty;
            this._msn = string.Empty;
            this._address = string.Empty;
        }
        ///// <summary>
        ///// 企业用户构造
        ///// </summary>
        ///// <param name="uid"></param>
        ///// <param name="cellphone"></param>
        ///// <param name="province"></param>
        ///// <param name="city"></param>
        ///// <param name="company"></param>
        ///// <param name="address"></param>
        //public UserProfile(Guid uid, string cellphone, string province, string city, string company, string address)
        //{
        //    this._uid = uid;
        //    this._cellphone = cellphone;
        //    this._province = province;
        //    this._city = city;
        //    this._company = company;
        //    this._address = address;
        //}

        
        #endregion
       
        #region 从持久化创建
        private UserProfile() { }
        public static UserProfile CreateFromReader(IDataReader dr)
        {
            UserProfile instance = new UserProfile();
            instance._uid = new Guid(dr["UserID"].ToString());
            instance._sex = Convert.ToBoolean(dr["Sex"]);
            instance._birthday = Convert.ToDateTime(dr["BirthDay"]);
            instance._province = dr["Province"].ToString();
            instance._city = dr["City"].ToString();
            instance._company = dr["Company"].ToString();
            instance._hobby = dr["Hobby"].ToString();
            instance._qq = dr["QQ"].ToString();
            instance._msn = dr["MSN"].ToString();
            instance._cellphone = dr["MobilePIN"].ToString();
            instance._address = dr["Address"].ToString();

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

            strSql.Append("INSERT INTO fbs_UserProfile(");
            strSql.Append("UserID, Sex, BirthDay, Province, City, Company, Hobby, QQ, MSN, MobilePIN,Address)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_UserID, @in_Sex, @in_BirthDay, @in_Province, @in_City, @in_Company, @in_Hobby, @in_QQ, @in_MSN, @in_MobilePIN, @in_Address)");

            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_Sex", DbType.Boolean);
            cmdParms.Add("@in_BirthDay", DbType.DateTime);
            cmdParms.Add("@in_Province", DbType.String);
            cmdParms.Add("@in_City", DbType.String);
            cmdParms.Add("@in_Company", DbType.String);
            cmdParms.Add("@in_Hobby", DbType.String);
            cmdParms.Add("@in_QQ", DbType.String);
            cmdParms.Add("@in_MSN", DbType.String);
            cmdParms.Add("@in_MobilePIN", DbType.String);
            cmdParms.Add("@in_Address", DbType.String);
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

            strSql.Append("UPDATE fbs_UserProfile SET ");
            strSql.Append("Sex=@in_Sex,");
            strSql.Append("BirthDay=@in_BirthDay,");
            strSql.Append("Province=@in_Province,");
            strSql.Append("City=@in_City,");
            strSql.Append("Company=@in_Company,");
            strSql.Append("Hobby=@in_Hobby,");
            strSql.Append("QQ=@in_QQ,");
            strSql.Append("MSN=@in_MSN,");
            strSql.Append("MobilePIN=@in_MobilePIN,");
            strSql.Append("Address=@in_Address");
            strSql.Append(" WHERE UserID=@in_UserID");

            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_Sex", DbType.Boolean);
            cmdParms.Add("@in_BirthDay", DbType.DateTime);
            cmdParms.Add("@in_Province", DbType.String);
            cmdParms.Add("@in_City", DbType.String);
            cmdParms.Add("@in_Company", DbType.String);
            cmdParms.Add("@in_Hobby", DbType.String);
            cmdParms.Add("@in_QQ", DbType.String);
            cmdParms.Add("@in_MSN", DbType.String);
            cmdParms.Add("@in_MobilePIN", DbType.String);
            cmdParms.Add("@in_Address", DbType.String);
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

            strSql.Append("DELETE FROM fbs_UserProfile");
            strSql.Append(" WHERE UserID=@in_UserID");

            cmdParms.Add("@in_UserID", DbType.Guid);
        }

        #endregion

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 转换到数据行
        /// </summary>
        /// <param name="t">数据表</param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同

            if (t.Columns.Count == 0)
            {
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("Sex", typeof(bool));
                t.Columns.Add("BirthDay", typeof(DateTime));
                t.Columns.Add("Province", typeof(string));
                t.Columns.Add("City", typeof(string));
                t.Columns.Add("Company", typeof(string));
                t.Columns.Add("Hobby", typeof(string));
                t.Columns.Add("QQ", typeof(string));
                t.Columns.Add("MSN", typeof(string));
                t.Columns.Add("MobilePIN", typeof(string));
                t.Columns.Add("Address", typeof(string));
            }

            System.Data.DataRow row = t.NewRow();
            row["UserID"] = this._uid;
            row["Sex"] = this._sex;
            row["BirthDay"] = this._birthday;
            row["Province"] = this._province;
            row["City"] = this._city;
            row["Company"] = this._company;
            row["Hobby"] = this._hobby;
            row["QQ"] = this._qq;
            row["MSN"] = this._msn;
            row["MobilePIN"] = this._cellphone;
            row["Address"] = this._address;

            t.Rows.Add(row);
        }

        #endregion

        #region 属性
        private Guid _uid;
        private bool _sex;
        private DateTime _birthday;
        private string _province;
        private string _city;
        private string _company;
        private string _hobby;
        private string _qq;
        private string _msn;
        private string _cellphone;
        private string _address;

        public string Address
        {
            set {this._address=value; }
            get { return this._address; }
        }
        

        public bool Sex
        {
            get { return this._sex; }
            set { this._sex = value; }
        }
        

        public DateTime Birthday
        {
            get { return this._birthday; }
            set { this._birthday = value; }
        }


        public string Province
        {
            get { return this._province; }
            set { this._province = value; }
        }
        
        
        
        public string City
        {
            get { return this._city; }
            set { this._city = value; }
        }
        
        
        public string Company
        {
            get { return this._company; }
            set { this._company = value; }
        }

        

        public string Hobby
        {
            get { return this._hobby; }
            set { this._hobby = value; }
        }
        
        
        public string QQ
        {
            get { return this._qq; }
            set { this._qq = value; }
        }
        
        public string Msn
        {
            get { return this._msn; }
            set { this._msn = value; }
        }
        

        public string Cellphone
        {
            get { return this._cellphone; }
            set { this._cellphone = value; }
        }

        public Guid UserID
        {
            get { return this._uid; }
        }
        
        #endregion
    }
}
