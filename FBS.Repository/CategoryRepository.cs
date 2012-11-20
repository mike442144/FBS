using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Repository.Persistence;
using System.Data;
using FBS.Domain.Aggregate.Entity;
using FBS.Domain.Aggregate.ValueObject;

namespace FBS.Repository
{
    public class CategoryRepository
    {
        private HashSet<CategoryVO> _list = null;
        private HashSet<CategoryVO> _questionCategoryList = null;

        public CategoryRepository()
        {
            _list = new HashSet<CategoryVO>();
            this._questionCategoryList = new HashSet<CategoryVO>();
        }

        public void Add(CategoryVO category)
        {
            this._list.Add(category);
        }

        public void AddQuestionCategory(CategoryVO category)
        {
            this._questionCategoryList.Add(category);
        }

        public void PersistAll()
        {
            foreach (CategoryVO c in this._list)
                BlogCategoryPersist.Add(c);

            foreach (CategoryVO c in this._questionCategoryList)
                BlogCategoryPersist.AddQuestionCategory(c);

            this._list.Clear();
            this._questionCategoryList.Clear();
        }
    }

}
