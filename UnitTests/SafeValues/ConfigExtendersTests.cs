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
        ConfigValueTest(float.E.ToString(), float.E);
        ConfigValueTest(float.Epsilon.ToString(), float.Epsilon);
        ConfigValueTest(float.MaxValue.ToString(), float.MaxValue);
        ConfigValueTest(float.MinValue.ToString(), float.MinValue);
        ConfigValueTest(float.NaN.ToString(), float.NaN);
        ConfigValueTest(float.NegativeInfinity.ToString(), float.NegativeInfinity);
        ConfigValueTest(float.NegativeZero.ToString(), float.NegativeZero);
        ConfigValueTest(float.Pi.ToString(), float.Pi);
        ConfigValueTest(float.PositiveInfinity.ToString(), float.PositiveInfinity);
        ConfigValueTest(float.Tau.ToString(), float.Tau);
    }

    [Fact]
    public void GoodDouble_Should_ConvertToConfigValue()
    {
        ConfigValueTest(double.E.ToString(), double.E);
        ConfigValueTest(double.Epsilon.ToString(), double.Epsilon);
        ConfigValueTest(double.MaxValue.ToString(), double.MaxValue);
        ConfigValueTest(double.MinValue.ToString(), double.MinValue);
        ConfigValueTest(double.NaN.ToString(), double.NaN);
        ConfigValueTest(double.NegativeInfinity.ToString(), double.NegativeInfinity);
        ConfigValueTest(double.NegativeZero.ToString(), double.NegativeZero);
        ConfigValueTest(double.Pi.ToString(), double.Pi);
        ConfigValueTest(double.PositiveInfinity.ToString(), double.PositiveInfinity);
        ConfigValueTest(double.Tau.ToString(), double.Tau);
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
    public void GoodGuid_Should_ConvertToConfigValue() =>
        Guid.NewGuid().Do(v => ConfigValueTest(v.ToString(), v));

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
        "".ToConfigUri("X", true, UriKind.Absolute).Value.Should().BeNull();
        URI.ToConfigUri("X").Value.Should().Be(new Uri(URI));
    }

    [Fact]
    public void GoodEmail_Should_ConvertToConfigEmail()
    {
        const string EMAIL = "dude@someco.com";

        EMAIL.ToConfigEmail("X").Value.Should().Be(Email.Create(EMAIL));
        "".ToConfigEmail("X", true).Value.Should().BeNull();
        EMAIL.ToConfigEmail("X").Value.Should().Be(Email.Create(EMAIL));
    }

    [Fact]
    public void GoodPhone_Should_ConvertToConfigPhone()
    {
        const string EMAIL = "+1 (234) 567-8901";

        EMAIL.ToConfigPhone("X").Value.Should().Be(Phone.Create(EMAIL));
        "".ToConfigPhone("X", true).Value.Should().BeNull();
        EMAIL.ToConfigPhone("X").Value.Should().Be(Phone.Create(EMAIL));
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