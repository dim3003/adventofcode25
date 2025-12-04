using System.IO;

int numberOfZeros = 0;
int currentDialNumber = 50;

foreach (var line in File.ReadLines("data.txt"))
{
    int n = int.Parse(line.Substring(1));
    int delta = line[0] == 'R' ? n : -n;

    int prev = currentDialNumber;
    int newRaw = prev + delta;

    int crossings = 0;
    if(delta > 0){
      crossings += (newRaw - (newRaw % 100)) / 100; 
    }
    if(newRaw <= 0){
      crossings += (-newRaw - (-newRaw % 100)) /100 + (prev == 0 ? 0 : 1);
    }

    numberOfZeros += crossings;
    currentDialNumber = ((newRaw % 100) + 100) % 100;
}

Console.WriteLine(numberOfZeros);
