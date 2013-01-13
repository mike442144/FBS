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
using System.IO;

namespace FBS.Web.News.Admin.Pages.ArticlesPages
{
    public partial class UpLoadImgWindow : System.Web.UI.Page
    {
        public string Msg = string.Empty;
        public string ImgName = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            ImgName = GetImage();
        }

        protected string GetImage()
        {
            if (Request.Files.Count > 0)//有文件流
            {
                if (!Utils.AviatorWebRequest.CheckType())
                {
                    this.Msg = "不是有效的图片格式！";
                    return string.Empty;
                }
                if (!Utils.AviatorWebRequest.CheckLength())
                {
                    this.Msg = "图片过大，请压缩后上传！";
                    return string.Empty;
                }
                //类型、大小检查通过
                string PicName = Guid.NewGuid().ToString();
                string path = Server.MapPath("~/FLYUpload/Images/");
                string name = PicName + ".jpg";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                
                try
                {
                    Utils.AviatorWebRequest.SaveRequestFile(path + name);
                }
                catch (Exception error)
                {
                    Msg = error.Message;
                }

                return name;

            }
            return string.Empty;
        }
    }
}
