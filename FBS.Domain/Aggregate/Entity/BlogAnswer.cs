using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using System.Data.Common;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class BlogAnswer:IAggregateRoot
    {
        /// <summary>
        /// 博客回答
        /// </summary>
        /// <param name="body"></param>
        /// <param name="account"></param>
        /// <param name="userName"></param>
        /// <param name="questionId"></param>
        public BlogAnswer(string body,Guid account,string userName,string tiny,Guid questionId)
        {
            this._body = body;
           
            this._creationDate = DateTime.Now;
            this._id = Guid.NewGuid();

            this._questionId = questionId;

            this._accountMessageVO = new AccountMessageVO(account, userName,tiny);
        }

        #region 从持久化创建
        private BlogAnswer() { }
        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>文章实例</returns>
        public static BlogAnswer CreateFromReader(IDataReader rd)
        {
            BlogAnswer a = new BlogAnswer();

            a._id = new Guid(rd["AnswerID"].ToString());
            a._accountMessageVO = new AccountMessageVO(new Guid(rd["UserID"].ToString()), rd["UserName"].ToString(),rd["UserTiny"].ToString() );
            a._body = Utils.Utils.HtmlDecode(rd["Body"].ToString());
            a._gainPoints = Convert.ToInt32(rd["GainPoints"]);
            a._questionId=new Guid(rd["QuestionID"].ToString());
            
            a._creationDate = Convert.ToDateTime(rd["CreationDate"]);

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

            strSql.Append("INSERT INTO fbs_BlogAnswer(");
            strSql.Append("AnswerID, Body, UserID, UserName,UserTiny, CreationDate, QuestionID, GainPoints)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_AnswerID, @in_Body, @in_UserID, @in_UserName,@in_UserTiny, @in_CreationDate, @in_QuestionID, @in_GainPoints)");

            cmdParms.Add("@in_AnswerID", DbType.Guid);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserTiny",DbType.String);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_QuestionID", DbType.Guid);
            cmdParms.Add("@in_GainPoints", DbType.Int32);

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

            strSql.Append("UPDATE fbs_BlogAnswer SET ");
            strSql.Append("Body=@in_Body,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("UserTiny=@in_UserTiny,");
            strSql.Append("CreationDate=@in_CreationDate,");
            strSql.Append("QuestionID=@in_QuestionID,");
            strSql.Append("GainPoints=@in_GainPoints");
            strSql.Append(" WHERE AnswerID=@in_AnswerID");

            cmdParms.Add("@in_AnswerID", DbType.Guid);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserTiny",DbType.String);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_QuestionID", DbType.Guid);
            cmdParms.Add("@in_GainPoints", DbType.Int32);
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

            strSql.Append("DELETE FROM fbs_BlogAnswer");
            strSql.Append(" WHERE AnswerID=@in_AnswerID");

            cmdParms.Add("@in_AnswerID", DbType.Guid);
        }

        #endregion

        private Guid _id;

        private string _body;
        private int _gainPoints;

        public int GainPoints
        {
            get { return this._gainPoints; }
        }

        public string Body
        {
            get { return _body; }
        }
        private AccountMessageVO _accountMessageVO;
        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get { return _creationDate; }
        }
        private Guid _questionId;

        public Guid UserID
        {
            get { return this._accountMessageVO.Id; }
        }

        public string UserName
        {
            get { return this._accountMessageVO.UserName; }
        }

        public string UserTiny
        {
            get { return this._accountMessageVO.Tiny; }
        }
        public int UserPoints
        {
            get { return this._accountMessageVO.Points; }
        }
        public Guid QuestionId
        {
            get { return _questionId; }
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
        /// <param name="t">数据表</param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("AnswerID",typeof(Guid));
                t.Columns.Add("Body",typeof(string));
                t.Columns.Add("UserID",typeof(Guid));
                t.Columns.Add("UserName",typeof(string));
                t.Columns.Add("UserTiny",typeof(string));
                t.Columns.Add("CreationDate",typeof(DateTime));
                t.Columns.Add("QuestionID",typeof(Guid));
                t.Columns.Add("GainPoints",typeof(int));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["AnswerID"] = this._id;
            row["Body"] = this._body;
            row["UserID"] = this._accountMessageVO.Id;
            row["UserName"] = this._accountMessageVO.UserName;
            row["UserTiny"] = this._accountMessageVO.Tiny;
            row["CreationDate"] = this._creationDate;
            row["QuestionID"] = this._questionId;
            row["GainPoints"] = this._gainPoints;
            //添加
            t.Rows.Add(row);
        }

        #endregion
    }
}
