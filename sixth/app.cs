var stringData = new List<string[]>();
foreach(var line in File.ReadLines("data.txt"))
{
  stringData.Add(line.Split(' ', StringSplitOptions.RemoveEmptyEntries));
}

int[][] numbers = stringData
  .Take(4)
  .Select(r => r.Select(s => int.Parse(s)).ToArray())
  .ToArray();
string[] operators = stringData[4];

int rowLength = numbers[0].Length;
int[] result = new int[rowLength];

for(int i = 0; i < rowLength; i++)
{
  switch(operators[i])
  {
    case "+":
      result[i] = numbers.Sum(row => row[i]);
      break;
    case "*":
      result[i] = numbers.Aggregate(1, (acc, row) => acc * row[i]);
      break;
    default:
      break;
  }
}

long total = result.Sum(x => (long)x);
Console.WriteLine(total);

