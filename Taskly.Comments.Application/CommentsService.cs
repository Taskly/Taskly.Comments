using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Taskly.Comments.Application.Entities;
using Taskly.Comments.Model;
using Taskly.Comments.Model.Exceptions;

namespace Taskly.Comments.Application
{
    public class CommentsService : ICommentsService
    {
        public CommentsService(IMapper mapper, CommentsDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<Comment>> GetCommentsByLocator(Locator locator, int pageIndex, int pageSize)
        {
            if (locator is null)
            {
                throw new InvalidArgumentDomainException("Invalid locator.");
            }

            PaginatedList<CommentEntity> entities = await _dbContext.Comments
                .AsQueryable()
                .ApplyLocator(locator)
                .OrderByDescending(x => x.Id)
                .ToPaginatedListAsync(pageIndex, pageSize);

            // PaginatedList<Comment> comments = new PaginatedList<Comment>(
            //     entities.Select(x => _mapper.Map<Comment>(x)).ToList(), entities.TotalItems, pageIndex, pageSize);
            // return comments;
            PaginatedList<Comment> comments = _mapper.Map<PaginatedList<Comment>>(entities);
            return comments;
        }

        public Task<PaginatedList<Comment>> GetCommentsByUser(string userId, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            CommentEntity commentEntity = await FindCommentEntity(id);
            Comment comment = _mapper.Map<Comment>(commentEntity);
            return comment;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            if (comment is null)
            {
                throw new InvalidArgumentDomainException("Invalid comment.");
            }

            CommentEntity entity = _mapper.Map<CommentEntity>(comment);
            _dbContext.Comments.Add(entity);
            await _dbContext.SaveChangesAsync();

            _mapper.Map(entity, comment);
            return comment;
        }

        public async Task<DeletedComment> MarkAsDeleted(int id, string removalUserId)
        {
            if (string.IsNullOrEmpty(removalUserId))
            {
                throw new InvalidArgumentDomainException("Removal user ID required.");
            }

            CommentEntity commentEntity = await FindCommentEntity(id);
            Comment comment = _mapper.Map<Comment>(commentEntity);
            if (comment.Status == CommentStatus.Deleted)
            {
                throw new InvalidOperationDomainException($"Comment '{id}' already deleted.");
            }

            var deletedComment = new DeletedComment(comment, removalUserId);

            _mapper.Map(deletedComment, commentEntity);
            await _dbContext.SaveChangesAsync();
            return deletedComment;
        }

        private async Task<CommentEntity> FindCommentEntity(int id)
        {
            CommentEntity entity = await _dbContext.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (entity is null)
            {
                throw new NotFoundDomainException("Comment", id);
            }

            return entity;
        }

        private readonly IMapper _mapper;
        private readonly CommentsDbContext _dbContext;
    }
}
