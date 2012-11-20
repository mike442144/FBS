using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Aggregate.ValueObject
{
    /// <summary>
    /// 账户值模型
    /// </summary>
    [Serializable]
    public struct AccountMessageVO
    {

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">账户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="head">头像图片</param>
        /// <param name="tiny">头像缩略图</param>
        public AccountMessageVO(Guid id,string userName,string head,string tiny)
        {
            this._id = id;
            this._userName = userName;
            this._head = head;
            this._tiny = tiny;
            this._points = -1;//invalid state default.

            CheckState();//check if the value object is valid
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">账户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="head">头像图片</param>
        /// <param name="tiny">头像缩略图</param>
        /// <param name="points">积分</param>
        public AccountMessageVO(Guid id, string userName, string head, string tiny, int points)
        {
            this._id = id;
            this._userName = userName;
            this._head = head;
            this._tiny = tiny;
            this._points = points;

            CheckState();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">账户ID</param>
        /// <param name="userName">用户名</param>
        public AccountMessageVO(Guid id, string userName)
        {
            this._id=id;
            this._userName=userName;

            this._head = "unknow";
            this._tiny = "unknow";
            this._points = -1;

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">账户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="head">头像图片</param>
        public AccountMessageVO(Guid id, string userName, string head)
        {
            this._id = id;
            this._userName = userName;
            this._head = head;

            this._tiny = "unknow";
            this._points = -1;
        }

        #endregion

        #region 辅助函数
        /// <summary>
        /// 检查状态
        /// </summary>
        private void CheckState()
        {
            if (this._id == Guid.Empty)
                throw new ArgumentException("id：传入Guid类型参数为空");

            bool hasNullRef = string.IsNullOrEmpty(this._userName) && 
                              string.IsNullOrEmpty(this._head) && 
                              string.IsNullOrEmpty(this._tiny);
            if (hasNullRef)
                throw new ArgumentNullException();
        }

        #endregion

        #region 私有属性

        private readonly Guid _id;
        private readonly string _userName;
        private int _points;
        private readonly string _head;
        private readonly string _tiny;

        #endregion

        #region 公共属性

        public string UserName
        {
            get 
            { 
                return 
                    string.IsNullOrEmpty(this._userName) ? string.Empty : this._userName; 
            }
        }

        public Guid Id
        {
            get { return this._id; }
        }

        public int Points
        {
            get { return this._points; }
            set 
            { 
                if(value>0)
                    this._points = value;
            }
        }

        public string Head
        { 
            get 
            { 
                return 
                    string.IsNullOrEmpty(this._head) ? string.Empty : this._head; 
            } 
        }

        public string Tiny
        {
            get 
            {
                return
                    string.IsNullOrEmpty(this._tiny) ? string.Empty : this._tiny;
            }
        }

        #endregion

        #region 重写 instance Equals()
        public override bool Equals(object obj)
        {
            AccountMessageVO rightObj;
            bool doesEqual = true;

            if (obj == null)
                return false;
            if (obj is AccountMessageVO)
                rightObj = (AccountMessageVO)obj;
            else
                return false;

            doesEqual=(
                rightObj.Id==this.Id&&
                rightObj.UserName==this.UserName&&
                rightObj.Head==this.Head&&
                rightObj.Tiny==this.Tiny&&
                rightObj.Points==this.Points
                );

            return doesEqual;
        }

        #endregion
    }
}
