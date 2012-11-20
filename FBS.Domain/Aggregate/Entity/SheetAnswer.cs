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
    public class SheetAnswer : IAggregateRoot
    {
        #region 创建问卷
        /// <summary>
        /// 创建问卷信息
        /// </summary>
        /// <param name="title">主题</param>
        /// <param name="description">描述</param>
        public SheetAnswer(Guid QuestionID, string AnswerName,Guid formId)
        {
            this._id = Guid.NewGuid();
            this._questionID = QuestionID;
            this._answerName =AnswerName;
            this._count = 0;
            this._formId = formId;
        }

        private SheetAnswer() { }
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
                t.Columns.Add("AnswerID", typeof(Guid));
                t.Columns.Add("QuestionID", typeof(Guid));
                t.Columns.Add("AnswerName", typeof(string));
                t.Columns.Add("Count", typeof(int));
                t.Columns.Add("FormID",typeof(Guid));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["AnswerID"] = this._id;
            row["QuestionID"] = this._questionID;
            row["AnswerName"] = this._answerName;
            row["Count"] = this._count;
            row["FormID"] = this._formId;

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
        public static SheetAnswer CreateFromReader(IDataReader rd)
        {
            SheetAnswer instance = new SheetAnswer();

            instance._id = new Guid(rd["AnswerID"].ToString());
            instance._questionID =new Guid( rd["QuestionID"].ToString());
            instance._answerName = rd["AnswerName"].ToString();
            instance._count = Convert.ToInt32(rd["Count"]);
            instance._formId = new Guid(rd["FormID"].ToString());

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

            strSql.Append("INSERT INTO fbs_SheetAnswer(");
            strSql.Append("AnswerID, QuestionID, AnswerName,Count,FormID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_AnswerID, @in_QuestionID, @in_AnswerName,@in_Count,@in_FormID)");

            cmdParms.Add("@in_AnswerID", DbType.Guid);
            cmdParms.Add("@in_QuestionID", DbType.Guid);
            cmdParms.Add("@in_AnswerName", DbType.String);
            cmdParms.Add("@in_Count", DbType.Int32);
            cmdParms.Add("@in_FormID",DbType.Guid);
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

            strSql.Append("UPDATE fbs_SheetAnswer SET ");
            strSql.Append("QuestionID=@in_QuestionID,");
            strSql.Append("AnswerName=@in_AnswerName,");
            strSql.Append("Count=@in_Count,");
            strSql.Append("FormID=@in_FormID");
            strSql.Append(" WHERE AnswerID=@in_AnswerID");

            cmdParms.Add("@in_AnswerID", DbType.Guid);
            cmdParms.Add("@in_QuestionID", DbType.Guid);
            cmdParms.Add("@in_AnswerName", DbType.String);
            cmdParms.Add("@in_Count", DbType.Int32);
            cmdParms.Add("@in_FormID",DbType.Guid);
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

            strSql.Append("DELETE FROM fbs_SheetAnswer");
            strSql.Append(" WHERE AnswerID=@in_AnswerID");

            cmdParms.Add("@in_AnswerID", DbType.Guid);
        }
        #endregion

        #region  属性
        private Guid _id;
        private Guid _questionID;
        private string _answerName;
        private int _count;
        private Guid _formId;

        public Guid FormID
        {
            get { return this._formId; }
        }

        public Guid QuestionID
        {
            get { return this._questionID; }
            set { this._questionID = value; }
        }

        public string AnswerName
        {
            get { return this._answerName; }
            set { this._answerName = value; }
        }

        public int Count
        {
            get { return this._count; }
            set { this._count = value; }
        }
        #endregion
    }
}
