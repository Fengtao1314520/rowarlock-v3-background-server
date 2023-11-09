using System.Reflection;

namespace Ro.CrossPlatform.Extension;

public static class DynamicExt
{
    public static dynamic GetPropertyValue(this object obj, string propertyName)
    {
        Type type = obj.GetType();
        PropertyInfo property = type.GetRuntimeProperties()
            .First(p => p.Name == propertyName);
        return property.GetValue(obj) ?? throw new InvalidOperationException();
    }
}