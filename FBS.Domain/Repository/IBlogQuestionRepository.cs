using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Repository
{
    public interface IBlogQuestionRepository:IRepository<BlogQuestion>
    {
        void AnswerToQuestion(BlogAnswer answer);
        IList<BlogQuestion> FetchQuestions(int startIndex, int count, Guid categoryId);
    }
}
