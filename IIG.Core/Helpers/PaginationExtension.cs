using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Helpers
{
    public static class PaginationExtensions
    {
        public static int Skip(this int currPage, int pageSize)
        {
            return (currPage - 1) * pageSize;
        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<T> Paging<T>(this IEnumerable<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
