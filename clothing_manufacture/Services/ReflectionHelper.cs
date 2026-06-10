using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.Services;

public static class ReflectionHelper
{
    public static object? GetPropertyValue(object? source, string propertyPath)
    {
        if (source == null || string.IsNullOrWhiteSpace(propertyPath))
            return null;

        object? current = source;

        foreach (string part in propertyPath.Split('.'))
        {
            if (current == null)
                return null;

            PropertyInfo? property = current.GetType().GetProperty(part);

            if (property == null)
                return null;

            current = property.GetValue(current);
        }

        return current;
    }

    public static void SetPropertyValue(object target, string propertyName, object? value)
    {
        PropertyInfo? property = target.GetType().GetProperty(propertyName);

        if (property == null || !property.CanWrite)
            return;

        property.SetValue(target, value);
    }

    public static Type? GetPropertyType(Type targetType, string propertyName)
    {
        return targetType.GetProperty(propertyName)?.PropertyType;
    }

    public static IQueryable GetQueryable(DbContext db, Type entityType)
    {
        MethodInfo method = typeof(DbContext)
            .GetMethods()
            .First(m => m.Name == nameof(DbContext.Set)
                        && m.IsGenericMethod
                        && m.GetParameters().Length == 0);

        object? result = method.MakeGenericMethod(entityType).Invoke(db, null);
        return (IQueryable)result!;
    }

    public static IQueryable ApplyIncludes(IQueryable query, Type entityType, IEnumerable<string> includePaths)
    {
        MethodInfo includeMethod = typeof(EntityFrameworkQueryableExtensions)
            .GetMethods()
            .Where(m => m.Name == nameof(EntityFrameworkQueryableExtensions.Include))
            .Where(m => m.GetParameters().Length == 2)
            .First(m => m.GetParameters()[1].ParameterType == typeof(string));

        foreach (string includePath in includePaths.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
        {
            query = (IQueryable)includeMethod.MakeGenericMethod(entityType).Invoke(null, new object[] { query, includePath })!;
        }

        return query;
    }
}
