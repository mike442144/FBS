using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;
using FBS.Utils;
using System.Web;
using System.Threading;
using System.Web.Security;
using FBS.Domain.Aggregate.ValueObject;

namespace FBS.Service
{
    public class UserInfoService
    {
        #region 获取博客好友、建立好友关系、解除好友关系、申请加为好友、接受好友请求

        /// <summary>
        /// 获取用户好友列表
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>好友列表</returns>
        public IList<FriendModel> FetchFriendsByUserID(Guid uid,int startIndex,int count)
        {
            IRepository<UserFriend> accountRep = Factory.Factory<IRepository<UserFriend>>.GetConcrete<UserFriend>();
            IList<UserFriend> friendList = accountRep.FindAll(new Specification<UserFriend>(uf=>uf.Id==uid).Skip(startIndex).Take(count).OrderByDescending(uf=>uf.CreatedOn));

            IList<FriendModel> targets = new List<FriendModel>();
            foreach (UserFriend uf in friendList)
            {
                FriendModel tmp = new FriendModel();
                tmp.UserID = uf.FriendId;
                tmp.UserName = uf.FriendName;
                tmp.Head = uf.FriendHead;

                targets.Add(tmp);
            }

            return targets;
        }
        /// <summary>
        /// 获取用户好友列表
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns>好友列表</returns>
        public IList<UserStateModel> FetchFriendsStateModelByUserID(Guid uid, int startIndex, int count)
        {
            IRepository<UserFriend> accountRep = Factory.Factory<IRepository<UserFriend>>.GetConcrete<UserFriend>();
            IList<UserFriend> friendList = accountRep.FindAll(new Specification<UserFriend>(uf => uf.Id == uid).Skip(startIndex).Take(count).OrderByDescending(uf => uf.CreatedOn));

            IList<UserStateModel> targets = new List<UserStateModel>();
            foreach (UserFriend uf in friendList)
            {
                UserStateModel tmp = new UserStateModel();
                tmp.UserHead = uf.FriendHead;
                tmp.UserID = uf.FriendId;
                //tmp.UserTiny=uf.
                //tmp.
                tmp.UserName = uf.FriendName;
                targets.Add(tmp);
            }

            return targets;
        }
        /// <summary>
        /// 获取好友总数
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns></returns>
        public int GetFriendsCountByUserID(Guid uid)
        {
            IRepository<UserFriend> accountRep = Factory.Factory<IRepository<UserFriend>>.GetConcrete<UserFriend>();
           return  accountRep.Count(new Specification<UserFriend>(uf => uf.Id == uid));
        }
        /// <summary>
        /// 是否为好友
        /// </summary>
        /// <param name="selfId">用户ID</param>
        /// <param name="targetUid">目标用户ID</param>
        /// <returns>true:是,false:否</returns>
        public bool IsFriendOfSelf(Guid selfId,Guid targetUid)
        {
            IRepository<UserFriend> friendRep = Factory.Factory<IRepository<UserFriend>>.GetConcrete<UserFriend>();
            
            return friendRep.Exists(new Specification<UserFriend>(uf => uf.Id == selfId && uf.FriendId == targetUid));
        }

        /// <summary>
        /// 建立好友关系
        /// </summary>
        /// <param name="model">建立好友模型</param>
        private void CreateFriendRelationShip(NewFriendRelationShipModel model)
        {
            IRepository<UserFriend> friendRep = Factory.Factory<IRepository<UserFriend>>.GetConcrete<UserFriend>();
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();
            IRepository<UserProperty> propertyRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();

            //新建好友关系
            UserFriend uf1 = new UserFriend(model.RequestUser.UserID, model.ReciveUser.UserID, model.ReciveUser.UserName, model.ReciveUser.UserTiny);
            UserFriend uf2 = new UserFriend(model.ReciveUser.UserID, model.RequestUser.UserID, model.RequestUser.UserName, model.RequestUser.UserTiny);

            //加入仓库
            friendRep.Add(uf1);
            friendRep.Add(uf2);

            //需要添加各自的好友数
            UserProperty requestUser = propertyRep.Find(new Specification<UserProperty>(up => up.UserID == model.RequestUser.UserID));
            UserProperty receiveUser = propertyRep.Find(new Specification<UserProperty>(up => up.UserID == model.ReciveUser.UserID));
            
            //添加好友数
            requestUser.AddFriendsCount();
            receiveUser.AddFriendsCount();
            //更新数据
            propertyRep.Update(requestUser);
            propertyRep.Update(receiveUser);

            //好友关系持久化
            friendRep.PersistAll();

            //好友数量持久化
            propertyRep.PersistAll();
            
        }

        /// <summary>
        /// 解除好友关系 (暂未实现)
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="fid">好友编号</param>
        public void RemoveFriendRelationShip(Guid uid,Guid fid)
        {
            IRepository<UserFriend> friendRep = Factory.Factory<IRepository<UserFriend>>.GetConcrete<UserFriend>();

        }

        /// <summary>
        /// 请求加为好友
        /// </summary>
        public void RequestBeingFriend(NewFriendRelationShipModel model)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();

            string subject = model.RequestUser.UserName+"想加你为好友";
            ShortMessage sm = new ShortMessage(model.RequestUser.UserID, model.RequestUser.UserName, model.RequestUser.UserTiny, model.ReciveUser.UserID, model.ReciveUser.UserName, model.ReciveUser.UserTiny, subject);
            string content = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.RequestUser.UserID.ToString()) + "' target='_blank'>" + model.RequestUser.UserName + "</a>想加你为好友 <span>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</span>";
            content += "<span><a href='javascript:void(0)' onclick=\"accept('" + model.RequestUser.UserID.ToString() + "','" + sm.Id + "')\">同意</a></span><span><a href='javascript:void(0)' onclick=\"refuse('" + model.RequestUser.UserID.ToString() + "','" + sm.Id + "')\">拒绝</a></span>";

            sm.Body = content;
            smRep.Add(sm);
            smRep.PersistAll();
        }

        /// <summary>
        /// 接受添加好友请求
        /// </summary>
        public void AcceptBeingFriend(NewFriendRelationShipModel model)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();

            this.CreateFriendRelationShip(model);

            string subject = model.ReciveUser.UserName + "接受了你的好友请求";
            string content = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.ReciveUser.UserID.ToString()) + "' target='_blank'>" + model.ReciveUser.UserName + "</a>同意了你的好友请求 <span>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</span>";
            //发送短消息
            ShortMessage sm = new ShortMessage(model.ReciveUser.UserID, model.ReciveUser.UserName, model.ReciveUser.UserTiny, model.RequestUser.UserID, model.RequestUser.UserName, model.RequestUser.UserTiny, subject, content);
            smRep.Add(sm);
            smRep.PersistAll();
        }

        /// <summary>
        /// 拒绝好友请求
        /// </summary>
        public void RefuseBeingFriend(NewFriendRelationShipModel model)
        {
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();

            string subject = model.ReciveUser.UserName+"拒绝了你的好友请求";
            string content = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.ReciveUser.UserID.ToString()) + "' target='_blank'>" + model.ReciveUser.UserName + "</a>拒绝了你的好友请求 <span>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</span>";

            ShortMessage sm = new ShortMessage(model.ReciveUser.UserID, model.ReciveUser.UserName,model.ReciveUser.UserTiny, model.RequestUser.UserID, model.RequestUser.UserName, model.RequestUser.UserTiny, subject, content);
            smRep.Add(sm);
            smRep.PersistAll();
        }

        #endregion 

        #region 用户信息
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <returns>角色名数组,Guid为空时返回null</returns>
        public string[] GetUserRoles(Guid uid)
        {
            string[] roles = null;

            if (!uid.Equals(Guid.Empty))
            {
                //用户仓储
                IAccountRepository accRep = Factory.Factory<IAccountRepository>.GetConcrete();
                Account acc = null;
                acc = accRep.GetByKey(uid);
                if (acc != null)
                    roles = acc.Roles.Split('|');
            }
            
            return roles;
        }

        ///// <summary>
        ///// 获取博主的信息
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public UserInfoModel GetUserInfoModelBy(Guid id)
        //{
        //    IRepository<UserProperty> iRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
        //    UserProperty u = iRep.Find(new Specification<UserProperty>(ut => ut.UserID == id));
        //    UserInfoModel umodel = new UserInfoModel();
        //    umodel.Followees = u.FolloweesCount;
        //    umodel.Followers = u.FollowersCount;
        //    umodel.UserID = u.UserID;

        //    return umodel;
        //}

        
        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExistEmail(string email)
        {
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account acc = null;
            acc = accRep.Find(new Specification<Account>(s => s.Email == email));
            if (acc == null)
            { return false; }
            else
            { return true; }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        public void ChangeUserInfo(ModifyUserNameModel m)
        {
            //IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            //var usr = aRep.Find(new Specification<Account>(u => u.Id == m.UserID));
            //usr.UserName = m.NewName;

            //aRep.Update(usr);
            //aRep.PersistAll();
        }

        /// <summary>
        /// 显示城市 
        /// </summary>
        /// <param name="pid">省份名称</param>
        /// <returns>城市模型集合</returns>
        public IList<CityModel> DisplayCitys(int pid)
        {
            IList<CityModel> targets = new List<CityModel>();

            IXmlRepository rep = Factory.Factory<IXmlRepository>.GetConcrete();
            foreach (City c in rep.Select("ID", pid.ToString()))
            {
                CityModel model = new CityModel();
                model.CityName = c.Name;

                targets.Add(model);
            }

            return targets;
        }
        /// <summary>
        /// 获取用户属性
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserPropertyDspModel GetUserProperty(Guid id)
        {
            IRepository<UserProperty> uRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty up = uRep.Find(new Specification<UserProperty>(s => s.UserID == id));
            UserPropertyDspModel model = new UserPropertyDspModel();
            if (up != null)
            {
                
                model.BlogName = up.BlogName;
                model.BlogTheme = up.BlogTheme;
                model.Brief = up.Brief;
                model.FolloweesCount = up.FolloweesCount;
                model.FollowersCount = up.FollowersCount;
                model.UserID = up.UserID;
                model.Type = up.Type;
            }
            return model;

 
        }
        ///<summary>
        /// 获取同城用户列表
        /// </summary>
        /// <returns>
        /// 同城用户集合
        /// </returns>
        public IList<UserStateModel> GetUserProfileModelListByCity(Guid selfUserID,int index,int count)
        {
            IRepository<UserProfile> profileRep = Factory.Factory<IRepository<UserProfile>>.GetConcrete<UserProfile>();
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            UserProfile selfProfile = profileRep.Find(new Specification<UserProfile>(up => up.UserID == selfUserID));
            IList<UserProfile> alllist =  profileRep.FindAll(new Specification<UserProfile>(p => p.City == selfProfile.City).Skip(index).Take(count).OrderByDescending(er=>er.Id));
            IList<UserStateModel> target = new List<UserStateModel>();

            foreach (UserProfile u in alllist)
            {
                if (u.UserID == selfUserID)
                    continue;
                UserStateModel m = new UserStateModel();
                Account a = aRep.Find(new Specification<Account>(ac=>ac.Id==u.UserID));
                m.UserID = a.Id;
                m.UserName = a.UserName;
                m.UserHead = a.UserHead;
                m.UserTiny = a.Tiny;
                target.Add(m);   
            }

            return target;
        }

        /// <summary>
        /// 获取同城用户个数
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns></returns>
        public int GetSameCityCountByUserID(Guid uid)
        {
            IRepository<UserProfile> profileRep = Factory.Factory<IRepository<UserProfile>>.GetConcrete<UserProfile>();
            UserProfile selfProfile = profileRep.Find(new Specification<UserProfile>(up => up.UserID == uid));
            return profileRep.Count(new Specification<UserProfile>(p => p.City == selfProfile.City));
        }
        
        /// <summary>
        /// 同名用户个数通过用户编号
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <returns></returns>
        public int GetSameNameCountByUserID(Guid uid)
        {
            IRepository<Account> profileRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account ac = profileRep.GetByKey(uid);
            return profileRep.Count(new Specification<Account>(p => p.UserName == ac.UserName));
        }
        /// <summary>
        /// 通过用户名获取同名用户个数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetSameNameCountByName(string name)
        {
            IRepository<Account> profileRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            int k= profileRep.Count(new Specification<Account>(p => p.UserName == name));
            return k;
        }
        /// <summary>
        /// 查找推荐机构
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IList<UserStateModel> GetRecommendUser(string username)
        {
            IRepository<UserProperty> uRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            IList<UserProperty> u = uRep.FindAll(new Specification<UserProperty>(p => p.BlogName == username & p.Type == "company"));
            IList<UserStateModel> usm = new List<UserStateModel>();
            //先通过博客名查找
            if (u.Count!=0)
            {
                foreach (UserProperty upy in u)
                {
                    Account a = null;
                    UserStateModel model = new UserStateModel();
                    a = aRep.Find(new Specification<Account>(p => p.Id == upy.UserID));
                    model.UserID = upy.UserID;
                    model.UserName = upy.BlogName;
                    model.UserTiny = a.Tiny;
                    model.UserHead = a.UserHead;
                    model.Display = upy.Display;
                    usm.Add(model);
                }
               
            }
            else if (u.Count==0)
            {
                //再通过用户名查找
                IList<Account> alist = aRep.FindAll(new Specification<Account>(ac => ac.UserName == username));
                if (alist.Count != 0)
                {
                    foreach (Account acc in alist)
                    {
                        UserStateModel model = new UserStateModel();
                        UserProperty up = uRep.Find(new Specification<UserProperty>(p => p.UserID == acc.Id & p.Type == "company"));
                        if (up != null)
                        {   
                            model.UserID = acc.Id;
                            model.UserName = acc.UserName;
                            model.UserTiny = acc.Tiny;
                            model.UserHead = acc.UserHead;
                            model.Display = up.Display;
                            usm.Add(model);
                        }
                    }
                   
                }
            }
            return usm;
        }
        
        /// <summary>
        /// 通过用户名称查找好友
        /// </summary>
        /// <param name="blogname">用名称</param>
        /// <param name="index">列表起始</param>
        /// <param name="nowposition">个数</param>
        /// <returns></returns>

        public IList<UserStateModel> GetUserListByBlogName(Guid selfUserID, string blogname,int index,int nowposition)
        {
            IRepository<UserProperty> uRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            
            //IList<UserProperty> klist= uRep.FindAll(new Specification<UserProperty>(p => p.use == blogname).Skip(index).Take(nowposition).OrderByDescending(er => er.Id));
            IList<Account> klist = aRep.FindAll(new Specification<Account>(p => p.UserName == blogname).Skip(index).Take(nowposition).OrderByDescending(er => er.Id));
            IList<UserStateModel> mylist = new List<UserStateModel>();

            foreach (Account pro in klist)
            {
                if (pro.Id == selfUserID)//排除自己
                    continue;
                UserProperty p = uRep.Find(new Specification<UserProperty>(pp => pp.UserID == pro.Id));
                   // p=ulist[0];
                    // UserProperty p = uRep.GetByKey(pro.);
                if (p != null)
                {
                    mylist.Add(new UserStateModel() { Display = p.Display, UserHead = pro.UserHead, UserID = pro.Id, UserName = pro.UserName, UserTiny = pro.Tiny });
                }
            }
            //if (mylist.Count < nowposition && mylist.Count != 0)
            //{
            //    IList<Account> olist = aRep.FindAll(new Specification<Account>(p => p.UserName == blogname).Skip(0).Take(nowposition - mylist.Count).OrderByDescending(er => er.Id));
            //    foreach( Account proc in olist)
            //    {
            //        mylist.Add(new UserStateModel() {UserHead = proc.UserHead, UserID = proc.Id, UserName = proc.UserName, UserTiny = proc.Tiny });
            //    }
            //}
            //else
            //{
            //    if (mylist.Count == 0)
            //    {
            //        IList<Account> olist = aRep.FindAll(new Specification<Account>(p => p.UserName == blogname).Skip(index).Take(nowposition - mylist.Count).OrderByDescending(er => er.Id));
            //        foreach (Account proc in olist)
            //        {
            //            mylist.Add(new UserStateModel() { UserHead = proc.UserHead, UserID = proc.Id, UserName = proc.UserName, UserTiny = proc.Tiny });
            //        }
            //    }
            //}
            return mylist;
        }
        ///<summary>
        ///
        /// 获取人物点击数
        /// </summary>
        /// <returns>
        /// 热门人物点击数
        /// </returns>

        public int GetClickByUserID(Guid aid)
        {
            IRepository<UserProperty> profileRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            return profileRep.Find(new Specification<UserProperty>(p => p.UserID == aid)).FolloweesCount;
        }

        public UserPropertyDspModel GetUserPropertyByID(Guid aid)
        {
            IRepository<UserProperty> profileRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty p = profileRep.Find(new Specification<UserProperty>(k => k.UserID == aid));
            UserPropertyDspModel model = null;
            if (p != null)
            {
               model= new UserPropertyDspModel() { BlogTheme=p.BlogTheme, Id=p.Id, Brief = p.Brief, Display = p.Display, FolloweesCount = p.FolloweesCount, FollowersCount = p.FollowersCount, Type = p.Type, UserID = p.UserID, BlogName = p.BlogName };
            }
            return model;
        }



      

        /*
         个人：personal false
         企业：company true
         */
        public bool CheckBlogerTypeByID(Guid aid)
        {
            IRepository<UserProperty> profileRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty userp= profileRep.Find(new Specification<UserProperty>(p => p.UserID == aid));
            if (userp.Type == "company")
            { 
                return true; 
            }
           
            if (userp.Type == "personal")
            {
                    return false;
            }
            return false;
        }
        /// <summary>
        /// 修改博客名
        /// </summary>
        /// <param name="userpro"></param>
        public void ModifBlogName(UserPropertyDspModel userpro)
        {
            IRepository<UserProperty> profileRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty p = profileRep.Find(new Specification<UserProperty>(c => c.UserID == userpro.UserID));
            if (p != null)
            {
                p.UserID = userpro.UserID;
                p.BlogName = userpro.BlogName;
                profileRep.Update(p);
                profileRep.PersistAll();
            }
 
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model">用户密码修改模型</param>
        public void ModifUserPWD(ModifyUserPWDModel model)
        {
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account ac = aRep.GetByKey(model.UserID);
            ac.ModifyPWD(model.UserPWD);
            aRep.Update(ac);
            aRep.PersistAll();
        }
        /// <summary>
        /// 更改主题
        /// </summary>
        /// <param name="userpro"></param>
        public  void  ModifyUserTheme(UserPropertyDspModel userpro)
        {
            IRepository<UserProperty> profileRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty p = profileRep.Find(new Specification<UserProperty>(c=>c.UserID==userpro.UserID));
            if (p != null)
            {
                p.UserID= userpro.UserID;
                p.BlogTheme = userpro.BlogTheme;
            }
            profileRep.Update(p);
            profileRep.PersistAll();
        }
        /// <summary>
        /// 更改单位简介
        /// </summary>
        /// <param name="userpro"></param>
        public void ModifyUserProperty(UserPropertyDspModel userpro)
        {

            IRepository<UserProperty> profileRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty p = profileRep.Find(new Specification<UserProperty>(c => c.UserID == userpro.UserID));
            if (p != null)
            {

                p.UserID = userpro.UserID;
                p.Brief = userpro.Brief;
                p.Type = userpro.Type;
                
                
            }
            profileRep.Update(p);
            profileRep.PersistAll();
 
        }
        ///<summary>
        /// 获取热门人物
        /// </summary>
        /// <returns>
        /// 热门人物集合
        /// </returns>
        public IList<UserStateModel> GetAllHotUser(int index,int count)
        {
            IRepository<UserProperty> profileRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IList<UserStateModel> targets = new List<UserStateModel>();

            IList<UserProperty> alllist = profileRep.FindAll(new Specification<UserProperty>(b =>b.Id!=Guid.Empty).Skip(index).Take(count).OrderBy(kk=>kk.FollowersCount));

            foreach (UserProperty model in alllist)
            {
                targets.Add(this.GetUserInfo(model.UserID));
            }

            return targets;
        
        }
        /// <summary>
        /// 获取专家之星
        /// </summary>
        /// <param name="index">起始位置</param>
        /// <param name="count">总数</param>
        /// <returns></returns>
        public IList<UserInfoModel> GetProfessional(int index, int count)
        {
            IRepository<Account> aRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            IList<UserInfoModel> targets = new List<UserInfoModel>();

            IList<Account> alllist = aRep.FindAll(new Specification<Account>(b => b.Id != Guid.Empty).Skip(index).Take(count).OrderByDescending(kk => kk.Points));

            foreach (Account ac in alllist)
            {
                UserInfoModel model = this.GetUserInfoModel(ac.Id); 
                targets.Add(model);
            }

            return targets;

        }

        /// <summary>
        /// 显示省份
        /// </summary>
        /// <returns>省份模型集合</returns>
        public IList<ProvinceModel> DisplayProvinces()
        {
            IList<ProvinceModel> targets = new List<ProvinceModel>();

            IXmlRepository rep = Factory.Factory<IXmlRepository>.GetConcrete();
            var x = rep.FindAll();

            foreach (Province p in x)
            {
                ProvinceModel instance = new ProvinceModel();
                instance.ProvinceName = p.Name;
                instance.ProvinceID = p.ID;
                targets.Add(instance);
            }

            return targets;
        }


        /// <summary>
        /// 通过省份编号获得用户总数
        /// </summary>
        /// <param name="ProvinceID"></param>
        /// <returns></returns>
        public int GetUserCountByProvince(string  ProvinceName)
        {
            IRepository<UserProfile> profileRep = Factory.Factory<IRepository<UserProfile>>.GetConcrete<UserProfile>();
            int k=0;
            k = profileRep.Count(new Specification<UserProfile>(p => p.Province == ProvinceName));
            return k;
        }
        /// <summary>
        /// 获取注册用户总数
        /// </summary>
        /// <returns></returns>
        public int GetAllUserCount()
        {
            IRepository<UserProfile> profileRep = Factory.Factory<IRepository<UserProfile>>.GetConcrete<UserProfile>();
            int k = 0;
            k = profileRep.Count(new Specification<UserProfile>(p => p.UserID != Guid.Empty));
            return k;
        }


        /// <summary>
        /// 选择城市
        /// </summary>
        /// <param name="model">城市选择模型</param>
        public void SelectCity(SelectCityModel model)
        {
            IRepository<UserProfile> profileRep = Factory.Factory<IRepository<UserProfile>>.GetConcrete<UserProfile>();

            UserProfile profile = profileRep.Find(new Specification<UserProfile>(up => up.UserID == model.UserID));

            profile.Province = model.Province;
            profile.City = model.City;

            profileRep.Update(profile);
            profileRep.PersistAll();
        }

        /// <summary>
        /// 初始化用户资料，修改
        /// </summary>
        /// <param name="model">初始化用户资料模型</param>
        public void ModifyUserProfile(UserProfileModel model)
        {
            IRepository<UserProfile> profileRep = Factory.Factory<IRepository<UserProfile>>.GetConcrete<UserProfile>();

            UserProfile profile = profileRep.Find(new Specification<UserProfile>(p=>p.UserID==model.UserID));

            IRepository<Account> acountRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            Account acount = acountRep.Find(new Specification<Account>(p => p.Id == model.UserID));
            //修改用户资料

            acount.AccountMsgVO = new 
                AccountMessageVO(
                acount.Id, 
                model.UserName, 
                acount.UserHead, 
                acount.Tiny, 
                acount.Points);
            
            profile.Birthday = model.Birthday;
            profile.Cellphone = model.Cellphone;
            profile.City = model.City;
            profile.Company = model.Company;
            profile.Hobby = model.Hobby;
            profile.Msn = model.Msn;
            profile.Province = model.Province;
            profile.QQ = model.QQ;
            profile.Sex = model.Sex;
            profile.Address = model.Address;

            FormsAuthenticationTicket ticket = AuthenticationHelper.CreateAuthenticationTicket(acount.Id.ToString(), acount.UserName, true);
            AuthenticationHelper.SetAuthenticalCookie(ticket);
            profileRep.Update(profile);
            acountRep.Update(acount);
            profileRep.PersistAll();
            acountRep.PersistAll();
        }




        /// <summary>
        /// 获取一个用户资料
        /// </summary>
        /// <param name="aid">用户资料标示</param>
        public UserProfileModel GetOneUserProfileContentByID(Guid aid)
        {
            IRepository<UserProfile> profileRep = Factory.Factory<IRepository<UserProfile>>.GetConcrete<UserProfile>();
            IRepository<Account> accountRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            UserProfile profile = profileRep.Find(new Specification<UserProfile>(up=>up.UserID==aid));
            Account account = accountRep.Find(new Specification<Account>(up=>up.Id==aid));

            UserProfileModel target = new UserProfileModel() { UserID=profile.UserID,Birthday=profile.Birthday,Cellphone=profile.Cellphone,City=profile.City,Province=profile.Province,Company=profile.Company,Hobby=profile.Hobby,Msn=profile.Msn,QQ=profile.QQ,Sex=profile.Sex, UserName=account.UserName, Address=profile.Address };

            return target;
        }


        
        /// <summary>
        /// 用户修改头像
        /// </summary>
        /// <param name="model">修改模型</param>
        public void ModifyUserHead(ModifyUserHeadModel model)
        {
            IRepository<Account> accountRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account usr = accountRep.GetByKey(model.UserID);

            string subDir=System.DateTime.Now.ToString("yyyyMMdd");

            //生成两种尺寸头像
            //ImageResizer ir = new ImageResizer("~/usrimg/", "~/usrimg/"+subDir+"/");
            PanoramaCutting pc = new PanoramaCutting("~/usrimg/", "~/usrimg/" + subDir + "/");
            string head = pc.GetImage(model.UserHead, 172, 124);
            string tiny = pc.GetImage(model.UserHead, 50, 50);

            //可能会再加入回复框中显示的标准,即第三种情况

            //Utils.Tools.CreateThumbForFile("~/usrimg/"+subDir+"/"+Guid.NewGuid().ToString().Replace('-',''),);
            //修改头像
            usr.AccountMsgVO = new 
                AccountMessageVO(
                model.UserID, 
                usr.UserName, 
                subDir + "/" + head, 
                subDir + "" + tiny, 
                usr.Points);
            
            //usr.UserHead = subDir +"/"+ head;
            //usr.Tiny = subDir +"/"+ tiny;

            //更新
            accountRep.Update(usr);
            accountRep.PersistAll();
        }

        /// <summary>
        /// 获取用户状态模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserStateModel GetUserInfo(Guid id)
        {
            //IAccountRepository accRep = Factory.Factory<IAccountRepository>.GetConcrete();
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            Account user = null;
            UserStateModel usm = null;
            user = accRep.GetByKey(id);
            if (user == null)
            {
                throw new NoAccountException("用户不存在");
            }
            usm = new UserStateModel() { UserID = user.Id, UserName = user.UserName, UserTiny = user.Tiny, UserHead = user.UserHead };

            return usm;
        }

        public UserInfoModel GetUserInfoModel(Guid id)
        {
            //IAccountRepository accRep = Factory.Factory<IAccountRepository>.GetConcrete();
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();

            Account user = null;
            UserInfoModel usm = null;
            user = accRep.GetByKey(id);
            UserPropertyDspModel sm = this.GetUserProperty(id);
            usm = new UserInfoModel() { UserTiny = user.UserHead, Followees = sm.FolloweesCount, Followers = sm.FollowersCount, UserHead = user.UserHead, UserID = user.Id, UserName = user.UserName };

            return usm;
        }

        //获取用户收到的系统通知
        public void GetUserNotices()
        {

        }
        /// <summary>
        /// 提问时获取用户的积分
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public int GetUserPoints(Guid uid)
        {
            IRepository<Account> accRep = Factory.Factory<IRepository<Account>>.GetConcrete<Account>();
            Account user = accRep.GetByKey(uid);
            return user.Points;

        }
        /// <summary>
        /// 获取用户类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserTypeByUserID(Guid id)
        {
            IRepository<UserProperty> uRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            UserProperty up = uRep.Find(new Specification<UserProperty>(u => u.UserID == id));
            return up.Type;
        }
        #endregion

        #region 关注、粉丝操作

        /// <summary>
        /// 关注某人
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="follow">关注对象编号</param>
        public void FollowSomeBody(AddFolloweeModel model)
        {
            IRepository<UserFollow> followRep = Factory.Factory<IRepository<UserFollow>>.GetConcrete<UserFollow>();
            IRepository<UserProperty> userProRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            IRepository<ShortMessage> smRep = Factory.Factory<IRepository<ShortMessage>>.GetConcrete<ShortMessage>();

            UserProperty userProperty = userProRep.Find(new Specification<UserProperty>(uf => uf.UserID == model.UserID));
            UserProperty followProperty = userProRep.Find(new Specification<UserProperty>(uf => uf.UserID == model.FolloweeID));

            

            //新建用户关注实例
            UserFollow newUserFollow = new UserFollow(model.UserID, model.UserName, model.UserHead, model.FolloweeID, model.FolloweeName, model.FolloweeHead);
            
            //加入仓储
            followRep.Add(newUserFollow);
            //持久化
            followRep.PersistAll();
            
            //增加关注数
            userProperty.AddFolloweesCount();
            //增加粉丝数
            followProperty.AddFollowersCount();
            userProRep.Update(userProperty);
            userProRep.Update(followProperty);
            userProRep.PersistAll();

            //发送消息
            string subject = model.UserName + "关注了你";
            string content = "<a href='" + Utils.UrlFactory.CreateUrl(FBS.Utils.UrlFactory.PageName.UserHome, model.UserID.ToString()) + "' target='_blank'>" + model.UserName + "</a>关注了你 <span>" + DateTime.Now.ToString("yy-MM-dd HH:mm:ss") + "</span>";
            ShortMessage newMsg=new ShortMessage(model.UserID, model.UserName, model.UserHead,model.FolloweeID, model.FolloweeName,model.FolloweeHead,  subject, content);

            //UserEntryService k = new UserEntryService();
            UserStateModel me= this.GetUserInfo(model.UserID);
            UserStateModel follow = this.GetUserInfo(model.FolloweeID);
            NewFeedModel feedModel = new NewFeedModel() { Content="", Sharer=me, SourceUser=follow, Subject="", Type=FeedType.Support   };
            BlogService my=new BlogService();
            my.CreateFeed(feedModel);

            smRep.Add(newMsg);
            smRep.PersistAll();
        }

        /// <summary>
        /// 取消对某人的关注
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="follow">关注对象编号</param>

        public void CancelFollow(Guid uid,Guid follow)
        {
            IRepository<UserFollow> followRep = Factory.Factory<IRepository<UserFollow>>.GetConcrete<UserFollow>();
            IRepository<UserProperty> userProRep = Factory.Factory<IRepository<UserProperty>>.GetConcrete<UserProperty>();
            

            UserProperty userProperty = userProRep.Find(new Specification<UserProperty>(uf => uf.UserID == uid));
            UserProperty followProperty = userProRep.Find(new Specification<UserProperty>(uf => uf.UserID == follow));

            UserFollow userFollow = followRep.Find(new Specification<UserFollow>(uf => uf.UserID == uid && uf.Followee == follow));

            //取消关注
            followRep.Remove(userFollow);
            //持久化
            followRep.PersistAll();

            //减少关注数
            userProperty.ReduceFolloweesCount();
            //减少粉丝数
            followProperty.ReduceFollowersCount();
            userProRep.Update(userProperty);
            userProRep.Update(followProperty);
            userProRep.PersistAll();

            //短消息


            UserInfoService k = new UserInfoService();
            UserStateModel me = k.GetUserInfo(uid);
            UserStateModel follower = k.GetUserInfo(follow);
            NewFeedModel feedModel = new NewFeedModel() { Content = "", Sharer = me, SourceUser = follower, Subject = "", Type = FeedType.CancelSupport };
            BlogService my = new BlogService();
            my.CreateFeed(feedModel);


        }

        /// <summary>
        /// 获取某人的关注
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">数目</param>
        public IList<UserStateModel> FetchFolloweesByUserID(Guid uid,int startIndex,int count)
        {
            IRepository<UserFollow> userFollowRep = Factory.Factory<IRepository<UserFollow>>.GetConcrete<UserFollow>();
            IList<UserStateModel> targets=new List<UserStateModel>();

            var x= userFollowRep.FindAll(new Specification<UserFollow>(uf => uf.UserID == uid).Skip(startIndex).Take(count).OrderByDescending(uf=>uf.Id));
            foreach (UserFollow uf in x)
            {
                UserStateModel tmp = new UserStateModel() {UserID=uf.Followee,UserName=uf.FolloweeName,UserTiny=uf.FolloweeHead};
                targets.Add(tmp);
            }

            return targets;
        }

        /// <summary>
        /// 获取某人的粉丝
        /// </summary>
        /// <param name="uid">用户编号</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">数目</param>
        public IList<UserStateModel> FetchFollowersByUserID(Guid uid,int startIndex,int count)
        {
            IRepository<UserFollow> userFollowRep = Factory.Factory<IRepository<UserFollow>>.GetConcrete<UserFollow>();
            IList<UserStateModel> targets = new List<UserStateModel>();

            var x = userFollowRep.FindAll(new Specification<UserFollow>(uf => uf.Followee == uid).Skip(startIndex).Take(count).OrderByDescending(uf => uf.Id));
            foreach (UserFollow uf in x)
            {
                UserStateModel tmp = new UserStateModel() { UserID = uf.UserID, UserName = uf.UserName, UserTiny = uf.UserHead };
                targets.Add(tmp);
            }

            return targets;
        }
        /// <summary>
        /// 获取某人的关注
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IList<UserStateModel> FetchFolloweesByuserID(Guid uid)
        {
            IRepository<UserFollow> userFollowRep = Factory.Factory<IRepository<UserFollow>>.GetConcrete<UserFollow>();
            IList<UserStateModel> targets = new List<UserStateModel>();

            var x = userFollowRep.FindAll(new Specification<UserFollow>(uf=>uf.UserID==uid));
            foreach (UserFollow uf in x)
            {
                UserStateModel tmp = new UserStateModel() { UserID = uf.Followee, UserName = uf.FolloweeName, UserTiny = uf.FolloweeHead };
                targets.Add(tmp);
            }

            return targets;
        }
        /// <summary>
        /// 判断是否粉丝
        /// </summary>
        /// <param name="uid">其他博主的id</param>
        /// <param name="id">登录博主的id</param>
        /// <returns></returns>
        public bool IsFollowerOrNot(Guid uid, Guid id)
        {
            bool myFlag = false;
            IRepository<UserFollow> userFollowRep = Factory.Factory<IRepository<UserFollow>>.GetConcrete<UserFollow>();
            IList<UserStateModel> targets = new List<UserStateModel>();

            var x = userFollowRep.FindAll(new Specification<UserFollow>(uf => uf.UserID == id));
            foreach (UserFollow uf in x)
            {
                UserStateModel tmp = new UserStateModel() { UserID = uf.Followee, UserName = uf.FolloweeName, UserTiny = uf.FolloweeHead };
                targets.Add(tmp);
            }

            

            foreach (UserStateModel model in targets)
            {
                if (model.UserID == uid)
                {
                    myFlag = true;
                }
 
            }
            return myFlag;
 
        }

        #endregion

    }
}
