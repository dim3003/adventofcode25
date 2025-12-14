var lines = File.ReadAllLines("data.txt");


double EuclideanDistance(int[] a, int[] b)
{
    if (a.Length != b.Length)
        throw new ArgumentException("Arrays must have the same length.");

    double total = 0.0;
    for (int i = 0; i < a.Length; i++)
    {
        double diff = a[i] - b[i];
        total += diff * diff;
    }
    return Math.Sqrt(total);
}

var points = lines.Select(l => l.Split(',').Select(int.Parse).ToArray()).ToArray();
int n = points.Length;

var distances = new List<(double distance, int i, int j)>();
for (int i = 0; i < n; i++)
{
    for (int j = i + 1; j < n; j++)
    {
        double dist = EuclideanDistance(points[i], points[j]);
        distances.Add((dist, i, j));
    }
}

distances.Sort((a, b) => a.distance.CompareTo(b.distance));
var uf = new UnionFind(n);
int circuits = n;
(int lastI, int lastJ) = (-1, -1);

foreach (var (_, i, j) in distances)
{
    int rootI = uf.Find(i);
    int rootJ = uf.Find(j);
    
    if (rootI != rootJ)  // this union merges two separate circuits
    {
        uf.Union(i, j);
        circuits--;
        lastI = i;
        lastJ = j;

        if (circuits == 1)
            break;  // all junction boxes are now in a single circuit
    }
}

long result = (long)points[lastI][0] * points[lastJ][0];  // multiply X coordinates
Console.WriteLine(result);

class UnionFind
{
    private int[] parent;
    private int[] size;

    public UnionFind(int n)
    {
        parent = new int[n];
        size = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
            size[i] = 1;
        }
    }

    public int Find(int x)
    {
        if (parent[x] != x)
            parent[x] = Find(parent[x]); // path compression
        return parent[x];
    }

    public void Union(int x, int y)
    {
        int rootX = Find(x);
        int rootY = Find(y);
        if (rootX == rootY) return;

        if (size[rootX] < size[rootY])
        {
            parent[rootX] = rootY;
            size[rootY] += size[rootX];
        }
        else
        {
            parent[rootY] = rootX;
            size[rootX] += size[rootY];
        }
    }

    public int CircuitSize(int x)
    {
        return size[Find(x)];
    }
}

