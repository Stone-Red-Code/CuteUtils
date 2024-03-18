namespace CuteUtils.FluentMath.Shapes;

/// <summary>
/// Represents a rectangle
/// </summary>
/// <remarks>
/// Creates a new rectangle instance
/// </remarks>
/// <param name="length">The length of the rectangle</param>
/// <param name="width">The width of the rectangle</param>
public class Rectangle(double length, double width)
{
    /// <summary>
    /// The length of the <see cref="Rectangle"/>
    /// </summary>
    public double Length { get; set; } = length;

    /// <summary>
    /// The width of the <see cref="Rectangle"/>
    /// </summary>
    public double Width { get; set; } = width;

    /// <summary>
    /// The diagonal of the <see cref="Rectangle"/>
    /// </summary>
    public double Diagonal => Math.Sqrt(Math.Pow(Length, 2) + Math.Pow(Width, 2));

    /// <summary>
    /// The area of the <see cref="Rectangle"/>
    /// </summary>
    public double Area => Length * Width;

    /// <summary>
    /// The perimeter of the <see cref="Rectangle"/>
    /// </summary>
    public double Perimeter => (Length * 2) + (Width * 2);

    public bool IsSquare => Length == Width;
    public bool IsRectangle => !IsSquare;

    public bool IsGolden => Length / Width == 1.61803398875;
}