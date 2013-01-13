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
using FBS.Service.ActionModels;
using FBS.Service;

namespace FBS.Web.News.Admin.Pages.GoodsPages
{
    public partial class UpdateGoods : System.Web.UI.Page
    {
        public string GoodsID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    GoodsID = Request.QueryString["id"].ToString();
                    Label1.Text = GoodsID;
                    BindMyPage();
                }
            }
        }


        public void BindMyPage()
        {
            GoodsDetailsModel model = new GoodsService().GetOneGoodsContentByID(new Guid(GoodsID));
            TB_GoodsName.Text = model.GoodsName;
            TB_NowPrice.Text = model.GoodsNowPrice.ToString();
            TB_OldPrice.Text = model.GoodsOldPrice.ToString();
            C_BeginTime.SelectedDate = model.GoodsBeginTime;
            C_EndTime.SelectedDate = model.GoodsEndTime;
            //FU_Pic.= model.GoodsPicURL;
            editor1.InnerText = model.GoodsDetailsContent;
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
                        GoodsDspModel model = new GoodsDspModel()
                        {
                            GoodsID = new Guid(Label1.Text.ToString().Trim()),
                            GoodsBeginTime = C_BeginTime.SelectedDate,
                            GoodsBuyCount = 0,
                            GoodsDetailsContent = editor1.InnerText,
                            GoodsEndTime = C_EndTime.SelectedDate,
                            GoodsIsOn = false,
                            GoodsName = TB_GoodsName.Text.ToString().Trim(),
                            GoodsNowPrice = float.Parse(TB_NowPrice.Text.ToString().Trim()),
                            GoodsOldPrice = float.Parse(TB_OldPrice.Text.ToString().Trim()),
                            GoodsPicURL = filePath
                        };
                        mygoodsservice.UpdateGoods(model);
                        Response.Write("<script>alert('修改成功')</script>");
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
