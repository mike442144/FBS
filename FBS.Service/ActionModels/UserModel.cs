using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.Specialized;

namespace FBS.Service.ActionModels
{
    [DisplayName("用户修改密码模型")]
    public class UserChangePasswordModel
    {
        [DisplayName("原始密码")]
        public string OldPassword { get; set; }

        [DisplayName("新密码")]
        public string NewPassword { get; set; }

        [DisplayName("确认新密码")]
        public string ConfirmPassword { get; set; }
    }

    [DisplayName("用户登录模型")]
    public class UserLogOnModel
    {
        [DisplayName("邮箱")]
        public string Email { get; set; }
        [DisplayName("密码")]
        public string Password { get; set; }
        [DisplayName("记住密码？")]
        public bool RememberMe { get; set; }
        [DisplayName("用户昵称")]
        public string UserName { get; set; }
    }

    [DisplayName("用户注册模型")]
    public class UserRegisterModel
    {
        public UserRegisterModel()
        {
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.Email = string.Empty;
            this.ConfirmPassword = string.Empty;
            this.RoleName = string.Empty;
        }

        [DisplayName("邮箱")]
        public string Email { get; set; }
        [DisplayName("密码")]
        public string Password { get; set; }
        [DisplayName("确认密码")]
        public string ConfirmPassword { get; set; }
        [DisplayName("用户昵称")]
        public string UserName { get; set; }
        [DisplayName("角色")]
        public string RoleName { get; set; }
    }

    [DisplayName("用户状态模型")]
    public class UserStateModel
    {
        [DisplayName("用户昵称")]
        public string UserName { get; set; }

        [DisplayName("用户编号")]
        public Guid UserID { get; set; }

        [DisplayName("用户缩略头像")]
        public string UserTiny { get; set; }

        [DisplayName("用户头像")]
        public string UserHead { get; set; }

        [DisplayName("显示推荐")]
        public bool Display { get; set; }
      
    }

    [DisplayName("用户信息模型")]
    public class UserInfoModel
    {
        [DisplayName("用户编号")]
        public Guid UserID { get; set; }

        [DisplayName("用户昵称")]
        public string UserName { get; set; }

        [DisplayName("用户头像")]
        public string UserHead { get; set; }

        [DisplayName("注册日期")]
        public DateTime RegisterDate { get; set; }

        [DisplayName("关注数")]
        public int Followees { get; set; }

        [DisplayName("粉丝数")]
        public int Followers { get; set; }

        [DisplayName("用户头像")]
        public string UserTiny { get; set; }
    }

    [DisplayName("好友信息模型")]
    public class FriendModel
    {
        [DisplayName("用户编号")]
        public Guid UserID { get; set; }

        [DisplayName("用户昵称")]
        public string UserName { get; set; }

        [DisplayName("头像")]
        public string Head { get; set; }
    }

    [DisplayName("建立好友关系模型")]
    public class NewFriendRelationShipModel
    {
        [DisplayName("请求加为好友用户")]
        public UserStateModel RequestUser { get; set; }

        [DisplayName("收到请求用户")]
        public UserStateModel ReciveUser { get; set; }
    }

    /// <summary>
    /// 修改头像模型
    /// </summary>
    public class ModifyUserHeadModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 新头像
        /// </summary>
        public string UserHead { get; set; }
    }

    public class ModifyUserNameModel
    {
        public Guid UserID { get; set; }
        [DisplayName("旧昵称")]
        public string OldName { get; set; }
        [DisplayName("新昵称")]
        public string NewName { get; set; }
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    public class ModifyUserPWDModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 用户新密码
        /// </summary>
        public string UserPWD { get; set; }
    }
     /// <summary>
    /// 修改用户模型
    /// </summary>

    public class UserPropertyDspModel
    {
        public Guid Id
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public Guid UserID
        {
            get;
            set;
        }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FollowersCount
        {
            get;
            set;
        }

        /// <summary>
        /// 关注数
        /// </summary>
        public int FolloweesCount
        {
            get;
            set;
        }
        /// <summary>
        /// 显示
        /// </summary>
        public bool Display
        {
            get;
            set;
        }

        /// <summary>
        /// 简介
        /// </summary>
        public string Brief
        {
            get;
            set;
        }

        ///<summary>
        ///博客主题
        ///</summary>
        public string BlogTheme
        {
            get;
            set;
        }
        ///<summary>
        ///博客名称
        ///</summary>
        public string BlogName
        {
            get;
            set;
        }
    }
    /// <summary>
    /// 后台管理用户模型
    /// </summary>
    public class AdminAccountDspModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid AccountID { set; get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Role { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }



    /// <summary>
    /// 受禁用户模型
    /// </summary>
    public class ForbiddenAccountDspModel
    {
        /// <summary>
        /// 禁用编号
        /// </summary>
        public Guid ForbiddenID { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid AccountID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户受禁ip地址
        /// </summary>
        public string IP { get; set; }
        /// <summary>
        /// 被禁时间
        /// </summary>
        public DateTime ForbiddenTime { get; set; }
        /// <summary>
        /// 激活时间
        /// </summary>
        public DateTime RefreshTime { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 被禁类型  例子:广告专业户，灌水专业户,不和谐专业户
        /// </summary>
        public string ForbiddenType { get; set; }
    }

}
