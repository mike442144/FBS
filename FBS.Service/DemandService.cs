using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;

namespace FBS.Service
{
    public class DemandService
    {
        /// <summary>
        /// 创建需求
        /// </summary>
        /// <param name="model">新建文章分类模型</param>
        public void CreateDemand(NewDemandModel model)
        {
            IRepository<Demand> rep = Factory.Factory<IRepository<Demand>>.GetConcrete<Demand>();


            try
            {  
                rep.Add(new Demand(model.CustomerName,model.CustomerPhoneNum,model.CustomerOtherConnect,model.DemandCity,model.BusinessmanName,model.GroupOnType,model.DemandContent));
                rep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 获取广告详细内容
        /// </summary>
        /// <param name="model">新建广告模型</param>
        public DemandDetailsModel GetOneDemandContentByID(Guid aid)
        {
            IRepository<Demand> rep = Factory.Factory<IRepository<Demand>>.GetConcrete<Demand>();

            Demand demand = null;
            DemandDetailsModel target = null;

            try
            {
                demand = rep.GetByKey(aid);

                target = new DemandDetailsModel()
                {
                    BusinessmanName=demand.BusinessmanName,
                    CustomerName=demand.CustomerName,
                    CustomerOtherConnect=demand.CustomerOtherConnect,
                    CustomerPhoneNum=demand.CustomerPhoneNum,
                    DemandCity=demand.DemandCity,
                    DemandContent=demand.DemandContent,
                    GroupOnType=demand.GroupOnType,
                };
            }

            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

            return target;
        }

        /// <summary>
        /// 列出所有商品
        /// </summary>
        /// 
        public IList<DemandDspModel> FetchDemand()
        {
            IRepository<Demand> articleRep = Factory.Factory<IRepository<Demand>>.GetConcrete<Demand>();
            IList<DemandDspModel> mylist = new List<DemandDspModel>();
            foreach (Demand mygoods in articleRep.FindAll())
            {
                mylist.Add(new DemandDspModel() { BusinessmanName=mygoods.BusinessmanName, CustomerName=mygoods.CustomerName, CustomerOtherConnect=mygoods.CustomerOtherConnect, CustomerPhoneNum=mygoods.CustomerPhoneNum, DemandID=mygoods.DemandID, DemandCity=mygoods.DemandCity, DemandContent=mygoods.DemandContent, GroupOnType=mygoods.GroupOnType, });
            }
            return mylist;
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId"></param>
        public void RemoveDemandByID(Guid demandId)
        {
            IRepository<Demand> articleRep = Factory.Factory<IRepository<Demand>>.GetConcrete<Demand>();

            articleRep.RemoveByKey(demandId);
            articleRep.PersistAll();
        }

    }
}
