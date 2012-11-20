using System;
using System.Collections.Generic;
using System.Text;
using FBS.Domain.Aggregate.Entity;
using System.Collections;

namespace FBS.Domain.Aggregate.ValueObject
{
    [Serializable]
    public class ThreadTagsVO
    {
        private ForumThread _forumThread;
        private IList<ThreadTag> _tags;
        private IList<ThreadTag> _lastTags;
        private bool _reload;

        public ThreadTagsVO(ForumThread forumThread)
        {
            this._forumThread = forumThread;
            this._tags = new List<ThreadTag>();
            this._lastTags = new List<ThreadTag>();
        }

        public string[] TagTitles
        {
            get { return this._forumThread.RootMessage.MessageVO.TagTitle; }
        }

        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="tags">标签类集合</param>
        public void SetTags(IList<ThreadTag> tags)
        {
            this._tags = tags;
            string[] tagTitles=new string[tags.Count];
            int i = -1;
            foreach (ThreadTag tag in tags)
            {
                tagTitles[++i] = tag.Title;
            }
            this._forumThread.RootMessage.MessageVO.TagTitle = tagTitles;
        }

        /// <summary>
        /// 变更主题标签
        /// </summary>
        /// <param name="tagTitles">标签标题数组</param>
        public void ChangeTags(string[] tagTitles)
        {
            if (tagTitles == null || tagTitles.Length == 0)
                return;
            this._lastTags = this._tags;
            this._tags = ArrayConvertToList(tagTitles);

            /*
             添加变更标签事件
             */

            this._reload = true;

        }

        /// <summary>
        /// 把标签标题转化为标签对象
        /// </summary>
        /// <param name="tagArray">标签标题数组</param>
        /// <returns>标签对象</returns>
        private IList<ThreadTag> ArrayConvertToList(string[] tagArray)
        {
            IList<ThreadTag> tags = new List<ThreadTag>();
            foreach (string title in tagArray) 
            {
                ThreadTag tag = new ThreadTag();
                if (!string.IsNullOrEmpty(title))
                {
                    tag.Title = title;
                    tags.Add(tag);
                }
            }

            return tags;
        }
    }
}
