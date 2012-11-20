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
    public class AdvertisementService
    {
        /// <summary>
        /// 创建广告
        /// </summary>
        /// <param name="model">新建广告模型</param>
        public void CreateAdvertisement(NewAdvertisementModel model)
        {
            IRepository<Advertisement> rep = Factory.Factory<IRepository<Advertisement>>.GetConcrete<Advertisement>();

            try
            {
                rep.Add(new Advertisement(model.AdvertisementID,model.AdvertisementType,model.AdvertisementContentURL,model.AdvertisementPriority,model.AdvertisementBeginTime,model.AdvertisementEndTime,model.AdvertisementURL));
                rep.PersistAll();
            }
            catch { }
        }
        /// <summary>
        /// 获取广告详细内容
        /// </summary>
        /// <param name="model">新建广告模型</param>
        public AdvertisementDetailsModel GetOneAdvertisementContentByID( Guid aid)
        {
            IRepository<Advertisement> rep = Factory.Factory<IRepository<Advertisement>>.GetConcrete<Advertisement>();

            Advertisement advertisement = null;
            AdvertisementDetailsModel target = null;
            
            try
            {
                advertisement = rep.GetByKey(aid);

                target = new AdvertisementDetailsModel()
                {
                   AdvertisementType=advertisement.AdvertisementType,
                   AdvertisementContentURL =advertisement.AdvertisementContentURL,
                   AdvertisementPriority =advertisement.AdvertisementPriority,
                   AdvertisementBeginTime=advertisement.AdvertisementBeginTime,
                   AdvertisementEndTime=advertisement.AdvertisementEndTime,
                   AdvertisementURL=advertisement.AdvertisementURL,
                };
            }

            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

            return target;
        }


        public IList<AdvertisementDspModel> GetSomeAdvertisementContent(int startIndex, int count)
        {
            IList<Advertisement> mentlist = new List<Advertisement>();
            IRepository<Advertisement> mentrep = Factory.Factory<IRepository<Advertisement>>.GetConcrete<Advertisement>();
            try
            {
                mentlist = mentrep.FindAll(new Specification<Advertisement>(c=>(c.AdvertisementEndTime<DateTime.Now)).Skip(startIndex).Take(count).OrderByDescending(b => b.AdvertisementPriority));
            }
            catch { }
            IList<AdvertisementDspModel> mylist = new List<AdvertisementDspModel>();
            foreach (Advertisement model in mentlist)
            {
                mylist.Add(new AdvertisementDspModel() { AdvertisementContentURL=model.AdvertisementContentURL, AdvertisementID=model.AdvertisementID, AdvertisementPriority=model.AdvertisementPriority, AdvertisementType=model.AdvertisementType, AdvertisementURL=model.AdvertisementURL});
            }
            return mylist;
        }
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="aid">广告编号</param>
        public void DeleteAdvertisementByID(Guid aid)
        {
            IRepository<Advertisement> mentrep = Factory.Factory<IRepository<Advertisement>>.GetConcrete<Advertisement>();
            mentrep.RemoveByKey(aid);
            mentrep.PersistAll();
        }


    }
}
