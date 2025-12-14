var lines = File.ReadLines("data.txt")
                .Where((line, index) => index % 2 == 0)
                .ToList();

// Dictionary to track how many timelines reach each column
var currentCounts = new Dictionary<int, long>
{
    [lines[0].IndexOf('S')] = 1
};

foreach (var line in lines.Skip(1))
{
    var nextCounts = new Dictionary<int, long>();
    var splitters = line
        .Select((c, i) => (c, i))
        .Where(x => x.c == '^')
        .Select(x => x.i)
        .ToHashSet();

    foreach (var kv in currentCounts)
    {
        int index = kv.Key;
        long count = kv.Value;

        if (splitters.Contains(index))
        {
            // Particle splits: add count to both left and right positions
            nextCounts[index - 1] = nextCounts.GetValueOrDefault(index - 1) + count;
            nextCounts[index + 1] = nextCounts.GetValueOrDefault(index + 1) + count;
        }
        else
        {
            // Particle continues straight down
            nextCounts[index] = nextCounts.GetValueOrDefault(index) + count;
        }
    }

    currentCounts = nextCounts;
}

// Total timelines = sum of counts at the bottom row
long totalTimelines = currentCounts.Values.Sum();
Console.WriteLine(totalTimelines);
