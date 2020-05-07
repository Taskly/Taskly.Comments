using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taskly.Comments.Application.Entities;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application
{
    public class CommentsService : ICommentsService
    {
        public CommentsService(CommentsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Comment>> GetCommentsByLocator(Locator locator, bool includeDeleted = false)
        {
            IQueryable<CommentEntity> query = _dbContext.Comments.AsQueryable();

            List<CommentEntity> entities = await query.ToListAsync();
            return entities.Select(x => x.ToModel()).ToList();
        }

        public Task<List<Comment>> GetCommentsByAuthor(string authorId, bool includeDeleted = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> AddComment(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> AddReply(string parentId, Comment comment)
        {
            throw new System.NotImplementedException();
        }

        public Task<Comment> MarkAsDeleted(Comment comment)
        {
            throw new System.NotImplementedException();
        }

        private IQueryable<CommentEntity> ApplyLocator(IQueryable<CommentEntity> query, Locator locator)
        {
            if (!string.IsNullOrEmpty(locator.Section))
            {
                query = query.Where(x => x.LocatorSection == locator.Section);
            }

            return query;
        }

        private readonly CommentsDbContext _dbContext;
    }
}
