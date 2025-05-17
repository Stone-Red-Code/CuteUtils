using System.Reflection;

namespace CuteUtils.Reflection;

/// <summary>
/// Provides extension methods for reflection operations.
/// </summary>
public static class ReflectionExtensions
{
    /// <summary>
    /// Creates a new instance of the specified type and copies the properties from the source object to the new instance.
    /// </summary>
    /// <typeparam name="T">The type of the new instance.</typeparam>
    /// <param name="obj">The source object.</param>
    /// <returns>A new instance of the specified type with copied properties.</returns>
    public static T CopyProperties<T>(this object obj) where T : new()
    {
        T newObj = new T();

        Type objType = obj.GetType();
        Type newObjType = newObj.GetType();

        foreach (PropertyInfo propertyInfo in objType.GetProperties())
        {
            PropertyInfo? newObjPropertyInfo = newObjType.GetProperty(propertyInfo.Name);
            if (newObjPropertyInfo is not null && newObjPropertyInfo.PropertyType.IsAssignableFrom(propertyInfo.PropertyType))
            {
                newObjPropertyInfo.SetValue(newObj, propertyInfo.GetValue(obj));
            }
        }

        return newObj;
    }

    /// <summary>
    /// Copies the properties from the source object to the specified target object.
    /// </summary>
    /// <typeparam name="T">The type of the target object.</typeparam>
    /// <param name="obj">The source object.</param>
    /// <param name="newObj">The target object.</param>
    /// <returns>The target object with copied properties.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the target object is null.</exception>
    public static T CopyProperties<T>(this object obj, T newObj)
    {
        ArgumentNullException.ThrowIfNull(newObj);

        Type objType = obj.GetType();
        Type newObjType = newObj.GetType();

        foreach (PropertyInfo propertyInfo in objType.GetProperties())
        {
            PropertyInfo? newObjPropertyInfo = newObjType.GetProperty(propertyInfo.Name);
            if (newObjPropertyInfo is not null && newObjPropertyInfo.PropertyType.IsAssignableFrom(propertyInfo.PropertyType))
            {
                newObjPropertyInfo.SetValue(newObj, propertyInfo.GetValue(obj));
            }
        }

        return newObj;
    }
}