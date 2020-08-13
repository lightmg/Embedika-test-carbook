using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CarsCatalog.Helpers
{
    public static class EnumerableExtensions
    {
        private static readonly MethodInfo orderByMethodInfo =
            typeof(Enumerable).GetStaticMethod(nameof(Enumerable.OrderBy), 2);

        private static readonly MethodInfo orderByDescendingMethodInfo =
            typeof(Enumerable).GetStaticMethod(nameof(Enumerable.OrderByDescending), 2);

        public static IQueryable<TEntity> If<TEntity>(this IQueryable<TEntity> enumerable, bool condition,
            Func<IQueryable<TEntity>, IQueryable<TEntity>> transform) =>
            condition
                ? transform.Invoke(enumerable)
                : enumerable;

        public static IEnumerable<TEntity> If<TEntity>(this IEnumerable<TEntity> enumerable, bool condition,
            Func<IEnumerable<TEntity>, IEnumerable<TEntity>> transform) =>
            condition
                ? transform.Invoke(enumerable)
                : enumerable;

        public static bool IsNullOrEmpty<TEntity>(this IEnumerable<TEntity> enumerable) =>
            enumerable == null || !enumerable.Any();

        public static IEnumerable<TEntity> EmptyIfNull<TEntity>(this IEnumerable<TEntity> enumerable) =>
            enumerable ?? Enumerable.Empty<TEntity>();

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key,
            Func<TValue> valueFactory)
        {
            if (dictionary.TryGetValue(key, out var value))
                return value;
            value = valueFactory.Invoke();
            dictionary.Add(key, value);
            return value;
        }

        public static IEnumerable<TEntity> OrderByPropertyName<TEntity>(this IEnumerable<TEntity> enumerable,
            string propertyName, bool byDescending = false)
        {
            var propertyGetter = PropertyGettersStorage.Getter(typeof(TEntity), propertyName);
            var methodInfo = byDescending ? orderByDescendingMethodInfo : orderByMethodInfo;
            return (IEnumerable<TEntity>) methodInfo
                .MakeGenericMethod(typeof(TEntity), propertyGetter.PropertyType)
                .Invoke(null, new object[] {enumerable, propertyGetter.Compiled});
        }
    }
}