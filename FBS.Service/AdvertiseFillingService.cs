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
    public class AdvertiseFillingService
    {
        /// <summary>
        /// 创建广告匹配模型
        /// </summary>
        /// <param name="model">新建广告匹配模型</param>
        public void CreateAdvertiseFilling(NewAdvertiseFillingModel model)
        {
            IRepository<AdvertiseFilling> rep = Factory.Factory<IRepository<AdvertiseFilling>>.GetConcrete<AdvertiseFilling>();

            try
            {
                rep.Add(new AdvertiseFilling(model.PageID, model.AdvertisementID, model.PositionName));
                rep.PersistAll();
            }
            catch { }
        }

        

        /// <summary>
        /// 获取广告匹配模型
        /// </summary>
        /// <param name="aid">广告匹配模型编号</param>
        public AdvertiseFillingDspModel GetOneAdvertisementContentByID(Guid aid)
        {
            IRepository<AdvertiseFilling> rep = Factory.Factory<IRepository<AdvertiseFilling>>.GetConcrete<AdvertiseFilling>();

            AdvertiseFilling advertisement = null;
            AdvertiseFillingDspModel target = null;

            try
            {
                advertisement = rep.GetByKey(aid);

                target = new AdvertiseFillingDspModel()
                { AdvertisementID=advertisement.AdvertisementID,
                     ID=advertisement.Id,
                      PageID=advertisement.PageID
                };
            }

            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

            return target;
        }


        /// <summary>
        /// 获取广告编号
        /// </summary>
        /// <param name="aid">广告页面编号</param>
        public IList<AdvertisementDetailsModel> GetSomeAdvertisementContentByPageID(Guid aid,string type)
        {
            IRepository<AdvertiseFilling> rep = Factory.Factory<IRepository<AdvertiseFilling>>.GetConcrete<AdvertiseFilling>();
            IList<AdvertisementDetailsModel> mylist =new List<AdvertisementDetailsModel>();
            AdvertisementService myservice = new AdvertisementService();
            IList<AdvertiseFilling> target = new List<AdvertiseFilling>();
            target = rep.FindAll(new Specification<AdvertiseFilling>(c => c.PageID == aid&&c.PositionName==type));
            if (target.Count != 0)
            {
                foreach (AdvertiseFilling filling in target)
                {
                    AdvertisementDetailsModel adv = myservice.GetOneAdvertisementContentByID(filling.AdvertisementID);
                    if (adv.AdvertisementBeginTime <= DateTime.Now && adv.AdvertisementEndTime >= DateTime.Now)
                    {
                        mylist.Add(adv);
                    }
                }
                
            }
            return mylist;
        }

    }
}
