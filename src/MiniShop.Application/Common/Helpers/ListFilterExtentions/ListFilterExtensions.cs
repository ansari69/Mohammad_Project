using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MiniShop.Application.Common.Helpers.ListFilterExtentions
{
   public static class ListFilterExtensions
    {

        #region OrderBy

        public static IOrderedQueryable<T> FilterOrderBy<T>(this IQueryable<T> source, string property, bool byDescending)
        {
            if (byDescending)
                return ApplyOrder<T>(source, property, "OrderByDescending");
            else
                return ApplyOrder<T>(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> FilterOrderBy<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderBy");
        }

        public static IOrderedQueryable<T> FilterOrderByDescending<T>(this IQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }

        public static IOrderedQueryable<T> FilterThenBy<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> FilterThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source,
            string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }


        #endregion

        #region General Extensions

        public static IEnumerable<T> ToPaginatedList<T>(this IEnumerable<T> query, int pageId,
            int eachPerPage)
        {
            int skip = (pageId - 1) * eachPerPage;
            return query.Skip(skip).Take(eachPerPage);
        }

        public static IQueryable<T> ToPaginatedQuery<T>(this IQueryable<T> query, int pageId,
            int eachPerPage)
        {
            int skip = (pageId - 1) * eachPerPage;
            return query.Skip(skip).Take(eachPerPage);
        }

        public static int TotalPagesCount<T>(this IQueryable<T> query, int eachPerPage)
        {
            return Convert.ToInt32(Math.Ceiling(query.Count() / Convert.ToDouble(eachPerPage)));
        }

        public static int TotalPagesCount<T>(this IEnumerable<T> query, int eachPerPage)
        {
            return Convert.ToInt32(Math.Ceiling(query.Count() / Convert.ToDouble(eachPerPage)));
        }

        #endregion
    }
}
