using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taskly.Comments.Model.Exceptions;

namespace Taskly.Comments.Application
{
    public class PaginatedList<T> : List<T>
    {
        private PaginatedList()
        {
        }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItems = count;
            AddRange(items);
        }

        public const int PageMaxSize = 100;

        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public int TotalItems { get; private set; }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 1 || pageSize < 1 || pageIndex > PageMaxSize)
            {
                throw new InvalidArgumentDomainException("Invalid pagination parameters.");
            }

            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
