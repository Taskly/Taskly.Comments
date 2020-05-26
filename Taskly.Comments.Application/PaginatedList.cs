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
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalItems = count;
            AddRange(items);
        }

        public const int PageMaxSize = 100;

        public int PageIndex { get; }

        public int TotalPages { get; }

        public int TotalItems { get; }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 1 || pageSize < 1)
            {
                throw new InvalidArgumentDomainException("Invalid pagination parameters.");
            }

            if (pageIndex > PageMaxSize)
            {
                throw new InvalidArgumentDomainException($"Invalid page size. Maximum page size is '{PageMaxSize}");
            }

            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
