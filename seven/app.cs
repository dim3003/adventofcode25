var lines = File.ReadLines("data.txt")
                .Where((line, index) => index % 2 == 0)
                .ToList();

var currentIndexes = new HashSet<int>
{
    lines[0].IndexOf('S')
};

int splitCount = 0;

foreach (var line in lines.Skip(1))
{

    var nextIndexes = new HashSet<int>();
    var indexesOfHat = line
      .Select((c, i) => (c, i))
      .Where(x => x.c == '^')
      .Select(x => x.i);
    foreach (int index in indexesOfHat)
    {
        if (currentIndexes.Contains(index))
        {
            splitCount++;
            nextIndexes.Add(index + 1);
            nextIndexes.Add(index - 1);
        }
        else
        {
            nextIndexes.Add(index);
        }
    }
    currentIndexes = nextIndexes;
}

Console.WriteLine(splitCount);
