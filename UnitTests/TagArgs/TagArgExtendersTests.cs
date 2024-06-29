// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class TagArgExtendersTests
{
    [Fact]
    public void GoodByte_Should_ConvertToTagValue()
    {
        ParseableArgTest("0", byte.MinValue);
        ParseableArgTest("255", byte.MaxValue);
    }

    [Fact]
    public void GoodChar_Should_ConvertToTagValue()
    {
        ParseableArgTest("A", 'A');
        ParseableArgTest("Z", 'Z');
    }

    [Fact]
    public void GoodShort_Should_ConvertToTagValue()
    {
        ParseableArgTest("-32768", short.MinValue);
        ParseableArgTest("32767", short.MaxValue);
    }

    [Fact]
    public void GoodInt32_Should_ConvertToTagValue()
    {
        ParseableArgTest("-2147483648", int.MinValue);
        ParseableArgTest("2147483647", int.MaxValue);
    }

    [Fact]
    public void GoodIn264_Should_ConvertToTagValue()
    {
        ParseableArgTest("-9223372036854775808", long.MinValue);
        ParseableArgTest("9223372036854775807", long.MaxValue);
    }

    [Fact]
    public void GoodFloat_Should_ConvertToTagValue()
    {
        ParseableArgTest(float.E.ToString(), float.E);
        ParseableArgTest(float.Epsilon.ToString(), float.Epsilon);
        ParseableArgTest(float.MaxValue.ToString(), float.MaxValue);
        ParseableArgTest(float.MinValue.ToString(), float.MinValue);
        ParseableArgTest(float.NaN.ToString(), float.NaN);
        ParseableArgTest(float.NegativeInfinity.ToString(), float.NegativeInfinity);
        ParseableArgTest(float.NegativeZero.ToString(), float.NegativeZero);
        ParseableArgTest(float.Pi.ToString(), float.Pi);
        ParseableArgTest(float.PositiveInfinity.ToString(), float.PositiveInfinity);
        ParseableArgTest(float.Tau.ToString(), float.Tau);
    }

    [Fact]
    public void GoodDouble_Should_ConvertToTagValue()
    {
        ParseableArgTest(double.E.ToString(), double.E);
        ParseableArgTest(double.Epsilon.ToString(), double.Epsilon);
        ParseableArgTest(double.MaxValue.ToString(), double.MaxValue);
        ParseableArgTest(double.MinValue.ToString(), double.MinValue);
        ParseableArgTest(double.NaN.ToString(), double.NaN);
        ParseableArgTest(double.NegativeInfinity.ToString(), double.NegativeInfinity);
        ParseableArgTest(double.NegativeZero.ToString(), double.NegativeZero);
        ParseableArgTest(double.Pi.ToString(), double.Pi);
        ParseableArgTest(double.PositiveInfinity.ToString(), double.PositiveInfinity);
        ParseableArgTest(double.Tau.ToString(), double.Tau);
    }

    [Fact]
    public void GoodDateTime_Should_ConvertToTagValue()
    {
        ParseableArgTest("01/01/0001 00:00:00.000", DateTime.MinValue);
        ParseableArgTest("12/31/9999 23:59:59.9999999", DateTime.MaxValue);
    }

    [Fact]
    public void GoodTimeSpan_Should_ConvertToTagValue()
    {
        ParseableArgTest("-10675199.02:48:05.4775808", TimeSpan.MinValue);
        ParseableArgTest("10675199.02:48:05.4775807", TimeSpan.MaxValue);
    }

    [Fact]
    public void GoodTimeOnly_Should_ConvertToTagValue()
    {
        ParseableArgTest("00:00:00.000", TimeOnly.MinValue);
        ParseableArgTest("23:59:59.9999999", TimeOnly.MaxValue);
    }

    [Fact]
    public void GoodDateOnly_Should_ConvertToTagValue()
    {
        ParseableArgTest("01/01/0001", DateOnly.MinValue);
        ParseableArgTest("12/31/9999", DateOnly.MaxValue);
    }

    [Fact]
    public void GoodGuid_Should_ConvertToTagValue() =>
        Guid.NewGuid().Do(v => ParseableArgTest(v.ToString(), v));

    [Fact]
    public void GoodUri_Should_ConvertToConfigUri()
    {
        const string URI = "https://cnn.com";

        URI.ToUriArg("X").Arg.Should().Be(new Uri(URI));
        "".ToUriArg("X", UriKind.Absolute, true).Arg.Should().BeNull();
        URI.ToUriArg("X").Arg.Should().Be(new Uri(URI));
    }

    [Fact]
    public void GoodEmail_Should_ConvertToConfigEmail()
    {
        const string EMAIL = "dude@someco.com";

        EMAIL.ToEmailArg("X").Arg.Should().Be(Email.Create(EMAIL));
        "".ToEmailArg("X", true).Arg.Should().BeNull();
        EMAIL.ToEmailArg("X").Arg.Should().Be(Email.Create(EMAIL));
    }

    [Fact]
    public void GoodPhone_Should_ConvertToConfigPhone()
    {
        const string PHONE = "+1 (234) 567-8901";

        PHONE.ToPhoneArg("X").Arg.Should().Be(Phone.Create(PHONE));
        "".ToPhoneArg("X", true).Arg.Should().BeNull();
        PHONE.ToPhoneArg("X").Arg.Should().Be(Phone.Create(PHONE));
    }

    [Fact]
    public void GoodEnum_Should_ConvertToConfigEnum()
    {
        "Absolute".ToEnumArg<UriKind>("X").Arg.Should().Be(UriKind.Absolute);
        "".ToEnumArg<UriKind>("X", true).Arg.Should().Be(default);
        "Absolute".ToEnumArg<UriKind>("X").Arg.Should().Be(UriKind.Absolute);
    }

    [Fact]
    public void GoodTag_Should_ConvertToConfigTag()
    {
        const string TAG = "Tag1";

        TAG.ToTagArg("X").Arg.Should().Be(Tag.Create(TAG));
        "".ToTagArg("X", true).Arg.Should().BeNull();
        TAG.ToTagArg("X").Arg.Should().Be(Tag.Create(TAG));
    }

    [Fact]
    public void GoodMultiTag_Should_ConvertToConfigMultiTag()
    {
        const string MT = "Tag1:Tag2:Tag3";

        MT.ToMultiTagArg("X").Arg.Should().Be(MultiTag.Create(MT));
        "".ToMultiTagArg("X", true).Arg.Should().BeNull();
        MT.ToMultiTagArg("X").Arg.Should().Be(MultiTag.Create(MT));
    }

    private static void ParseableArgTest<T>(string input, T expected)
        where T : struct, IParsable<T>
    {
        var requiredArg = input!.ToParseableTagArg<T>("X");
        var nullArg = "".ToParseableTagArg<T>("X", true);
        var nonNullArg = input!.ToParseableTagArg<T>("X", true);

        requiredArg.Tag.Should().Be(Tag.Create("X"));
        nullArg.Tag.Should().Be(Tag.Create("X"));
        nonNullArg.Tag.Should().Be(Tag.Create("X"));

        requiredArg.IsValid.Should().BeTrue();
        nullArg.IsValid.Should().BeTrue();
        nonNullArg.IsValid.Should().BeTrue();

        requiredArg.Message.Should().BeNull();
        nullArg.Message.Should().BeNull();
        nonNullArg.Message.Should().BeNull();

        requiredArg.Arg.Should().Be(expected);
        nullArg.Arg.Should().Be(default(T));
        nonNullArg.Arg.Should().Be(expected);
    }
}