using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Repository.Persistence
{
    class SitePersist : DBUtility.DALHelper
    {
        static DbConnection conn = null;
        static DbTransaction trans = null;

        public static void PersistAll(DataTable t, DbOperation op)
        {
            using (conn = DataHelper.CreateConnection())
            {
                conn.Open();
                trans = conn.BeginTransaction();
                foreach (DataRow row in t.Rows)
                    Persist(row, op);
                trans.Commit();
                conn.Close();
            }
            t.Clear();
            t = null;
        }

        private static void Persist(DataRow r, DbOperation op)
        {
            //查询语句
            StringBuilder strSql = new StringBuilder();

            //属性与类型字典
            Dictionary<string, DbType> dic = new Dictionary<string, DbType>();

            //命令参数列表
            DbParameter[] cmdParms = null;

            //反射获得函数
            Type type = typeof(Site);
            System.Reflection.MethodInfo method = null;

            switch (op)
            {
                case DbOperation.Insert: method = type.GetMethod("PrepareAddCommand"); break;
                case DbOperation.Update: method = type.GetMethod("PrepareUpdateCommand"); break;
                case DbOperation.Delete: method = type.GetMethod("PrepareDeleteCommand"); break;
                default: break;
            }


            //准备参数
            object[] parms = new object[2];
            parms[0] = strSql;
            parms[1] = dic;

            //调用函数
            method.Invoke(null, parms);

            //生成数据库执行参数
            cmdParms = new DbParameter[dic.Count];
            int i = -1;
            foreach (string name in dic.Keys)
                cmdParms[++i] = DataHelper.CreateInDbParameter(name, dic[name], r[i]);

            //执行数据库命令
            DataHelper.ExecuteNonQuery(trans, CommandType.Text, strSql.ToString(), cmdParms);

            method = null;
        }
    }
}
