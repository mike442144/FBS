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
using System.Collections.Generic;
using System.IO;

namespace Aviator.Web.Admin
{
    public partial class AddAdvertisement : CheckAdminUser
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                    BindAdvertisePage();
            }
        }

        protected void BT_AddAdvertise_Click(object sender, EventArgs e)
        {
            AdvertisementService myadvertise_service = new AdvertisementService();
            AdvertisePageService myadverpage_service = new AdvertisePageService();
            AdvertiseFillingService myadverfill_service = new AdvertiseFillingService();
            bool fileOk = false;
            NewAdvertiseFillingModel Fillmodel = new NewAdvertiseFillingModel();
            NewAdvertisementModel mynewadvertiseMode = new NewAdvertisementModel();
            if (FU_AdvertiseContent.HasFile)
            {
                mynewadvertiseMode.AdvertisementPriority = System.Int16.Parse(TB_AdvertisePriority.Text.ToString().Trim());
                // mynewadvertiseMode.AdvertisementContentURL = FU_AdvertiseContent.PostedFile.FileName;
                if (C_AdvertiseBegin.SelectedDate > C_AdvertiseEnd.SelectedDate)
                {
                    Response.Write("<script>alert('开始日期不能大于结束日期！！')</script>");
                }
                else
                {
                    mynewadvertiseMode.AdvertisementBeginTime = C_AdvertiseBegin.SelectedDate;
                    mynewadvertiseMode.AdvertisementEndTime = C_AdvertiseEnd.SelectedDate;
                    Guid aid = Guid.NewGuid();
                    mynewadvertiseMode.AdvertisementID = aid;

                    if (DD_AdvertiseType.SelectedItem.Value == "true")
                    {
                        string fileExtension = System.IO.Path.GetExtension(FU_AdvertiseContent.FileName).ToLower();
                        //限定只能上传jpg和gif图片
                        string[] allowExtension = { ".jpg", ".gif", ".bmp", ".png", ".jpeg" };
                        //对上传的文件的类型进行一个个匹对
                        for (int i = 0; i < allowExtension.Length; i++)
                        {
                            if (fileExtension == allowExtension[i])
                            {
                                fileOk = true;
                                break;
                            }
                        }

                        if (fileOk)
                        {
                            mynewadvertiseMode.AdvertisementType = "image";
                            string filePath = "~/Upload/" + aid.ToString() + fileExtension;
                            mynewadvertiseMode.AdvertisementContentURL = aid.ToString() + fileExtension;
                            FU_AdvertiseContent.SaveAs(MapPath(filePath));//把文件上传到服务器的绝对路径上
                        }
                        else
                        {
                            Response.Write("<script>alert('文件格式错误！')</script>");
                        }

                    }
                    else
                    {

                        string fileExtension = System.IO.Path.GetExtension(FU_AdvertiseContent.FileName).ToLower();

                        string[] allowExtension = { ".swf" };

                        for (int i = 0; i < allowExtension.Length; i++)
                        {
                            if (fileExtension == allowExtension[i])
                            {
                                fileOk = true;
                                break;
                            }
                        }

                        if (fileOk)
                        {
                            mynewadvertiseMode.AdvertisementType = "flash";
                            mynewadvertiseMode.AdvertisementType = "image";

                            string filePath = "~/Upload/" + aid.ToString() + fileExtension;
                            mynewadvertiseMode.AdvertisementContentURL = aid.ToString() + fileExtension;

                            FU_AdvertiseContent.SaveAs(MapPath(filePath));//把文件上传到服务器的绝对路径上
                        }
                        else
                        {
                            Response.Write("<script>alert('文件格式错误！')</script>");
                        }

                    }
                    if (fileOk)
                    {
                        mynewadvertiseMode.AdvertisementURL = TB_AdvertisementURL.Text.ToString().Trim();
                        myadvertise_service.CreateAdvertisement(mynewadvertiseMode);
                        Fillmodel.PageID = new Guid(DD_AdvertisePage.SelectedItem.Value);
                        Fillmodel.AdvertisementID = aid;
                        Fillmodel.PositionName = TB_PositionName.Text.ToString().Trim();
                        myadverfill_service.CreateAdvertiseFilling(Fillmodel);
                    }
                }

            }
            else
            {
                Response.Write("<script>alert('请您选择上传的文件！！')</script>");
            }
        }

        public void BindAdvertisePage()
        {
            AdvertisePageService pageservice = new AdvertisePageService();
            IList<AdvertisePageDspModel> mylist= pageservice.FetchAdvertisePageDspModel();
            foreach (AdvertisePageDspModel model in mylist)
            {
                DD_AdvertisePage.Items.Add(new ListItem(model.PageDescription,model.PageID.ToString()));
            }
        }
    }
}
