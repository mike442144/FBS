using System.Data;
using System.Data.Common;
using System.Data.OleDb;
#if ORACLE
using System.Data.OracleClient;
#endif
using System.Data.SqlClient;
#if SQLITE
using System.Data.SQLite;
#endif
using System;
#if MYSQL
using MySql.Data.MySqlClient;
#endif

namespace FBS.DBUtility
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public class DBHelper
    {
        /// <summary>
        /// 枚举：数据库类型
        /// </summary>
        public enum DatabaseTypes
        {
            Sql, MySql, Oracle, OleDb, SQLite
        }

        private DatabaseTypes _databaseType;
        private string _connectionString;
        private IDBHelper _dbHelper;

        public DBHelper()
        { }

        public DBHelper(DatabaseTypes databaseType, string connectionString)
        {
            DatabaseType = databaseType;
            this._connectionString = connectionString;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseTypes DatabaseType
        {
            get
            {
                return _databaseType;
            }
            set
            {
                _databaseType = value;

                switch (value)
                {
                    case DatabaseTypes.OleDb:
                        _dbHelper = new OleDbHelper();
                        break;
                    case DatabaseTypes.MySql:
                        _dbHelper = new MySqlHelper();
                        break;
                    case DatabaseTypes.Oracle:
                        _dbHelper = new OracleHelper();
                        break;
                    case DatabaseTypes.SQLite:
                        //_dbHelper = new SQLiteHelper();
                        break;
                    case DatabaseTypes.Sql:
                    default:
                        _dbHelper = new SqlHelper();
                        break;
                }
            }
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        public DbConnection CreateConnection()
        {
            switch (_databaseType)
            {
                case DatabaseTypes.MySql:
#if MYSQL
                    return new MySqlConnection(_connectionString);
#else
                    throw new SystemException("DBUtility未打开MYSQL编译开关。");
#endif
                case DatabaseTypes.Oracle:
#if ORACLE
                    return new OracleConnection(_connectionString);
#else
                    throw new SystemException("DBUtility未打开ORACLE编译开关。");
#endif
                case DatabaseTypes.OleDb:
                    return new OleDbConnection(_connectionString);
                case DatabaseTypes.SQLite:
#if SQLITE
                    return new SQLiteConnection(_connectionString);
#else
                    throw new SystemException("DBUtility未打开SQLITE编译开关。");
#endif
                case DatabaseTypes.Sql:
                default:
                    return new SqlConnection(_connectionString);
            }
        }

        #region === 创造DbParameter的实例 ===

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public DbParameter CreateInDbParameter(string paraName, DbType dbType, int size, object value)
        {
            return CreateDbParameter(paraName, dbType, size, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输入DbParameter的实例
        /// </summary>
        public DbParameter CreateInDbParameter(string paraName, DbType dbType, object value)
        {
            return CreateDbParameter(paraName, dbType, 0, value, ParameterDirection.Input);
        }

        /// <summary>
        /// 创造输出DbParameter的实例
        /// </summary>        
        public DbParameter CreateOutDbParameter(string paraName, DbType dbType, int size)
        {
            return CreateDbParameter(paraName, dbType, size, null, ParameterDirection.Output);
        }

        /// <summary>
        /// 创造输出DbParameter的实例
        /// </summary>        
        public DbParameter CreateOutDbParameter(string paraName, DbType dbType)
        {
            return CreateDbParameter(paraName, dbType, 0, null, ParameterDirection.Output);
        }

        /// <summary>
        /// 创造返回DbParameter的实例
        /// </summary>        
        public DbParameter CreateReturnDbParameter(string paraName, DbType dbType, int size)
        {
            return CreateDbParameter(paraName, dbType, size, null, ParameterDirection.ReturnValue);
        }

        /// <summary>
        /// 创造返回DbParameter的实例
        /// </summary>        
        public DbParameter CreateReturnDbParameter(string paraName, DbType dbType)
        {
            return CreateDbParameter(paraName, dbType, 0, null, ParameterDirection.ReturnValue);
        }

        /// <summary>
        /// 创造DbParameter的实例
        /// </summary>
        public DbParameter CreateDbParameter(string paraName, DbType dbType, int size, object value, ParameterDirection direction)
        {
            DbParameter para;
            switch (_databaseType)
            {
                case DatabaseTypes.MySql:
#if MYSQL
                    para = new MySqlParameter();
                    break;
#else
                    throw new SystemException("DBUtility未打开MYSQL编译开关。");
#endif
                case DatabaseTypes.Oracle:
#if ORACLE
                    para = new OracleParameter();
                    break;
#else
                    throw new SystemException("DBUtility未打开ORACLE编译开关。");
#endif
                case DatabaseTypes.OleDb:
                    para = new OleDbParameter();
                    break;
                case DatabaseTypes.SQLite:
#if SQLITE
                    para = new SQLiteParameter();
                    break;
#else
                    throw new SystemException("DBUtility未打开SQLITE编译开关。");
#endif
                case DatabaseTypes.Sql:
                default:
                    para = new SqlParameter();
                    break;
            }

            para.ParameterName = paraName;

            if (size != 0)
                para.Size = size;

            para.DbType = dbType;

            if (value != null)
                para.Value = value;

            para.Direction = direction;

            return para;
        }

        #endregion

        #region === 数据库执行方法 ===

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="tblName">表名</param>
        /// <param name="fldName">字段名</param>
        /// <param name="fldSort">排序字段</param>
        /// <param name="fldDir">升序{False}/降序(True)</param>
        /// <param name="condition">条件(不需要where)</param>
        /// <param name="first">从第几条数据开始（从1开始）</param>
        /// <param name="last">到第几条结束</param>
        public DbDataReader GetPageList(string tblName, string fldSort, string condition, int first, int last)
        {
            return _dbHelper.GetPageList(_connectionString, tblName, fldSort, condition, first, last);
        }

        /// <summary>
        /// 执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _dbHelper.ExecuteNonQuery(_connectionString, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 在事务中执行 Transact-SQL 语句并返回受影响的行数。
        /// </summary>
        public int ExecuteNonQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _dbHelper.ExecuteNonQuery(trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 在事务中执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _dbHelper.ExecuteQuery(trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行查询，返回DataSet
        /// </summary>
        public DataSet ExecuteQuery(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _dbHelper.ExecuteQuery(_connectionString, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 在事务中执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _dbHelper.ExecuteReader(trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行查询，返回DataReader
        /// </summary>
        public DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _dbHelper.ExecuteReader(_connectionString, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 在事务中执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(DbTransaction trans, CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _dbHelper.ExecuteScalar(trans, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        public object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] cmdParms)
        {
            return _dbHelper.ExecuteScalar(_connectionString, cmdType, cmdText, cmdParms);
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <param name="sql">查询字符串</param>
        /// <returns></returns>
        public int ExecuteNonQuery( string sql)
        {
            using (SqlConnection conn = new SqlConnection(this._connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;
                int val = 0;
                SqlDataAdapter sda= new SqlDataAdapter(cmd);
                DataSet dsCount = new DataSet();
                sda.Fill(dsCount);
                val = System.Int16.Parse(dsCount.Tables[0].Rows[0][0].ToString());
                conn.Close();
                return val;
            }
        }

        #endregion
    }
}
