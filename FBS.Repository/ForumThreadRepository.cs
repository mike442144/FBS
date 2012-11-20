using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Entity;
using FBS.Domain.Specifications;
using FBS.DBUtility;
using System.Collections;
using FBS.Repository.Persistence;
using System.Data;

namespace FBS.Repository
{
    public class ForumThreadRepository:IForumThreadRepository
    {
        private HashSet<ForumThread> _forumThread=null;
        private HashSet<ForumThread> _newThreads = null;
        private HashSet<ForumThread> _removedThreads = null;
        private HashSet<ForumThread> _modifiedThreads = null;

        ForumThreadBuilder _messageRepository = null;

        //private MemcachedClient _memdb;
        private bool _isLoaded=false;
        private string _cacheThreadListKey="FBS.ForumThread";
        private IList<Guid> _threadKeys;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ForumThreadRepository()
        {
            //this._forumThread = new HashSet<ForumThread>();
            //this._newThreads = new HashSet<ForumThread>();
            //this._removedThreads = new HashSet<ForumThread>();
            //this._modifiedThreads = new HashSet<ForumThread>();

            //this._forumThread = ForumThreadPersist.GetAllForumThread();

            #region memcached 操作
            //if (_memdb == null)
            //{
            //    string[] serverlist = new string[] { "192.168.1.119:11211" };
            //    string poolName = "MemcacheIOPool";
            //    SockIOPool pool = SockIOPool.GetInstance(poolName);
            //    //设置连接池的初始容量，最小容量，最大容量，Socket 读取超时时间，Socket连接超时时间
            //    pool.SetServers(serverlist);
            //    pool.InitConnections = 1;
            //    pool.MinConnections = 1;
            //    pool.MaxConnections = 500;
            //    pool.SocketConnectTimeout = 1000;
            //    pool.SocketTimeout = 3000;
            //    pool.MaintenanceSleep = 30;
            //    pool.Failover = true;
            //    pool.Nagle = false;
            //    pool.Initialize();//容器初始化
            //    this._memdb = new MemcachedClient();
            //    this._memdb.PoolName = poolName;
            //    this._memdb.EnableCompression = false;
            //}
            

            //if (this._threadKeys == null)
            //    this._threadKeys = new List<Guid>();
            //this.LoadFromMemdb();
            #endregion
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~ForumThreadRepository()
        {
            //在仓储生命结束时把内存中的更改持久化到数据库
            //this.PersistAll();
            
            #region memcached操作
            //if (this._isLoaded)
            //    if (this._memdb.KeyExists(this._cacheThreadListKey))
            //        this._memdb.Replace(this._cacheThreadListKey, this._forumThread);
            //    else
            //        this._memdb.Add(this._cacheThreadListKey, this._forumThread, DateTime.Now.AddYears(1));
            #endregion

        }

        /// <summary>
        /// 数据存入memcached
        /// </summary>
        private void StoreInMemdb()
        {
            foreach (ForumThread thread in this._forumThread)
            {

            }
        }

        /// <summary>
        /// 从memcached加载数据
        /// </summary>
        private void LoadFromMemdb()
        {
            //string[] ids = new string[this._forumThread.Count];

            //if (this._forumThread == null)
            //{

            //    int i = -1;
            //    foreach (ForumThread thread in this._forumThread)
            //    {
            //        ids[++i] = thread.Id.ToString();
            //    }
            //}
            //Hashtable ht = new Hashtable();
            //ht = this._memdb.GetMultiple(ids);
            //IList<ForumThread> list = (IList<ForumThread>)ht.Values;
            //if(!this._isLoaded&&this._memdb.KeyExists(this._cacheThreadListKey))
            //    this._forumThread=(IList<ForumThread>)this._memdb.Get(this._cacheThreadListKey);

        }

        #region IRepository<ForumThread> 成员

        /// <summary>
        /// 持久化所有.....还有部分未实现
        /// </summary>
        public void PersistAll()
        {
            #region 转换成DataTable
            //新添加元素
            DataTable newThreadsTable=new DataTable("NewForumThreads");
            DataTable newMessagesTable = new DataTable("NewMessages");

            //修改过的元素
            DataTable modifiedThreadsTable = new DataTable("ModifiedForumThreads");
            

            //被删除的元素
            DataTable removedThreadsTable = new DataTable("RemovedForumThreads");


            foreach (ForumThread th in this._newThreads)
            {
                th.AlterToRow(newThreadsTable);
                th.RootMessage.AlterToRow(newMessagesTable);
            }
            
            foreach (ForumThread th in this._modifiedThreads)
                th.AlterToRow(modifiedThreadsTable);

            foreach (ForumThread th in this._removedThreads)
                th.AlterToRow(removedThreadsTable);
            
            

            #endregion

            #region 持久化
            
            //添加
            if(newThreadsTable.Rows.Count!=0)
                ForumThreadPersist.AddAll(newThreadsTable);

            if(newMessagesTable.Rows.Count!=0)
                ForumMessagePersist.AddAll(newMessagesTable);
            
            //更新
            if(modifiedThreadsTable.Rows.Count!=0)
                ForumThreadPersist.UpdateAll(modifiedThreadsTable);

            //删除
            if(removedThreadsTable.Rows.Count!=0)
                ForumThreadPersist.RemoveAll(removedThreadsTable);

            
            #endregion

            #region 释放内存

            this._newThreads.Clear();
            this._removedThreads.Clear();
            this._modifiedThreads.Clear();
            #endregion
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Add(ForumThread entity)
        {
            this._forumThread.Add(entity);//在数组中添加实体
            this._newThreads.Add(entity);
            //this._memdb.Add(this._cacheThreadListKey+":"+entity.Id.ToString(), entity);//放入内存
            
            //this._threadKeys.Add(entity.Id);//Guid数组
            //this._memdb.Replace(this._cacheThreadListKey, this._threadKeys);//Guid数组替换进缓存

            //TODO
            //持久化到数据库
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="specification">规则</param>
        /// <returns>是否存在</returns>
        public bool Exists(ISpecification<ForumThread> specification)
        {
            try
            {
                return this._forumThread.Any(specification.GetExpression().Compile());
            }
            catch (NullReferenceException exp)
            {
                throw new ArgumentNullException("specification",exp);
            }
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="specification">规则</param>
        /// <returns>帖子</returns>
        public ForumThread Find(ISpecification<ForumThread> specification)
        {
            return this._forumThread.Single(specification.GetExpression().Compile());
        }

        /// <summary>
        /// 获取帖子
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns>帖子</returns>
        public ForumThread GetByKey(Guid id)
        {
            ForumThread tmp = null;
            try
            {
                var list= ForumThreadPersist.GetThread("select * from fbs_ForumThread inner join fbs_Message on fbs_ForumThread.RootMessageID=fbs_Message.MessageID where fbs_ForumThread.ThreadID='" + id.ToString() + "'");
                if (list != null && list.Count == 1)
                    tmp = list.SingleOrDefault();
            }
            catch (InvalidOperationException error)
            { }
            return tmp;
        }

        /// <summary>
        /// 获取所有帖子
        /// </summary>
        /// <returns>帖子集合</returns>
        public IList<ForumThread> FindAll()
        {
            return this._forumThread.ToList();
        }

        /// <summary>
        /// 获取满足一定条件的帖子
        /// </summary>
        /// <param name="specification">规则</param>
        /// <returns>帖子集合</returns>
        public IList<ForumThread> FindAll(ISpecification<ForumThread> specification)
        {
            return this._forumThread.TakeWhile(specification.GetExpression().Compile()).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        public void Remove(ForumThread entity)
        {
            this._forumThread.Remove(entity);
            this._removedThreads.Add(entity);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体</param>
        public void Update(ForumThread entity)
        {

            //更新原有集合的实体
            this._forumThread.Remove(entity);
            this._forumThread.Add(entity);

            //添加到已经修改过的集合
            this._modifiedThreads.Add(entity);
        }

        /// <summary>
        /// 获取论坛下的帖子
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<ForumThread> GetThreadsInOneForum(Guid forumId, int startIndex, int count)
        {
            //return this._forumThread.Where(t => t.ForumID == forumId).Skip(startIndex).Take(count).OrderByDescending(f => f.ModifiedDate).ToList();
            return ForumThreadPersist.GetAllForumThread(forumId, startIndex, count).ToList();

        }
        /// <summary>
        /// 不分页获取所有的贴子，按时间倒序
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        public IList<ForumThread> GetAllThreadsInOneForum(Guid forumId)
        {
            return this._forumThread.Where(t => t.ForumID == forumId).OrderByDescending(f=>f.CreationDate).ToList();
        }

        public IList<ForumThread> GetThread(string cmdText)
        {
            return ForumThreadPersist.GetThread(cmdText).ToList();
        }

        public IList<ForumThread> FetchTopThreadsInOneForum(Guid forumId, int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        public IList<ForumThread> FetchAllTopThreads(int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        public IList<ForumThread> FetchAllEssence()
        {
            throw new NotImplementedException();
        }

        public IList<ForumThread> FetchEssenceInOneForum(Guid forumId, int startIndex, int count)
        {
            throw new NotImplementedException();
        }

        public void RemoveByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Count(ISpecification<ForumThread> specification)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
