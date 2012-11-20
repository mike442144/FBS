using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Repository;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Specifications;
using FBS.Repository.Persistence;
using System.Data;

namespace FBS.Repository
{
    public class BlogQuestionRepository:IBlogQuestionRepository
    {
        #region 问题集合
        private HashSet<BlogQuestion> _questions = null;
        private HashSet<BlogQuestion> _newQ = null;
        private HashSet<BlogQuestion> _removedQ = null;
        private HashSet<BlogQuestion> _modifiedQ = null;
        #endregion

        #region 回答集合

        Dictionary<Guid, HashSet<BlogAnswer>> _answerDic = null;
        Dictionary<Guid, HashSet<BlogAnswer>> _newanswerDic = null;
        Dictionary<Guid, HashSet<BlogAnswer>> _modifiedanswerDic = null;
        Dictionary<Guid, HashSet<BlogAnswer>> _removedanswerDic = null;

        #endregion

        public BlogQuestionRepository()
        {
            #region 初始化问题集合
            this._questions = new HashSet<BlogQuestion>();
            this._newQ = new HashSet<BlogQuestion>();
            this._modifiedQ = new HashSet<BlogQuestion>();
            this._removedQ = new HashSet<BlogQuestion>();
            #endregion


            #region 初始化答案集合
            this._answerDic = new Dictionary<Guid, HashSet<BlogAnswer>>();
            this._newanswerDic = new Dictionary<Guid, HashSet<BlogAnswer>>();
            this._modifiedanswerDic = new Dictionary<Guid, HashSet<BlogAnswer>>();
            this._removedanswerDic = new Dictionary<Guid, HashSet<BlogAnswer>>();
            #endregion


            //初始化加载问题集合
            this._questions = BlogQuestionPersist.GetAllQuestions();
        }

        /// <summary>
        /// 回答问题
        /// </summary>
        /// <param name="answer">答案</param>
        public void AnswerToQuestion(BlogAnswer answer)
        {
            if (!this._newanswerDic.ContainsKey(answer.QuestionId))
                this._newanswerDic.Add(answer.QuestionId, new HashSet<BlogAnswer>());

            this._newanswerDic[answer.QuestionId].Add(answer);
        }


        #region IRepository<BlogQuestion> 成员

        /// <summary>
        /// 添加回答
        /// </summary>
        /// <param name="entity"></param>
        public void Add(BlogQuestion entity)
        {
            this._questions.Add(entity);
            this._newQ.Add(entity);
        }

        /// <summary>
        /// 检查是否存在
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public bool Exists(ISpecification<BlogQuestion> specification)
        {
            return this._questions.Any(specification.GetExpression().Compile());
        }

        /// <summary>
        /// 找出满足条件的对象
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public BlogQuestion Find(ISpecification<BlogQuestion> specification)
        {
            return this._questions.SingleOrDefault(specification.GetExpression().Compile());
        }

        /// <summary>
        /// 按键值取出
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogQuestion GetByKey(Guid id)
        {
            BlogQuestion question = null;
            try
            {
                question = this._questions.Single(q => q.Id == id);
            }
            catch { }

            return question;
        }

        public IList<BlogQuestion> FindAll()
        {
            throw new NotImplementedException();
        }

        public IList<BlogQuestion> FindAll(ISpecification<BlogQuestion> specification)
        {
            throw new NotImplementedException();
        }

        public void Remove(BlogQuestion entity)
        {
            this._questions.Remove(entity);
            this._removedQ.Add(entity);
        }

        public void Update(BlogQuestion entity)
        {
            this._questions.Remove(entity);
            this._questions.Add(entity);

            this._modifiedQ.Add(entity);
        }

        public void PersistAll()
        {
            #region 转换成DataTable
            //新添加元素
            DataTable newQuestionsTable = new DataTable("NewQuestions");
            DataTable newAnswersTable = new DataTable("NewAnswers");

            //修改过的元素
            DataTable modifiedQuestionsTable = new DataTable("ModifiedQuestions");


            //被删除的元素
            DataTable removedQuestionsTable = new DataTable("RemovedQuestions");

            //新博文
            foreach (BlogQuestion q in this._newQ)
                q.AlterToRow(newQuestionsTable);

            //新回复
            foreach (Guid id in this._newanswerDic.Keys)
                foreach (BlogAnswer a in this._newanswerDic[id])
                    a.AlterToRow(newAnswersTable);

            //更新的博文
            foreach (BlogQuestion q in this._modifiedQ)
                q.AlterToRow(modifiedQuestionsTable);

            //删除的博文
            foreach (BlogQuestion q in this._removedQ)
                q.AlterToRow(removedQuestionsTable);



            #endregion

            #region 持久化

            //添加
            BlogQuestionPersist.AddAll(newQuestionsTable);
            BlogQuestionPersist.AnswerToQuestion(newAnswersTable);

            //更新
            BlogQuestionPersist.UpdateAll(modifiedQuestionsTable);

            //删除
            //BlogQuestionPersist.RemoveAll(removedQuestionsTable);


            #endregion

            #region 释放内存

            this._newQ.Clear();
            this._newanswerDic.Clear();

            this._removedQ.Clear();
            this._modifiedQ.Clear();
            #endregion
        }


        public IList<BlogQuestion> FetchQuestions(int startIndex, int count, Guid categoryId)
        {
            return BlogQuestionPersist.GetList(categoryId, startIndex, count);
        }

        public void RemoveByKey(Guid id)
        {
            throw new NotImplementedException();
        }


        public int Count(ISpecification<BlogQuestion> specification)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
