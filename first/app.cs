using System.IO;

int numberOfZeros = 0;
int currentDialNumber = 50;
foreach (var line in File.ReadLines("data.txt"))
{
    var numberOfDials = int.Parse(line.Substring(1));

    if (line[0] == 'R')
        currentDialNumber += numberOfDials;
    else
        currentDialNumber -= numberOfDials;

    currentDialNumber = ((currentDialNumber % 100) + 100) % 100;

    if (currentDialNumber == 0)
        numberOfZeros++;
}

Console.WriteLine(numberOfZeros);
