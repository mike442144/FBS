using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FBS.Service.ActionModels
{
    [DisplayName("许可协议")]
    public class StepOfLicense
    {
        [DisplayName("许可协议")]
        public string Info { get; set; }
    }

    [DisplayName("运行环境检查")]
    public class StepOfCheck
    {
        public string OSVersion;
        public string RunTimeVersion;
    }

    [DisplayName("配置数据库")]
    public class StepOfDbCnf
    {
        [DisplayName("数据库名称")]
        public string DbName { get; set; }
        [DisplayName("数据库用户名")]
        public string DbUser { get; set; }
        [DisplayName("数据库密码")]
        public string DbPsd { get; set; }
        [DisplayName("数据库地址")]
        public string DbAddr { get; set; }
        //[DisplayName("数据库表前缀")]
        //public string DbTablePrefix { get; set; }
    }

    [DisplayName("配置网站信息")]
    public class StepOfSiteCnf
    {
        public StepOfSiteCnf()
        {
            this.SiteName = string.Empty;
            this.SiteDesc = string.Empty;
            this.SiteUrl = string.Empty;
            this.CopyRight = string.Empty;
            this.Version = new Version();
            this.FounderEmail = string.Empty;
            this.FounderName = string.Empty;
            this.FounderPsd = string.Empty;
        }

        [DisplayName("网站名称")]
        public string SiteName { get; set; }
        [DisplayName("网站描述")]
        public string SiteDesc { get; set; }
        [DisplayName("您的网址")]
        public string SiteUrl { get; set; }
        [DisplayName("创始人用户名称")]
        public string FounderName { get; set; }
        [DisplayName("密码")]
        public string FounderPsd { get; set; }
        [DisplayName("联系邮箱")]
        public string FounderEmail { get; set; }
        public string CopyRight;
        public Version Version;
    }
}
