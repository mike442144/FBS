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
using System.IO;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Web.News.Admin.Pages.AdvertisementPages
{
    public partial class AddAdvertisement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AdvertiseService myservice = new AdvertiseService();
            AdvertiseDspModel adv = new AdvertiseDspModel();


            string section = Request.Form["section"];
            string page = Request.Form["page"];
            string area = Request.Form["area"];
            string position = Request.Form["position"];
            if (section == "" && page == "" && area == "" && position == "") {

                Response.Write("<script>alert('广告位选择不完整！')</script>"); 
            }

            string location = section + "." + page + "." + area + "." + position;
            string filename = adv.ID + Path.GetExtension(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(Server.MapPath("~") + "/FLYUpload/Images/" + filename);

            adv.Path = "FLYUpload/Images/" + filename;

            if (rbAnimatedGif.Checked)
                adv.ContentType = AdvertiseType.AnimatedGif;
            else
                adv.ContentType = AdvertiseType.FlashMovie;

            adv.Height = int.Parse(txtHeight.Text);
            adv.Width = int.Parse(txtWidth.Text);
            if (txtUrl.Text.Trim().Length > 0)
                adv.Url = txtUrl.Text;
            adv.AdvertiseName = AdvertiseName.Text.ToString().Trim();
            adv.Location = "Advertiser." + location;//"Advertiser.User.Login.area1.position1";
            adv.Priority =Int16.Parse( Priority.Text.ToString());
            if (Request.Form["begintime"] == null || Request.Form["begintime"] == "点击选择日期" || Request.Form["endtime"] == null || Request.Form["endtime"] == "点击选择日期")
            {
                Response.Write("<script>alert('请选择日期')</script>");
                return;
            }
            string begintime = Request.Form["begintime"];
            string endtime =Request.Form["endtime"];
            DateTime begin;
            DateTime end;
            try
            {
               begin = DateTime.Parse(begintime);
               end = DateTime.Parse(endtime);
               adv.BeginTime = begin;
               adv.EndTime = end;
            }
            catch {
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

            myservice.AddOne(adv);
            //adv.Url = "caojun";
            //myservice.UpdateAdvertiseDspModel(adv);
            //myservice.DeleteAdvertiseDspModel(adv);
            Response.Write("<script>alert('添加成功')</script>");
        }

        
    }
}
