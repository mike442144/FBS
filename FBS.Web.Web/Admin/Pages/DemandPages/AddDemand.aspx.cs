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
using Aviator.Service;
using Aviator.Service.ActionModels;

namespace Aviator.Web.Admin.Pages.DemandPages
{
    public partial class AddDemand : CheckAdminUser
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Form["name"]!=null)
                {
                    string name = Request.Form["name"];
                    string telnumber = Request.Form["telnumber"];
                    string othernumber = Request.Form["othernumber"];
                    string city = Request.Form["city"];
                    string suppliersname = Request.Form["suppliersname"];
                    string grouptype = Request.Form["grouptype"];
                    string groupcontent = Request.Form["groupcontent"];
                    DemandService myservice = new DemandService();
                    myservice.CreateDemand(new NewDemandModel() { BusinessmanName=suppliersname, CustomerName=name, CustomerOtherConnect=othernumber, CustomerPhoneNum=telnumber, DemandCity=city, DemandContent=groupcontent, GroupOnType=grouptype,  });
                    Response.Write("<script>alert('谢谢您，我们会在第一时间和您联系！')</script>");
                }
            }
        }
    }
}
