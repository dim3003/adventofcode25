using System.IO;
int numberOfZeros = 0;
int currentDialNumber = 50;
foreach (var line in File.ReadLines("data.txt"))
{
    int n = int.Parse(line.Substring(1));
    int delta = line[0] == 'R' ? n : -n;
    int newRaw = currentDialNumber + delta;
    if(delta > 0)
      numberOfZeros += (newRaw - (newRaw % 100)) / 100; 
    if(newRaw <= 0)
      numberOfZeros += (-newRaw - (-newRaw % 100)) /100 + (currentDialNumber == 0 ? 0 : 1);
    currentDialNumber = ((newRaw % 100) + 100) % 100;
}
Console.WriteLine(numberOfZeros);
