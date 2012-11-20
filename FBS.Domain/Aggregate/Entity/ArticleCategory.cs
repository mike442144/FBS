using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class ArticleCategory:CategoryBase
    {
        #region 新建分类
        /// <summary>
        /// 新建根分类
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <param name="desc">描述</param>
        /// <param name="icon">图标</param>
        /// <param name="priority">显示优先级</param>
        public ArticleCategory(string name, string desc, string icon, uint priority)
        {
            this._name = name;
            this._description = desc;
            this._icon = icon;
            this._priority = priority;

            this._deepth = 1;
            this._parentId = Guid.Empty;

            this._categoryId = Guid.NewGuid();
        }

        /// <summary>
        /// 新建分类
        /// </summary>
        /// <param name="name">分类名称</param>
        /// <param name="desc">描述</param>
        /// <param name="icon">图标</param>
        /// <param name="priority">显示优先级</param>
        /// <param name="deepth">深度</param>
        /// <param name="parentId">父分类编号</param>
        public ArticleCategory(string name, string desc, string icon, uint priority, uint deepth, Guid parentId)
        {
            this._name = name;
            this._description = desc;
            this._icon = icon;
            this._priority = priority;

            this._deepth = deepth;
            this._parentId = parentId;

            this._categoryId = Guid.NewGuid();
        }

        private ArticleCategory() { }

        #endregion

        #region 从持久化创建对象
        /// <summary>
        /// 从数据读取器创建对象
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>博客文章分类对象</returns>
        public static ArticleCategory CreateFromReader(IDataReader rd)
        {
            ArticleCategory instance = new ArticleCategory();
            instance._categoryId = new Guid(rd["CategoryID"].ToString());
            instance._name = rd["CategoryName"].ToString();
            instance._icon = rd["IconName"].ToString();
            instance._description = rd["Description"].ToString();
            instance._priority = Convert.ToUInt32(rd["Priority"]);
            instance._deepth = Convert.ToUInt32(rd["Deepth"]);
            instance._parentId = new Guid(rd["ParentID"].ToString());

            return instance;
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

            strSql.Append("INSERT INTO fbs_CMSCategory(");
            strSql.Append("CategoryID, CategoryName, ParentID, Description, IconName, Deepth, Priority)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_CategoryID, @in_CategoryName, @in_ParentID, @in_Description, @in_IconName, @in_Deepth, @in_Priority)");

            cmdParms.Add("@in_CategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_ParentID", DbType.Guid);
            cmdParms.Add("@in_Description", DbType.String);
            cmdParms.Add("@in_IconName", DbType.String);
            cmdParms.Add("@in_Deepth", DbType.Int16);
            cmdParms.Add("@in_Priority", DbType.Int16);
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

            strSql.Append("UPDATE fbs_CMSCategory SET ");
            strSql.Append("CategoryName=@in_CategoryName,");
            strSql.Append("ParentID=@in_ParentID,");
            strSql.Append("Description=@in_Description,");
            strSql.Append("IconName=@in_IconName,");
            strSql.Append("Deepth=@in_Deepth,");
            strSql.Append("Priority=@in_Priority");
            strSql.Append(" WHERE CategoryID=@in_CategoryID");

            cmdParms.Add("@in_CategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_ParentID", DbType.Guid);
            cmdParms.Add("@in_Description", DbType.String);
            cmdParms.Add("@in_IconName", DbType.String);
            cmdParms.Add("@in_Deepth", DbType.Int16);
            cmdParms.Add("@in_Priority", DbType.Int16);
        }

        /// <summary>
        /// 生成删除命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareDeleteCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms) 
        {
            if (strSql == null || cmdParms == null)
                return;

            strSql.Append("DELETE FROM fbs_CMSCategory");
            strSql.Append(" WHERE CategoryID=@in_CategoryID");

            cmdParms.Add("@in_CategoryID", DbType.Guid);
        }

        #endregion

        #region CategoryBase成员

        public override Guid Id
        {
            get
            {
                return this._categoryId;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 把对象转化到数据行
        /// </summary>
        /// <param name="t">数据表</param>
        public override void AlterToRow(System.Data.DataTable t)
        {
            //数据行的属性顺序与数据库表中的属性顺序必须相同
            //若表中无列
            if (t.Columns.Count == 0)
            {
                t.Columns.Add("CategoryID", typeof(Guid));
                t.Columns.Add("CategoryName", typeof(string));
                t.Columns.Add("ParentID", typeof(Guid));
                t.Columns.Add("Description", typeof(string));
                t.Columns.Add("IconName", typeof(string));
                t.Columns.Add("Deepth", typeof(uint));
                t.Columns.Add("OrderPriority", typeof(uint));

            }

            //添加行
            System.Data.DataRow row = t.NewRow();
            row["CategoryID"] = this._categoryId;
            row["CategoryName"] = this._name;
            row["ParentID"] = this._parentId;
            row["Description"] = this._description;
            row["IconName"] = this._icon;
            row["Deepth"] = this._deepth;
            row["OrderPriority"] = this._priority;

            t.Rows.Add(row);
        }

        #endregion

        #region 属性

        public string Name
        {
            set { this._name = value; }
            get { return this._name; }
        }
        public Guid ParentID
        {
            set { this._parentId = value; }
            get { return this._parentId; }
        }

        public uint Deepth
        {
            set { this._deepth = value; }
            get { return this._deepth; }
        }
        public string Description
        {
            set { this._description = value; }
            get { return this._description; }
        }
        public string Icon
        {
            set { this._icon = value; }
            get { return this._icon; }
        }
        public uint Priority
        {
            set { this._priority = value; }
            get { return this._priority; }
        }
        private Guid _parentId;
        private uint _deepth;

        #endregion
    }
}
