using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.DBUtility;
using FBS.Service.ActionModels;
using System.Data.Common;
using System.Configuration;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;

namespace FBS.Service
{
    public class SearchHelper
    {

        DBHelper helper = new DBHelper();
        /// <summary>
        /// 通过关键字查找文章模型
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="index">分页起始点</param>
        /// <param name="count">取数据条数</param>
        /// <returns>查找的文章列表集合</returns>
        public SearchHelper()
        {
            helper.ConnectionString = ConfigurationManager.ConnectionStrings["sqlstrconn"].ConnectionString;
            helper.DatabaseType = FBS.DBUtility.DBHelper.DatabaseTypes.Sql;
        }
        /// <summary>
        /// 根据查找类型查找出搜索结果数
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="type">查找类型</param>
        /// <returns></returns>
        public int GetResultCountByKeyWords(string keywords, string type)
        {
            //DbDataReader reader = helper.GetPageList("fbs_CMSArticle", "CreatedOn", "Title like '%" + keywords + "%' or Body like '%" + keywords + "%' oreder by CreatedOn", index, count);
            if (type == "1")
            {
                return helper.ExecuteNonQuery("select count(*) from fbs_CMSArticle where Title like '%" + keywords + "%'");
               // return helper.ExecuteNonQuery(System.Data.CommandType.Text, "select * from fbs_CMSArticle where Title like "+"@keywords"+" or Body like "+"@keywords", new System.Data.SqlClient.SqlParameter("@keywords", "'%"+keywords+"%'"));
            }
            if(type=="0")
            {
                return helper.ExecuteNonQuery("select count(*) from fbs_Story where Title like '%" + keywords + "%'");
                //return helper.ExecuteNonQuery(System.Data.CommandType.Text, "select * from fbs_Story where Title like " + "@keywords" + " or  Description like" + " @keywords", new System.Data.SqlClient.SqlParameter("@keywords", "'%" + keywords + "%'"));
            }
            if (type == "2")
            {
                return helper.ExecuteNonQuery("select count(*) from fbs_Message where Subject like '%" + keywords + "%'");
            }

            if (type == "3")
            {
                return helper.ExecuteNonQuery("select count(*) from fbs_BlogQuestion where Subject like '%" + keywords + "%'");
            }
            return 0;
        }
        public IList<ArticleDspModel> FetchArticleDspModelByKeyWords(string keywords,int index,int count)
        {
            IList<ArticleDspModel> articlelist = new List<ArticleDspModel>();//"Title like '%" + keywords + "%' or Body like '%" + keywords + "%'"
            DbDataReader reader = helper.GetPageList("fbs_CMSArticle", "CreatedOn", "Title like '%" + keywords + "%'", index, count);
            if (reader.HasRows)
            { 
            while(reader.Read())
            {
               ArticleDspModel model=new ArticleDspModel();
               model.ArticleID = new Guid(reader["ArticleID"].ToString());
               model.ClickCount = System.Int16.Parse(reader["ClickCount"].ToString());
               model.CreatedOn = DateTime.Parse(reader["CreatedOn"].ToString());
               model.ImgName = reader["ImgName"].ToString();
               model.Title = reader["Title"].ToString();
               articlelist.Add(model);
            }
            }
            return articlelist;
        }

        /// <summary>
        /// 通过关键查找博文类型
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="index">分页起始点</param>
        /// <param name="count">取数据条数</param>
        /// <returns>查找的文章列表集合</returns>
        public IList<BlogStoryDspModel> FetchBlogStoryDspModelByKeyWords(string keywords, int index, int count)
        {
            IList<BlogStoryDspModel> bloglist = new List<BlogStoryDspModel>();
            DbDataReader reader = helper.GetPageList("fbs_Story", "CreatedOn", "Title like '%" + keywords + "%'", index, count);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BlogStoryDspModel model = new BlogStoryDspModel();
                    model.CommentsCount = System.Int16.Parse(reader["CommentCount"].ToString());
                    model.Description = reader["Description"].ToString();
                    model.PublishTime = DateTime.Parse(reader["CreatedOn"].ToString());
                    model.ReadCount = System.Int16.Parse(reader["ClickCount"].ToString());
                    model.StoryID = new Guid(reader["StoryID"].ToString());
                    model.Title = reader["Title"].ToString();
                    model.UserID =new Guid( reader["UserID"].ToString());
                    model.WriterName = reader["UserName"].ToString();
                    bloglist.Add(model);
                }
            }
            return bloglist;        
        }

        /// <summary>
        /// 通过关键字找问与答
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="index">起始</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        public IList<BlogQuestionDspModel> FetchBlogQuestionDspModelByKeyWords(string keywords, int index, int count)
        {
            IList<BlogQuestionDspModel> bloglist = new List<BlogQuestionDspModel>();
            DbDataReader reader = helper.GetPageList("fbs_BlogQuestion", "CreationDate", "Subject like '%" + keywords + "%'", index, count);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    BlogQuestionDspModel model = new BlogQuestionDspModel();
                    model.AnswerCount =int.Parse(reader["AnswerCount"].ToString());
                    model.CategoryID = new Guid(reader["CategoryID"].ToString());
                    model.CategoryName = reader["CategoryName"].ToString();
                    model.QuestionID = new Guid(reader["QuestionID"].ToString());
                    model.RewardPoints = int.Parse(reader["RewardPoints"].ToString());
                    model.Subject = reader["Subject"].ToString();
                    model.UserID = new Guid(reader["UserID"].ToString());
                    model.UserName = reader["UserName"].ToString();
                    model.Body = reader["Body"].ToString();
                    model.CreationDate =DateTime.Parse(reader["CreationDate"].ToString());
                    bloglist.Add(model);
                }
            }
            return bloglist;   
        }

        /// <summary>
        /// 通过关键字查找帖子
        /// </summary>
        /// <param name="keywords">关键字</param>
        /// <param name="index">分页起始点</param>
        /// <param name="count">取数据条数</param>
        /// <returns>查找的帖子列表集合</returns>
        public IList<ThreadsDspModel> FetchThreadsDspModelByKeyWords(string keywords, int index, int count)
        {
            IList<ThreadsDspModel> mylist = new List<ThreadsDspModel>();
            DbDataReader reader = helper.GetPageList("fbs_Message", "CreationDate", "Subject like '%" + keywords + "%' and ParentMessageID='" + Guid.Empty + "'", index, count);
            IForumsRepository forumRep = FBS.Factory.Factory<IForumsRepository>.GetConcrete();
            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ThreadsDspModel model = new ThreadsDspModel();
                    Forum forum = forumRep.GetByKey(new Guid(reader["ForumID"].ToString()));
                    model.Body = reader["Body"].ToString();
                    model.ClickCount =System.Int16.Parse(reader["RewardPoints"].ToString());
                    model.CreationDate =DateTime.Parse( reader["CreationDate"].ToString());
                    model.ID =new Guid( reader["ThreadID"].ToString());
                    model.LastModified =DateTime.Parse(reader["ModifiedDate"].ToString());
                    model.MessageCount = 0;
                    //model.MessageID =new Guid( reader["MessageID"].ToString());
                    model.Title = reader["Subject"].ToString();
                    model.UserID =new Guid( reader["AccountID"].ToString());
                    model.UserName = forum.Name;//注意这里借用变量了，注意呀
                    model.MessageID = forum.Id;//注意这里同样借用变量了，注意
                    mylist.Add(model);
                }
            }
            return mylist;
        }
    }
}
