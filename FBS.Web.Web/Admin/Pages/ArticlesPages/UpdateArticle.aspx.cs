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
using System.Collections.Generic;
using System.IO;

namespace FBS.Web.News.Backstage
{
    public partial class UpdateArticle : System.Web.UI.Page
    {
        CMSService cmssservice = new CMSService();
        public string Msg = string.Empty;
        public string ImgUrl = string.Empty;
        public string ImgName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ImgName = GetImage();
        }

        
        protected void ArticlesUpdateBtn_Click(object sender, EventArgs e)
        {
            ModifiedArticleModel model = new ModifiedArticleModel();
            CMSService mycms = new CMSService();
            string body = Request.Form["editorcontent"];
            string[] k = Utils.Utils.GetHtmlImageUrlList(body);

            int point = 0;
            string url = "";
            if (k.Length > 0)
            {
                foreach (string y in k)
                {
                    if (!y.Contains("/plugins/emoticons/"))
                    {
                        url = y;
                        break;
                    }
                    point++;
                }
                if (point == k.Length)
                {
                    model.ImgName = string.Empty;
                }
                else
                {
                    if (url.Contains("FLYUpload/Images/"))
                    {
                        model.ImgName = Utils.Utils.GetHtmlImageUrlList(body)[point];
                    }
                    else
                    {
                        try
                        {
                            model.ImgName = url;
                            //String fileExt = Path.GetExtension(url).ToLower();
                            //System.Drawing.Image tempimage = System.Drawing.Bitmap.FromFile(url);
                            //string filename = "/FLYUpload/Images/" + Guid.NewGuid().ToString() + fileExt;
                            //tempimage.Save(filename);
                            //tempimage.Dispose();
                        }
                        catch (Exception ee)
                        {
                            model.ImgName = string.Empty;
                        }
                    }
                }
            }
            else
            {
                model.ImgName = string.Empty;
            }

            model.Title = ArticleTitle.Text.ToString().Trim();
            model.Body = body;
            model.CategoryID = new Guid(ArticleCategory.SelectedItem.Value);
            model.CategoryName = ArticleCategory.SelectedItem.Text.ToString().Replace("┣", "").Replace("-", ""); ;
            model.SourceSite = ArticleWeb.Text.ToString().Trim();
            model.SourceUrl = ArticleLink.Text.ToString().Trim();
            model.ArticleID = new Guid(Lable_id.Text.Trim());
            model.BriefTitle = ArticleBriefTitle.Text.ToString().Trim();
            //从cookie获取用户名及用户的ID
            HttpCookie hc = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket fat = FormsAuthentication.Decrypt(hc.Value);
            model.UserID = new Guid(fat.Name);
            model.UserName = fat.UserData;
            mycms.UpdateArticle(model);
            //Response.Redirect("ArticlesList.aspx?Type='update'");
            Response.Write("<script>alert('修改成功')</script>");
        }

        public void BindCategory(Guid CategoryID)
        {
            IList<ArticleCategoryModel> mylist = new CategoryService().FetchArticleCategory();
            IList<ListItem> treelist = GetArticleCategory(mylist, Guid.Empty);
            ArticleCategory.Items.Clear();
            int item = 0;
            int count = 0;
            foreach (ListItem ls in treelist)
            {

                ArticleCategory.Items.Add(ls);
                if (ls.Value == CategoryID.ToString())
                {
                    item = count;
                }
                count++;
            }
            ArticleCategory.SelectedIndex = item;
           
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

                ImgUrl = path + name;
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

        //获取文章类别森林
        IList<ListItem> treelist = new List<ListItem>();
        public IList<ListItem> GetArticleCategory(IList<ArticleCategoryModel> mylist, Guid ParentID)
        {
            foreach (ArticleCategoryModel model in mylist)
            {

                if (ParentID.Equals(model.ParentID))
                {
                    if (Guid.Empty.Equals(model.ParentID))
                    {
                        treelist.Add(new ListItem(model.CategoryName, model.CategoryID.ToString()));
                    }
                    else
                    {
                        string index = "";
                        for (int i = 0; i < model.Deepth; i++)
                        {
                            index += "--";
                        }
                        index += "┣";
                        treelist.Add(new ListItem(index + model.CategoryName, model.CategoryID.ToString()));
                    }
                    GetArticleCategory(mylist, model.CategoryID);
                }

            }
            return treelist;

        }

        protected override void OnPreRender(EventArgs e)
        {
            Lable_id.Text = Request["id"].ToString();
            if (Lable_id.Text != null)
            {
                ArticleDetailsModel model = cmssservice.GetOneArticleContentByID(new Guid(Lable_id.Text));
                ArticleTitle.Text = model.Title;

                editorcontent.Value = model.Body;
                BindCategory(model.CategoryID);
                ArticleWeb.Text = model.SourceSite;
                ArticleLink.Text = model.SourceUrl;
                ArticleBriefTitle.Text = model.BriefTitle;
            }
            base.OnPreRender(e);
        }

        protected void BT_Onclick(object sender, EventArgs e)
        {
            ImgName = GetImage();
            L_ImgName.Text = ImgName;
            //  Response.Write("<script>document.getElementById(set_img).click();</script>");
        }
    }
}
