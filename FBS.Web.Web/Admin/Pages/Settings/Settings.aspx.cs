using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FBS.Service;
using FBS.Service.ActionModels;

namespace FBS.Web.News.Admin.Pages.Settings
{
    public partial class Settings : System.Web.UI.Page
    {
        SiteService siteInfoService = new SiteService();
        private SiteCnf info = null;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            if(info==null) info = siteInfoService.GetSiteInfo();
            this.txName.Text = info.SiteName;
            this.txDesc.Text = info.SiteDesc;
            this.txUrl.Text = info.SiteUrl;
            this.txCopyRight.Text = info.CopyRight;
            base.OnPreRender(e);
        }

        protected void saveBtn_Click(object sender, EventArgs e)
        {
            if (info == null) info = siteInfoService.GetSiteInfo();
            info.SiteName = this.txName.Text.Trim();
            info.SiteDesc = this.txDesc.Text.Trim();
            info.SiteUrl = this.txUrl.Text.Trim();
            info.CopyRight = this.txCopyRight.Text.Trim();
            try
            {
                siteInfoService.SetSiteInfo(info);
            }
            catch (Exception error) { this.msg.Text = error.Message; }
        }
    }
}