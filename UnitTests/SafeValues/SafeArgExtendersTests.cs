// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class SafeArgExtendersTests
{
    [Fact]
    public void GoodValues_Should_Parse()
    {
        SoftValueTest("0", byte.MinValue);
        SoftValueTest("255", byte.MaxValue);

        SoftValueTest("A", 'A');
        SoftValueTest("Z", 'Z');

        SoftValueTest("-32768", short.MinValue);
        SoftValueTest("32767", short.MaxValue);

        SoftValueTest("-2147483648", int.MinValue);
        SoftValueTest("2147483647", int.MaxValue);

        SoftValueTest("-9223372036854775808", long.MinValue);
        SoftValueTest("9223372036854775807", long.MaxValue);

        SoftValueTest("2.71828175", float.E);
        SoftValueTest("1.401298E-45", float.Epsilon);
        SoftValueTest("3.40282347E+38", float.MaxValue);
        SoftValueTest("-3.40282347E+38", float.MinValue);
        SoftValueTest("NaN", float.NaN);
        SoftValueTest("-∞", float.NegativeInfinity);
        SoftValueTest("-0", float.NegativeZero);
        SoftValueTest("3.14159274", float.Pi);
        SoftValueTest("∞", float.PositiveInfinity);
        SoftValueTest("6.28318548", float.Tau);

        SoftValueTest("2.7182818284590451", double.E);
        SoftValueTest("4.94065645841247E-324", double.Epsilon);
        SoftValueTest("1.7976931348623157E+308", double.MaxValue);
        SoftValueTest("-1.7976931348623157E+308", double.MinValue);
        SoftValueTest("NaN", double.NaN);
        SoftValueTest("-∞", double.NegativeInfinity);
        SoftValueTest("-0", double.NegativeZero);
        SoftValueTest("3.1415926535897931", double.Pi);
        SoftValueTest("∞", double.PositiveInfinity);
        SoftValueTest("6.2831853071795862", double.Tau);

        SoftValueTest("01/01/0001 00:00:00.000", DateTime.MinValue);
        SoftValueTest("12/31/9999 23:59:59.9999999", DateTime.MaxValue);

        SoftValueTest("-10675199.02:48:05.4775808", TimeSpan.MinValue);
        SoftValueTest("10675199.02:48:05.4775807", TimeSpan.MaxValue);

        SoftValueTest("00:00:00.000", TimeOnly.MinValue);
        SoftValueTest("23:59:59.9999999", TimeOnly.MaxValue);

        SoftValueTest("01/01/0001", DateOnly.MinValue);
        SoftValueTest("12/31/9999", DateOnly.MaxValue);

        Guid.NewGuid().Do(v => SoftValueTest(v.ToString(), v));

        "A".ToSoftString("X").Value.Should().Be("A");
        "".ToSoftString("X", true).Value.Should().BeNull();
        "A".ToSoftString("X").Value.Should().Be("A");

        const string URI = "https://cnn.com";

        URI.ToSoftUri("X").Value.Should().Be(new Uri(URI));
        "".ToSoftUri("X", UriKind.Absolute, true).Value.Should().BeNull();
        URI.ToSoftUri("X").Value.Should().Be(new Uri(URI));

        "Absolute".ToSoftEnum<UriKind>("X").Value.Should().Be(UriKind.Absolute);
        "".ToSoftEnum<UriKind>("X", true).Value.Should().BeNull();
        "Absolute".ToSoftEnum<UriKind>("X").Value.Should().Be(UriKind.Absolute);
    }

    private static void SoftValueTest<T>(string input, T expected)
        where T : struct, IParsable<T>
    {
        var requiredValue = input!.ToSoftValue<T>("X");
        var nullValue = "".ToSoftValue<T>("X", true);
        var nonNullValue = input!.ToSoftValue<T>("X", true);

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