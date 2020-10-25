using System;
using System.Linq;
using System.Linq.Expressions;

namespace UserManagement.Helpers
{
    public static class QueryableHelper
    {
        public static IQueryable<T> PageBy<T>(this IQueryable<T> query, int skipCount, int maxResultCount) where T : class
        {
            return query.Skip(skipCount).Take(maxResultCount);
        }

        public static TQueryable PageBy<T, TQueryable>(this TQueryable query, int skipCount, int maxResultCount) where T : class
            where TQueryable : IQueryable<T>
        {
            return (TQueryable)query.Skip(skipCount).Take(maxResultCount);
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate) where T : class
        {

            return condition
                ? query.Where(predicate)
                : query;
        }


        public static TQueryable WhereIf<T, TQueryable>(this TQueryable query, bool condition, Expression<Func<T, bool>> predicate) where T : class
            where TQueryable : IQueryable<T>
        {

            return condition
                ? (TQueryable)query.Where(predicate)
                : query;
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate) where T : class
        {

            return condition
                ? query.Where(predicate)
                : query;
        }
        public static TQueryable WhereIf<T, TQueryable>(this TQueryable query, bool condition, Expression<Func<T, int, bool>> predicate) where T : class
            where TQueryable : IQueryable<T>
        {

            return condition
                ? (TQueryable)query.Where(predicate)
                : query;
        }
    }
}