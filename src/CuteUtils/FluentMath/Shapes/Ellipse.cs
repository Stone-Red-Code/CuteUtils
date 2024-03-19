namespace CuteUtils.FluentMath.Shapes;

/// <summary>
/// Represents an ellipse shape.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Ellipse"/> class with the specified major and minor axes.
/// </remarks>
/// <param name="majorAxis">The length of the major axis.</param>
/// <param name="minorAxis">The length of the minor axis.</param>
public class Ellipse(double majorAxis, double minorAxis)
{
    /// <summary>
    /// Gets or sets the length of the major axis.
    /// </summary>
    public double MajorAxis { get; set; } = majorAxis;

    /// <summary>
    /// Gets or sets the length of the minor axis.
    /// </summary>
    public double MinorAxis { get; set; } = minorAxis;

    /// <summary>
    /// Gets the area of the ellipse.
    /// </summary>
    public double Area => Math.PI * MajorAxis * MinorAxis;

    /// <summary>
    /// Gets the circumference of the ellipse.
    /// </summary>
    public double Circumference => Math.PI * ((3 * (MajorAxis + MinorAxis)) - Math.Sqrt(((3 * MajorAxis) + MinorAxis) * (MajorAxis + (3 * MinorAxis))));

    /// <summary>
    /// Gets a value indicating whether the ellipse is a circle.
    /// </summary>
    public bool IsCircle => MajorAxis == MinorAxis;
}