using System.Threading.Tasks;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application
{
    public interface ICommentsService
    {
        Task<PaginatedList<Comment>> GetCommentsByLocator(Locator locator, int pageIndex, int pageSize);

        Task<PaginatedList<Comment>> GetCommentsByUser(string userId, int pageIndex, int pageSize);

        Task<Comment> GetCommentById(int id);

        Task<Comment> AddComment(Comment comment);

        Task<DeletedComment> MarkAsDeleted(int id, string removalUserId);
    }
}
