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
    public class SheetQuestion : IAggregateRoot
    {
        #region 创建问题
        /// <summary>
        /// 创建问卷问题
        /// </summary>
        /// <param name="title">主题</param>
        /// <param name="description">描述</param>
        public SheetQuestion(Guid fid, string questionName)
        {
            this._id = Guid.NewGuid();
            this._formid = fid;
            this._questionName = questionName;
        }

        private SheetQuestion() { }
        #endregion

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
                t.Columns.Add("QuestionID", typeof(Guid));
                t.Columns.Add("FormID", typeof(Guid));
                t.Columns.Add("QuestionName", typeof(string));

            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["QuestionID"] = this._id;
            row["FormID"] = this._formid;
            row["QuestionName"] = this._questionName;


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
        public static SheetQuestion CreateFromReader(IDataReader rd)
        {
            SheetQuestion a = new SheetQuestion();

            a._id = new Guid(rd["QuestionID"].ToString());
            a._formid =new Guid( rd["FormID"].ToString());
            a._questionName = rd["QuestionName"].ToString();

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

            strSql.Append("INSERT INTO fbs_SheetQuestion(");
            strSql.Append("QuestionID, FormID, QuestionName)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_QuestionID, @in_FormID, @in_QuestionName)");

            cmdParms.Add("@in_QuestionID", DbType.Guid);
            cmdParms.Add("@in_FormID", DbType.Guid);
            cmdParms.Add("@in_QuestionName", DbType.String);

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

            strSql.Append("UPDATE fbs_SheetQuestion SET ");
            strSql.Append("FormID=@in_FormID,");
            strSql.Append("QuestionName=@in_QuestionName");

            strSql.Append(" WHERE QuestionID=@in_QuestionID");

            cmdParms.Add("@in_QuestionID", DbType.Guid);
            cmdParms.Add("@in_FormID", DbType.Guid);
            cmdParms.Add("@in_QuestionName", DbType.String);

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

            strSql.Append("DELETE FROM fbs_SheetQuestion");
            strSql.Append(" WHERE QuestionID=@in_QuestionID");

            cmdParms.Add("@in_QuestionID", DbType.Guid);
        }
        #endregion

        #region  属性
        private Guid _id;
       
        private string _questionName;
        private Guid _formid;

        public Guid FormID
        {
            get { return this._formid; }
            set{this._formid=value;}
        }
        public string QuestionName
        {
            get { return this._questionName; }
            set { this._questionName = value; }
        }
        #endregion
    }
}
