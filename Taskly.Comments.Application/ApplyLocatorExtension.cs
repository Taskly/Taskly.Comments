using System.Linq;
using Taskly.Comments.Application.Entities;
using Taskly.Comments.Model;

namespace Taskly.Comments.Application
{
    public static class ApplyLocatorExtension
    {
        public static IQueryable<CommentEntity> ApplyLocator(this IQueryable<CommentEntity> query, Locator locator)
        {
            query = query.Where(x => x.LocatorSection == locator.Section);

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
    }
}
