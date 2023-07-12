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
                "Value": "ABCDEFGHT001"
              },
              "Boolean": {
                "Kind": "Boolean",
                "Value": true
              },
              "ActorId": {
                "Kind": "ActorId",
                "Value": "ABCDEFGH"
              },
              "DateOnly": {
                "Kind": "DateOnly",
                "Value": "12/31/9999"
              },
              "DateTime": {
                "Kind": "DateTime",
                "Value": "01/02/2023 03:04:05.006"
              },
              "Delta": {
                "Kind": "Delta",
                "Value": "Gain=3.5"
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
              "MultiTag": {
                "Kind": "MultiTag",
                "Value": "A:B:C:D:E:F:G:H:I:J"
              },
              "Phone": {
                "Kind": "Phone",
                "Value": "+1 (215) 316-8538"
              },
              "String": {
                "Kind": "String",
                "Value": "Hello darkness my old friend"
              },
              "TimeOnly": {
                "Kind": "TimeOnly",
                "Value": "01:02:03.004"
              },
              "TimeSpan": {
                "Kind": "TimeSpan",
                "Value": "1.02:03:04.005"
              },
              "Tag": {
                "Kind": "Tag",
                "Value": "SomeTag"
              },
              "TradeDate": {
                "Kind": "TradeDate",
                "Value": "12/16/2019"
              },
              "Uri": {
                "Kind": "Uri",
                "Value": "http://google.com/"
              }
            }                     
            """;

        static string NoWhitespace(string value) =>
            new(value.Where(c => !char.IsWhiteSpace(c)).ToArray());

        NoWhitespace(actual).Should().Be(NoWhitespace(expected));
    }

    [Fact]
    public void ArgSet_Serializer_Deserializes()
    {
        var source = GetArgSet();

        var options = GetJsonSerializerOptions();

        var json = JsonSerializer.Serialize(source, options);

        var target = JsonSerializer.Deserialize<ArgSet>(json, options);

        target!.Count.Should().Be(source!.Count);

        void Validate<T>(MultiTag multiTag, Func<ArgSet, MultiTag, T> getValue) =>
            getValue(target, multiTag).Should().Be(getValue(source, multiTag));

        Validate(Create("AccountId"), (a, k) => a.Get<AccountId>(k));
        Validate(Create("Boolean"), (a, k) => a.Get<bool>(k));
        Validate(Create("ActorId"), (a, k) => a.Get<ActorId>(k));
        Validate(Create("DateOnly"), (a, k) => a.Get<DateOnly>(k));
        Validate(Create("DateTime"), (a, k) => a.Get<DateTime>(k));
        Validate(Create("Delta"), (a, k) => a.Get<Delta>(k));
        Validate(Create("Double"), (a, k) => a.Get<double>(k));
        Validate(Create("Email"), (a, k) => a.Get<Email>(k));
        Validate(Create("Enum"), (a, k) => a.Get<ArgKind>(k));
        Validate(Create("Guid"), (a, k) => a.Get<Guid>(k));
        Validate(Create("Float"), (a, k) => a.Get<float>(k));
        Validate(Create("Int32"), (a, k) => a.Get<int>(k));
        Validate(Create("Int64"), (a, k) => a.Get<long>(k));
        Validate(Create("MultiTag"), (a, k) => a.Get<MultiTag>(k));
        Validate(Create("Phone"), (a, k) => a.Get<Phone>(k));
        Validate(Create("String"), (a, k) => a.Get<string>(k));
        Validate(Create("TimeOnly"), (a, k) => a.Get<TimeOnly>(k));
        Validate(Create("TimeSpan"), (a, k) => a.Get<TimeSpan>(k));
        Validate(Create("Tag"), (a, k) => a.Get<Tag>(k));
        Validate(Create("TradeDate"), (a, k) => a.Get<TradeDate>(k));
        Validate(Create("Uri"), (a, k) => a.Get<Uri>(k));
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

        argSet.Set(Create("AccountId"), AccountId.Create("ABCDEFGHT001"));
        argSet.Set(Create("Boolean"), true);
        argSet.Set(Create("ActorId"), ActorId.Create("ABCDEFGH"));
        argSet.Set(Create("DateOnly"), DateOnly.MaxValue);
        argSet.Set(Create("DateTime"), new DateTime(2023, 1, 2, 3, 4, 5, 6));
        argSet.Set(Create("Delta"), Delta.Create("Gain=3.5"));
        argSet.Set(Create("Double"), double.MaxValue);
        argSet.Set(Create("Email"), Email.Create("louis@squideyes.com"));
        argSet.Set(Create("Enum"), ArgKind.TimeSpan);
        argSet.Set(Create("Float"), float.MaxValue);
        argSet.Set(Create("Guid"), new Guid("2ED156F8-D481-4E8D-AE2B-89D80010ACCB"));
        argSet.Set(Create("Int32"), int.MaxValue);
        argSet.Set(Create("Int64"), long.MaxValue);
        argSet.Set(Create("MultiTag"), Create("A:B:C:D:E:F:G:H:I:J"));
        argSet.Set(Create("Phone"), Phone.Create("+1 (215) 316-8538"));
        argSet.Set(Create("String"), "Hello darkness my old friend");
        argSet.Set(Create("TimeOnly"), new TimeOnly(1, 2, 3, 4));
        argSet.Set(Create("TimeSpan"), new TimeSpan(1, 2, 3, 4, 5));
        argSet.Set(Create("Tag"), Tag.Create("SomeTag"));
        argSet.Set(Create("TradeDate"), KnownTradeDates.From(TradeDate.MinValue));
        argSet.Set(Create("Uri"), new Uri("http://google.com"));

        return argSet;
    }
}