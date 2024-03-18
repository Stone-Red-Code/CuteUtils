namespace CuteUtils.FluentMath.Shapes;

public class Ellipse(double majorAxis, double minorAxis)
{
    public double MajorAxis { get; set; } = majorAxis;
    public double MinorAxis { get; set; } = minorAxis;

    public double Area => Math.PI * MajorAxis * MinorAxis;
    public double Circumference => Math.PI * ((3 * (MajorAxis + MinorAxis)) - Math.Sqrt(((3 * MajorAxis) + MinorAxis) * (MajorAxis + (3 * MinorAxis))));

    public bool IsCircle => MajorAxis == MinorAxis;
}