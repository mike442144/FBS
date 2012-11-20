using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using FBS.Domain.Aggregate.ValueObject;

namespace FBS.Repository.Persistence
{
    public class BlogCategoryPersist:DBUtility.DALHelper
    {
        public static void Add(CategoryVO model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_BlogCategory(");
            strSql.Append("BlogCategoryID,CategoryName,Description,IconName,OrderPriority)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_BlogCategoryID,@in_Name,@in_Description,@in_IconName,@in_OrderPriority)");
            DbParameter[] cmdParms = new DbParameter[]{
				DataHelper.CreateInDbParameter("@in_BlogCategoryID", DbType.Guid, model.CategoryId),
				DataHelper.CreateInDbParameter("@in_Name", DbType.String, model.Name),
				DataHelper.CreateInDbParameter("@in_Description", DbType.String, model.Description),
				DataHelper.CreateInDbParameter("@in_IconName", DbType.String,model.IconName),
				DataHelper.CreateInDbParameter("@in_OrderPriority", DbType.Int16, model.Priority)};

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public static void AddQuestionCategory(CategoryVO model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_BlogQuestionCategory(");
            strSql.Append("QuestionCategoryID, CategoryName, Description, IconName, OrderPriority)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_QuestionCategoryID, @in_CategoryName, @in_Description, @in_IconName, @in_OrderPriority)");

            DbParameter[] cmdParms = new DbParameter[]{
                DataHelper.CreateInDbParameter("@in_QuestionCategoryID", DbType.Guid, model.CategoryId),
                DataHelper.CreateInDbParameter("@in_CategoryName", DbType.String, model.Name),
                DataHelper.CreateInDbParameter("@in_Description", DbType.String, model.Description),
                DataHelper.CreateInDbParameter("@in_IconName", DbType.String, model.IconName),
                DataHelper.CreateInDbParameter("@in_OrderPriority", DbType.Int16, model.Priority)};

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public static void AddCMSCategory(CategoryVO model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_CMSCategory(");
            strSql.Append("CategoryID, CategoryName, ParentID, Description, IconName, Deepth, Priority)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_CategoryID, @in_CategoryName, @in_ParentID, @in_Description, @in_IconName, @in_Deepth, @in_Priority)");

            DbParameter[] cmdParms = new DbParameter[]{
                DataHelper.CreateInDbParameter("@in_CategoryID", DbType.Guid, model.CategoryId),
                DataHelper.CreateInDbParameter("@in_CategoryName", DbType.String, model.Name),
                DataHelper.CreateInDbParameter("@in_ParentID", DbType.Guid, model.ParentId),
                DataHelper.CreateInDbParameter("@in_Description", DbType.String, model.Description),
                DataHelper.CreateInDbParameter("@in_IconName", DbType.String, model.IconName),
                DataHelper.CreateInDbParameter("@in_Deepth", DbType.Int16, model.Deepth),
                DataHelper.CreateInDbParameter("@in_Priority", DbType.Int16, model.Priority)};

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
    }
}
