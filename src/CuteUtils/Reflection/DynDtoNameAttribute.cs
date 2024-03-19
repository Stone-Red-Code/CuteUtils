namespace CuteUtils.Reflection;

/// <summary>
/// Represents an attribute that specifies the dynamic DTO name for a property.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="DynDtoNameAttribute"/> class with the specified name.
/// </remarks>
/// <param name="name">The dynamic DTO name.</param>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class DynDtoNameAttribute(string name) : Attribute
{
    /// <summary>
    /// Gets or sets the dynamic DTO name.
    /// </summary>
    public string Name { get; set; } = name;
}