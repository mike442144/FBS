using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Service.ActionModels
{
    /// <summary>
    /// 初始化用户资料模型
    /// </summary>
    public class UserProfileModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserID
        {
            get;
            set;
        }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Sex
        {
            get;
            set;
        }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday
        {
            get;
            set;
        }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province
        {
            get;
            set;
        }


        /// <summary>
        /// 城市
        /// </summary>
        public string City
        {
            get;
            set;
        }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company
        {
            get;
            set;
        }
        //地址
        public string Address { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }
        /// <summary>
        /// 兴趣爱好
        /// </summary>
        public string Hobby
        {
            get;
            set;
        }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ
        {
            get;
            set;
        }

        /// <summary>
        /// MSN
        /// </summary>
        public string Msn
        {
            get;
            set;
        }

        /// <summary>
        /// 手机
        /// </summary>
        public string Cellphone
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 省份
    /// </summary>
    public class ProvinceModel
    {
        /// <summary>
        /// 省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 省份编号
        /// </summary>
        public Int32 ProvinceID { get; set; }
    }

    /// <summary>
    /// 城市
    /// </summary>
    public class CityModel
    {
        /// <summary>
        /// 城市名称
        /// </summary>
        public string CityName { get; set; }
    }

    /// <summary>
    /// 用户名
    /// </summary>
    

    /// <summary>
    /// 选择城市模型
    /// </summary>
    public class SelectCityModel
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid UserID { get; set; }

        /// <summary>
        /// 省份名称
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市名称
        /// </summary>
        public string City { get; set; }
    }
}
