/*
 * 显示系统所有信息，包括：
 * 1.系统信息：运行时环境，使用的操作系统，内存使用情况；
 * 2.数据统计信息：站点版本，访问量，文章数等；
 * 
 * 
 **/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using System.Reflection;
using FBS.Service;

namespace FBS.App
{
    /// <summary>
    /// 显示系统所有信息
    /// </summary>
    class ShowAllInfo
    {
        public ShowAllInfo()
        {
            this.EnvInfo = new EnvironmentInfo();
            this.StaInfo = new StatisticsInfo();
        }

        public EnvironmentInfo EnvInfo;
        public StatisticsInfo StaInfo;
    }

    public class EnvironmentInfo
    {
        
        public EnvironmentInfo()
        {
            this._siteVersion= AppDomain.CurrentDomain.DomainManager.EntryAssembly.GetName().Version;//Assembly.GetExecutingAssembly().GetName().Version;//获取当前网站程序集的版本
            this.OsVersion = Environment.OSVersion.ToString();
            this.RuntimeVersion = Environment.Version.ToString();
            //this._memInUse = AppDomain.CurrentDomain
        }



        #region 系统信息

        public string SiteVersion
        {
            get { return this._siteVersion.ToString(); }
        }

        private Version _siteVersion;

        public readonly string OsVersion;
        public readonly string RuntimeVersion;
        public readonly float MemInUse;

        #endregion
    }

    /// <summary>
    /// 统计信息
    /// </summary>
    public class StatisticsInfo
    {
        public StatisticsInfo()
        {
            CMSService cms = new CMSService();

            ArticleDspModel a = cms.GetLatestArticle(string.Empty);
            if (null != a)
            {
                this.LastArticle = a;
            }
            
            this.ArticleCount = cms.GetArticleCountOfCategory(Guid.Empty);

            //访问统计
            //
        }

        #region 数据信息
        public readonly int VisitedCount;
        public readonly int ArticleCount;
        //最后发表时间
        //private DateTime _lastPost;
        public readonly ArticleDspModel LastArticle;
        //访问量最多的文章
        private readonly ArticleDspModel MostVisited;
        #endregion
    }
}
