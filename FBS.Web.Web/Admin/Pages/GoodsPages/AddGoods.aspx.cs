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


namespace Aviator.Web.Admin.Pages.GoodsPages
{
    public partial class AddGoods : CheckAdminUser
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BT_AddGoods_Click(object sender, EventArgs e)
        {
            GoodsService mygoodsservice = new GoodsService();
            if (FU_Pic.HasFile)
            {
                bool fileOk = false;
                if (C_BeginTime.SelectedDate > C_EndTime.SelectedDate)
                {
                    Response.Write("<script>alert('开始日期不能大于结束日期！！')</script>");
                }
                else
                {
                   
                        string fileExtension = System.IO.Path.GetExtension(FU_Pic.FileName).ToLower();
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
                            Guid aid = Guid.NewGuid();
                            string filePath = "../../../images/Groupon/Upload/" + aid.ToString() + fileExtension;
                            FU_Pic.SaveAs(MapPath(filePath));//把文件上传到服务器的绝对路径上
                            GoodsDspModel model = new GoodsDspModel() {  GoodsID=aid,GoodsBeginTime = C_BeginTime.SelectedDate, GoodsBuyCount = 0, GoodsDetailsContent = editor1.InnerText, GoodsEndTime = C_EndTime.SelectedDate, GoodsIsOn = false, GoodsName = TB_GoodsName.Text.ToString().Trim(), GoodsNowPrice = float.Parse(TB_NowPrice.Text.ToString().Trim()), GoodsOldPrice = float.Parse(TB_OldPrice.Text.ToString().Trim()), GoodsPicURL = filePath };
                            mygoodsservice.CreateGoods(model);
                            Response.Write("<script>alert('添加成功')</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('文件格式错误！')</script>");
                        }

                    }
                }
            else
            {
                Response.Write("<script>alert('请您选择上传的文件！！')</script>");
            }  
          
        }
    }
}
