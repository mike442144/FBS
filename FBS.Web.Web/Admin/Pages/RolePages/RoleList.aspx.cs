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
using System.Xml;
namespace FBS.Web.News.Admin
{
    public partial class RoleList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        public void BindGridView()
        {
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            XmlNodeList mylist = xmlDoc.GetElementsByTagName("Roles")[0].ChildNodes;
            DataSet ds = new DataSet("ds_mission");
            DataTable dt = new DataTable("dt_mission");
            dt.Columns.Add(new DataColumn("Name", typeof(string)));//为dt_dry表内建立Column
            dt.Columns.Add(new DataColumn("Test", typeof(string)));
            for (int i = 0; i < mylist.Count;i++ )
            {
                XmlNode node = mylist[i];
                DataRow dr = dt.NewRow();
                dr["Name"] = node.Attributes["Name"].Value;
                dr["Test"] = node.Attributes["Name"].Value;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            RolesGridView.DataSource=ds.Tables[0].DefaultView;
            RolesGridView.DataBind();
        }

        protected void Row_Deleted(object sender, GridViewDeleteEventArgs e)
        {
            GridView g = (GridView)sender;
            string k = g.DataKeys[e.RowIndex].Value.ToString();
            string fpath = HttpContext.Current.Server.MapPath("~/App_Data/Permissions.xml");
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fpath);
            XmlNode root = xmlDoc.GetElementsByTagName("Roles")[0];
            XmlNodeList mylist = root.ChildNodes;
            foreach (XmlNode node in mylist)
            {
                if (node.Attributes["Name"].Value == k)
                {
                    root.RemoveChild(node);
                    break;
                }
            }
            xmlDoc.Save(fpath);
            Response.Write("<script>alert('删除成功')</script>");
        }

        protected void gvBaidu_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //如果当前行尾数据行
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //添加鼠标在当前行时的background-color属性
                e.Row.Attributes.Add("onmouseover", "c=this.style.backgroundColor;this.style.backgroundColor='#fcdead';");

                //鼠标离开当前行后
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=c");
            }
        }

        protected void RolesGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            BindGridView();
        }
    }
}
