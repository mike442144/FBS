using System;
using FBS.Domain.QueryObject;

namespace FBS.Repository
{
    public class AviatorDb<TEntity>:DBUtility.DALHelper
    {
        public AviatorDb()
        {
            DbQueryProvider provider = new DbQueryProvider(DataHelper.CreateConnection());
            this.TEntitys = new Query<TEntity>(provider);
        }
        public Query<TEntity> TEntitys;
    }
}
