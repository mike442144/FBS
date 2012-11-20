using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using FBS.Domain.Aggregate.Entity;
using FBS.Utils;

namespace FBS.Repository.Persistence
{
    internal class AccountPersist:DBUtility.DALHelper
    {
        public static HashSet<Account> GetAllAccounts()
        {
            HashSet<Account> accounts = new HashSet<Account>();
            string sql = "select * from fbs_Account";

            using (IDataReader reader = DataHelper.ExecuteReader(CommandType.Text, sql))
            {
                while (reader.Read())
                    accounts.Add(Account.CreateFromReader(reader));
            }

            return accounts;
        }

        public static void PersistAll(DataTable t)
        {
            foreach (DataRow row in t.Rows)
            {
                Persist(row);
            }
        }

        /// <summary>
        /// 持久化单个实体
        /// </summary>
        /// <param name="row">数据行</param>
        private static void Persist(DataRow row)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("INSERT INTO fbs_Account(");
            strSql.Append("AccountID,Email,Name,Role,Salt,HashPsd,Points)");
            strSql.Append(" VALUES (");
            strSql.Append("@in_ID,@in_Email,@in_Name,@in_Role,@in_Salt,@in_HashPsd,@in_Points)");
            DbParameter[] cmdParms = new DbParameter[]{
                DataHelper.CreateInDbParameter("@in_ID", DbType.Guid, row["ID"]),
                DataHelper.CreateInDbParameter("@in_Email", DbType.String, row["Email"]),
                DataHelper.CreateInDbParameter("@in_Name", DbType.String, row["Name"]),
                DataHelper.CreateInDbParameter("@in_Role", DbType.String, row["Role"]),
                DataHelper.CreateInDbParameter("@in_Salt", DbType.Binary,row["Salt"]),
                DataHelper.CreateInDbParameter("@in_HashPsd", DbType.Binary,row["HashPsd"]),
                DataHelper.CreateInDbParameter("@in_Points", DbType.Int32,row["Points"])
            };

            DataHelper.ExecuteNonQuery(CommandType.Text, strSql.ToString(), cmdParms);
        }

    }
}
