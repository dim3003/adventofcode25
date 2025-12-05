using System;
using Microsoft.VisualBasic.FileIO;

string filePath = "data.csv";
ulong total = 0;


ulong GetNumberFromPosition(string numberAsString, int start, int? end = null){
  int length = end.HasValue
        ? end.Value - start
        : numberAsString.Length - start;
  return ulong.Parse(numberAsString.Substring(start, length));
}

static bool IsInvalidRepeatedPattern(string s)
{
    int n = s.Length;
    if (n < 2) return false; // single digit can't be "repeated at least twice"

    // Build LPS array (KMP prefix function)
    int[] lps = new int[n];
    int len = 0; // length of previous longest prefix suffix
    for (int i = 1; i < n; i++)
    {
        while (len > 0 && s[i] != s[len])
            len = lps[len - 1];

        if (s[i] == s[len])
            len++;

        lps[i] = len;
    }

    int longestPrefixSuffix = lps[n - 1];
    if (longestPrefixSuffix == 0) return false;

    int period = n - longestPrefixSuffix;
    return period < n && (n % period == 0);
}

using (TextFieldParser parser = new TextFieldParser(filePath))
{
    parser.TextFieldType = FieldType.Delimited;
    parser.SetDelimiters(",");

    while (!parser.EndOfData)
    {
        string[] fields = parser.ReadFields();
        foreach (var field in fields)
        {
          var dashPosition = field.IndexOf("-");
          var firstNumber = GetNumberFromPosition(field, 0, dashPosition);
          var secondNumber = GetNumberFromPosition(field, dashPosition + 1);

          for(var number = firstNumber; number <= secondNumber; number++){
            var s = number.ToString();
            if(IsInvalidRepeatedPattern(s))
              total += number;
          }
        }
    }
}


Console.WriteLine("TOTAL = " + total);
