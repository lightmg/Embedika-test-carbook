using System;

namespace CarsCatalog.Helpers
{
    public readonly struct PropertyGetter
    {
        public readonly Delegate Compiled;
        public readonly Type PropertyType;

        public PropertyGetter(Delegate compiled, Type propertyType)
        {
            Compiled = compiled;
            PropertyType = propertyType;
        }
    }
}