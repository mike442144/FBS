using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class BlogQuestionCategory:CategoryBase
    {
        #region 新建分类

        /// <summary>
        /// 新建分类
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="desc">描述</param>
        /// <param name="icon">图标</param>
        /// <param name="priority">优先级</param>
        public BlogQuestionCategory(string name, string desc, string icon, uint priority)
        {
            this._name = name;
            this._description = desc;
            this._icon = icon;
            this._priority = priority;

            this._categoryId = Guid.NewGuid();
        }

        private BlogQuestionCategory() { }

        #endregion

        #region 持久化辅助

        /// <summary>
        /// 从数据读取器创建对象
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>博客文章分类对象</returns>
        public static BlogQuestionCategory CreateFromReader(IDataReader rd)
        {
            BlogQuestionCategory instance = new BlogQuestionCategory();
            instance._categoryId = new Guid(rd["QuestionCategoryID"].ToString());
            instance._name = rd["CategoryName"].ToString();
            instance._icon = rd["IconName"].ToString();
            instance._description = rd["Description"].ToString();
            instance._priority = Convert.ToUInt32(rd["OrderPriority"]);


            return instance;
        }

        /// <summary>
        /// 生成添加命令
        /// </summary>
        /// <param name="strSql">SQL字符串</param>
        /// <param name="cmdParms">参数字典</param>
        public static void PrepareAddCommand(StringBuilder strSql, Dictionary<string, DbType> cmdParms)
        {
            if (strSql == null || cmdParms == null)
                return;

            strSql.Append("INSERT INTO fbs_BlogQuestionCategory(");
            strSql.Append("QuestionCategoryID, CategoryName, Description, IconName, OrderPriority)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_QuestionCategoryID, @in_CategoryName, @in_Description, @in_IconName, @in_OrderPriority)");

            cmdParms.Add("@in_QuestionCategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_Description", DbType.String);
            cmdParms.Add("@in_IconName", DbType.String);
            cmdParms.Add("@in_OrderPriority", DbType.Int16);
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

            strSql.Append("UPDATE fbs_BlogQuestionCategory SET ");
            strSql.Append("CategoryName=@in_CategoryName,");
            strSql.Append("Description=@in_Description,");
            strSql.Append("IconName=@in_IconName,");
            strSql.Append("OrderPriority=@in_OrderPriority");
            strSql.Append(" WHERE QuestionCategoryID=@in_QuestionCategoryID");

            cmdParms.Add("@in_QuestionCategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_Description", DbType.String);
            cmdParms.Add("@in_IconName", DbType.String);
            cmdParms.Add("@in_OrderPriority", DbType.Int16);
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

            strSql.Append("DELETE FROM fbs_BlogQuestionCategory");
            strSql.Append(" WHERE QuestionCategoryID=@in_QuestionCategoryID");

            cmdParms.Add("@in_QuestionCategoryID", DbType.Guid);
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
                t.Columns.Add("QuestionCategoryID", typeof(Guid));
                t.Columns.Add("CategoryName", typeof(string));
                t.Columns.Add("Description", typeof(string));
                t.Columns.Add("IconName", typeof(string));
                t.Columns.Add("OrderPriority", typeof(uint));

            }

            //添加行
            System.Data.DataRow row = t.NewRow();
            row["QuestionCategoryID"] = this._categoryId;
            row["CategoryName"] = this._name;
            row["Description"] = this._description;
            row["IconName"] = this._icon;
            row["OrderPriority"] = this._priority;

            t.Rows.Add(row);
        }

        #endregion

        #region 属性
        public Guid QuestionCategory
        {
            set { this._categoryId = value; }
            get { return this._categoryId; }
        }
        public string CategoryName
        {
            set { this._name = value; }
            get { return this._name; }
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
