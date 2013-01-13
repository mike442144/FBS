using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using FBS.Service;
using FBS.Service.ActionModels;
using System.IO;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Web.News.Admin.Pages.AdvertisementPages
{
    public partial class UpdateAdvertisement : System.Web.UI.Page
    {
        public string id = string.Empty;
        public string BeginTime="";
        public string EndTime = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"].ToString();
                    InitAdvertise(id);
                }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AdvertiseService myservice = new AdvertiseService();
            AdvertiseDspModel adv = myservice.GetAdvertiseDspModelById(new Guid(Label1.Text.Trim()));

            string filename = adv.ID + Path.GetExtension(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(Server.MapPath("~") + "/FLYUpload/Images/" + filename);

            adv.Path = "/FLYUpload/Images/" + filename;

            if (rbAnimatedGif.Checked)
                adv.ContentType = AdvertiseType.AnimatedGif;
            else
                adv.ContentType = AdvertiseType.FlashMovie;

            adv.Height = int.Parse(txtHeight.Text);
            adv.Width = int.Parse(txtWidth.Text);
            if (txtUrl.Text.Trim().Length > 0)
                adv.Url = txtUrl.Text;
            adv.AdvertiseName = AdvertiseName.Text.ToString().Trim();
            adv.Priority = Int16.Parse(Priority.Text.ToString());
            if (Request.Form["begintime"] == null || Request.Form["begintime"] == "点击选择日期" || Request.Form["endtime"] == null || Request.Form["endtime"] == "点击选择日期")
            {
                Response.Write("<script>alert('请选择日期')</script>");
                return;
            }
            string begintime = Request.Form["begintime"];
            string endtime = Request.Form["endtime"];
            DateTime begin;
            DateTime end;
            try
            {
                begin = DateTime.Parse(begintime);
                end = DateTime.Parse(endtime);
                adv.BeginTime = begin;
                adv.EndTime = end;
            }
            catch
            {
                Response.Write("<script>alert('日期格式有误')</script>");
                return;
            }
            if (adv.BeginTime >= adv.EndTime)
            {
                Response.Write("<script>alert('结束时间应该大于开始时间')</script>");
                return;
            }
            if (adv.EndTime < DateTime.Now)
            {
                Response.Write("<script>alert('此广告无效的，结束时间已过')</script>");
                return;
            }

            myservice.UpdateAdvertiseDspModel(adv);
            //adv.Url = "caojun";
            //myservice.UpdateAdvertiseDspModel(adv);
            //myservice.DeleteAdvertiseDspModel(adv);
            Response.Write("<script>alert('修改成功')</script>");
        }

        void InitAdvertise(string id)
        {
            AdvertiseService myservice = new AdvertiseService();
            AdvertiseDspModel adv = myservice.GetAdvertiseDspModelById(new Guid(id));
            if (adv != null)
            {
                AdvertiseName.Text = adv.AdvertiseName;
                BeginTime = adv.BeginTime.ToString("yyyy-MM-dd");
                EndTime = adv.EndTime.ToString("yyyy-MM-dd");
                Priority.Text = adv.Priority.ToString();
                txtWidth.Text =adv.Width.ToString();
                txtHeight.Text =adv.Height.ToString();
                txtUrl.Text = adv.Url;
                Label1.Text = id;
            }
            else
            {
                return;
            }
        }
    }
}
