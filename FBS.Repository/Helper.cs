using System;
using System.Linq;
using FBS.Domain.Repository;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using FBS.DBUtility;

namespace FBS.Repository
{
    public class Helper:DBUtility.DALHelper,IHelper
    {
        public void ExecCommand(string sql)
        {
   
        }
        public void ExecScriptFile(string sqlScript)
        {
            using (var conn = DataHelper.CreateConnection())
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                var trans = conn.BeginTransaction();
                try
                
                {
                    var cmd = conn.CreateCommand();
                    cmd.Transaction = trans;
                    var correct = sqlScript.Split(new string[] { "GO" }, StringSplitOptions.None).All(item =>
                    {
                        cmd.CommandText = item;
                        cmd.ExecuteNonQuery();
                        return true;
                    });
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }
    }
}
