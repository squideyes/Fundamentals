using FluentAssertions;
using SquidEyes.Fundamentals;
using System.Text.Encodings.Web;
using System.Text.Json;
using static SquidEyes.Fundamentals.ConfigKey;

namespace SquidEyes.UnitTests;

public class ArgSetTests
{
    [Fact]
    public void ArgSet_Serializer_Serializes()
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
    public void ArgSet_Serializer_Deserializes()
    {
        var source = GetArgSet();

        var options = GetJsonSerializerOptions();

        var json = JsonSerializer.Serialize(source, options);

        var target = JsonSerializer.Deserialize<ArgSet>(json, options);

        target!.Count.Should().Be(source!.Count);

        void Validate<T>(ConfigKey key, Func<ArgSet, ConfigKey, T> getValue) =>
            getValue(target, key).Should().Be(getValue(source, key));

        Validate(From("AccountId"), (a, k) => a.GetAccountId(k));
        Validate(From("Boolean"), (a, k) => a.GetBoolean(k));
        Validate(From("ClientId"), (a, k) => a.GetClientId(k));
        Validate(From("DateOnly"), (a, k) => a.GetDateOnly(k));
        Validate(From("DateTime"), (a, k) => a.GetDateTime(k));
        Validate(From("Double"), (a, k) => a.GetDouble(k));
        Validate(From("Email"), (a, k) => a.GetEmail(k));
        Validate(From("Enum"), (a, k) => a.GetEnum<ArgKind>(k));
        Validate(From("Guid"), (a, k) => a.GetGuid(k));
        Validate(From("Float"), (a, k) => a.GetFloat(k));
        Validate(From("Int32"), (a, k) => a.GetInt32(k));
        Validate(From("Int64"), (a, k) => a.GetInt64(k));
        Validate(From("Offset"), (a, k) => a.GetOffset(k));
        Validate(From("Phone"), (a, k) => a.GetPhone(k));
        Validate(From("ShortId"), (a, k) => a.GetShortId(k));
        Validate(From("String"), (a, k) => a.GetString(k));
        Validate(From("TimeOnly"), (a, k) => a.GetTimeOnly(k));
        Validate(From("TimeSpan"), (a, k) => a.GetTimeSpan(k));
        Validate(From("Token"), (a, k) => a.GetToken(k));
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
        argSet.Upsert(From("DateTime"), new DateTime(2023, 1, 2, 3, 4, 5, 6));
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
        argSet.Upsert(From("TimeOnly"), new TimeOnly(1, 2, 3, 4));
        argSet.Upsert(From("TimeSpan"), new TimeSpan(1, 2, 3, 4, 5));
        argSet.Upsert(From("Token"), Token.From("SomeToken"));

        return argSet;
    }
}
