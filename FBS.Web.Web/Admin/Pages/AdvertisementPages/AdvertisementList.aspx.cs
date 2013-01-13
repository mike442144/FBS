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
using FBS.Service;
using FBS.Service.ActionModels;

namespace FBS.Web.News.Admin.Pages.AdvertisementPages
{
     
    public partial class AdvertisementList1 : System.Web.UI.Page
    {
        public   string location = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void BindGridView(string location)
        {
            AdvertiseService service = new AdvertiseService();
            AdvertiseDspModel[] mylist= service.GetObjectsByLocation(location);
            DataSet ds = new DataSet("ds_advertise");
            DataTable dt = new DataTable("ds_advertise");
            dt.Columns.Add(new DataColumn("Id", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));//为dt_dry表内建立Column
            dt.Columns.Add(new DataColumn("Begin", typeof(string)));
            dt.Columns.Add(new DataColumn("End", typeof(string)));
            for (int i = 0; i < mylist.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = mylist[i].ID;
                dr["Begin"] = mylist[i].BeginTime.ToString("yyyy-MM-dd");
                dr["End"] = mylist[i].EndTime.ToString("yyyy-MM-dd");
               
                dr["Name"] = mylist[i].AdvertiseName;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);
            AdvertiseGridView.DataSource = ds.Tables[0].DefaultView;
            AdvertiseGridView.DataBind();
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

        protected void Row_Deleted(object sender, GridViewDeleteEventArgs e)
        {
            GridView g = (GridView)sender;
            string k = g.DataKeys[e.RowIndex].Value.ToString();

            AdvertiseService service = new AdvertiseService();
            service.DeleteAdvertiseDspModel(service.GetAdvertiseDspModelById(new Guid(k)));
            Response.Write("<script>alert('删除成功')</script>");
        }


        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView g = (GridView)sender;
            g.PageIndex = e.NewPageIndex;
            BindGridView(this.L_location.Text.Trim());
        }

        protected void ViewAdvertise_Click(object sender, EventArgs e)
        {
            if (Request.Form["section"] != null && Request.Form["page"] != null && Request.Form["area"] != null && Request.Form["position"] != null)
            {
                location = "Advertiser." + Request.Form["section"] + "." + Request.Form["page"] + "." + Request.Form["area"] + "." + Request.Form["position"];
                L_location.Text = location;
                BindGridView(location);
            }
        }

    }
}
