using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Repository.Persistence
{
    public class BlogStoryPersist:DBUtility.DALHelper
    {
        public static HashSet<BlogStory> GetAll()
        {
            HashSet<BlogStory> list = new HashSet<BlogStory>();
            string sql = "select * from fbs_Story";
            using (DbDataReader dr = DataHelper.ExecuteReader(CommandType.Text,sql))
            {
                while (dr.Read())
                {
                    list.Add(BlogStory.CreateFromReader(dr));
                }
            }

            return list;
        }

        /// <summary>
        /// 加载评论
        /// </summary>
        /// <param name="dic"></param>
        public static void LoadCommentsByStoryID(Dictionary<Guid,HashSet<BlogComment>> dic,Guid id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM fbs_Comment ");
            strSql.Append(" WHERE StoryID=@in_StoryID");
            DbParameter[] cmdParms = new DbParameter[]{
				DataHelper.CreateInDbParameter("@in_StoryID", DbType.Guid, id)};

            HashSet<BlogComment> targets = new HashSet<BlogComment>();

            using (DbDataReader dr = DataHelper.ExecuteReader(CommandType.Text, strSql.ToString(), cmdParms))
            {
                while (dr.Read())
                    targets.Add(BlogComment.CreateFromReader(dr));
            }

            if (!dic.ContainsKey(id))
                dic.Add(id, new HashSet<BlogComment>());
            dic[id] = targets;

        }

        public static void AddComments(DataTable t)
        {
            foreach (DataRow row in t.Rows)
                AddComment(row);
        }

        private static void AddComment(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_Comment(");
            strSql.Append("CommentID,StoryID,UserID,UserName,Body,CreatedOn)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_CommentID,@in_StoryID,@in_UserID,@in_UserName,@in_Body,@in_CreatedOn)");
            DbParameter[] cmdParms = new DbParameter[]{
				DataHelper.CreateInDbParameter("@in_CommentID", DbType.Guid, r["CommentID"]),
				DataHelper.CreateInDbParameter("@in_StoryID", DbType.Guid, r["StoryID"]),
				DataHelper.CreateInDbParameter("@in_UserID", DbType.Guid, r["UserID"]),
				DataHelper.CreateInDbParameter("@in_UserName", DbType.String, r["UserName"]),
				DataHelper.CreateInDbParameter("@in_Body", DbType.String, r["Body"]),
				DataHelper.CreateInDbParameter("@in_CreatedOn", DbType.DateTime, r["CreatedOn"])};

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        public static void AddAll(DataTable t)
        {
            foreach (DataRow row in t.Rows)
                AddPersist(row);
        }

        public static void UpdateAll(DataTable t)
        {
            foreach (DataRow row in t.Rows)
                UpdatePersist(row);
        }

        public static void RemoveAll(DataTable t)
        { }

        private static void UpdatePersist(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("UPDATE fbs_Story SET ");
            strSql.Append("Title=@in_Title,");
            strSql.Append("Description=@in_Description,");
            strSql.Append("Url=@in_Url,");
            strSql.Append("CategoryID=@in_CategoryID,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("CreatedOn=@in_CreatedOn,");
            strSql.Append("IsPublishedToHomepage=@in_IsPublishedToHomepage,");
            strSql.Append("ClickCount=@in_ClickCount,");
            strSql.Append("CommentCount=@in_CommentCount");
            strSql.Append(" WHERE StoryID=@in_StoryID");
            DbParameter[] cmdParms = new DbParameter[]{
				DataHelper.CreateInDbParameter("@in_Title", DbType.String, r["Title"]),
				DataHelper.CreateInDbParameter("@in_Description", DbType.String, r["Description"]),
				DataHelper.CreateInDbParameter("@in_Url", DbType.String, r["Url"]),
				DataHelper.CreateInDbParameter("@in_CategoryID", DbType.Guid, r["CategoryID"]),
				DataHelper.CreateInDbParameter("@in_UserID", DbType.Guid, r["UserID"]),
				DataHelper.CreateInDbParameter("@in_UserName", DbType.String, r["UserName"]),
				DataHelper.CreateInDbParameter("@in_CreatedOn", DbType.DateTime, r["CreatedOn"]),
				DataHelper.CreateInDbParameter("@in_IsPublishedToHomepage", DbType.Boolean, r["IsPublishedToHomepage"]),
                DataHelper.CreateInDbParameter("@in_ClickCount", DbType.Int32, r["ClickCount"]),
                DataHelper.CreateInDbParameter("@in_CommentCount", DbType.Int32, r["CommentCount"]),
				DataHelper.CreateInDbParameter("@in_StoryID", DbType.Guid,r["StoryID"])};

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

        private static void AddPersist(DataRow r)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_Story(");
            strSql.Append("StoryID,Title,Description,Url,CategoryID,UserID,UserName,CreatedOn)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_StoryID,@in_Title,@in_Description,@in_Url,@in_CategoryID,@in_UserID,@in_UserName,@in_CreatedOn,@in_ClickCount,@in_CommentCount)");
            DbParameter[] cmdParms = new DbParameter[]{
				DataHelper.CreateInDbParameter("@in_StoryID", DbType.Guid,r["StoryID"]),
				DataHelper.CreateInDbParameter("@in_Title", DbType.String, r["Title"]),
				DataHelper.CreateInDbParameter("@in_Description", DbType.String, r["Description"]),
				DataHelper.CreateInDbParameter("@in_Url", DbType.String, r["Url"]),
				DataHelper.CreateInDbParameter("@in_CategoryID", DbType.Guid,r["CategoryID"]),
				DataHelper.CreateInDbParameter("@in_UserID", DbType.Guid, r["UserID"]),
				DataHelper.CreateInDbParameter("@in_UserName", DbType.String, r["UserName"]),
				DataHelper.CreateInDbParameter("@in_CreatedOn", DbType.DateTime, r["CreatedOn"]),
                DataHelper.CreateInDbParameter("@in_ClickCount",DbType.Int32,r["ClickCount"]),
                DataHelper.CreateInDbParameter("@in_CommentCount",DbType.Int32,r["CommentCount"])
            };

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }
    }
}
