namespace CuteUtils.FluentMath.Shapes;

public class Circle(double radius)
{
    public double Radius { get; set; } = radius;

    public double Diameter => Radius * 2;

    public double Circumference => 2 * Math.PI * Radius;

    public double Area => Math.PI * Math.Pow(Radius, 2);
}