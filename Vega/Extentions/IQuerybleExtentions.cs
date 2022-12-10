using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System;
using Vega.Domains;

namespace Vega.Extentions
{
    public static class IQuerybleExtentions 
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObj, Dictionary<string, Expression<Func<T, object>>> columnMap)
        {
            if (string.IsNullOrWhiteSpace(queryObj.SortBy) || !columnMap.ContainsKey(queryObj.SortBy))
                return query;

            if (queryObj.IsSortAscending)
                return query.OrderBy(columnMap[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnMap[queryObj.SortBy]);
        }


        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {

            //if (queryObj.PageSize <= 0)
            //    queryObj.PageSize = 10;

            queryObj.PageSize = queryObj.PageSize <= 0 ? Convert.ToByte(10) : queryObj.PageSize;
            queryObj.Page = queryObj.Page <= 0 ? 1 : queryObj.PageSize;

            return  query.Skip((queryObj.Page - 1) * queryObj.PageSize).Take(queryObj.PageSize);
        }
    }
}
