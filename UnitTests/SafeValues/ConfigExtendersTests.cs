// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ConfigExtendersTests
{
    [Fact]
    public void GoodByte_Should_ConvertToConfigValue()
    {
        ConfigValueTest("0", byte.MinValue);
        ConfigValueTest("255", byte.MaxValue);
    }

    [Fact]
    public void GoodChar_Should_ConvertToConfigValue()
    {
        ConfigValueTest("A", 'A');
        ConfigValueTest("Z", 'Z');
    }

    [Fact]
    public void GoodShort_Should_ConvertToConfigValue()
    {
        ConfigValueTest("-32768", short.MinValue);
        ConfigValueTest("32767", short.MaxValue);
    }

    [Fact]
    public void GoodInt32_Should_ConvertToConfigValue()
    {
        ConfigValueTest("-2147483648", int.MinValue);
        ConfigValueTest("2147483647", int.MaxValue);
    }

    [Fact]
    public void GoodIn264_Should_ConvertToConfigValue()
    {
        ConfigValueTest("-9223372036854775808", long.MinValue);
        ConfigValueTest("9223372036854775807", long.MaxValue);
    }

    [Fact]
    public void GoodFloat_Should_ConvertToConfigValue()
    {
        ConfigValueTest("2.71828175", float.E);
        ConfigValueTest("1.401298E-45", float.Epsilon);
        ConfigValueTest("3.40282347E+38", float.MaxValue);
        ConfigValueTest("-3.40282347E+38", float.MinValue);
        ConfigValueTest("NaN", float.NaN);
        ConfigValueTest("-∞", float.NegativeInfinity);
        ConfigValueTest("-0", float.NegativeZero);
        ConfigValueTest("3.14159274", float.Pi);
        ConfigValueTest("∞", float.PositiveInfinity);
        ConfigValueTest("6.28318548", float.Tau);
    }

    [Fact]
    public void GoodDouble_Should_ConvertToConfigValue()
    {
        ConfigValueTest("2.7182818284590451", double.E);
        ConfigValueTest("4.94065645841247E-324", double.Epsilon);
        ConfigValueTest("1.7976931348623157E+308", double.MaxValue);
        ConfigValueTest("-1.7976931348623157E+308", double.MinValue);
        ConfigValueTest("NaN", double.NaN);
        ConfigValueTest("-∞", double.NegativeInfinity);
        ConfigValueTest("-0", double.NegativeZero);
        ConfigValueTest("3.1415926535897931", double.Pi);
        ConfigValueTest("∞", double.PositiveInfinity);
        ConfigValueTest("6.2831853071795862", double.Tau);
    }

    [Fact]
    public void GoodDateTime_Should_ConvertToConfigValue()
    {
        ConfigValueTest("01/01/0001 00:00:00.000", DateTime.MinValue);
        ConfigValueTest("12/31/9999 23:59:59.9999999", DateTime.MaxValue);
    }

    [Fact]
    public void GoodTimeSpan_Should_ConvertToConfigValue()
    {
        ConfigValueTest("-10675199.02:48:05.4775808", TimeSpan.MinValue);
        ConfigValueTest("10675199.02:48:05.4775807", TimeSpan.MaxValue);
    }

    [Fact]
    public void GoodTimeOnly_Should_ConvertToConfigValue()
    {
        ConfigValueTest("00:00:00.000", TimeOnly.MinValue);
        ConfigValueTest("23:59:59.9999999", TimeOnly.MaxValue);
    }

    [Fact]
    public void GoodDateOnly_Should_ConvertToConfigValue()
    {
        ConfigValueTest("01/01/0001", DateOnly.MinValue);
        ConfigValueTest("12/31/9999", DateOnly.MaxValue);
    }

    [Fact]
    public void GoodGuid_Should_ConvertToConfigValue()
    {
        Guid.NewGuid().Do(v => ConfigValueTest(v.ToString(), v));
    }

    [Fact]
    public void GoodString_Should_ConvertToConfigString()
    {
        "A".ToConfigString("X").Value.Should().Be("A");
        "".ToConfigString("X", true).Value.Should().BeNull();
        "A".ToConfigString("X").Value.Should().Be("A");
    }

    [Fact]
    public void GoodUri_Should_ConvertToConfigUri()
    {
        const string URI = "https://cnn.com";

        URI.ToConfigUri("X").Value.Should().Be(new Uri(URI));
        "".ToConfigUri("X", UriKind.Absolute, true).Value.Should().BeNull();
        URI.ToConfigUri("X").Value.Should().Be(new Uri(URI));
    }

    [Fact]
    public void GoodEnum_Should_ConvertToConfigEnum()
    {
        "Absolute".ToConfigEnum<UriKind>("X").Value.Should().Be(UriKind.Absolute);
        "".ToConfigEnum<UriKind>("X", true).Value.Should().BeNull();
        "Absolute".ToConfigEnum<UriKind>("X").Value.Should().Be(UriKind.Absolute);
    }

    private static void ConfigValueTest<T>(string input, T expected)
        where T : struct, IParsable<T>
    {
        var requiredValue = input!.ToConfigValue<T>("X");
        var nullValue = "".ToConfigValue<T>("X", true);
        var nonNullValue = input!.ToConfigValue<T>("X", true);

        requiredValue.Tag.Should().Be(Tag.Create("X"));
        nullValue.Tag.Should().Be(Tag.Create("X"));
        nonNullValue.Tag.Should().Be(Tag.Create("X"));

        requiredValue.IsValid.Should().BeTrue();
        nullValue.IsValid.Should().BeTrue();
        nonNullValue.IsValid.Should().BeTrue();

        requiredValue.Message.Should().BeNull();
        nullValue.Message.Should().BeNull();
        nonNullValue.Message.Should().BeNull();

        requiredValue.Value.Should().Be(expected);
        nullValue.Value.Should().BeNull();
        nonNullValue.Value.Should().Be(expected);
    }
}