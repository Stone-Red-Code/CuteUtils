namespace CuteUtils.FluentMath.Shapes;

/// <summary>
/// Represents a triangle with three sides.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Triangle"/> class with the specified side lengths.
/// </remarks>
/// <param name="sideA">The length of side A.</param>
/// <param name="sideB">The length of side B.</param>
/// <param name="sideC">The length of side C.</param>
public class Triangle(double sideA, double sideB, double sideC)
{
    /// <summary>
    /// Gets or sets the length of side A.
    /// </summary>
    public double SideA { get; set; } = sideA;

    /// <summary>
    /// Gets or sets the length of side B.
    /// </summary>
    public double SideB { get; set; } = sideB;

    /// <summary>
    /// Gets or sets the length of side C.
    /// </summary>
    public double SideC { get; set; } = sideC;

    /// <summary>
    /// Gets the area of the triangle.
    /// </summary>
    public double Area
    {
        get
        {
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }
    }

    /// <summary>
    /// Gets the perimeter of the triangle.
    /// </summary>
    public double Perimeter => SideA + SideB + SideC;

    /// <summary>
    /// Gets a value indicating whether the triangle is right-angled.
    /// </summary>
    public bool IsRightAngled
    {
        get
        {
            double[] sides = [SideA, SideB, SideC];
            Array.Sort(sides);
            return Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) == Math.Pow(sides[2], 2);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the triangle is equilateral.
    /// </summary>
    public bool IsEquilateral => SideA == SideB && SideB == SideC;

    /// <summary>
    /// Gets a value indicating whether the triangle is isosceles.
    /// </summary>
    public bool IsIsosceles => SideA == SideB || SideB == SideC || SideA == SideC;

    /// <summary>
    /// Gets a value indicating whether the triangle is scalene.
    /// </summary>
    public bool IsScalene => !IsEquilateral && !IsIsosceles;
}