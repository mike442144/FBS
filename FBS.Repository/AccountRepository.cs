using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Specifications;
using FBS.Domain.Aggregate.Entity;
using System.Collections;
using FBS.Domain.Entity;
using FBS.DBUtility;
using FBS.Repository.Persistence;
using System.Data;

namespace FBS.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private bool _isLoaded = false;
        private string _cacheAccountsKey = "FBS.Accounts";
        //private MemcachedClient _memdb = null;
        /// <summary>
        /// 所有用户集合
        /// </summary>
        private HashSet<Account> _forumAccounts=null;

        /// <summary>
        /// 新注册的用户
        /// </summary>
        private HashSet<Account> _newAccounts = null;
        private HashSet<Account> _removedAccounts = null;
        private HashSet<Account> _modifiedAccounts = null;


        public AccountRepository()
        {
            //初始化属性
            this._forumAccounts = new HashSet<Account>();
            this._newAccounts = new HashSet<Account>();
            this._removedAccounts = new HashSet<Account>();
            this._modifiedAccounts = new HashSet<Account>();

            //加载数据
            this._forumAccounts = AccountPersist.GetAllAccounts();
        }


        #region IRepository<Account> 成员

        /// <summary>
        /// 持久化所有
        /// </summary>
        public void PersistAll()
        {
            DataTable newAccountTable = new DataTable("newAccountTable");

            foreach (Account a in this._newAccounts)
                a.AlterToRow(newAccountTable);
            
            AccountPersist.PersistAll(newAccountTable);
            this._newAccounts.Clear();
            //其它更改提交
        }

        /// <summary>
        /// 添加新账户
        /// </summary>
        /// <param name="entity">新账户</param>
        public void Add(Account entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Account");
            this._forumAccounts.Add(entity);
            this._newAccounts.Add(entity);
        }

        /// <summary>
        /// 检查是否存在指定规则的账户
        /// </summary>
        /// <param name="specification">规则</param>
        /// <returns></returns>
        public bool Exists(ISpecification<Account> specification)
        {
            return this._forumAccounts.Any(specification.GetExpression().Compile());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public Account Find(ISpecification<Account> specification)
        {
            Account tmp = null;
            try
            {
                tmp = this._forumAccounts.SingleOrDefault(specification.GetExpression().Compile());
            }
            catch (InvalidOperationException error)
            {
            }

            return tmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account GetByKey(Guid id)
        {
            Account tmp = null;
            try
            {
                tmp = this._forumAccounts.Single(a => a.Id == id);
            }
            catch (InvalidOperationException)
            {
            }

            return tmp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Account> FindAll()
        {
            return this._forumAccounts.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public IList<Account> FindAll(ISpecification<Account> specification)
        {
            return this._forumAccounts.TakeWhile(specification.GetExpression().Compile()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(Account entity)
        {
            this._forumAccounts.Remove(entity);
            this._removedAccounts.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Account entity)
        {
            throw new NotImplementedException("update");
        }

        public void RemoveByKey(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Count(ISpecification<Account> specification)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
