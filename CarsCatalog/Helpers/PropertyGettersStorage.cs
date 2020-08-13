using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarsCatalog.Helpers
{
    public class PropertyGettersStorage
    {
        private static readonly Implementation storage = new Implementation();
        public static PropertyGetter Getter(Type type, string propertyName) => storage.Get(type, propertyName);

        private class Implementation
        {
            private readonly Dictionary<Type, Dictionary<string, PropertyGetter>> visitorsCache =
                new Dictionary<Type, Dictionary<string, PropertyGetter>>();

            public PropertyGetter Get(Type type, string propertyName)
            {
                if (!visitorsCache.TryGetValue(type, out var visitorsOfType))
                {
                    visitorsOfType = new Dictionary<string, PropertyGetter>();
                    visitorsCache.Add(type, visitorsOfType);
                }

                if (visitorsOfType.TryGetValue(propertyName, out var visitor))
                    return visitor;
                visitor = CreateGetter(type, propertyName);
                return visitor;
            }

            private static PropertyGetter CreateGetter(Type type, string propertyName)
            {
                var parameter = Expression.Parameter(type);
                var property = Expression.PropertyOrField(parameter, propertyName);
                return new PropertyGetter(Expression.Lambda(property, parameter).Compile(), property.Type);
            }
        }
    }
}