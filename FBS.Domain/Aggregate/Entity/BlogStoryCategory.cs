using System;
using System.Collections.Generic;
using System.Text;
using FBS.Domain.Entity;
using System.Data;

namespace FBS.Domain.Aggregate.Entity
{
    public class BlogStoryCategory:CategoryBase
    {
        #region 新建分类

        public BlogStoryCategory(string name,string desc,string icon,uint priority)
        {
            this._name = name;
            this._description = desc;
            this._icon = icon;
            this._priority = priority;

            this._categoryId = Guid.NewGuid();
        }

        public  BlogStoryCategory() { }

        #endregion

        #region 持久化相关

        /// <summary>
        /// 从数据读取器创建对象
        /// </summary>
        /// <param name="rd">数据读取器</param>
        /// <returns>博客文章分类对象</returns>
        public static BlogStoryCategory CreateFromReader(IDataReader rd)
        {
            BlogStoryCategory instance = new BlogStoryCategory();
            instance._categoryId = new Guid(rd["BlogCategoryID"].ToString());
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

            strSql.Append("INSERT INTO fbs_BlogCategory(");
            strSql.Append("BlogCategoryID, CategoryName, IconName, Description, OrderPriority)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_BlogCategoryID, @in_CategoryName, @in_IconName, @in_Description, @in_OrderPriority)");

            cmdParms.Add("@in_BlogCategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_IconName", DbType.String);
            cmdParms.Add("@in_Description", DbType.String);
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

            strSql.Append("UPDATE fbs_BlogCategory SET ");
            strSql.Append("CategoryName=@in_CategoryName,");
            strSql.Append("IconName=@in_IconName,");
            strSql.Append("Description=@in_Description,");
            strSql.Append("OrderPriority=@in_OrderPriority");
            strSql.Append(" WHERE BlogCategoryID=@in_BlogCategoryID");

            cmdParms.Add("@in_BlogCategoryID", DbType.Guid);
            cmdParms.Add("@in_CategoryName", DbType.String);
            cmdParms.Add("@in_IconName", DbType.String);
            cmdParms.Add("@in_Description", DbType.String);
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

            strSql.Append("DELETE FROM fbs_BlogCategory");
            strSql.Append(" WHERE BlogCategoryID=@in_BlogCategoryID");

            cmdParms.Add("@in_BlogCategoryID", DbType.Guid);
        }


        #endregion

        #region IEntity 成员

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
                t.Columns.Add("BlogCategoryID", typeof(Guid));
                t.Columns.Add("CategoryName", typeof(string));
                t.Columns.Add("IconName", typeof(string));
                t.Columns.Add("Description", typeof(string));
                t.Columns.Add("OrderPriority", typeof(uint));

            }

            //添加行
            System.Data.DataRow row = t.NewRow();
            row["BlogCategoryID"] = this._categoryId;
            row["CategoryName"] = this._name;
            row["IconName"] = this._icon;
            row["Description"] = this._description;
            row["OrderPriority"] = this._priority;

            t.Rows.Add(row);
        }

        #endregion

        #region 公共属性
        /// <summary>
        /// 获取分类名称
        /// </summary>
        public string CategoryName
        {
            get { return this._name; }
            set { this._name = value; }
        }

        public string IconName
        {
            get { return this._icon; }
            set { this._icon = value; }
        }

        public string Description
        {
            get { return this._description; }
            set { this._description = value; }
        }

        public uint OrderPriority
        {
            get { return this._priority; }
            set { this._priority = value; }
        }



        #endregion
    }
}
