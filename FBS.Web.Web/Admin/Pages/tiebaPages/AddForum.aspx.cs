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
    public partial class AddForum : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BT_AddForum_Click(object sender, EventArgs e)
        {
            ForumService service = new ForumService();
            NewForumModel model = new NewForumModel() { CreationTime=DateTime.Now, ForumDsp=TB_Desription.Text.Trim(), ForumName=TB_Name.Text.Trim(), ModifiedTime=DateTime.Now, ThreadCount=0 };
            service.CreateForum(model);
            Response.Redirect("ForumList.aspx");
        }
    }
}
