using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using System.Data;
using System.Data.Common;
using System.Web;


namespace FBS.Domain.Aggregate.Entity
{
    public class FormInfo : IAggregateRoot
    {
        #region 创建问卷
        /// <summary>
        /// 创建问卷信息
        /// </summary>
        /// <param name="title">主题</param>
        /// <param name="description">描述</param>
        public FormInfo(string title,string description,bool display)
        {
            this._id = Guid.NewGuid();
            this._title = title;
            this._description = description;
            this._display = display;
        }

        public FormInfo() { }
        #endregion

        public void SettingOnIndex(bool isOn)
        {
            this._display = isOn;
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
        /// 转换到数据行
        /// </summary>
        /// <param name="t"></param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("FormID", typeof(Guid));
                t.Columns.Add("Title", typeof(string));
                t.Columns.Add("Description", typeof(string));
                t.Columns.Add("Display", typeof(bool));
                
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["FormID"] = this._id;
            row["Title"] = this._title;
            row["Description"] = this._description;
            row["Display"] = this._display;
           

            //添加
            t.Rows.Add(row);
        }

        #endregion

        #region 从持久化创建
        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>问卷实例</returns>
        public static FormInfo CreateFromReader(IDataReader rd)
        {
            FormInfo a = new FormInfo();

            a._id = new Guid(rd["FormID"].ToString());
            a._title = rd["Title"].ToString();
            a._description = rd["Description"].ToString();
            a._display = Convert.ToBoolean(rd["Display"]);

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

            strSql.Append("INSERT INTO fbs_FormInfo(");
            strSql.Append("FormID, Title, Description,Display)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_FormID, @in_Title, @in_Description,@in_Display)");

            cmdParms.Add("@in_FormID", DbType.Guid);
            cmdParms.Add("@in_Title", DbType.String);
            cmdParms.Add("@in_Description", DbType.String);
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

            strSql.Append("UPDATE fbs_FormInfo SET ");
            strSql.Append("Title=@in_Title,");
            strSql.Append("Description=@in_Description,");
            strSql.Append("Display=@in_Display");
          
            strSql.Append(" WHERE FormID=@in_FormID");

            cmdParms.Add("@in_FormID", DbType.Guid);
            cmdParms.Add("@in_Title", DbType.String);
            cmdParms.Add("@in_Description", DbType.String);
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

            strSql.Append("DELETE FROM fbs_FormInfo");
            strSql.Append(" WHERE FormID=@in_FormID");

            cmdParms.Add("@in_FormID", DbType.Guid);
        }
        #endregion

        #region  属性
        private Guid _id;
        private string _title;
        private string _description;
        private bool _display;
        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public bool Display
        {
            get { return this._display; }
            set { this._display = value; }
        }
        #endregion
    }
}
