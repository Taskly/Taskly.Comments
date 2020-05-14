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

        public async Task<List<Comment>> GetCommentsByLocator(Locator locator)
        {
            /*IEnumerable<CommentDto> commentsDto = Data.Skip(page * pageSize).Take(pageSize);
            switch (sort)
            {
                case CommentsListSortType.NewToOld:
                    commentsDto = commentsDto.OrderByDescending(x => x.Id);
                    break;
                case CommentsListSortType.OldToNew:
                    commentsDto = commentsDto.OrderBy(x => x.Id);
                    break;
            }*/

            IQueryable<CommentEntity> query = _dbContext.Comments.AsQueryable();
            query = ApplyLocator(query, locator);
            query = query.OrderByDescending(x => x.Id);
            List<CommentEntity> entities = await query.ToListAsync();
            return entities.Select(x => x.ToModel()).ToList();
        }

        public async Task<List<Comment>> GetCommentsByAuthor(string authorId)
        {
            List<CommentEntity> entities =
                await _dbContext.Comments.Where(x => x.AuthorId == authorId).ToListAsync();
            List<Comment> comments = entities.Select(x => x.ToModel()).ToList();
            return comments;
        }

        public async Task<List<DeletedComment>> GetDeletedCommentsByAuthor(string authorId)
        {
            List<DeletedCommentEntity> entities =
                await _dbContext.DeletedComments.Where(x => x.AuthorId == authorId).ToListAsync();
            List<DeletedComment> comments = entities.Select(x => x.ToModel()).ToList();
            return comments;
        }

        public async Task<Comment> GetCommentById(string id)
        {
            int entityId = int.Parse(id);
            CommentEntity commentEntity = await _dbContext.Comments.Where(x => x.Id == entityId).FirstOrDefaultAsync();
            if (commentEntity != null)
            {
                return commentEntity.ToModel();
            }

            DeletedCommentEntity deletedCommentEntity =
                await _dbContext.DeletedComments.Where(x => x.Id == entityId).FirstOrDefaultAsync();

            return deletedCommentEntity.ToModel();
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            var entity = new CommentEntity(comment, string.Empty);
            _dbContext.Comments.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ToModel();
        }

        public async Task<Comment> AddReply(string parentId, Comment comment)
        {
            var entity = new CommentEntity(comment, parentId);
            _dbContext.Comments.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity.ToModel();
        }

        public async Task<DeletedComment> MarkAsDeleted(string id)
        {
            int entityId = int.Parse(id);
            CommentEntity commentEntity = await _dbContext.Comments.Where(x => x.Id == entityId).FirstOrDefaultAsync();
            Comment comment = commentEntity.ToModel();
            var deletedComment = new DeletedComment(comment);
            var deletedCommentEntity = new DeletedCommentEntity(deletedComment, commentEntity.ParentId.ToString());

            // TODO: Transaction?
            _dbContext.Comments.Remove(commentEntity);
            _dbContext.DeletedComments.Add(deletedCommentEntity);
            await _dbContext.SaveChangesAsync();
            return deletedCommentEntity.ToModel();
        }

        private IQueryable<CommentEntity> ApplyLocator(IQueryable<CommentEntity> query, Locator locator)
        {
            if (!string.IsNullOrEmpty(locator.Section))
            {
                query = query.Where(x => x.LocatorSection == locator.Section);
            }

            if (!string.IsNullOrEmpty(locator.Subsection))
            {
                query = query.Where(x => x.LocatorSubsection == locator.Subsection);
            }

            if (!string.IsNullOrEmpty(locator.Element))
            {
                query = query.Where(x => x.LocatorElement == locator.Element);
            }

            return query;
        }

        private readonly CommentsDbContext _dbContext;
    }
}
