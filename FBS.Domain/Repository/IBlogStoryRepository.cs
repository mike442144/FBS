using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FBS.Domain.Aggregate.Entity;

namespace FBS.Domain.Repository
{
    public interface IBlogStoryRepository:IRepository<BlogStory>
    {
        void AddComment(BlogComment model);
        void UpdateComment(BlogComment model);
        void RemoveComment(Guid commentId,Guid storyId);
        IList<BlogComment> GetComments(Guid storyId);
    }
}
