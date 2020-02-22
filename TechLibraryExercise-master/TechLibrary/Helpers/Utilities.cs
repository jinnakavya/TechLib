using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechLibrary.Contracts.Responses;

namespace TechLibrary.Helpers
{
    public static class Utilities
    {
        public async static Task<PagedResponse<T>> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResponse<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }
    }
}
