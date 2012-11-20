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
   public  class Album:IAggregateRoot
   {
       #region 构造函数

       public Album(Guid uid,string uname,string photoname,string description,DateTime time)
       {
           this._id = Guid.NewGuid();
           this._accountMessageVO = new AccountMessageVO( uid, uname );
           this._photoname = photoname;
           this._description = description;
           this._time = time;
          
 
       }

       private Album() { }
       #endregion

       #region 从持久化创建

       /// <summary>
       /// 从数据读取器创建
       /// </summary>
       /// <param name="rd">数据读取器</param>
       /// <returns>文章实例</returns>
       public static Album CreateFromReader(IDataReader rd)
       {
           Album a = new Album();

           a._id = new Guid(rd["AlbumID"].ToString());
           a._accountMessageVO = new AccountMessageVO( new Guid(rd["UserID"].ToString()), rd["UserName"].ToString());
           a._photoname = rd["PhotoName"].ToString();
           a._description = rd["Description"].ToString();
           a._time = Convert.ToDateTime(rd["Time"].ToString());
         
          

           return a;
       }

       #endregion

       #region IEntity 成员

       /// <summary>
       /// 照片编号
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
               t.Columns.Add("AlbumID", typeof(Guid));
               t.Columns.Add("UserID", typeof(Guid));
               t.Columns.Add("UserName", typeof(string));
               t.Columns.Add("PhotoName", typeof(string));
               t.Columns.Add("Description", typeof(string));
               t.Columns.Add("Time", typeof(DateTime));
          
           }

           DataRow row = t.NewRow();
           row["AlbumID"] = this._id;
           row["UserID"] = this._accountMessageVO.Id;
           row["UserName"] = this._accountMessageVO.UserName;
           row["PhotoName"] = HttpUtility.HtmlEncode(this._photoname);
           row["Description"] = HttpUtility.HtmlEncode(this._description);
           row["Time"] = this._time;
         
           t.Rows.Add(row);
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


           strSql.Append("INSERT INTO fbs_Album(");
           strSql.Append("AlbumID, UserID, UserName, PhotoName, Description,Time)");
           strSql.Append(" VALUES (");
           strSql.Append("@in_AlbumID, @in_UserID, @in_UserName, @in_PhotoName, @in_Description,@in_Time)");

           cmdParms.Add("@in_AlbumID", DbType.Guid);
           cmdParms.Add("@in_UserID", DbType.Guid);
           cmdParms.Add("@in_UserName", DbType.String);
           cmdParms.Add("@in_PhotoName", DbType.String);
           cmdParms.Add("@in_Description", DbType.String);
           cmdParms.Add("@in_Time", DbType.DateTime);
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

           strSql.Append("UPDATE fbs_Album SET ");
           strSql.Append("UserID=@in_UserID,");
           strSql.Append("UserName=@in_UserName,");
           strSql.Append("PhotoName=@in_PhotoName,");
           strSql.Append("Description=@in_Description,");
           strSql.Append("Time=@in_Time");
           strSql.Append(" WHERE AlbumID=@in_AlbumID");


           cmdParms.Add("@in_AlbumID", DbType.Guid);
           cmdParms.Add("@in_UserID", DbType.Guid);
           cmdParms.Add("@in_UserName", DbType.String);
           cmdParms.Add("@in_PhotoName", DbType.String);
           cmdParms.Add("@in_Description", DbType.String);
           cmdParms.Add("@in_Time", DbType.DateTime);
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

           strSql.Append("DELETE FROM fbs_Album");
           strSql.Append(" WHERE AlbumID=@in_AlbumID");

           cmdParms.Add("@in_AlbumID", DbType.Guid);
       }

       #endregion
       #region 属性

       private Guid _id;



       private AccountMessageVO _accountMessageVO;
       public Guid UserID
       {
           get { return this._accountMessageVO.Id; }
       }

       public string UserName
       {
           get { return this._accountMessageVO.UserName; }
       }

       private string _photoname;
       public string PhotoName
       {
           set { this._photoname = value; }
           get { return this._photoname; }
       }

       private string _description;

       public string Description
       {
           get { return this._description; }
           set { this._description = value; }
       }

       private DateTime _time;
       public DateTime Time
       {
           get { return this._time; }
           set { this._time = value; }
       }
       #endregion
   }
}
