
ulong GetNumberFromPosition(string numberAsString, int start, int? end = null){
  int length = end.HasValue
        ? end.Value - start
        : numberAsString.Length - start;
  return ulong.Parse(numberAsString.Substring(start, length));
}


var listOfIntervals =  new List<UlongRange>();

var lines = File.ReadAllLines("data.txt");
int idx = Array.FindIndex(lines, string.IsNullOrWhiteSpace);

var intervals= lines.Take(idx).ToArray();
var ids = lines.Skip(idx + 1).ToArray();

foreach(var line in intervals)
{
    var dashPosition = line.IndexOf("-");
    var firstNumber = GetNumberFromPosition(line, 0, dashPosition);
    var secondNumber = GetNumberFromPosition(line, dashPosition + 1);
    var interval = new UlongRange(firstNumber, secondNumber);
    listOfIntervals.Add(interval);
}

bool IsFresh(List<UlongRange> intervals, ulong id)
{
  foreach(var interval in listOfIntervals)
  {
    if(interval.Contains(id))
      return true;
  }
  return false;
}
  

int freshCount = 0;
foreach(var line in ids)
{
  var id = ulong.Parse(line);
  if(IsFresh(listOfIntervals, id))
    freshCount++;
}
Console.WriteLine(freshCount);

public readonly record struct UlongRange(ulong Start, ulong End)
{
    public bool Contains(ulong x) => x >= Start && x <= End;
}
