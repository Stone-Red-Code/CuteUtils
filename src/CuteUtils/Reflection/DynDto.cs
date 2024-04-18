using System.Dynamic;
using System.Reflection;

namespace CuteUtils.Reflection;

/// <summary>
/// Provides utility methods for converting objects to and from dynamic DTOs.
/// </summary>
public static class DynDto
{
    /// <summary>
    /// Converts an object to a dynamic DTO.
    /// </summary>
    /// <param name="data">The object to convert.</param>
    /// <returns>The dynamic DTO.</returns>
    public static ExpandoObject ToDto(this object data)
    {
        ExpandoObject dto = new ExpandoObject();

        PropertyInfo[] properties = data.GetType().GetProperties();

        foreach (PropertyInfo property in properties)
        {
            DynDtoNameAttribute? dynDtoNameAttribute = property.GetCustomAttribute<DynDtoNameAttribute>();

            if (dynDtoNameAttribute is not null)
            {
                _ = dto.TryAdd(dynDtoNameAttribute.Name, property.GetValue(data));
            }
        }

        return dto;
    }

    /// <summary>
    /// Converts an object to a specified type of DTO.
    /// </summary>
    /// <typeparam name="T">The type of DTO.</typeparam>
    /// <param name="data">The object to convert.</param>
    /// <param name="dto">The DTO instance to populate.</param>
    /// <returns>The populated DTO.</returns>
    public static T ToDto<T>(this object data, T dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        PropertyInfo[] dataProperties = data.GetType().GetProperties();
        PropertyInfo[] dtoProperties = dto.GetType().GetProperties();

        foreach (PropertyInfo dataProperty in dataProperties)
        {
            DynDtoNameAttribute? dynDtoNameAttribute = dataProperty.GetCustomAttribute<DynDtoNameAttribute>();
            PropertyInfo? dtoPropertyInfo = Array.Find(dtoProperties, p => p.Name == dynDtoNameAttribute?.Name);

            if (dynDtoNameAttribute is not null && dtoPropertyInfo is not null)
            {
                object? value = dataProperty.GetValue(data);
                dtoPropertyInfo.SetValue(dto, value);
            }
        }

        return dto;
    }

    /// <summary>
    /// Converts an object to a new instance of a specified type of DTO.
    /// </summary>
    /// <typeparam name="T">The type of DTO.</typeparam>
    /// <param name="data">The object to convert.</param>
    /// <returns>The new instance of the DTO.</returns>
    public static T ToDto<T>(this object data) where T : new()
    {
        return ToDto(data, new T());
    }

    /// <summary>
    /// Converts a dynamic DTO to an object.
    /// </summary>
    /// <typeparam name="T">The type of object.</typeparam>
    /// <param name="dto">The dynamic DTO.</param>
    /// <param name="data">The object instance to populate.</param>
    /// <returns>The populated object.</returns>
    public static T FromDto<T>(this object dto, T data)
    {
        ArgumentNullException.ThrowIfNull(data);

        PropertyInfo[] dataProperties = data.GetType().GetProperties();
        PropertyInfo[] dtoProperties = dto.GetType().GetProperties();

        foreach (PropertyInfo property in dataProperties)
        {
            DynDtoNameAttribute? dynDtoNameAttribute = property.GetCustomAttribute<DynDtoNameAttribute>();
            if (dynDtoNameAttribute is not null)
            {
                PropertyInfo? propertyInfo = Array.Find(dtoProperties, x => x.Name == dynDtoNameAttribute.Name);

                if (propertyInfo is not null)
                {
                    property.SetValue(data, propertyInfo.GetValue(dto));
                }
            }
        }

        return data;
    }

    /// <summary>
    /// Converts a dynamic DTO to an object.
    /// </summary>
    /// <typeparam name="T">The type of object.</typeparam>
    /// <param name="dto">The dynamic DTO.</param>
    /// <param name="data">The new instance of the object.</param>
    /// <returns>The populated object.</returns>
    public static T FromDto<T>(this ExpandoObject dto, T data)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(data);

        IDictionary<string, object> dtoProperties = dto!;

        PropertyInfo[] dataProperties = data.GetType().GetProperties();

        foreach (PropertyInfo property in dataProperties)
        {
            DynDtoNameAttribute? dynDtoNameAttribute = property.GetCustomAttribute<DynDtoNameAttribute>();
            if (dynDtoNameAttribute is not null && dtoProperties.TryGetValue(dynDtoNameAttribute.Name, out object? value))
            {
                property.SetValue(data, value);
            }
        }

        return data;
    }

    /// <summary>
    /// Converts a dynamic DTO to a new instance of an object.
    /// </summary>
    /// <typeparam name="T">The type of object.</typeparam>
    /// <param name="dto">The dynamic DTO.</param>
    /// <returns>The new instance of the object.</returns>
    public static T FromDto<T>(this object dto) where T : new()
    {
        return FromDto(dto, new T());
    }

    /// <summary>
    /// Converts a dynamic DTO to a new instance of an object.
    /// </summary>
    /// <typeparam name="T">The type of object.</typeparam>
    /// <param name="dto">The dynamic DTO.</param>
    /// <returns>The new instance of the object.</returns>
    public static T FromDto<T>(this ExpandoObject dto) where T : new()
    {
        return FromDto(dto, new T());
    }
}