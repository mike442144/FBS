using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FBS.Domain.Aggregate.ValueObject
{
    /// <summary>
    /// 文章的值模型
    /// </summary>
    [Serializable]
    public struct ArticleVO
    {
        #region 构造函数

        /// <summary>
        /// 文章值模型构造函数
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="briefTitle">副标题</param>
        /// <param name="body">正文</param>
        /// <param name="clickCount">点击数</param>
        /// <param name="commentCount">评论数</param>
        /// <param name="sourceUrl">原始url</param>
        /// <param name="sourceSite">来源网站</param>
        public ArticleVO(string title, string briefTitle, string body, int clickCount, 
            int commentCount,string sourceUrl, string sourceSite)
        {
            this._title = title;
            this._briefTitle = briefTitle;
            this._body = body;
            this._clickCount = clickCount;
            this._commentCount = commentCount;
            this._sourceUrl = sourceUrl;
            this._sourceSite = sourceSite;
        }

        #endregion

        #region 公共属性

        public string Title 
        { 
            get 
            { 
                return 
                    string.IsNullOrEmpty(this._title) ? string.Empty : this._title; 
            } 
        }
        public string BriefTitle 
        { 
            get 
            { 
                return 
                    string.IsNullOrEmpty(this._briefTitle) ? string.Empty : this._briefTitle; 
            } 
        }
        public string Body 
        { 
            get 
            { 
                return 
                    string.IsNullOrEmpty(this._body) ? string.Empty : this._body; 
            } 
        }

        public int ClickCount 
        { 
            get 
            { 
                return this._clickCount; 
            } 
            set 
            { 
                this._clickCount = value; 
            } 
        }

        public int CommentCount 
        {
            get 
            {
                return this._commentCount;
            }
            set 
            {
                this._commentCount = value;
            }
        }

        public string SourceUrl 
        { 
            get 
            { 
                return 
                    string.IsNullOrEmpty(this._sourceUrl) ? string.Empty : this._sourceUrl; 
            } 
        }

        public string SourceSite 
        {
            get 
            {
                return
                    string.IsNullOrEmpty(this._sourceSite) ? string.Empty : this._sourceSite;
            }
        }
        #endregion

        #region 私有属性

        private readonly string _title;
        private readonly string _briefTitle;
        private readonly string _body;
        private int _clickCount;
        private int _commentCount;
        private readonly string _sourceUrl;
        private readonly string _sourceSite;
        #endregion

        #region 重写 instance Equls()

        public override bool Equals(object obj)
        {
            ArticleVO rightObj;//右操作数
            bool doesEqual=true;

            if (obj == null)
                return false;

            if (obj is ArticleVO)
                rightObj = (ArticleVO)obj;
            else
                return false;

            //判断是否所有属性都相等
            doesEqual = (rightObj.Title == this.Title &&
                        rightObj.BriefTitle == this.BriefTitle &&
                        rightObj.Body == this.Body &&
                        rightObj.ClickCount == this.ClickCount &&
                        rightObj.CommentCount == this.CommentCount);

            return doesEqual;
        }
        #endregion
    }
}
