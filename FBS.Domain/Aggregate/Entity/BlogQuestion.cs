using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using FBS.Domain.Aggregate.ValueObject;
using System.Data;
using System.Web;

namespace FBS.Domain.Aggregate.Entity
{
    public class BlogQuestion:IAggregateRoot
    {
        #region 新建问题

        /// <summary>
        /// 提问
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        /// <param name="categoryName">分类名称</param>
        /// <param name="accountId">用户编号</param>
        /// <param name="accountName">用户昵称</param>
        /// <param name="subject">标题</param>
        /// <param name="body">内容</param>
        /// <param name="rewardPoints">奖励</param>
        public BlogQuestion(Guid categoryId,string categoryName,Guid accountId,string accountName,string usrTiny,string subject,string body,int rewardPoints)
        {
            this._categoryVO = new CategoryVO() {  CategoryId=categoryId, Name=categoryName};
            this._accountMessageVO = new AccountMessageVO(accountId, accountName,usrTiny );
            this._state = new QuestionState() { AnswerCount=0, RewardPoints=rewardPoints, ClickCount=0, Subject=subject, Body=body };

            this._id = Guid.NewGuid();
            this._creationDate = DateTime.Now;
        }
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
                t.Columns.Add("QuestionID",typeof(Guid));
                t.Columns.Add("Subject",typeof(string));
                t.Columns.Add("Body",typeof(string));
                t.Columns.Add("UserID",typeof(Guid));
                t.Columns.Add("UserName",typeof(string));
                t.Columns.Add("UserTiny",typeof(string));
                t.Columns.Add("CategoryID",typeof(Guid));
                t.Columns.Add("CategoryName",typeof(string));
                t.Columns.Add("ClickCount",typeof(Int32));
                t.Columns.Add("RewardPoints",typeof(Int32));
                t.Columns.Add("AnswerCount",typeof(Int32));
                t.Columns.Add("CreationDate",typeof(DateTime));
                t.Columns.Add("BestAnswerID",typeof(Guid));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["QuestionID"] = this._id;
            row["Subject"] = this._state.Subject;
            row["Body"] = HttpUtility.HtmlEncode(this._state.Body);
            row["UserID"] = this._accountMessageVO.Id;
            row["UserName"] = this._accountMessageVO.UserName;
            row["UserTiny"] = this._accountMessageVO.Tiny;
            row["CategoryID"] = this._categoryVO.CategoryId;
            row["CategoryName"] = this._categoryVO.Name;
            row["ClickCount"] = this._state.ClickCount;
            row["RewardPoints"] = this._state.RewardPoints;
            row["AnswerCount"] = this._state.AnswerCount;
            row["CreationDate"] = this._creationDate;
            row["BestAnswerID"] = this._state.BestAnswerId;

            //添加
            t.Rows.Add(row);
        }

        #endregion

        #region 从持久化创建
        private BlogQuestion() { }
        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd"></param>
        /// <returns></returns>
        public static BlogQuestion CreateFromReader(IDataReader rd)
        {
            BlogQuestion q = new BlogQuestion();
            q._id=new Guid(rd["QuestionID"].ToString());
            q._creationDate = Convert.ToDateTime(rd["CreationDate"]);
            q._accountMessageVO = new AccountMessageVO(new Guid(rd["UserID"].ToString()), rd["UserName"].ToString(), rd["UserTiny"].ToString());
            q._categoryVO = new CategoryVO() { CategoryId=new Guid(rd["CategoryID"].ToString()),Name=rd["CategoryName"].ToString()};
            q._state = new QuestionState() { Subject=rd["Subject"].ToString(), Body=HttpUtility.HtmlDecode(rd["Body"].ToString()), ClickCount=Convert.ToInt32(rd["ClickCount"]), RewardPoints=Convert.ToInt32(rd["RewardPoints"]), AnswerCount=Convert.ToInt32(rd["AnswerCount"])};
            if (rd["BestAnswerID"].ToString() != Guid.Empty.ToString())
                q._state.BestAnswerId = new Guid(rd["BestAnswerID"].ToString());
            return q;
        }
        #endregion

        #region 辅助方法

        /// <summary>
        /// 设置最佳答案
        /// </summary>
        /// <param name="aid"></param>
        public void SettingBestAnswer(Guid aid)
        {
            this._state.BestAnswerId = aid;
        }

        /// <summary>
        /// 增加点击数
        /// </summary>
        public void AddClickCount()
        {
            this._state.ClickCount++;
        }

        /// <summary>
        /// 增加答案数
        /// </summary>
        public void AddAnswerCount()
        {
            this._state.AnswerCount++;
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

            strSql.Append("INSERT INTO fbs_BlogQuestion(");
            strSql.Append("QuestionID, Subject, Body, UserID, UserName,UserTiny,CategoryID, CategoryName, ClickCount, RewardPoints, AnswerCount, CreationDate, BestAnswerID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_QuestionID, @in_Subject, @in_Body, @in_UserID, @in_UserName,@in_UserTiny,@in_CategoryID, @in_CategoryName, @in_ClickCount, @in_RewardPoints, @in_AnswerCount, @in_CreationDate, @in_BestAnswerID)");

            cmdParms.Add("@in_QuestionID", DbType.Guid);
            cmdParms.Add("@in_Subject", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserTiny",DbType.String);
            cmdParms.Add("@in_CategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_ClickCount", DbType.Int32);
            cmdParms.Add("@in_RewardPoints", DbType.Int32);
            cmdParms.Add("@in_AnswerCount", DbType.Int32);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_BestAnswerID", DbType.Guid);
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

            strSql.Append("UPDATE fbs_BlogQuestion SET ");
            strSql.Append("Subject=@in_Subject,");
            strSql.Append("Body=@in_Body,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("UserTiny=@in_UserTiny,");
            strSql.Append("CategoryID=@in_CategoryID,");
            strSql.Append("CategoryName=@in_CategoryName,");
            strSql.Append("ClickCount=@in_ClickCount,");
            strSql.Append("RewardPoints=@in_RewardPoints,");
            strSql.Append("AnswerCount=@in_AnswerCount,");
            strSql.Append("CreationDate=@in_CreationDate,");
            strSql.Append("BestAnswerID=@in_BestAnswerID");
            strSql.Append(" WHERE QuestionID=@in_QuestionID");

            cmdParms.Add("@in_QuestionID", DbType.Guid);
            cmdParms.Add("@in_Subject", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserTiny",DbType.String);
            cmdParms.Add("@in_CategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_ClickCount", DbType.Int32);
            cmdParms.Add("@in_RewardPoints", DbType.Int32);
            cmdParms.Add("@in_AnswerCount", DbType.Int32);
            cmdParms.Add("@in_CreationDate", DbType.DateTime);
            cmdParms.Add("@in_BestAnswerID", DbType.Guid);
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

            strSql.Append("DELETE FROM fbs_BlogQuestion");
            strSql.Append(" WHERE QuestionID=@in_QuestionID");

            cmdParms.Add("@in_QuestionID", DbType.Guid);
        }

        #endregion

        #region 属性
        private Guid _id;
        private CategoryVO _categoryVO;
        

        public Guid CategoryID
        {
            get { return this._categoryVO.CategoryId; }
        }
        public string CategoryName
        {
            get { return this._categoryVO.Name; }
        }

        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get { return _creationDate; }
        }
        private AccountMessageVO _accountMessageVO;

        public Guid UserID
        {
            get { return this._accountMessageVO.Id; }
        }
        public string UserName
        {
            get { return this._accountMessageVO.UserName; }
        }

        public int UserPoints
        {
            get { return this._accountMessageVO.Points; }
        }

        public string UserTiny
        {
            get { return this._accountMessageVO.Tiny; }
        }

        public int Points
        {
            get { return this._accountMessageVO.Points; }
        }

        private QuestionState _state;

        public QuestionState State
        {
            get { return _state; }
        }

        public Guid BestAnswerID
        {
            get { return this._state.BestAnswerId; }
        }

        public int RewardPoints
        {
            get { return this.State.RewardPoints;}
        }

        #endregion
    }
}
