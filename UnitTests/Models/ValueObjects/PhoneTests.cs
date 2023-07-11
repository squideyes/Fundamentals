// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class PhoneTests
{
    [Fact]
    public void Create_GoodInput_Contructs()
    {
        const string INPUT = "+442079476330";

        var actorId = Phone.Create(INPUT);

        actorId.Value.Should().Be(INPUT);
        actorId.Input.Should().Be(INPUT);
        actorId.ToString().Should().Be(INPUT);
    }

    [Theory]
    [InlineData("1+12153165555")]
    [InlineData("+1215316555")]
    [InlineData("+121531655555")]
    [InlineData("")]
    [InlineData(null)]
    public void Create_BadInput_ThrowsError(string input)
    {
        FluentActions.Invoking(() => Phone.Create(input))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("+442079476330", true)]
    [InlineData("+44 207 947 6330", true)]
    [InlineData("215-316-5555", true)]
    [InlineData("1215-316-5555", true)]
    [InlineData("12153165555", true)]
    [InlineData("+12153165555", true)]
    [InlineData("+1 215-316-5555", true)]
    [InlineData("+1 (215) 316-5555", true)]
    [InlineData("+1 (215) 3165555", true)]
    [InlineData("1+12153165555", false)]
    [InlineData("+1215316555", false)]
    [InlineData("+121531655555", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValue_MixedInput_ReturnsExpected(string input, bool expected) =>
        input.IsPhoneInput().Should().Be(expected);

    [Theory]
    [InlineData("+442079476330", true)]
    [InlineData("+44 207 947 6330", true)]
    [InlineData("215-316-5555", true)]
    [InlineData("1215-316-5555", true)]
    [InlineData("12153165555", true)]
    [InlineData("+12153165555", true)]
    [InlineData("+1 215-316-5555", true)]
    [InlineData("+1 (215) 316-5555", true)]
    [InlineData("+1 (215) 3165555", true)]
    [InlineData("1+12153165555", false)]
    [InlineData("+1215316555", false)]
    [InlineData("+121531655555", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void TryCreate_MixedInput_ReturnsExpected(string input, bool expected) =>
        Phone.TryCreate(input, out var _).Should().Be(expected);

    [Fact]
    public void TypeEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.Equals(a2).Should().BeTrue();
        a1.Equals(b).Should().BeFalse();
        a2.Equals(a1).Should().BeTrue();
        a2.Equals(b).Should().BeFalse();
        b.Equals(a1).Should().BeFalse();
        b.Equals(a2).Should().BeFalse();
        a1.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void OperatorEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 == a2).Should().BeTrue();
        (a1 == b).Should().BeFalse();
        (a2 == a1).Should().BeTrue();
        (a2 == b).Should().BeFalse();
        (b == a1).Should().BeFalse();
        (b == a2).Should().BeFalse();
        (a1 == null).Should().BeFalse();
        ((Phone)null! == null!).Should().BeTrue();
        (null! == a1).Should().BeFalse();
        (null! == a2).Should().BeFalse();
        (null! == b).Should().BeFalse();
    }

    [Fact]
    public void OperatorNotEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 != a2).Should().BeFalse();
        (a1 != b).Should().BeTrue();
        (a2 != a1).Should().BeFalse();
        (a2 != b).Should().BeTrue();
        (b != a1).Should().BeTrue();
        (b != a2).Should().BeTrue();
        (a1 != null).Should().BeTrue();
        ((Phone)null! != null!).Should().BeFalse();
        (null! != a1).Should().BeTrue();
        (null! != a2).Should().BeTrue();
        (null! != b).Should().BeTrue();
    }

    [Fact]
    public void ObjectEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.Equals((object)a2).Should().BeTrue();
        a1.Equals((object)b).Should().BeFalse();
        a2.Equals((object)a1).Should().BeTrue();
        a2.Equals((object)b).Should().BeFalse();
        b.Equals((object)a1).Should().BeFalse();
        b.Equals((object)a2).Should().BeFalse();
        a1.Equals((object)null!).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_GoodInput_EqualsInputGetHashCode()
    {
        var actorId = Phone.Create("+442079476330");

        actorId.GetHashCode().Should().Be(actorId.Input!.GetHashCode());
    }

    [Fact]
    public void CompareTo_MixedInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.CompareTo(a2).Should().Be(0);
        a1.CompareTo(null).Should().Be(1);
        a1.CompareTo(b).Should().Be(-1);
        b.CompareTo(a1).Should().Be(1);
    }

    [Fact]
    public void OperatorCompare_MixedInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 < a2).Should().BeFalse();
        (a1 <= a2).Should().BeTrue();
        (a1 < b).Should().BeTrue();
        (a1 <= b).Should().BeTrue();
        (a1 > a2).Should().BeFalse();
        (a1 >= a2).Should().BeTrue();
        (b > a1).Should().BeTrue();
        (b >= a1).Should().BeTrue();
    }

    private static (Phone A1, Phone A2, Phone B) GetInstances()
    {
        var a1 = Phone.Create("+442079476330");
        var a2 = Phone.Create("+442079476330");
        var b = Phone.Create("+442079476331");

        return (a1, a2, b);
    }
}