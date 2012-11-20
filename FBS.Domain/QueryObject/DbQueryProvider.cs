using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Reflection;
using System.Linq.Expressions;
using System.Data;

namespace FBS.Domain.QueryObject
{
    public class DbQueryProvider:QueryProvider
    {
        DbConnection connection;

        public DbQueryProvider(DbConnection connection)
        {
            this.connection = connection;
        }

        public override string GetQueryText(Expression expression)
        {
            return this.Translate(expression);
        }

        public override object Execute(Expression expression)
        {
            DbCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = this.Translate(expression);
            if(cmd.Connection.State!=ConnectionState.Open) cmd.Connection.Open();
            DbDataReader reader = cmd.ExecuteReader();
            Type elementType = TypeSystem.GetElementType(expression.Type);
            if (elementType.Equals(typeof(int)))
            {
                int c = 0;
                if (reader.Read())
                    c=reader.GetInt32(0);
                reader.Close();
                reader.Dispose();
                return c;
            }
            else
            {
                return Activator.CreateInstance(
                    typeof(ObjectReader<>).MakeGenericType(elementType),
                    new object[] { reader });
            }
        }

        private string Translate(Expression expression)
        {
            expression = Evaluator.PartialEval(expression);
            return new QueryTranslator().Translate(expression);
        }
    }
}
