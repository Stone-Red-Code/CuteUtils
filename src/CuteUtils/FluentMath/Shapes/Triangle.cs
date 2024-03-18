namespace CuteUtils.FluentMath.Shapes;

public class Triangle(double sideA, double sideB, double sideC)
{
    public double SideA { get; set; } = sideA;
    public double SideB { get; set; } = sideB;
    public double SideC { get; set; } = sideC;

    public double Area
    {
        get
        {
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }
    }

    public double Perimeter => SideA + SideB + SideC;

    public bool IsRightAngled
    {
        get
        {
            double[] sides = [SideA, SideB, SideC];
            Array.Sort(sides);
            return Math.Pow(sides[0], 2) + Math.Pow(sides[1], 2) == Math.Pow(sides[2], 2);
        }
    }

    public bool IsEquilateral => SideA == SideB && SideB == SideC;

    public bool IsIsosceles => SideA == SideB || SideB == SideC || SideA == SideC;

    public bool IsScalene => !IsEquilateral && !IsIsosceles;
}