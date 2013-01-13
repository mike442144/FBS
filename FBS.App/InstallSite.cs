/*  网站安装操作类
 *  
 * 
 */

using System;
using System.Collections.Generic;
using FBS.Service.ActionModels;
using System.Web.Configuration;
using System.Configuration;
using System.Web;
using FBS.Service;

namespace FBS.App
{
    #region 安装步骤

    /// <summary>
    /// 网站安装步骤
    /// </summary>
    public class InstallStep
    {
        public override string ToString()
        {
            return this._name;
        }

        /// <summary>
        /// 显示许可协议
        /// </summary>
        /// <returns></returns>
        public License ShowLicense()
        {
            this._licenseContent = "LicenseContent";
            License sol = new License();
            sol.Info = this._licenseContent;

            return sol;
        }
        
        /// <summary>
        /// 检测网站运行环境,并返回检测信息
        /// </summary>
        /// <returns></returns>
        public CheckEvn Check()
        {

            CheckEvn soc = new CheckEvn();
            soc.OSVersion = Environment.OSVersion.ToString();
            soc.RunTimeVersion = Environment.Version.ToString();

            return soc;
        }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="sodc"></param>
        /// <returns></returns>
        public void SetDbCnf(DbCnf sodc)
        {
            var cnf = WebConfigurationManager.OpenWebConfiguration("~");//打开配置文件
            if (cnf == null) throw new NullReferenceException("未能正常读取配置文件,web.config");
            ConnectionStringsSection section = cnf.ConnectionStrings;//配置连接字符串节点
            if (section == null)
                throw new ConfigurationErrorsException("web.config中不存在connectionstrings节点,请检查文件是否已经损坏，或被恶意篡改！");
            var setting = new ConnectionStringSettings();
            setting.ConnectionString = "Data Source=" + sodc.DbAddr + ";Initial Catalog=" + sodc.DbName + ";User Id=" + sodc.DbUser + ";Password=" + sodc.DbPsd;
            setting.Name = "sqlstrconn";
            setting.ProviderName = "System.Data.SqlClient";
            section.ConnectionStrings.Clear();
            section.ConnectionStrings.Add(setting);
            try
            {
                cnf.Save();//保存
                var sqlScript = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/fbs.sql"));
                Domain.Repository.IHelper helper = FBS.Factory.Factory<FBS.Domain.Repository.IHelper>.GetConcrete();
                helper.ExecScriptFile(sqlScript);
            }
            catch 
            { 
                throw; 
            }
        }

        /// <summary>
        /// 配置网站信息
        /// </summary>
        /// <param name="sosc"></param>
        /// <returns></returns>
        public void SetSiteCnf(SiteCnf sosc)
        {
            SiteService service = new SiteService();
            service.SetSiteInfo(sosc);//初始化网站信息

            UserEntryService us = new UserEntryService();//添加创始人
            UserRegisterModel m = new UserRegisterModel();
            m.Email = sosc.FounderEmail;
            m.Password = sosc.FounderPsd;
            m.UserName = sosc.FounderName;
            m.RoleName = "founder";

            us.Register(m);//注册创始人

            HttpContext.Current.Application.Add("SitUrl", sosc.SiteUrl);
        }
        
        protected string _name;
        protected string _desc;

        private string _licenseContent;
    }
    

    #endregion
}
