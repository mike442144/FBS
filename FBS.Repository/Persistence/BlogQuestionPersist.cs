using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using System.Data;
using System.Data.Common;

namespace FBS.Repository.Persistence
{
    internal class BlogQuestionPersist:DBUtility.DALHelper
    {
        public static HashSet<BlogQuestion> GetAllQuestions() 
        {
            string sql = "select * from fbs_BlogQuestion as b inner join fbs_BlogQuestionCategory as c on b.CategoryID=c.QuestionCategoryID order by CreationDate asc";
            HashSet<BlogQuestion> list=new HashSet<BlogQuestion>();
            using (DbDataReader rd = DataHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (rd.Read())
                {
                    list.Add(BlogQuestion.CreateFromReader(rd));
                }
            }

            return list;
        }


        public static void AnswerToQuestion(DataTable t)
        {
            foreach (DataRow r in t.Rows)
                PersistAddAnswer(r);
        }

        public static void AddAll(DataTable t)
        {
            foreach (DataRow r in t.Rows)
                PersistAdd(r);
        }

        public static void UpdateAll(DataTable t)
        {
            foreach (DataRow r in t.Rows)
                PersistUpdate(r);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="categoryId">分类编号</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">取出数目</param>
        public static IList<BlogQuestion> GetList(Guid categoryId,int startIndex,int count)
        {
            IList<BlogQuestion> list = new List<BlogQuestion>();

            using (DbDataReader rd = DataHelper.GetPageList("fbs_BlogQuestion", "CreationDate DESC", categoryId == Guid.Empty ? null : ("CategoryID='" + categoryId.ToString() + "'"), startIndex, count))
            {
                while (rd.Read())
                    list.Add(BlogQuestion.CreateFromReader(rd));
            }

            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        public static void PersistAddAnswer(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_BlogAnswer(");
            strSql.Append("AnswerID, Body, UserID,UserName, CreationDate, QuestionID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_AnswerID, @in_Body, @in_UserID,@in_UserName, @in_CreationDate, @in_QuestionID)");

            DbParameter[] cmdParms = new DbParameter[]{
                DataHelper.CreateInDbParameter("@in_AnswerID", DbType.Guid, r["AnswerID"]),
                DataHelper.CreateInDbParameter("@in_Body", DbType.String, r["Body"]),
                DataHelper.CreateInDbParameter("@in_UserID", DbType.Guid, r["UserID"]),
                DataHelper.CreateInDbParameter("@in_UserName", DbType.String, r["UserName"]),
                DataHelper.CreateInDbParameter("@in_CreationDate", DbType.DateTime, r["CreationDate"]),
                DataHelper.CreateInDbParameter("@in_QuestionID", DbType.Guid, r["QuestionID"])
            };

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        private static void PersistUpdate(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE fbs_BlogQuestion SET ");
            strSql.Append("Subject=@in_Subject,");
            strSql.Append("Body=@in_Body,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName");
            strSql.Append("CategoryID=@in_CategoryID,");
            strSql.Append("CategoryName=@in_CategoryName");
            strSql.Append("ClickCount=@in_ClickCount,");
            strSql.Append("RewardPoints=@in_RewardPoints,");
            strSql.Append("AnswerCount=@in_AnswerCount,");
            strSql.Append("CreationDate=@in_CreationDate,");
            strSql.Append("BestAnswerID=@in_BestAnswerID");
            strSql.Append(" WHERE QuestionID=@in_QuestionID");

            DbParameter[] cmdParms = new DbParameter[]{
                DataHelper.CreateInDbParameter("@in_Subject", DbType.String, r["Subject"]),
                DataHelper.CreateInDbParameter("@in_Body", DbType.String, r["Body"]),
                DataHelper.CreateInDbParameter("@in_UserID", DbType.Guid, r["UserID"]),
                DataHelper.CreateInDbParameter("@in_UserName", DbType.String, r["UserName"]),
                DataHelper.CreateInDbParameter("@in_CategoryID", DbType.Guid, r["CategoryID"]),
                DataHelper.CreateInDbParameter("@in_CategoryName",DbType.String,r["CategoryName"]),
                DataHelper.CreateInDbParameter("@in_ClickCount", DbType.Int32, r["ClickCount"]),
                DataHelper.CreateInDbParameter("@in_RewardPoints", DbType.Int32, r["RewardPoints"]),
                DataHelper.CreateInDbParameter("@in_AnswerCount", DbType.Int32, r["AnswerCount"]),
                DataHelper.CreateInDbParameter("@in_CreationDate", DbType.DateTime, r["CreationDate"]),
                DataHelper.CreateInDbParameter("@in_BestAnswerID", DbType.Guid, r["BestAnswerID"]),
                DataHelper.CreateInDbParameter("@in_QuestionID", DbType.Guid, r["QuestionID"])
            };

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="r"></param>
        private static void PersistAdd(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_BlogQuestion(");
            strSql.Append("QuestionID, Subject, Body, UserID,UserName, CategoryID,CategoryName, ClickCount, RewardPoints, AnswerCount,CreationDate,BestAnswerID)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_QuestionID, @in_Subject, @in_Body, @in_UserID,@in_UserName, @in_CategoryID,@in_CategoryName, @in_ClickCount, @in_RewardPoints, @in_AnswerCount,@in_CreationDate,@in_BestAnswerID)");

            DbParameter[] cmdParms = new DbParameter[]{
                DataHelper.CreateInDbParameter("@in_QuestionID", DbType.Guid, r["QuestionID"]),
                DataHelper.CreateInDbParameter("@in_Subject", DbType.String, r["Subject"]),
                DataHelper.CreateInDbParameter("@in_Body", DbType.String, r["Body"]),
                DataHelper.CreateInDbParameter("@in_UserID", DbType.Guid, r["UserID"]),
                DataHelper.CreateInDbParameter("@in_UserName", DbType.String, r["UserName"]),
                DataHelper.CreateInDbParameter("@in_CategoryID", DbType.Guid, r["CategoryID"]),
                DataHelper.CreateInDbParameter("@in_CategoryName",DbType.String,r["CategoryName"]),
                DataHelper.CreateInDbParameter("@in_ClickCount", DbType.Int32, r["ClickCount"]),
                DataHelper.CreateInDbParameter("@in_RewardPoints", DbType.Int32, r["RewardPoints"]),
                DataHelper.CreateInDbParameter("@in_AnswerCount", DbType.Int32, r["AnswerCount"]),
                DataHelper.CreateInDbParameter("@in_CreationDate", DbType.DateTime, r["CreationDate"]),
                DataHelper.CreateInDbParameter("@in_BestAnswerID", DbType.Guid, r["BestAnswerID"]??DBNull.Value)
                
            };

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
    }
}
