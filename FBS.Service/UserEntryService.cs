using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;
using FBS.Domain.Specifications;
using FBS.Utils;
using FBS.Service.ActionModels;
using System.Web.Security;
using FBS.Domain.Log;
using System.Web;
using System.Security.Principal;
using FBS.Domain.Security;

namespace FBS.Service
{
    /// <summary>
    /// 用户标识
    /// </summary>
    public class UserIdentity:IIdentity
    {
        /// <summary>
        /// 新建实例
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="isAuth">是否验证</param>
        /// <param name="name">用户ID</param>
        public UserIdentity(string type,bool isAuth,string name)
        {
            this._type = type;
            this._isauth = isAuth;
            this._name = name;
        }

        #region IIdentity 成员

        public string AuthenticationType
        {
            get { return this._type; }
        }

        public bool IsAuthenticated
        {
            get { return this._isauth; }
        }

        public string Name
        {
            get { return this._name; }
        }

        #endregion

        string _type;
        string _name;
        bool _isauth;
    }

    [Logging()]
    public class UserEntryService:ContextBoundObject
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="name">账户名</param>
        /// <param name="psd">账户密码</param>
        /// <returns>Account</returns>
        public Account Logon(UserLogOnModel model)
        {
            Account user=null;
            //创建账户仓储
            IRepository<Account> accountRep = FBS.Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            ISpecification<Account> namespec;
            
            if (string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.UserName))
            {
                //昵称登录
                namespec = new Specification<Account>(o => o.UserName == model.UserName);
            }
            else if (string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Email))
            {
                //邮箱登录
                namespec = new Specification<Account>(o => o.Email == model.Email);//查询条件
            }
            else
                throw new NullReferenceException("用户登录时，用户名和邮箱至少使用一个");

            if (accountRep.Exists(namespec))//这个账户是否存在
            {
                user = accountRep.Find(namespec);
                if (!user.CheckPsd(model.Password))
                {
                    throw new LogonException("密码错误");//账户存在，密码错误
                }
                else
                {

                    if (new UserEntryService().CheckForbidden(user.Id))
                    {
                        throw new LogonException("您由于不遵守相关规定，账户被禁用");//您由于不遵守相关规定，账户被禁用
                    }
                    //将Identify更新到HttpContext中
                    UserIdentity u = new UserIdentity("Forms", true, user.Id.ToString());
                    /*UserInfoService uis=new UserInfoService();
                    string[] roles=uis.GetUserRoles(user.Id);*/
                    string[] roles = user.Roles.Split('|');
                    if(roles==null)
                        roles=new string[1]{string.Empty};

                    System.Security.Principal.GenericPrincipal gp = new System.Security.Principal.GenericPrincipal(u, roles);
                    HttpContext.Current.User = gp;
                    
                    //添加ticket到cookie
                    FormsAuthenticationTicket ticket = AuthenticationHelper.CreateAuthenticationTicket(user.Id.ToString(), user.UserName, model.RememberMe);
                    AuthenticationHelper.SetAuthenticalCookie(ticket);
                }
            }
            else
            {
                throw new LogonException("账户不存在");//账户不存在
            }
            return user;
        }

        /// <summary>
        /// 新用户注册
        /// </summary>
        /// <param name="newUser">待注册的新帐号</param>
        /// <returns>注册是否成功</returns>
        public Account Register(UserRegisterModel newUser)
        {
            Account account = null;
            //创建账户仓储
            IRepository<Account> accountRep = FBS.Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            
            

            if (accountRep.Exists(new Specification<Account>(a => a.Email == newUser.Email)))
                throw new RegisterException("该邮箱已经被注册");
            if (string.IsNullOrEmpty(newUser.UserName)&&string.IsNullOrEmpty(newUser.RoleName))
                account = new Account(newUser.Email, newUser.Password);
            else
                account = new Account(newUser.Email, newUser.Password, newUser.UserName, newUser.RoleName);
            accountRep.Add(account);//添加新账户
            accountRep.PersistAll();

            //添加ticket到cookie
            FormsAuthenticationTicket ticket = AuthenticationHelper.CreateAuthenticationTicket(account.Id.ToString(), account.UserName, true);
            AuthenticationHelper.SetAuthenticalCookie(ticket);

            this.InitUserProfile(account.Id);
            this.InitUserProperty(account.Id);

            return account;
        }
        /// <summary>
        /// 检测邮箱是否注册
        /// </summary>
        /// <param name="model">用户注册模型</param>
        /// <returns></returns>
        public bool IsRegiste(UserRegisterModel model)
        {
            IRepository<Account> accountRep = FBS.Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            if (accountRep.Exists(new Specification<Account>(a => a.Email == model.Email)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 初始化用户资料
        /// </summary>
        /// <param name="uid"></param>
        private void InitUserProfile(Guid uid)
        {
            IRepository<UserProfile> profileRep = Factory.Factory<IRepository<UserProfile>>.GetConcrete<UserProfile>(); 
            
            //初始化用户资料
            UserProfile userProfile = new UserProfile(uid);

            //保存用户资料
            profileRep.Add(userProfile);
            profileRep.PersistAll();
        }

        /// <summary>
        /// 初始化用户设置
        /// </summary>
        /// <param name="uid"></param>
        private void InitUserProperty(Guid uid)
        {
            IRepository<UserProperty> usrProRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();

            //初始化用户默认设置
            UserProperty userProperty = new UserProperty(uid, "default", string.Empty, "personal", string.Empty);


            //保存用户设置
            usrProRep.Add(userProperty);
            usrProRep.PersistAll();
        }


        #region 后台管理员管理用户
        /// <summary>
        /// 禁用账户通过用户模型
        /// </summary>
        /// <param name="model">用户模型</param>
        /// <returns></returns>
        public bool ForbiddenAccountByAccount(ForbiddenAccountDspModel model)
        {
            IRepository<ForbiddenAccount> accRep = Factory.Factory<IRepository<ForbiddenAccount>>.GetConcrete<ForbiddenAccount>();
            ForbiddenAccount ak = accRep.Find(new Specification<ForbiddenAccount>(uf => uf.AccountID == model.AccountID));
            if (ak == null)
            {
                ForbiddenAccount fac = new ForbiddenAccount(model.AccountID, model.UserName, model.IP, model.ForbiddenTime, model.RefreshTime, model.State, model.ForbiddenType);
                accRep.Add(fac);
                accRep.PersistAll();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 激活用户通过用户模型
        /// </summary>
        /// <param name="model">用户模型</param>
        public void RefreshAccountByAccount(ForbiddenAccountDspModel model)
        {
            IRepository<ForbiddenAccount> accRep = Factory.Factory<IRepository<ForbiddenAccount>>.GetConcrete<ForbiddenAccount>();
            ForbiddenAccount fac = new ForbiddenAccount() { AccountID = model.AccountID, ForbiddenTime = model.ForbiddenTime, ForbiddenType = model.ForbiddenType, IP = model.IP, RefreshTime = model.RefreshTime, State = model.State, UserName = model.UserName };
            accRep.Update(fac);
            accRep.PersistAll();
        }
        /// <summary>
        /// 获取禁用列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IList<ForbiddenAccountDspModel> FetchForbiddenAccountDspModel(int index, int count)
        {
            IRepository<ForbiddenAccount> accRep = Factory.Factory<IRepository<ForbiddenAccount>>.GetConcrete<ForbiddenAccount>();
            IList<ForbiddenAccount> list = accRep.FindAll(new Specification<ForbiddenAccount>(uf => uf.ForbiddenId != Guid.Empty).Skip(index).Take(count).OrderByDescending(u => u.Id));

            IList<ForbiddenAccountDspModel> mylist = new List<ForbiddenAccountDspModel>();
            foreach (ForbiddenAccount ac in list)
            {
                ForbiddenAccountDspModel model = new ForbiddenAccountDspModel() { AccountID = ac.AccountID, ForbiddenID = ac.ForbiddenId, UserName = ac.UserName, State = ac.State, RefreshTime = ac.RefreshTime, ForbiddenTime = ac.ForbiddenTime, IP = ac.IP, ForbiddenType = ac.ForbiddenType };
                mylist.Add(model);
            }
            return mylist;
        }
        /// <summary>
        /// 检查是否被禁用
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns></returns>
        public bool CheckForbidden(Guid uid)
        {
            IRepository<ForbiddenAccount> accRep = Factory.Factory<IRepository<ForbiddenAccount>>.GetConcrete<ForbiddenAccount>();
            ForbiddenAccount ak = accRep.Find(new Specification<ForbiddenAccount>(uf => uf.AccountID == uid));
            if (ak != null) return true;
            else return false;
        }
        /// <summary>
        /// 判断是管理员
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsAdmin(Guid id)
        {
            bool flag = false;

            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account acc = null;
            acc = accRep.GetByKey(id);
            if (acc.Roles == "Admin")
            {
                flag = true;

            }
            return flag;

        }

        /// <summary>
        /// 获取管理员列表
        /// </summary>
        /// <returns></returns>
        public IList<AdminAccountDspModel> GetAdminList()
        {
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            IList<Account> list = aRep.FindAll(new Specification<Account>(s => s.Roles != "Member"));
            IList<AdminAccountDspModel> klist = new List<AdminAccountDspModel>();
            foreach (Account ac in list)
            {
                AdminAccountDspModel model = new AdminAccountDspModel() { Email = ac.Email, AccountID = ac.Id, Role = ac.Roles, UserName = ac.UserName };
                klist.Add(model);
            }
            return klist;
        }

        /// <summary>
        /// 给管理员设置权限
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="role"></param>
        public void SetAdminRole(Guid UserID, string role)
        {
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account acc = null;
            acc = accRep.GetByKey(UserID);
            acc.Roles = role;
            accRep.Update(acc);
            accRep.PersistAll();
        }

        /// <summary>
        /// 根据邮箱设置管理员权限
        /// </summary>
        /// <param name="email"></param>
        /// <param name="role"></param>
        public void SetAdminRole(string email, string role)
        {
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account acc = null;
            acc = accRep.Find(new Specification<Account>(s => s.Email == email));
            acc.Roles = role;
            accRep.Update(acc);
            accRep.PersistAll();
        }

        #endregion

        
    }
}
