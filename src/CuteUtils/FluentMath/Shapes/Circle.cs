namespace CuteUtils.FluentMath.Shapes;

/// <summary>
/// Represents a circle shape.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Circle"/> class with the specified radius.
/// </remarks>
/// <param name="radius">The radius of the circle.</param>
public class Circle(double radius)
{
    /// <summary>
    /// Gets or sets the radius of the circle.
    /// </summary>
    public double Radius { get; set; } = radius;

    /// <summary>
    /// Gets the diameter of the circle.
    /// </summary>
    public double Diameter => Radius * 2;

    /// <summary>
    /// Gets the circumference of the circle.
    /// </summary>
    public double Circumference => 2 * Math.PI * Radius;

    /// <summary>
    /// Gets the area of the circle.
    /// </summary>
    public double Area => Math.PI * Math.Pow(Radius, 2);
}