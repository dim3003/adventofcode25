using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        var input = File.ReadAllText("data.txt").Trim();
        
        Console.WriteLine($"Part One: {PartOne(input)}");
    }

    static object PartOne(string input)
    {
        // It's enough to check if the 3x3 area of the presents is less than 
        // the area under 🎄 tree. No packing is required.
        return input.Split(new[] { "\n\n" }, StringSplitOptions.None).Last()
            .Split("\n")
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => Regex.Matches(line, @"\d+").Select(m => int.Parse(m.Value)).ToArray())
            .Count(nums => nums[0] * nums[1] >= 9 * nums.Skip(2).Sum());
    }
}
