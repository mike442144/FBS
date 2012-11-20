using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Service.ActionModels;
using FBS.Web.Share;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Repository;

namespace FBS.Service
{
    public class VideoService
    {
        /// <summary>
        /// 分享新视频
        /// </summary>
        /// <param name="model">新视频模型</param>
        public void NewVideo(NewVideoModel model)
        {
            ShareThread st = null;
            VideoShareProvider.ParseHtml(model.RawUrl, ref st);
            string identify = DateTime.Now.Ticks.ToString();
            
            //如果评论为空则用视频默认标题
            string subject = string.IsNullOrEmpty(model.Comment) ? st.Subject : model.Comment;

            string finalSubject = "<a href='##' onclick=\"playVideo('"+identify+"')\" >" + subject + "</a>";
            
            //视频内容html
            string content = "<div class='video' id='prev_"+identify+"' style='background-image: url("+st.ThumbnailUrl+");'><a onclick=\"playVideo('"+identify+"')\" href='###'>播放</a></div>";
            content += "<div class='blogPicOri' id='disp_"+identify+"' style='visibility: visible; display: none; '><p><cite><a onclick=\"stopVideo('"+identify+"')\" title='收起' href='###'>收起</a></cite><cite class='MIB_line_l'>|</cite><cite class='MIB_line_l'>|</cite></p>";

            //播放器脚本
            content+="<embed id='JSONP_167HAG6ED_9' height='356' allowscriptaccess='always' style='visibility: visible;' pluginspage='http://get.adobe.com/cn/flashplayer/' flashvars='playMovie=true&amp;auto=1&amp;adss=0' width='380' allowfullscreen='true' quality='hight' src='" + st.PlayUrl + "' type='application/x-shockwave-flash' wmode='transparent'></div>";
            
            NewFeedModel fmodel = new NewFeedModel() {Sharer=model.Sharer,Type=FeedType.NewVideo,Subject=finalSubject,Content=content };
            BlogService bservice = new BlogService();
            ShareThreadService shareservice = new ShareThreadService();
            NewShareThreadModel sharemodel=new NewShareThreadModel(){ Body=st.Body, PlayUrl=st.PlayUrl, RawUrl=model.RawUrl, ShareTime=DateTime.Now, Source="博客", Subject=st.Subject, ThumbnailUrl=st.ThumbnailUrl};
            shareservice.CreateShareThread(sharemodel);
            bservice.CreateFeed(fmodel);
        }

        /// <summary>
        /// 分享视频
        /// </summary>
        /// <param name="model">分享模型</param>
        public void ShareVideo(SiteShareModel model)
        {
            IRepository<Feed> feedRep = Factory.Factory<IRepository<Feed>>.GetConcrete<Feed>();
            Feed feed=feedRep.GetByKey(model.FeedID);

            BlogService bservice = new BlogService();

            string subject = feed.Subject.Substring(feed.Subject.LastIndexOf("<a"));

            NewFeedModel fmodel = new NewFeedModel() {Sharer=model.Sharer,SourceUser=model.SourceUser,Type=FeedType.ShareVideo,Content=feed.Content,Subject=subject };

            bservice.CreateFeed(fmodel);
        }
    }
}
