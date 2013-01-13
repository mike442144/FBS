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
using Wuqi.Webdiyer;

namespace FBS.Web.News.Admin.Pages.Video
{
    public partial class VideoList : System.Web.UI.Page
    {
        ShareThreadService mservice = new ShareThreadService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                anp.RecordCount = mservice.GetShareThreadCountByType("新闻");
                BindGridview();
            }
           
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


        public void BindGridview()
        {
            this.VideosGridView.DataSource = mservice.FetchShareThreadDspModel(0,3,"新闻");
            this.VideosGridView.DataBind();
        }


    

        protected void getPager(object sender, EventArgs e)
        {
            AspNetPager g = (AspNetPager)sender;

            GetPageList(g);
        }

        private void GetPageList(AspNetPager anp)
        {
            this.VideosGridView.DataSource = mservice.FetchShareThreadDspModel(anp.StartRecordIndex - 1, anp.PageSize, "新闻");
            this.VideosGridView.DataBind();
        }

        protected void VideosGridView_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
           
        }

        protected void VideosGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView g = (GridView)sender;

            string k = g.DataKeys[e.RowIndex].Value.ToString();
            //string k = g.Rows[e.RowIndex].Cells[0].Text.ToString();
            mservice.RemoveShareThreadByKey(new Guid(k));
            anp.RecordCount = mservice.GetShareThreadCountByType("新闻");
            BindGridview();
            Response.Write("<script>alert('删除成功')</script>");
        }
    }
}
