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
    public class Article:IAggregateRoot
    {
        #region 构造函数
        public Article(string title,string brieftitle,string body,Guid uid,string uname,Guid cid,string cname,string sourceUrl,string sourceSite,string ImgName)
        {
            this._id = Guid.NewGuid();

            this._articleVO = new ArticleVO(title, brieftitle, body, 0,0, sourceUrl, sourceSite);

            this._creationDate = DateTime.Now;

            this._accountMessageVO = new AccountMessageVO(uid,uname );
         
            this._categoryVO = new CategoryVO() { CategoryId=cid,Name=cname };

            this._imgName = ImgName;
        }
        private Article() { }

        #endregion

        #region 从持久化创建

        /// <summary>
        /// 从数据读取器创建
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>文章实例</returns>
        public static Article CreateFromReader(IDataReader rd)
        {
            Article a = new Article();

            a._id=new Guid(rd["ArticleID"].ToString());
            a._accountMessageVO = new AccountMessageVO(new Guid(rd["UserID"].ToString()), rd["UserName"].ToString());
            a._articleVO = new ArticleVO(rd["Title"].ToString(), rd["BriefTitle"].ToString(), HttpUtility.HtmlDecode(rd["Body"].ToString()), Convert.ToInt32(rd["ClickCount"]), Convert.ToInt32(rd["CommentCount"]), (rd["SourceUrl"] ?? "").ToString(), (rd["SourceSite"] ?? "").ToString());
            a._categoryVO = new CategoryVO() { CategoryId=new Guid(rd["CategoryID"].ToString()), Name=rd["CategoryName"].ToString() };
            a._imgName = rd["ImgName"].ToString();
            a._creationDate = Convert.ToDateTime(rd["CreatedOn"]);

            return a;
        }

        #endregion

        #region 生成数据库命令

        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql,Dictionary<string,DbType> cmdParms)
        {
            if (strSql == null||cmdParms==null)
                return;


            strSql.Append("INSERT INTO fbs_CMSArticle(");
            strSql.Append("ArticleID, Title, BriefTitle, Body, CreatedOn, UserID, UserName, CategoryID, CategoryName, SourceUrl, SourceSite, ClickCount, CommentCount, ImgName)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ArticleID, @in_Title, @in_BriefTitle, @in_Body, @in_CreatedOn, @in_UserID, @in_UserName, @in_CategoryID, @in_CategoryName, @in_SourceUrl, @in_SourceSite, @in_ClickCount, @in_CommentCount, @in_ImgName)");

            cmdParms.Add("@in_ArticleID", DbType.Guid);
            cmdParms.Add("@in_Title", DbType.String);
            cmdParms.Add("@in_BriefTitle", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_CategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName",DbType.String);
            cmdParms.Add("@in_SourceUrl", DbType.String);
            cmdParms.Add("@in_SourceSite",DbType.String);
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
            if (null==strSql||null==cmdParms)
                return;

            strSql.Append("UPDATE fbs_CMSArticle SET ");
            strSql.Append("Title=@in_Title,");
            strSql.Append("BriefTitle=@in_BriefTitle,");
            strSql.Append("Body=@in_Body,");
            strSql.Append("CreatedOn=@in_CreatedOn,");
            strSql.Append("UserID=@in_UserID,");
            strSql.Append("UserName=@in_UserName,");
            strSql.Append("CategoryID=@in_CategoryID,");
            strSql.Append("CategoryName=@in_CategoryName,");
            strSql.Append("SourceUrl=@in_SourceUrl,");
            strSql.Append("SourceSite=@in_SourceSite,");
            strSql.Append("ClickCount=@in_ClickCount,");
            strSql.Append("CommentCount=@in_CommentCount,");
            strSql.Append("ImgName=@in_ImgName");
            strSql.Append(" WHERE ArticleID=@in_ArticleID");

            cmdParms.Add("@in_ArticleID", DbType.Guid);
            cmdParms.Add("@in_Title", DbType.String);
            cmdParms.Add("@in_BriefTitle", DbType.String);
            cmdParms.Add("@in_Body", DbType.String);
            cmdParms.Add("@in_CreatedOn", DbType.DateTime);
            cmdParms.Add("@in_UserID", DbType.Guid);
            cmdParms.Add("@in_UserName", DbType.String);
            cmdParms.Add("@in_CategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_SourceUrl", DbType.String);
            cmdParms.Add("@in_SourceSite", DbType.String);
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

            strSql.Append("DELETE FROM fbs_CMSArticle");
            strSql.Append(" WHERE ArticleID=@in_ArticleID");

            cmdParms.Add("@in_ArticleID", DbType.Guid);
        }

        #endregion

        #region IEntity 成员

        /// <summary>
        /// 文章编号
        /// </summary>
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
        /// 转化为数据行
        /// </summary>
        /// <param name="t">数据表</param>
        public void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同

            if (t.Columns.Count == 0)
            {
                t.Columns.Add("ArticleID", typeof(Guid));
                t.Columns.Add("Title", typeof(string));
                t.Columns.Add("BriefTitle",typeof(string));
                t.Columns.Add("Body", typeof(string));
                t.Columns.Add("CreatedOn", typeof(DateTime));
                t.Columns.Add("UserID", typeof(Guid));
                t.Columns.Add("UserName", typeof(string));
                t.Columns.Add("CategoryID", typeof(Guid));
                t.Columns.Add("CategoryName",typeof(string));
                t.Columns.Add("SourceUrl",typeof(string));
                t.Columns.Add("SourceSite",typeof(string));
                t.Columns.Add("ClickCount",typeof(int));
                t.Columns.Add("CommentCount",typeof(int));
                t.Columns.Add("ImgName", typeof(string));
            }

            DataRow row = t.NewRow();
            row["ArticleID"] = this._id;
            row["Title"] = this._articleVO.Title;
            row["BriefTitle"] = this._articleVO.BriefTitle;
            row["Body"] = HttpUtility.HtmlEncode( this._articleVO.Body);
            row["CreatedOn"] = this._creationDate;
            row["UserID"] = this._accountMessageVO.Id;
            row["UserName"] = this._accountMessageVO.UserName??"";
            row["CategoryID"] = this._categoryVO.CategoryId;
            row["CategoryName"] = this._categoryVO.Name??"";
            row["SourceUrl"] = this._articleVO.SourceUrl??"";
            row["SourceSite"] = this._articleVO.SourceSite??"";
            row["ClickCount"] = this._articleVO.ClickCount;
            row["CommentCount"] = this._articleVO.CommentCount;
            row["ImgName"] = this._imgName;
            t.Rows.Add(row);
        }

        #endregion

        public void AddCommentCount()
        {
            this._articleVO.CommentCount++;
        }

        public void AddClickCount()
        {
            this._articleVO.ClickCount++;
        }

        public void ReduceCommentCount()
        {
            this._articleVO.CommentCount--;
        }

        #region 属性

        private Guid _id;
        private ArticleVO _articleVO;
        public Guid CategoryID
        {
            get { return this._categoryVO.CategoryId; }
            set { this._categoryVO.CategoryId = value; }
        }

        public string CategoryName
        {
            get { return this._categoryVO.Name; }
            set { this._categoryVO.Name = value; }
        }

        public ArticleVO ArticleVO
        {
            get { return _articleVO; }
            set { _articleVO = value; }
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

        private DateTime _creationDate;

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { this._creationDate = value; }
        }

        public int ClickCount
        {
            get { return this.ArticleVO.ClickCount; }
        }
        private CategoryVO _categoryVO;


        private string _imgName;

        public string ImgName
        {
            set { this._imgName = value; }
            get { return this._imgName; }
        }
        #endregion
    }
}
