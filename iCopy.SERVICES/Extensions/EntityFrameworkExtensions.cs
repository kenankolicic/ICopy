using System;
using System.Linq;
using System.Linq.Expressions;

namespace iCopy.SERVICES.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static IOrderedQueryable<TModel> OrderByAscDesc<TModel, TKey>(this IQueryable<TModel> query, Expression<Func<TModel, TKey>> expression, string order)
        {
            if (order.Equals("asc", StringComparison.CurrentCultureIgnoreCase))
                return query.OrderBy(expression);
            return query.OrderByDescending(expression);
        }
    }
}
