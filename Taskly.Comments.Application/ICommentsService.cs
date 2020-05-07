using System.Collections.Generic;
using System.Threading.Tasks;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application
{
    public interface ICommentsService
    {
        Task<List<Comment>> GetCommentsByLocator(Locator locator, bool includeDeleted = false);

        Task<List<Comment>> GetCommentsByAuthor(string authorId, bool includeDeleted = false);

        Task<Comment> AddComment(Comment comment);

        Task<Comment> AddReply(string parentId, Comment comment);

        Task<Comment> MarkAsDeleted(Comment comment);
    }
}
