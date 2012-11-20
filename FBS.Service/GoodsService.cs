using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;

namespace FBS.Service
{
    public class GoodsService
    {
        /// <summary>
        /// 创建商品
        /// </summary>
        /// <param name="model">新建广告模型</param>
        public void CreateGoods(GoodsDspModel model)
        {
            IRepository<Goods> rep = Factory.Factory<IRepository<Goods>>.GetConcrete<Goods>();

            try
            {
                //string goodsname,float goodsnowprice,string goodspicurl,float goodsoldprice,int goodsbuycount,DateTime goodsbegintime,DateTime goodsendtime,string goodsdetailscontent,bool goodsison
                rep.Add(new Goods(model.GoodsID,model.GoodsName,model.GoodsNowPrice,model.GoodsPicURL,model.GoodsOldPrice,model.GoodsBuyCount,model.GoodsBeginTime,model.GoodsEndTime,model.GoodsDetailsContent,model.GoodsIsOn));
                rep.PersistAll();
            }
            catch { }
        }

        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="model">新建商品模型</param>
        public GoodsDetailsModel GetOneGoodsContentByID(Guid aid)
        {
            IRepository<Goods> rep = Factory.Factory<IRepository<Goods>>.GetConcrete<Goods>();

            Goods goods = null;
            GoodsDetailsModel target = null;

            try
            {
                goods = rep.GetByKey(aid);

                target = new GoodsDetailsModel()
                {
                     
                     GoodsBeginTime=goods.GoodsBeginTime,
                     GoodsBuyCount=goods.GoodsBuyCount,
                     GoodsDetailsContent=goods.GoodsDetailsContent,
                     GoodsEndTime=goods.GoodsEndTime,
                     GoodsIsOn=goods.GoodsIsOn,
                     GoodsName=goods.GoodsName,
                     GoodsNowPrice=goods.GoodsNowPrice,
                     GoodsOldPrice=goods.GoodsOldPrice,
                     GoodsPicURL=goods.GoodsPicURL,
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
        public IList<GoodsDspModel> FetchGoods()
        {
            IRepository<Goods> articleRep = Factory.Factory<IRepository<Goods>>.GetConcrete<Goods>();
            IList<GoodsDspModel> mylist = new List<GoodsDspModel>();
            foreach (Goods myarti in articleRep.FindAll())
            {
                mylist.Add(new GoodsDspModel() { GoodsID = myarti.GoodsID, GoodsBeginTime = myarti.GoodsBeginTime, GoodsBuyCount = myarti.GoodsBuyCount, GoodsDetailsContent = myarti.GoodsDetailsContent, GoodsEndTime = myarti.GoodsEndTime, GoodsIsOn = myarti.GoodsIsOn, GoodsName = myarti.GoodsName, GoodsNowPrice = myarti.GoodsNowPrice, GoodsOldPrice = myarti.GoodsOldPrice, GoodsPicURL = myarti.GoodsPicURL });
            }
            return mylist;
        }


        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="articleId"></param>
        public void RemoveGoodsByID(Guid articleId)
        {
            IRepository<Goods> articleRep = Factory.Factory<IRepository<Goods>>.GetConcrete<Goods>();

            articleRep.RemoveByKey(articleId);
            articleRep.PersistAll();
        }


        /// <summary>
        /// 修改商品
        /// </summary>
        /// <param name="model">修改商品</param>
        public void UpdateGoods(GoodsDspModel model)
        {
            IRepository<Goods> rep = Factory.Factory<IRepository<Goods>>.GetConcrete<Goods>();

            Goods a = null;
            try
            {
                a = rep.GetByKey(model.GoodsID);
                a.GoodsBeginTime = model.GoodsBeginTime;
                a.GoodsBuyCount = model.GoodsBuyCount;
                a.GoodsDetailsContent = model.GoodsDetailsContent;
                a.GoodsEndTime = model.GoodsEndTime;
                a.GoodsIsOn = model.GoodsIsOn;
                a.GoodsName = model.GoodsName;
                a.GoodsNowPrice = model.GoodsNowPrice;
                a.GoodsOldPrice = model.GoodsOldPrice;
                a.GoodsPicURL = model.GoodsPicURL;

                rep.Update(a);
                rep.PersistAll();
            }
            catch { }
        }
    }
}
