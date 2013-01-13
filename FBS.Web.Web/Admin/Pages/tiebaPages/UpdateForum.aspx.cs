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

namespace FBS.Web.News.Admin.Pages.tiebaPages
{
    public partial class UpdateForum : System.Web.UI.Page
    {
        public string id = Guid.Empty.ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"].ToString();
                    myid.Text = id;
                    Bind_Date();
                }
            }
        }

        public void Bind_Date()
        {
            ForumService service = new ForumService();
            ForumDspModel model=  service.GetForumDspModelByID(new Guid(id));
            TB_Desription.Text = model.ForumDsp.Trim();
            TB_Name.Text = model.ForumName.Trim();
        }

        protected void BT_UpdateForum_Click(object sender, EventArgs e)
        {
            ForumService service = new ForumService();
            ForumDspModel model = service.GetForumDspModelByID(new Guid(myid.Text.Trim()));
            model.ForumName = TB_Name.Text.Trim();
            model.ForumDsp = TB_Desription.Text.Trim();
            model.ModifiedTime = DateTime.Now;
            service.ModifyForum(model);
            Response.Redirect("ForumList.aspx");
        }
    }
}
