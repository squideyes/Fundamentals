// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Fundamentals;

public class CsvEnumerator : IEnumerable<string[]>, IDisposable
{
    private readonly StreamReader reader;
    private readonly int expectedFields;

    private bool skipFirst;

    public CsvEnumerator(StreamReader reader, int expectedFields, bool skipFirst = false)
    {
        reader.MayNot().BeNull();
        expectedFields.Must().BePositive();

        this.reader = reader;
        this.expectedFields = expectedFields;
        this.skipFirst = skipFirst;
    }

    public CsvEnumerator(Stream stream, int expectedFields, bool skipFirst = false)
        : this(new StreamReader(stream), expectedFields, skipFirst)
    {
    }

    public CsvEnumerator(string fileName, int expectedFields, bool skipFirst = false)
        : this(File.OpenRead(fileName), expectedFields, skipFirst)
    {
    }

    public void Dispose()
    {
        if (reader != null)
            reader.Dispose();
    }

    public IEnumerator<string[]> GetEnumerator()
    {
        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            var fields = line.Split(',');

            if (fields.Length != expectedFields)
                fields.Length.Must().Be(expectedFields);

            if (skipFirst)
            {
                skipFirst = false;

                continue;
            }

            yield return fields;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}