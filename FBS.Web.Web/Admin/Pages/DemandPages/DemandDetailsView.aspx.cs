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
using FBS.Domain.Aggregate.Entity;
using FBS.Service.ActionModels;

namespace FBS.Web.News.Admin.Pages.DemandPages
{
    public partial class DemandDetailsView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    string id = Request.QueryString["id"];
                    DemandService myservice = new DemandService();
                    DemandDetailsModel demand= myservice.GetOneDemandContentByID(new Guid(id));
                    LB_BussissName.Text = demand.BusinessmanName;
                    LB_City.Text = demand.DemandCity;
                    LB_Content.Text = demand.DemandContent;
                    LB_Name.Text = demand.CustomerName;
                    LB_Othernum.Text = demand.CustomerOtherConnect;
                    LB_Telnum.Text = demand.CustomerPhoneNum;
                    LB_Type.Text = demand.GroupOnType;
                }
               
            }
        }
    }
}
