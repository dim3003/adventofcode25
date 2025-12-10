using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics; // optional if you want BigInteger for huge results

class CephalopodMath
{
    static void Main()
    {
        var lines = File.ReadAllLines("data.txt");

        if (lines.Length < 2)
        {
            Console.Error.WriteLine("Need at least one digit row and one operator row.");
            return;
        }

        // Normalize line lengths (pad with spaces)
        int cols = lines.Max(l => l.Length);
        var grid = lines.Select(l => l.PadRight(cols, ' ').ToCharArray()).ToArray();
        int rows = grid.Length;
        int opRow = rows - 1; // last row contains operators

        // Helper: is this column entirely spaces (all rows)?
        bool IsSeparatorColumn(int c)
        {
            for (int r = 0; r < rows; r++)
                if (grid[r][c] != ' ') return false;
            return true;
        }

        // Split into groups of contiguous non-separator columns
        var groups = new List<(int start, int end)>();
        int cIndex = 0;
        while (cIndex < cols)
        {
            // skip separators
            while (cIndex < cols && IsSeparatorColumn(cIndex)) cIndex++;
            if (cIndex >= cols) break;

            int start = cIndex;
            while (cIndex < cols && !IsSeparatorColumn(cIndex)) cIndex++;
            int end = cIndex - 1;
            groups.Add((start, end));
        }

        long grandTotal = 0;

        foreach (var (start, end) in groups)
        {
            // find operator in the bottom row within this group
            char op = ' ';
            for (int c = start; c <= end; c++)
            {
                if (!char.IsWhiteSpace(grid[opRow][c]))
                {
                    op = grid[opRow][c];
                    break;
                }
            }

            if (op == ' ')
            {
                throw new InvalidOperationException($"No operator found in operator row between columns {start} and {end}.");
            }

            // Collect numbers: each column in the group (top..row-2) -> take only digits, concatenate
            var numbers = new List<long>();
            for (int c = start; c <= end; c++)
            {
                var sb = new System.Text.StringBuilder();
                for (int r = 0; r < opRow; r++) // exclude operator row
                {
                    if (char.IsDigit(grid[r][c]))
                        sb.Append(grid[r][c]);
                }

                // If the column contained no digits, skip it (shouldn't happen in well-formed input)
                if (sb.Length == 0) continue;

                numbers.Add(long.Parse(sb.ToString()));
            }

            // Evaluate the problem: apply the operator across all numbers (left-to-right)
            if (numbers.Count == 0) continue;

            long result = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                long n = numbers[i];
                switch (op)
                {
                    case '+':
                        result += n;
                        break;
                    case '*':
                        result *= n;
                        break;
                    default:
                        throw new InvalidOperationException($"Unsupported operator '{op}' in columns {start}-{end}.");
                }
            }

            grandTotal += result;
        }

        Console.WriteLine($"Grand total: {grandTotal}");
    }
}

