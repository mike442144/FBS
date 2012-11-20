using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;
using FBS.Domain.Aggregate.ValueObject;
using System.Web;

namespace FBS.Domain.Aggregate.Entity
{
    public class BlogStory:IAggregateRoot
    {

        #region 构造函数
        public BlogStory(Guid categoryId,string categoryName,Guid accountId,string userName,string userTiny ,string title,string body,bool isPublishedToHomepage,string imgname)
        {
            this._categoryVO = new CategoryVO();
            this._categoryVO.CategoryId = categoryId;
            this._categoryVO.Name = categoryName;

            this._accountVO = new AccountMessageVO(accountId,userName,userTiny);

            this._isPublishedToHomepage = isPublishedToHomepage;
            this._storyId = Guid.NewGuid();
            this._creationDate = DateTime.Now;

            this._state = new BlogStoryState();
            this._state.Title = title;
            this._state.Description = body;
            this._imgname = imgname;
        }

        private BlogStory() { }

        #endregion

        #region 辅助函数

        /// <summary>
        /// 从数据库创建
        /// </summary>
        /// <param name="rd">数据阅读器</param>
        /// <returns>BlogStory</returns>
        public static BlogStory CreateFromReader(IDataReader rd)
        {
            BlogStory story = new BlogStory();
            story._storyId = new Guid(rd["StoryID"].ToString());
            
            story._creationDate = Convert.ToDateTime(rd["CreatedOn"]);

            story._accountVO = new 
                AccountMessageVO(
                new Guid(rd["UserID"].ToString()),
                rd["UserName"].ToString(),
                rd["UserTiny"].ToString()
                );

            story._url = rd["Url"].ToString();
            story._isPublishedToHomepage = Convert.ToBoolean(rd["IsPublishedToHomepage"]);
            story._imgname = rd["ImgName"].ToString();
            story._categoryVO = new CategoryVO();
            story._categoryVO.CategoryId=new Guid(rd["CategoryID"].ToString());

            BlogStoryState state = new BlogStoryState();
            state.Title = rd["Title"].ToString();
            state.Description = HttpUtility.HtmlDecode(rd["Description"].ToString());
            state.ClickCount = Convert.ToInt32(rd["ClickCount"]);
            state.CommentCount = Convert.ToInt32(rd["CommentCount"]);

            story._state = state;

            return story;
        }

        /// <summary>
        /// 增加点击数
        /// </summary>
        public void AddClickCount()
        {
            this._state.ClickCount++;
        }

        /// <summary>
        /// 增加评论数
        /// </summary>
        public void AddCommentCount()
        {
            this._state.CommentCount++;
        }

        public void ReduceCommentCount()
        {
            this._state.CommentCount--;
        }

        #endregion

        #region IEntity 成员

        public Guid Id
        {
            get
            {
                return this._storyId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 转换到DataRow
        /// </summary>
        /// <param name="t">DataTable</param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //表中无列,则先添加列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("StoryID", typeof(Guid));
                t.Columns.Add("Title", typeof(string));
                t.Columns.Add("Description", typeof(string));
                t.Columns.Add("Url",typeof(string));
                t.Columns.Add("CategoryID", typeof(Guid));
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("UserName", typeof(string));
                t.Columns.Add("UserTiny",typeof(string));
                t.Columns.Add("CreatedOn",typeof(DateTime));
                t.Columns.Add("IsPublishedToHomepage",typeof(bool));
                t.Columns.Add("ClickCount",typeof(int));
                t.Columns.Add("CommentCount",typeof(int));
                t.Columns.Add("ImgName",typeof(string));
            }

            //新建行
            System.Data.DataRow row = t.NewRow();
            row["StoryID"] = this._storyId;
            row["CategoryID"] = this._categoryVO.CategoryId;
            row["Url"] = this._url;
            row["UserName"] = this._accountVO.UserName;
            row["UserID"] = this._accountVO.Id;
            row["UserTiny"] = this._accountVO.Tiny;
            row["CreatedOn"] = this._creationDate;
            row["IsPublishedToHomepage"] = this._isPublishedToHomepage;
            row["Title"] = this._state.Title;
            row["Description"] = HttpUtility.HtmlEncode(this._state.Description);
            row["ClickCount"] = this._state.ClickCount;
            row["CommentCount"] = this._state.CommentCount;
            row["ImgName"] = this._imgname;
            //添加
            t.Rows.Add(row);
        }

        #endregion

        #region 生成SQL命令

        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (strSql == null || cmdParms == null)
                return;

            strSql.Append("INSERT INTO fbs_Story(");
            strSql.Append("StoryID, Title, Description, Url, CategoryID, UserID, UserName, UserTiny, CreatedOn, IsPublishedToHomepage, ClickCount, CommentCount, ImgName)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_StoryID, @in_Title, @in_Description, @in_Url, @in_CategoryID, @in_UserID, @in_UserName, @in_UserTiny, @in_CreatedOn, @in_IsPublishedToHomepage, @in_ClickCount, @in_CommentCount, @in_ImgName)");

            cmdParms.Add("@in_StoryID", DbType.Guid);
            cmdParms.Add("@in_Title", DbType.String);
            cmdParms.Add("@in_Description", DbType.String);
            cmdParms.Add("@in_Url", DbType.String);
            cmdParms.Add("@in_CategoryID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserTiny",DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_IsPublishedToHomepage", DbType.Boolean);
            cmdParms.Add("@in_ClickCount", DbType.Int32);
            cmdParms.Add("@in_CommentCount", DbType.Int32);
            cmdParms.Add("@in_ImgName", DbType.String);
                
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

            strSql.Append("UPDATE fbs_Story SET ");
            strSql.Append("Title=@in_Title,");
            strSql.Append("Description=@in_Description,");
            strSql.Append("Url=@in_Url,");
            strSql.Append("CategoryID=@in_CategoryID,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("UserTiny=@in_UserTiny,");
            strSql.Append("CreatedOn=@in_CreatedOn,");
            strSql.Append("IsPublishedToHomepage=@in_IsPublishedToHomepage,");
            strSql.Append("ClickCount=@in_ClickCount,");
            strSql.Append("CommentCount=@in_CommentCount,");
            strSql.Append("ImgName=@in_ImgName");
            strSql.Append(" WHERE StoryID=@in_StoryID");

            cmdParms.Add("@in_StoryID", DbType.Guid);
            cmdParms.Add("@in_Title", DbType.String);
            cmdParms.Add("@in_Description", DbType.String);
            cmdParms.Add("@in_Url", DbType.String);
            cmdParms.Add("@in_CategoryID", DbType.Guid);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_UserTiny", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_IsPublishedToHomepage", DbType.Boolean);
            cmdParms.Add("@in_ClickCount", DbType.Int32);
            cmdParms.Add("@in_CommentCount", DbType.Int32);
            cmdParms.Add("@in_ImgName", DbType.String);

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
            strSql.Append("DELETE FROM fbs_Story");
            strSql.Append(" WHERE StoryID=@in_StoryID");

            cmdParms.Add("@in_StoryID", DbType.Guid);
        }

        #endregion

        #region 属性

        public string Title
        {
            get { return this._state.Title; }
            set { this._state.Title = value; }
        }

        public string Description
        {
            get { return this._state.Description; }
            set { this._state.Description = value; }
        }
        private CategoryVO _categoryVO;

        public Guid CategoryID
        {
            get { return this._categoryVO.CategoryId; }
            set { this._categoryVO.CategoryId = value; }
        }

        public string CategoryName
        {
            get { return this._categoryVO.Name; }
            set { this._categoryVO.Name=value; }
        }

        private BlogStoryState _state;

        public BlogStoryState State
        {
            get { return _state; }
        }

        public int ClickCount
        {
            get { return this._state.ClickCount; }
        }

        private Guid _storyId;

        private string _url;

        private AccountMessageVO _accountVO;

        public string UserName
        {
            get { return this._accountVO.UserName; }
        }

        public Guid UserID
        {
            get { return this._accountVO.Id; }
        }

        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { this._creationDate = value; }
        }

        private bool _isPublishedToHomepage;


        public string ImgName
        {
            get { return this._imgname; }
            set { this._imgname = value; }
        }

        private string _imgname;
        #endregion
    }
}
