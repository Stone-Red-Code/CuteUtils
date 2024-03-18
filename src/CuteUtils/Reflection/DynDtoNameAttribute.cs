namespace CuteUtils.Reflection;
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class DynDtoNameAttribute(string name) : Attribute
{
    public string Name { get; set; } = name;
}
