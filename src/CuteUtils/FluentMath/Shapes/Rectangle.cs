namespace CuteUtils.FluentMath.Shapes;

/// <summary>
/// Represents a rectangle shape.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Rectangle"/> class with the specified length and width.
/// </remarks>
/// <param name="length">The length of the rectangle.</param>
/// <param name="width">The width of the rectangle.</param>
public class Rectangle(double length, double width)
{
    /// <summary>
    /// Gets or sets the length of the rectangle.
    /// </summary>
    public double Length { get; set; } = length;

    /// <summary>
    /// Gets or sets the width of the rectangle.
    /// </summary>
    public double Width { get; set; } = width;

    /// <summary>
    /// Gets the diagonal length of the rectangle.
    /// </summary>
    public double Diagonal => Math.Sqrt(Math.Pow(Length, 2) + Math.Pow(Width, 2));

    /// <summary>
    /// Gets the area of the rectangle.
    /// </summary>
    public double Area => Length * Width;

    /// <summary>
    /// Gets the perimeter of the rectangle.
    /// </summary>
    public double Perimeter => (Length * 2) + (Width * 2);

    /// <summary>
    /// Gets a value indicating whether the rectangle is a square.
    /// </summary>
    public bool IsSquare => Length == Width;

    /// <summary>
    /// Gets a value indicating whether the rectangle is a rectangle (not a square).
    /// </summary>
    public bool IsRectangle => !IsSquare;

    /// <summary>
    /// Gets a value indicating whether the rectangle has a golden ratio.
    /// </summary>
    public bool IsGolden => Length / Width == 1.61803398875;
}