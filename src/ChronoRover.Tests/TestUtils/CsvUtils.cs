using Microsoft.VisualBasic.FileIO;

using System.Collections.Generic;

namespace ChronoRover.Tests.TestUtils;

public static class CsvUtils
{
    public static string[][] GetCsvData(string filePath)
    {
        var data = new List<string[]>();

        using var parser = new TextFieldParser(filePath);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        parser.TrimWhiteSpace = true;

        if (!parser.EndOfData) parser.ReadLine(); // skip header

        while (!parser.EndOfData)
        {
            var rowData = parser.ReadFields();
            data.Add(rowData);
        }

        return data.ToArray();
    }
}