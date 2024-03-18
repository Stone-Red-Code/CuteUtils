using System.Dynamic;
using System.Reflection;

namespace CuteUtils.Reflection;

public static class DynDto
{
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

    public static T ToDto<T>(this object data) where T : new()
    {
        return ToDto(data, new T());
    }

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

    public static T FromDto<T>(this object dto) where T : new()
    {
        return FromDto(dto, new T());
    }

    public static T FromDto<T>(this ExpandoObject dto) where T : new()
    {
        return FromDto(dto, new T());
    }
}