using FluentAssertions;
using SquidEyes.Fundamentals;
using System.Text.Encodings.Web;
using System.Text.Json;
using static SquidEyes.Fundamentals.ConfigKey;

namespace SquidEyes.UnitTests;

public class ArgSetTests
{
    [Fact]
    public void ArgSet_Roundtrips()
    {
        var actual = JsonSerializer.Serialize(
            GetArgSet(), GetJsonSerializerOptions());

        var expected = """
            {
              "AccountId": {
                "Kind": "AccountId",
                "Value": "ABCDEFGH(T001)"
              },
              "Boolean": {
                "Kind": "Boolean",
                "Value": true
              },
              "ClientId": {
                "Kind": "ClientId",
                "Value": "ABCDEFGH"
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
                "Type": "SquidEyes.Fundamentals.ArgKind",
                "Value": "TimeSpan"
              },
              "Float": {
                "Kind": "Float",
                "Value": 3.4028235E+38
              },
              "Guid": {
                "Kind": "Guid",
                "Value": "2ed156f8-d481-4e8d-ae2b-89d80010accb"
              },
              "Int32": {
                "Kind": "Int32",
                "Value": 2147483647
              },
              "Int64": {
                "Kind": "Int64",
                "Value": 9223372036854775807
              },
              "Offset": {
                "Kind": "Offset",
                "Value": "Gain=3.5"
              },
              "Phone": {
                "Kind": "Phone",
                "Value": "+12153168538"
              },
              "ShortId": {
                "Kind": "ShortId",
                "Value": "lvzto7gS74GDKJt2xBNfGx"
              },
              "String": {
                "Kind": "String",
                "Value": "Hello darkness my old friend"
              },
              "TimeOnly": {
                "Kind": "TimeOnly",
                "Value": "23:59:59.999"
              },
              "TimeSpan": {
                "Kind": "TimeSpan",
                "Value": "10675199.02:48:05.477"
              },
              "Token": {
                "Kind": "Token",
                "Value": "SomeToken"
              }
            }
            """;

        actual.Should().Be(expected);
    }

    [Fact]
    public void X()
    {
        var source = GetArgSet();

        var options = GetJsonSerializerOptions();

        var json = JsonSerializer.Serialize(source, options);

        var target = JsonSerializer.Deserialize<ArgSet>(json, options);
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        options.Converters.Add(new JsonStringArgSetConverter());

        return options;
    }

    private static ArgSet GetArgSet()
    {
        var argSet = new ArgSet();

        argSet.Upsert(From("AccountId"), AccountId.From("ABCDEFGH(T001)"));
        argSet.Upsert(From("Boolean"), true);
        argSet.Upsert(From("ClientId"), ClientId.From("ABCDEFGH"));
        argSet.Upsert(From("DateOnly"), DateOnly.MaxValue);
        argSet.Upsert(From("DateTime"), DateTime.MaxValue);
        argSet.Upsert(From("Double"), double.MaxValue);
        argSet.Upsert(From("Email"), Email.From("louis@squideyes.com"));
        argSet.Upsert(From("Enum"), ArgKind.TimeSpan);
        argSet.Upsert(From("Float"), float.MaxValue);
        argSet.Upsert(From("Guid"), new Guid("2ED156F8-D481-4E8D-AE2B-89D80010ACCB"));
        argSet.Upsert(From("Int32"), int.MaxValue);
        argSet.Upsert(From("Int64"), long.MaxValue);
        argSet.Upsert(From("Offset"), Offset.From("Gain=3.5"));
        argSet.Upsert(From("Phone"), Phone.From("+1 (215) 316-8538"));
        argSet.Upsert(From("ShortId"), ShortId.From("lvzto7gS74GDKJt2xBNfGx"));
        argSet.Upsert(From("String"), "Hello darkness my old friend");
        argSet.Upsert(From("TimeOnly"), TimeOnly.MaxValue);
        argSet.Upsert(From("TimeSpan"), TimeSpan.MaxValue);
        argSet.Upsert(From("Token"), Token.From("SomeToken"));

        return argSet;
    }
}
