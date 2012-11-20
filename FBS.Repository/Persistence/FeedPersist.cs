using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using System.Data.Common;
using System.Data;

namespace FBS.Repository.Persistence
{
    internal class FeedPersist:DBUtility.DALHelper
    {
        public static IList<Feed> FetchFeeds(Guid uid,int startIndex,int count)
        {
            IList<Feed> list = new List<Feed>();
            DbParameter[] parms=new DbParameter[]{
                DataHelper.CreateInDbParameter("@id",DbType.Guid,uid),
                DataHelper.CreateInDbParameter("@startIndex",DbType.Int32,startIndex),
                DataHelper.CreateInDbParameter("@count",DbType.Int32,count)
        };

            using (DbDataReader rd = DataHelper.ExecuteReader(CommandType.StoredProcedure,"GetFriendFeedsByUserID",parms ))
            {
                while (rd.Read())
                    list.Add(Feed.CreateFromReader(rd));
            }

            return list;
        }

        public static IList<Feed> FetchFeeds(Guid uid, int startIndex, int count,string type)
        {
            IList<Feed> list = new List<Feed>();
            DbParameter[] parms = new DbParameter[]{
                DataHelper.CreateInDbParameter("@id",DbType.Guid,uid),
                DataHelper.CreateInDbParameter("@startIndex",DbType.Int32,startIndex),
                DataHelper.CreateInDbParameter("@count",DbType.Int32,count),
                DataHelper.CreateInDbParameter("@Category",DbType.String,type)
        };

            using (DbDataReader rd = DataHelper.ExecuteReader(CommandType.StoredProcedure, "GetFriendFeedsByUserIDANDCategory", parms))
            {
                while (rd.Read())
                    list.Add(Feed.CreateFromReader(rd));
            }

            return list;
        }

        public static int GetFriendsFeedsCountByUserID(Guid uid, string type)
        {
            int count = 0;
            DbParameter[] parms = new DbParameter[]{
                DataHelper.CreateInDbParameter("@id",DbType.Guid,uid),
                DataHelper.CreateInDbParameter("@Category",DbType.String,type)
        };

            using (DbDataReader rd = DataHelper.ExecuteReader(CommandType.StoredProcedure, "GetFriendFeedsCountByUserIDANDCategory", parms))
            {
                while (rd.Read())
                {
                  count=System.Int16.Parse(rd[0].ToString());
                }
            }

            return count;
        }
    }
}
