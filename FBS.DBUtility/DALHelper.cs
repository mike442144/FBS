using System;
using System.Collections.Generic;
using System.Text;
using FBS.DBUtility;
using System.Configuration;

namespace FBS.DBUtility
{
    public class DALHelper
    {
        protected static DBHelper DataHelper = GetHelper("sqlstrconn");
        /// <summary>
        /// 从Web.config从读取数据库的连接以及数据库类型
        /// </summary>
        private static DBHelper GetHelper(string connectionStringName)
        {
            DBHelper dbHelper = new DBHelper();

            // 从Web.config中读取数据库类型
            string providerName = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ProviderName;
            switch (providerName)
            {
                case "System.Data.OracleClient":
                    dbHelper.DatabaseType = DBHelper.DatabaseTypes.Oracle;
                    break;
                case "MySql.Data.MySqlClient":
                    dbHelper.DatabaseType = DBHelper.DatabaseTypes.MySql;
                    break;
                case "System.Data.OleDb":
                    dbHelper.DatabaseType = DBHelper.DatabaseTypes.OleDb;
                    break;
                case "System.Data.SqlClient":
                default:
                    dbHelper.DatabaseType = DBHelper.DatabaseTypes.Sql;
                    break;
            }

            // 从Web.config中读取数据库连接
            switch (dbHelper.DatabaseType)
            {
                default:
                    dbHelper.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
                    break;
            }

            return dbHelper;
        }
    }
}
