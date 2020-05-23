using System.Linq;
using System.Threading.Tasks;

namespace Taskly.Comments.Application
{
    public static class PaginatedListExtension
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex,
            int pageSize)
        {
            return await PaginatedList<T>.CreateAsync(source, pageIndex, pageSize);
        }
    }
}
