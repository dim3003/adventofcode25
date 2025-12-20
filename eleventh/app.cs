using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Program
{
    record State(string node, string path);

    static void Main()
    {
        var input = File.ReadAllText("data.txt").Trim();
        
        Console.WriteLine($"Part One: {PartOne(input)}");
        Console.WriteLine($"Part Two: {PartTwo(input)}");
    }

    static object PartOne(string input) => 
        PathCount(Parse(input), "you", "out", new Dictionary<string, long>());
        
    static object PartTwo(string input)
    {
        var g = Parse(input);
        return 
            PathCount(g, "svr", "fft", new Dictionary<string, long>()) *
            PathCount(g, "fft", "dac", new Dictionary<string, long>()) *
            PathCount(g, "dac", "out", new Dictionary<string, long>()) +
            PathCount(g, "svr", "dac", new Dictionary<string, long>()) *
            PathCount(g, "dac", "fft", new Dictionary<string, long>()) *
            PathCount(g, "fft", "out", new Dictionary<string, long>());
    }

    static long PathCount(
        Dictionary<string, string[]> g,
        string from, string to,
        Dictionary<string, long> cache
    )
    {
        if (!cache.ContainsKey(from))
        {
            if (from == to)
            {
                cache[from] = 1;
            }
            else
            {
                var res = 0L;
                foreach (var next in g.GetValueOrDefault(from) ?? Array.Empty<string>())
                {
                    res += PathCount(g, next, to, cache);
                }
                cache[from] = res;
            }
        }
        return cache[from];
    }

    static Dictionary<string, string[]> Parse(string input)
    {
        return (
            from line in input.Split("\n")
            where !string.IsNullOrWhiteSpace(line)
            let parts = line.Split(" ").ToArray()
            let frm = parts[0].TrimEnd(':')
            let to = parts.Skip(1).ToArray()
            select new KeyValuePair<string, string[]>(frm, to)
        ).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
