using System;
using System.Linq;
using System.Reflection;

namespace CarsCatalog.Helpers
{
    public static class ReflectionHelpers
    {
        public static MethodInfo GetStaticMethod(this Type type, string methodName, int parametersLength)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Single(x => x.Name == methodName && x.GetParameters().Length == parametersLength);
        }
    }
}