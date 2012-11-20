using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using System.Data;
using FBS.Repository.Persistence;

namespace FBS.Repository
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class BlogStoryRepository:System.ContextBoundObject,IBlogStoryRepository
    {
        #region 博文集合
        private HashSet<BlogStory> _storys;
        private HashSet<BlogStory> _newStorys;
        private HashSet<BlogStory> _modifiedStorys;
        private HashSet<BlogStory> _removedStorys;
        #endregion

        #region 评论维护集合

        Dictionary<Guid, HashSet<BlogComment>> _commentDic = null;
        Dictionary<Guid, HashSet<BlogComment>> _newCommentDic = null;
        Dictionary<Guid, HashSet<BlogComment>> _modifiedCommentDic = null;
        Dictionary<Guid, HashSet<BlogComment>> _removedCommentDic = null;
        #endregion

        public BlogStoryRepository()
        {
            #region 初始化博文集合
            this._storys = new HashSet<BlogStory>();
            this._newStorys = new HashSet<BlogStory>();
            this._modifiedStorys = new HashSet<BlogStory>();
            this._removedStorys = new HashSet<BlogStory>();
            #endregion

            #region 初始化评论集合
            this._commentDic = new Dictionary<Guid, HashSet<BlogComment>>();
            this._newCommentDic = new Dictionary<Guid, HashSet<BlogComment>>();
            this._modifiedCommentDic = new Dictionary<Guid, HashSet<BlogComment>>();
            this._removedCommentDic = new Dictionary<Guid, HashSet<BlogComment>>();
            
            #endregion

            this._storys = BlogStoryPersist.GetAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public void AddComment(BlogComment model)
        {
            lock (this._newCommentDic)
            {
                if (!this._newCommentDic.ContainsKey(model.TargetId))
                    this._newCommentDic.Add(model.TargetId, new HashSet<BlogComment>());

                this._newCommentDic[model.TargetId].Add(model);
            }
        }

        public void UpdateComment(BlogComment model)
        {
            lock (this._modifiedCommentDic)
            {
                if (!this._modifiedCommentDic.ContainsKey(model.TargetId))
                    this._modifiedCommentDic.Add(model.TargetId, new HashSet<BlogComment>());

                this._modifiedCommentDic[model.TargetId].Add(model);
            }
        }

        public void RemoveComment(Guid commentId,Guid storyId)
        {
            BlogComment temp = null;
            lock (this._commentDic)
            {
                if (this._commentDic.ContainsKey(storyId))
                    try
                    {
                        temp = this._commentDic[storyId].SingleOrDefault(c => c.Id == commentId);
                    }
                    catch (InvalidOperationException) { }

                if (temp != null)
                {
                    this._commentDic[storyId].Remove(temp);

                    if (!this._removedCommentDic.ContainsKey(storyId))
                        this._removedCommentDic.Add(storyId, new HashSet<BlogComment>());
                    
                    this._removedCommentDic[storyId].Add(temp);
                }
            }

        }

        /// <summary>
        /// 获取评论
        /// </summary>
        /// <param name="id">博文编号</param>
        /// <returns></returns>
        public IList<BlogComment> GetComments(Guid id)
        {
            if (!this._commentDic.ContainsKey(id))
            {//本地缓存中没有，另外加载
                BlogStoryPersist.LoadCommentsByStoryID(this._commentDic,id);
            }

            return this._commentDic[id].ToList();
        }

        /// <summary>
        /// 获取用户的博文
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>博文列表</returns>
        public IList<BlogStory> GetStorys(Guid userId)
        {
            return null;
        }

        #region IRepository<BlogStory> 成员

        public void Add(BlogStory entity)
        {
            lock (this._storys)
            {
                this._storys.Add(entity);

                this._newStorys.Add(entity);
            }
        }

        public bool Exists(FBS.Domain.Specifications.ISpecification<BlogStory> specification)
        {
            return this._storys.Any(specification.GetExpression().Compile());
        }

        public BlogStory Find(FBS.Domain.Specifications.ISpecification<BlogStory> specification)
        {
            BlogStory story = null;
            try
            {
                this._storys.SingleOrDefault(specification.GetExpression().Compile());
            }
            catch(InvalidOperationException){}

            return story;
        }

        public BlogStory GetByKey(Guid id)
        {
            BlogStory story = null;
            try
            {
                story = this._storys.Single(s => s.Id == id);
            }
            catch (InvalidOperationException) { }

            return story;
        }

        public IList<BlogStory> FindAll()
        {
            return this._storys.ToList();
        }

        public IList<BlogStory> FindAll(FBS.Domain.Specifications.ISpecification<BlogStory> specification)
        {
            return this._storys.TakeWhile(specification.GetExpression().Compile()).ToList();
        }

        public void Remove(BlogStory entity)
        {
            this._storys.Remove(entity);
            this._removedStorys.Add(entity);
        }

        public void Update(BlogStory entity)
        {
            this._storys.Remove(entity);
            this._storys.Add(entity);

            this._modifiedStorys.Add(entity);
        }

        public void PersistAll()
        {
            #region 转换成DataTable
            //新添加元素
            DataTable newStorysTable = new DataTable("NewStorys");
            DataTable newCommentsTable = new DataTable("NewComments");

            //修改过的元素
            DataTable modifiedStorysTable = new DataTable("ModifiedStorys");
            DataTable modifiedCommentsTable = new DataTable("ModifiedComments");

            //被删除的元素
            DataTable removedStorysTable = new DataTable("RemovedStorys");
            DataTable removedCommentsTable = new DataTable("RemovedComments");

            //新博文
            foreach (BlogStory bs in this._newStorys)
                bs.AlterToRow(newStorysTable);

            //新回复
            foreach (Guid id in this._newCommentDic.Keys)
                foreach (BlogComment c in this._newCommentDic[id])
                    c.AlterToRow(newCommentsTable);
            
            //更新的博文
            foreach (BlogStory bs in this._modifiedStorys)
                bs.AlterToRow(modifiedStorysTable);
            //更新的回复
            foreach (Guid id in this._modifiedCommentDic.Keys)
                foreach (BlogComment c in this._modifiedCommentDic[id])
                    c.AlterToRow(modifiedCommentsTable);

            //删除的博文
            foreach (BlogStory bs in this._removedStorys)
                bs.AlterToRow(removedStorysTable);

            //删除的回复
            foreach (Guid id in this._removedCommentDic.Keys)
                foreach (BlogComment c in this._removedCommentDic[id])
                    c.AlterToRow(removedCommentsTable);

            #endregion

            #region 持久化

            //添加
            //BlogStoryPersist.AddAll(newStorysTable);
            TPersist<BlogStory>.PersistAll(newStorysTable,DbOperation.Insert);
            TPersist<BlogComment>.PersistAll(newCommentsTable, DbOperation.Insert);
            //BlogStoryPersist.AddComments(newCommentsTable);

            //更新
            TPersist<BlogStory>.PersistAll(modifiedStorysTable, DbOperation.Update);
            //TPersist<BlogComment>.PersistAll(modifiedCommentsTable, DbOperation.Update);

            //删除
            //BlogStoryPersist.RemoveAll(removedStorysTable);
            TPersist<BlogStory>.PersistAll(removedStorysTable, DbOperation.Delete);
            TPersist<BlogComment>.PersistAll(removedCommentsTable, DbOperation.Delete);

            #endregion

            #region 释放内存

            this._newStorys.Clear();
            this._newCommentDic.Clear();

            this._removedStorys.Clear();
            this._removedCommentDic.Clear();
            this._modifiedStorys.Clear();
            this._modifiedCommentDic.Clear();
            #endregion
        }

        /// <summary>
        /// 以键值删除
        /// </summary>
        /// <param name="id">键值</param>
        public void RemoveByKey(Guid id)
        {
            BlogStory storyToRemove = null;

            foreach (BlogStory story in this._storys)
                if (story.Id == id)
                {
                    storyToRemove = story;
                    break;
                }


            //从内存中加载到对象
            if (storyToRemove != null)
            {
                this._storys.Remove(storyToRemove);

                this._removedStorys.Add(storyToRemove);
            }
        }


        public int Count(FBS.Domain.Specifications.ISpecification<BlogStory> specification)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
