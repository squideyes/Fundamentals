// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;
using System.Text.Encodings.Web;
using System.Text.Json;
using static SquidEyes.Fundamentals.MultiTag;

namespace SquidEyes.UnitTests;

public class JsonStringTagArgSetConverterTests
{
    [Fact]
    public void TagArgSet_Serializer_Serializes()
    {
        var tagArgs = GetTagArgSet();

        var actual = JsonSerializer.Serialize(tagArgs, GetJsonSerializerOptions());

        const string EXPECTED = """
              {
                  "Bool": {
                    "Kind": "Bool",
                    "Value": true
                  },
                  "Byte": {
                    "Kind": "Byte",
                    "Value": 255
                  },
                  "Char": {
                    "Kind": "Char",
                    "Value": "Z"
                  },
                  "DateOnly": {
                    "Kind": "DateOnly",
                    "Value": "12/31/9999"
                  },
                  "DateTime": {
                    "Kind": "DateTime",
                    "Value": "12/31/9999 23:59:59.999"
                  },
                  "Double": {
                    "Kind": "Double",
                    "Value": 1.7976931348623157E+308
                  },
                  "Email": {
                    "Kind": "Email",
                    "Value": "louis@squideyes.com"
                  },
                  "Enum": {
                    "Kind": "Enum",
                    "Type": "System.UriKind",
                    "Value": "RelativeOrAbsolute"
                  },
                  "Float": {
                    "Kind": "Float",
                    "Value": 3.4028235E+38
                  },
                  "Guid": {
                    "Kind": "Guid",
                    "Value": "2ed156f8-d481-4e8d-ae2b-89d80010accb"
                  },
                  "Int16": {
                    "Kind": "Int16",
                    "Value": 32767
                  },
                  "Int32": {
                    "Kind": "Int32",
                    "Value": 2147483647
                  },
                  "Int64": {
                    "Kind": "Int64",
                    "Value": 9223372036854775807
                  },
                  "MultiTag": {
                    "Kind": "MultiTag",
                    "Value": "A:B:C:D:E:F:G:H:I:J"
                  },
                  "Phone": {
                    "Kind": "Phone",
                    "Value": "+1 (215) 333-4444"
                  },
                  "TimeOnly": {
                    "Kind": "TimeOnly",
                    "Value": "23:59:59.999"
                  },
                  "TimeSpan": {
                    "Kind": "TimeSpan",
                    "Value": "10675199.02:48:05.477"
                  },
                  "Tag": {
                    "Kind": "Tag",
                    "Value": "SomeTag"
                  },
                  "Uri": {
                    "Kind": "Uri",
                    "Value": "http://google.com/"
                  },
                  "Uppers": {
                    "Kind": "TextLine",
                    "Filter": "Uppers",
                    "Value": "A"
                  },
                  "Lower": {
                    "Kind": "TextLine",
                    "Filter": "Lowers",
                    "Value": "a"
                  },
                  "Digits": {
                    "Kind": "TextLine",
                    "Filter": "Digits",
                    "Value": "1"
                  },
                  "Spaces": {
                    "Kind": "0",
                    "Value": "{Invalid}"
                  },
                  "Symbols": {
                    "Kind": "TextLine",
                    "Filter": "Symbols",
                    "Value": "@"
                  },
                  "AllChars": {
                    "Kind": "0",
                    "Value": "{Invalid}"
                  }
            }            
            """;

        static string NoWhitespace(string value) =>
            new(value.Where(c => !char.IsWhiteSpace(c)).ToArray());

        NoWhitespace(actual).Should().Be(NoWhitespace(EXPECTED));
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        options.Converters.Add(new JsonStringTagArgSetConverter());

        return options;
    }

    private static TagArgSet GetTagArgSet()
    {
        var tagArgs = new TagArgSet();

        void AddMiscTagArg<T>(Tag tag, T arg) => tagArgs[tag] = TagArg<T>.Create(tag, arg);

        void AddTextLineTagArg(Tag tag, string arg, AsciiFilter filter) =>
            tagArgs[tag] = TagArg<string>.Create(tag, arg, true, filter);

        AddMiscTagArg("Bool", true);
        AddMiscTagArg("Byte", byte.MaxValue);
        AddMiscTagArg("Char", 'Z');
        AddMiscTagArg("DateOnly", DateOnly.MaxValue);
        AddMiscTagArg("DateTime", DateTime.MaxValue);
        AddMiscTagArg("Double", double.MaxValue);
        AddMiscTagArg("Email", Email.Create("louis@squideyes.com"));
        AddMiscTagArg("Enum", UriKind.RelativeOrAbsolute);
        AddMiscTagArg("Float", float.MaxValue);
        AddMiscTagArg("Guid", new Guid("2ed156f8d4814e8dae2b89d80010accb"));
        AddMiscTagArg("Int16", short.MaxValue);
        AddMiscTagArg("Int32", int.MaxValue);
        AddMiscTagArg("Int64", long.MaxValue);
        AddMiscTagArg("MultiTag", Create("A:B:C:D:E:F:G:H:I:J"));
        AddMiscTagArg("Phone", Phone.Create("+1 (215) 333-4444"));
        AddMiscTagArg("TimeOnly", TimeOnly.MaxValue);
        AddMiscTagArg("TimeSpan", TimeSpan.MaxValue);
        AddMiscTagArg("Tag", Tag.Create("SomeTag"));
        AddMiscTagArg("Uri", new Uri("http://google.com"));

        AddTextLineTagArg("Uppers", "A", AsciiFilter.Uppers);
        AddTextLineTagArg("Lower", "a", AsciiFilter.Lowers);
        AddTextLineTagArg("Digits", "1", AsciiFilter.Digits);
        AddTextLineTagArg("Spaces", " ", AsciiFilter.Spaces);
        AddTextLineTagArg("Symbols", "@", AsciiFilter.Symbols);
        AddTextLineTagArg("AllChars", "Aa1 @", AsciiFilter.AllChars);

        return tagArgs;
    }
}