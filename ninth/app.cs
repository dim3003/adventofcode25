// Read all points from file
var lines = File.ReadAllLines("data.txt");
var points = lines
    .Select(l => l.Split(','))
    .Select(p => new Point(long.Parse(p[0]), long.Parse(p[1])))
    .ToList();

long maxArea = 0;
Point cornerA = default, cornerB = default;

// Brute-force all pairs
for (int i = 0; i < points.Count; i++)
{
    for (int j = i + 1; j < points.Count; j++)
    {
        long area = CalculateArea(points[i], points[j]);
        if (area > maxArea)
        {
            maxArea = area;
            cornerA = points[i];
            cornerB = points[j];
        }
    }
}

Console.WriteLine($"Max Area: {maxArea}");
Console.WriteLine($"Corners: ({cornerA.X},{cornerA.Y}) and ({cornerB.X},{cornerB.Y})");

static long CalculateArea(Point a, Point b)
{
    long width = Math.Abs(a.X - b.X) + 1L;
    long height = Math.Abs(a.Y - b.Y) + 1L;
    return width * height;
}

public struct Point
{
    public long X, Y;
    public Point(long x, long y) { X = x; Y = y; }
};
