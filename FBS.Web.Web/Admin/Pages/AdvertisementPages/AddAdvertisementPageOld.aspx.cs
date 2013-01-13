using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FBS.Service;
using FBS.Service.ActionModels;

namespace Aviator.Web
{
    public partial class AddAdvertisementPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BT_AddPage_Click(object sender, EventArgs e)
        {
            AdvertisePageService myservice = new AdvertisePageService();
            NewAdvertisePageModel model = new NewAdvertisePageModel();
            model.PageID = Guid.NewGuid();
            //model.PageURL = TB_URL.Text.ToString().Trim();
            //model.PageDescription = TB_Desription.Text.ToString().Trim();
            myservice.CreateAdvertisePage(model);
        }
    }
}
