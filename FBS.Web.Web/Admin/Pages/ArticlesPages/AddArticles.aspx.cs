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
using FBS.Utils;


namespace FBS.Web.News.Backstage
{
    public partial class AddArticles : System.Web.UI.Page
    {
        public string Msg = string.Empty;
        public string ImgUrl = string.Empty;
        public string ImgName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategory();
            }
            
            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "SetEditorContents", "if(ContentStr!=null&&ContentStr!=\"\"){CKEDITOR.instances.editor1.insertHtml(\"<img src=" + "'" + ImgName + "'" + "/>\");}", true);
        }

        protected string GetImage()
        {
            if (Request.Files.Count > 0)//有文件流
            {
                if (!AviatorWebRequest.CheckType())
                {
                    this.Msg = "不是有效的图片格式！";
                    return string.Empty;
                }
                if (!AviatorWebRequest.CheckLength())
                {
                    this.Msg = "图片过大，请压缩后上传！";
                    return string.Empty;
                }
                //类型、大小检查通过
                string PicName = Guid.NewGuid().ToString();
                string path = Server.MapPath("~/FLYUpload/Images");
                //Request.Files.
                string name = PicName + ".jpg";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                ImgUrl = path +"/" + name;
                try
                {
                    AviatorWebRequest.SaveRequestFile(ImgUrl);
                }
                catch (Exception error)
                {
                    Msg = error.Message;
                }

                return name;
                
            }
            return string.Empty;
        }

        protected void ArticlesAddBtn_Click(object sender, EventArgs e)
        {
            NewArticleModel model=new NewArticleModel();
            // model.
            CMSService mycms = new CMSService();
            //string _Pic=string.Empty;
            //if (!string.IsNullOrEmpty(ImgName))
            //{
            //    _Pic = ImgName;
            //}
            string body = Request.Form["editorcontent"];
            //if (Utils.Utils.GetHtmlImageUrlList(body).Length > 0)
            //{
            //    string k = Utils.Utils.GetHtmlImageUrlList(body)[0].Replace("/FLYUpload/Images/","");
            //    model.ImgName =k;
            //}
            //else
            //{
            //    model.ImgName = string.Empty;
            //}

            string[] k = FBS.Utils.Utils.GetHtmlImageUrlList(body);

            int point = 0;
            string url = "";
            if (k.Length > 0)
            {
                foreach (string y in k)
                {
                    if (!y.Contains("/plugins/emoticons/"))//表情
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
                        model.ImgName = FBS.Utils.Utils.GetHtmlImageUrlList(body)[point];
                    }
                    else
                    {
                        //网络图片
                        try
                        {
                            //String fileExt = Path.GetExtension(url).ToLower();
                            //System.Drawing.Image tempimage = System.Drawing.Bitmap.FromFile(url);
                            //string filename = "/FLYUpload/Images/" + Guid.NewGuid().ToString() + fileExt;
                            //tempimage.Save(filename);
                            //tempimage.Dispose();

                            model.ImgName = url;

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
            model.CategoryName = ArticleCategory.SelectedItem.Text.ToString().Replace("┣", "").Replace("-","");
            model.SourceSite = ArticleWeb.Text.ToString().Trim();
            model.SourceUrl = ArticleLink.Text.ToString().Trim();
            model.BriefTitle = ArticleBriefTitle.Text.ToString().Trim();
            //从cookie获取用户名及用户的ID
            HttpCookie hc = Request.Cookies[FormsAuthentication.FormsCookieName];
            FormsAuthenticationTicket fat = FormsAuthentication.Decrypt(hc.Value);

            model.UserID =new Guid( fat.Name);
            model.UserName = fat.UserData;
            mycms.CreateArticle(model);
            //Response.Redirect("ArticlesList.aspx?Type='add'");
            Response.Write("<script>alert('添加成功')</script>");
        }

        public void BindCategory()
        { 
            IList<ArticleCategoryModel> mylist =  new CategoryService().FetchArticleCategory();
            IList<ListItem> treelist= GetArticleCategory(mylist, Guid.Empty);
            ArticleCategory.Items.Clear();
            foreach(ListItem ls in treelist)
            {
                ArticleCategory.Items.Add(ls);
            }
        }
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

        protected void BT_Onclick(object sender, EventArgs e)
        {
            ImgName = GetImage();
            L_ImgName.Text = ImgName;
          //  Response.Write("<script>document.getElementById(set_img).click();</script>");
        }
    }
}
