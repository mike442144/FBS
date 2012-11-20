using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;
using FBS.Domain.Specifications;

namespace FBS.Service
{
    public class AdvertisePageService
    {
        /// <summary>
        /// 创建文章
        /// </summary>
        /// <param name="model">新建文章模型</param>
        public void CreateAdvertisePage(NewAdvertisePageModel model)
        {
            IRepository<AdvertisePage> rep = Factory.Factory<IRepository<AdvertisePage>>.GetConcrete<AdvertisePage>();

            try
            {
                rep.Add(new AdvertisePage(model.PageURL,model.PageDescription));
                rep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 获取广告页面详细内容
        /// </summary>
        /// <param name="model"></param>
        public AdvertisePageDetailsModel GetOneAdvertisePageByID(Guid aid)
        {
            IRepository<AdvertisePage> rep = Factory.Factory<IRepository<AdvertisePage>>.GetConcrete<AdvertisePage>();

            AdvertisePage advertisementpage = null;
            AdvertisePageDetailsModel target = null;

            try
            {
                advertisementpage = rep.GetByKey(aid);

                target = new AdvertisePageDetailsModel()
                {
                    PageDescription=advertisementpage.PageDescription,
                    PageURL=advertisementpage.PageURL,
                };
            }

            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

            return target;
        }



        /// <summary>
        /// 列出所有广告页面
        /// </summary>
        /// 
        public IList<AdvertisePageDspModel> FetchAdvertisePageDspModel()
        {
            IRepository<AdvertisePage> pageRep = Factory.Factory<IRepository<AdvertisePage>>.GetConcrete<AdvertisePage>();
            IList<AdvertisePageDspModel> mylist = new List<AdvertisePageDspModel>();
            foreach (AdvertisePage myarti in pageRep.FindAll())
            {
                mylist.Add(new AdvertisePageDspModel() { PageDescription=myarti.PageDescription, PageID=myarti.PageID, PageURL=myarti.PageURL });
            }
            return mylist;
        }


        ///<summary>
        ///根据页面名称查找页面
        ///</summary>
        ///
        public AdvertisePageDspModel GetPageByPageName(string pagename)
        {
            IRepository<AdvertisePage> ipageRep = Factory.Factory<IRepository<AdvertisePage>>.GetConcrete<AdvertisePage>();
            
            AdvertisePageDspModel model = null;
             foreach (AdvertisePage pageRep in ipageRep.FindAll(new Specification<AdvertisePage>(c =>c.PageDescription==pagename)).Skip(0).Take(1))
             {
               model = new AdvertisePageDspModel() { PageDescription=pageRep.PageDescription, PageID=pageRep.PageID, PageURL=pageRep.PageURL };   
             }
             return model;
        }
        /// <summary>
        /// 删除广告页面
        /// </summary>
        /// <param name="aid">广告页面编号</param>
        public void DeletePageByKey(Guid aid)
        {
            IRepository<AdvertisePage> ipageRep = Factory.Factory<IRepository<AdvertisePage>>.GetConcrete<AdvertisePage>();
            ipageRep.RemoveByKey(aid);
            ipageRep.PersistAll();
        }
    }
}
