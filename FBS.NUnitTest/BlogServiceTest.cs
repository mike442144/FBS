#define NunitTest

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service;
using FBS.Service.ActionModels;
using NUnit.Framework;

namespace FBS.NUnitTest
{
    
    [TestFixture]
    public class BlogServiceTest
    {
        private NewBlogStoryModel model = null;
        private BlogService service = null;
        private Guid id = Guid.Empty;
        
        [SetUp]
        public void InitializeOperands()
        {
            
            service = new BlogService();
            model = new NewBlogStoryModel();
            model.AccountID = new Guid("a8074051-1818-4fd6-88cc-0d8e18fd3d7e");
            model.Body = "";
            model.CategoryID = new Guid("62fe6c96-c5ce-46d1-9bb7-ab076046c428");
            model.CategoryName = "看我发明";
            model.ImgName = "123";
            model.IsPublishedToHomepage = true;
            model.IsRequireDigest = true;
            IList<String> k = new List<String>();
            k.Add("123");
            model.Tags = k;
            model.Title = "12";
            model.UserName = "12";
            model.UserTiny = "123";
        }


        /// <summary>
        /// 测试
        /// </summary>
        [Test]
        public void AddTest()
        {
            //service = new BlogService();
            //model = new NewBlogStoryModel();
            //model.AccountID = new Guid("a8074051-1818-4fd6-88cc-0d8e18fd3d7e");
            //model.Body = "";
            //model.CategoryID = new Guid("62fe6c96-c5ce-46d1-9bb7-ab076046c428");
            //model.CategoryName = "看我发明";
            //model.ImgName = "123";
            //model.IsPublishedToHomepage = true;
            //model.IsRequireDigest = true;
            //IList<String> k = new List<String>();
            //k.Add("123");
            //model.Tags = k;
            //model.Title = "12";
            //model.UserName = "12";
            //model.UserTiny = "123";
            service.CreateBlogStory(model);
        }
        [Test]
        public void GetTest()
        {
            BlogStoryDetailsModel getmodel = service.GetOneBlogStoryContentByID(id);
            Assert.AreEqual(getmodel, null);
        }
    }
}
