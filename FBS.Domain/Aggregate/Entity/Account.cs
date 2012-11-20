using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using FBS.Utils;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    [Serializable]
    public class Account:IAggregateRoot
    {
        /// <summary>
        /// 测试构造函数
        /// </summary>
        public Account()
        {
            this._accountMessageVO = new AccountMessageVO();
        }

        /// <summary>
        /// 新建账户构造函数
        /// </summary>
        /// <param name="email"></param>
        /// <param name="psd"></param>
        public Account(string email, string psd)
        {
            this._email = email;
            HashHelper.SaltAndHashPassword(psd, out this._salt, out this._psd);
            this._uid=Guid.NewGuid();
            this._roleNames = "Member";
            
            //维护AccountMessageVO对象
            this._accountMessageVO = new 
                AccountMessageVO(this._uid,email,"default_head.gif","default_head.gif",60);
        }
        public Account(string email, string psd, string name,string role)
        {
            this._email = email;
            this._uid = Guid.NewGuid();
            HashHelper.SaltAndHashPassword(psd, out this._salt, out this._psd);
            this._roleNames = role;

            this._accountMessageVO = new 
                AccountMessageVO(this._uid,name,"default_head.gif","default_head.gif",60);
        }

        /// <summary>
        /// 登录时检查账户密码是否正确
        /// </summary>
        /// <param name="psd"></param>
        /// <returns></returns>
        public bool CheckPsd(string psd)
        {
            return HashHelper.ValidatePassword(psd, this._salt, this._psd);
        }

        /// <summary>
        /// 添加积分
        /// </summary>
        /// <param name="num">积分</param>
        public void AddPoints(int num)
        {
            this._accountMessageVO.Points += num;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="newpwd"></param>
        public void ModifyPWD(string newpwd)
        {
            HashHelper.SaltAndHashPassword(newpwd, out this._salt, out this._psd);
        }
        /// <summary>
        /// 减少积分
        /// </summary>
        /// <param name="num">积分</param>
        public void ReducePoints(int num)
        {
            this._accountMessageVO.Points -= num;
        }
        

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._uid;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 转化成数据行
        /// </summary>
        /// <param name="t"></param>
        public void AlterToRow(DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("AccountID",typeof(Guid));
                t.Columns.Add("Email",typeof(string));
                t.Columns.Add("Name",typeof(string));
                t.Columns.Add("Role",typeof(string));
                t.Columns.Add("Salt",typeof(Byte[]));
                t.Columns.Add("HashPsd",typeof(Byte[]));
                t.Columns.Add("Points",typeof(int));
                t.Columns.Add("Head", typeof(string));
                t.Columns.Add("Tiny", typeof(string));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["AccountID"] = this._uid;
            row["Name"] = this._accountMessageVO.UserName;
            row["Email"] = this._email;
            row["Role"] = this._roleNames;
            row["Salt"] = this._salt;
            row["HashPsd"] = this._psd;
            row["Points"] = this._accountMessageVO.Points;
            row["Head"] = this._accountMessageVO.Head;
            row["Tiny"] = this._accountMessageVO.Tiny;
            //添加
            t.Rows.Add(row);
        }


        #endregion

        #region 属性
        private Guid _uid;
        private string _email;

        public string Tiny
        {
            get { return this._accountMessageVO.Tiny; }
        }
        public string UserHead
        {
            get { return this._accountMessageVO.Head; }
        }
        public string Email
        {
            get { return _email; }
        }
        public string UserName
        {
            get { return this._accountMessageVO.UserName??""; }
        }
        private string _roleNames;

        public string Roles
        {
            get { return this._roleNames??""; }
            set { this._roleNames = value; }
        }

        private byte[] _psd;

        public byte[] Psd
        {
            get { return _psd; }
            set { this._psd = value; }
        }
        private byte[] _salt;

        public byte[] Salt
        {
            get { return _salt; }
        }

        public int Points 
        {
            get { return this._accountMessageVO.Points; }
        }
        public AccountMessageVO AccountMsgVO
        {
            get { return this._accountMessageVO; }
            set { this._accountMessageVO = value; }
        }
        private DateTime _createDate;
        private DateTime _modifiedDate;
        /// <summary>
        /// 存放用户发帖数等信息
        /// </summary>
        private AccountMessageVO _accountMessageVO;
        private Reward _reward;

        #endregion

        #region 从持久化创建

        /// <summary>
        /// 从数据阅读器创建
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static Account CreateFromReader(IDataReader rd)
        {
            Account a = new Account();
            
            a._uid = new Guid(rd["AccountID"].ToString());
            a._salt = (Byte[])rd["Salt"];
            a._roleNames=rd["Role"].ToString();
            a._email = rd["Email"].ToString();
            a._psd = (Byte[])rd["HashPsd"];
            a._accountMessageVO = new AccountMessageVO(a._uid, rd["Name"].ToString(),rd["Head"].ToString(),rd["Tiny"].ToString(),Convert.ToInt32(rd["Points"]));
            return a;
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

            strSql.Append("INSERT INTO fbs_Account(");
            strSql.Append("AccountID, Email, Name, Role, Salt, HashPsd, Points, Head, Tiny)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_AccountID, @in_Email, @in_Name, @in_Role, @in_Salt, @in_HashPsd, @in_Points, @in_Head, @in_Tiny)");

            cmdParms.Add("@in_AccountID", DbType.Guid);
            cmdParms.Add("@in_Email", DbType.String);
            cmdParms.Add("@in_Name", DbType.String);
            cmdParms.Add("@in_Role", DbType.String);
            cmdParms.Add("@in_Salt", DbType.Binary);
            cmdParms.Add("@in_HashPsd", DbType.Binary);
            cmdParms.Add("@in_Points", DbType.Int32);
            cmdParms.Add("@in_Head", DbType.String);
            cmdParms.Add("@in_Tiny", DbType.String);
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

            strSql.Append("UPDATE fbs_Account SET ");
            strSql.Append("Email=@in_Email,");
            strSql.Append("Name=@in_Name,");
            strSql.Append("Role=@in_Role,");
            strSql.Append("Salt=@in_Salt,");
            strSql.Append("HashPsd=@in_HashPsd,");
            strSql.Append("Points=@in_Points,");
            strSql.Append("Head=@in_Head,");
            strSql.Append("Tiny=@in_Tiny");
            strSql.Append(" WHERE AccountID=@in_AccountID");

            cmdParms.Add("@in_AccountID", DbType.Guid);
            cmdParms.Add("@in_Email", DbType.String);
            cmdParms.Add("@in_Name", DbType.String);
            cmdParms.Add("@in_Role", DbType.String);
            cmdParms.Add("@in_Salt", DbType.Binary);
            cmdParms.Add("@in_HashPsd", DbType.Binary);
            cmdParms.Add("@in_Points", DbType.Int32);
            cmdParms.Add("@in_Head", DbType.String);
            cmdParms.Add("@in_Tiny", DbType.String);

        }

        #endregion
    }
}
