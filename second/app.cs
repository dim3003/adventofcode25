using System;
using Microsoft.VisualBasic.FileIO;

string filePath = "data.csv";
using (TextFieldParser parser = new TextFieldParser(filePath))
{
    parser.TextFieldType = FieldType.Delimited;
    parser.SetDelimiters(",");

    while (!parser.EndOfData)
    {
        string[] fields = parser.ReadFields();
        foreach (var field in fields)
        {
            Console.WriteLine(field);
        }
        Console.WriteLine("-----");
    }
}
