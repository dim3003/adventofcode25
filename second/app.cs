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

            var halfPoint = s.Length / 2;
            if (s.Length % 2 != 0) continue;

            var firstHalf = GetNumberFromPosition(s, 0, halfPoint);
            var secondHalf = GetNumberFromPosition(s, halfPoint);

            if(firstHalf == secondHalf)
              total += number;
          }
        }
    }
}


Console.WriteLine("TOTAL = " + total);
