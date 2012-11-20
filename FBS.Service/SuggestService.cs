using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Service.ActionModels;
using FBS.Domain.Specifications;

namespace FBS.Service
{
    [FBS.Domain.Log.Logging()]
    public class SuggestService:ContextBoundObject
    {
        /// <summary>
        /// 删除某建议
        /// </summary>
        /// <param name="id"></param>
        public void RemoveSuggest(Guid id)
        {
            IRepository<Suggest> suggestRep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();
            try
            {
                suggestRep.RemoveByKey(id);
                suggestRep.PersistAll();
            }
            catch
            { }
            
        }

        /// <summary>
        /// 添加建议
        /// </summary>
        /// <param name="model">建议模型</param>
        public void CreateSuggest(SuggestModel model)
        {
            IRepository<Suggest> rep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();
            Suggest s = new Suggest(model.Body, model.UserID, model.UserName, model.Reply, model.Type);
            try
            {
                rep.Add(s);
                rep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 获取引领区下的所有建议
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">获取数目</param>
        /// <returns></returns>
        public IList<SuggestModel> FetchIntroduceSuggest( int startIndex, int count)
        {
            IRepository<Suggest> suggestRep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();
            IList<Suggest> alist = null;
            IList<SuggestModel> target = new List<SuggestModel>();
            try
            {
                
                alist = suggestRep.FindAll(new Specification<Suggest>(s=>s.Type=="来自引领区").Skip(startIndex).Take(count).OrderByDescending(s => s.CreationDate));
                foreach (Suggest a in alist)
                {
                    SuggestModel tmp = new SuggestModel() { SuggestID=a.Id, UserID=a.UserID, Body=a.Body, CreationDate=a.CreationDate, UserName=a.UserName, Reply=a.Reply, Type =a.Type };
                    target.Add(tmp);
                }
            }
            catch { }

            return target;
        }
        /// <summary>
        /// 获取建议直通下的所有建议
        /// </summary>
        /// <param name="startIndex">起始索引</param>
        /// <param name="count">获取数目</param>
        /// <returns></returns>
        public IList<SuggestModel> FetchSuggest(int startIndex, int count)
        {
            IRepository<Suggest> suggestRep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();
            IList<Suggest> alist = null;
            IList<SuggestModel> target = new List<SuggestModel>();
            try
            {

                alist = suggestRep.FindAll(new Specification<Suggest>(s => s.Type == "来自建议直通").Skip(startIndex).Take(count).OrderByDescending(s => s.CreationDate));
                foreach (Suggest a in alist)
                {
                    SuggestModel tmp = new SuggestModel() { SuggestID = a.Id, UserID = a.UserID, Body = a.Body, CreationDate = a.CreationDate, UserName = a.UserName, Reply = a.Reply, Type = a.Type };
                    target.Add(tmp);
                }
            }
            catch { }

            return target;
        }
        /// <summary>
        /// 不分页获取建议
        /// </summary>
        /// <returns></returns>
        public IList<SuggestModel> GetAllSuggest(int startIndex, int count)
        {
            IRepository<Suggest> suggestRep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();
            IList<Suggest> alist = null;
            IList<SuggestModel> target = new List<SuggestModel>();
            try
            {

                alist = suggestRep.FindAll(new Specification<Suggest>(s => s.Id != Guid.Empty).Skip(startIndex).Take(count).OrderByDescending(s => s.CreationDate));
                foreach (Suggest a in alist)
                {
                    SuggestModel tmp = new SuggestModel() { SuggestID = a.Id, UserID = a.UserID, Body = a.Body, CreationDate = a.CreationDate, UserName = a.UserName, Reply = a.Reply, Type = a.Type };
                    target.Add(tmp);
                }
            }
            catch { }

            return target;
        }

        /// <summary>
        /// 回复建议
        /// </summary>
        /// <param name="model"></param>
        public void ReplySuggest(SuggestModel model)
        {
            IRepository<Suggest> rep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();

            Suggest a = null;
            try
            {
                a = rep.GetByKey(model.SuggestID);
                a.Reply = model.Reply;
              
                rep.Update(a);
                rep.PersistAll();
            }
            catch{ }
        }
        /// <summary>
        /// 某个建议
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SuggestModel GetOneSuggest(Guid id)
        {
            IRepository<Suggest> rep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();
            Suggest a = null;
            SuggestModel model = new SuggestModel();
            try
            {
                a = rep.GetByKey(id);
                model.Body = a.Body;
                model.CreationDate = a.CreationDate;
                model.Reply = a.Reply;
                model.SuggestID = a.Id;
                model.UserID = a.UserID;
                model.UserName = a.UserName;
                model.Type = a.Type;

            }
            catch
            { }
            return model;
        }

        /// <summary>
        /// 获取建议直通下的总数
        /// </summary>
       
        /// <returns>文章数</returns>
        public int GetSuggestCount()
        {
            IRepository<Suggest> suggestRep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();
            int target = 0;
            try
            {
                target = suggestRep.FindAll(new Specification<Suggest>(s=>s.Type=="来自建议直通")).Count;
            }
            catch
            { }

            return target;
        }

        /// <summary>
        /// 获取引领区下建议数
        /// </summary>

        /// <returns>文章数</returns>
        public int GetIntroduceSuggestCount()
        {
            IRepository<Suggest> suggestRep = Factory.Factory<IRepository<Suggest>>.GetConcrete<Suggest>();
            int target = 0;
            try
            {
                target = suggestRep.FindAll(new Specification<Suggest>(s => s.Type == "来自引领区")).Count;
            }
            catch
            { }

            return target;
        }

    }
}
