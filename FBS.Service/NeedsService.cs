using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;

namespace FBS.Service
{
    public class NeedsService
    {
        /// <summary>
        /// 创建需求
        /// </summary>
        /// <param name="model">新建需求模型</param>
        public void CreateNeeds(NewNeedsModel model)
        {
            IRepository<Needs> rep = Factory.Factory<IRepository<Needs>>.GetConcrete<Needs>();

            try
            {
                rep.Add(new Needs(model.NeedsID,model.NeedsContent));
                rep.PersistAll();
            }
            catch { }
        }


        /// <summary>
        /// 获取需求详细内容
        /// </summary>
        /// <param name="aid">需求详细模型</param>
        public NeedsDetailsModel GetOneNeedsContentByID(Guid aid)
        {
            IRepository<Needs> rep = Factory.Factory<IRepository<Needs>>.GetConcrete<Needs>();

            Needs needs = null;
            NeedsDetailsModel target = null;

            try
            {
                needs = rep.GetByKey(aid);

                target = new NeedsDetailsModel()
                {
                    NeedsContent=needs.NeedsContent,
                };
            }

            catch (Exception error)
            {
                throw new Exception(error.Message);
            }

            return target;
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId"></param>
        public void RemoveNeedsByID(Guid needsId)
        {
            IRepository<Needs> Rep = Factory.Factory<IRepository<Needs>>.GetConcrete<Needs>();

            Rep.RemoveByKey(needsId);
            Rep.PersistAll();
        }


        /// <summary>
        /// 列出所有广告页面
        /// </summary>
        /// 
        public IList<NeedsDspModel> FetchNeedsDspModel()
        {
            IRepository<Needs> Rep = Factory.Factory<IRepository<Needs>>.GetConcrete<Needs>();
            IList<NeedsDspModel> mylist = new List<NeedsDspModel>();
            foreach (Needs myneeds in Rep.FindAll())
            {
                mylist.Add(new NeedsDspModel() {  NeedsContent=myneeds.NeedsContent, NeedsID=myneeds.NeedsID });
            }
            return mylist;
        }
    }
}
