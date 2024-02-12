// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class SemVerTests
{
    [Fact]
    public void Create_GoodInput_Contructs()
    {
        const string INPUT = "v1.2.3-alpha";

        var actorId = SemVer.Create(INPUT);

        actorId.Major.Should().Be(1);
        actorId.Minor.Should().Be(2);
        actorId.Patch.Should().Be(3);
        actorId.Label.Should().Be("alpha");
        actorId.Input.Should().Be(INPUT);
        actorId.ToString().Should().Be(INPUT);
    }

    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("1.2.3-alpha")]
    [InlineData("1.2.3")]
    [InlineData("vX.2.3-alpha")]
    [InlineData("v1.X.3-alpha")]
    [InlineData("v1.2.X-alpha")]
    [InlineData("vX.2.3")]
    [InlineData("v1.X.3")]
    [InlineData("v1.2.X")]
    [InlineData("v 1.2.3-alpha")]
    [InlineData("v1 .2.3-alpha")]
    [InlineData("v1. 2.3-alpha")]
    [InlineData("v1.2 .3-alpha")]
    [InlineData("v1.2. 3-alpha")]
    [InlineData("v1.2.3 -alpha")]
    [InlineData("v1.2.3- alpha")]
    [InlineData("v1.2.3-alpha ")]
    [InlineData("v 1.2.3")]
    [InlineData("v1 .2.3")]
    [InlineData("v1. 2.3")]
    [InlineData("v1.2 .3")]
    [InlineData("v1.2. 3")]
    [InlineData("v1.2.3 ")]
    [InlineData("v1.2.3- ")]
    [InlineData("v1.2.3 -")]
    [InlineData("v0.2.3-alpha")]
    [InlineData("v1.0.3-alpha")]
    [InlineData("v1.2.0-alpha")]
    [InlineData("v1.2.3-")]
    [InlineData("v1.2.3-abcdefghijk")]
    [InlineData("v0.2.3")]
    [InlineData("v1.0.3")]
    [InlineData("v1.2.0")]
    [InlineData("v1.2.3-ALPHA")]
    [InlineData("v1.2.3-AL@HA")]
    public void Create_BadInput_ThrowsError(string? input)
    {
        FluentActions.Invoking(() => SemVer.Create(input!))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("v1.2.3-alpha", true)]
    [InlineData("v1.2.3", true)]
    [InlineData(null!, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("1.2.3-alpha", false)]
    [InlineData("1.2.3", false)]
    [InlineData("vX.2.3-alpha", false)]
    [InlineData("v1.X.3-alpha", false)]
    [InlineData("v1.2.X-alpha", false)]
    [InlineData("vX.2.3", false)]
    [InlineData("v1.X.3", false)]
    [InlineData("v1.2.X", false)]
    [InlineData("v 1.2.3-alpha", false)]
    [InlineData("v1 .2.3-alpha", false)]
    [InlineData("v1. 2.3-alpha", false)]
    [InlineData("v1.2 .3-alpha", false)]
    [InlineData("v1.2. 3-alpha", false)]
    [InlineData("v1.2.3 -alpha", false)]
    [InlineData("v1.2.3- alpha", false)]
    [InlineData("v1.2.3-alpha ", false)]
    [InlineData("v 1.2.3", false)]
    [InlineData("v1 .2.3", false)]
    [InlineData("v1. 2.3", false)]
    [InlineData("v1.2 .3", false)]
    [InlineData("v1.2. 3", false)]
    [InlineData("v1.2.3 ", false)]
    [InlineData("v1.2.3- ", false)]
    [InlineData("v1.2.3 -", false)]
    [InlineData("v0.2.3-alpha", false)]
    [InlineData("v1.0.3-alpha", false)]
    [InlineData("v1.2.0-alpha", false)]
    [InlineData("v1.2.3-", false)]
    [InlineData("v1.2.3-abcdefghijk", false)]
    [InlineData("v0.2.3", false)]
    [InlineData("v1.0.3", false)]
    [InlineData("v1.2.0", false)]
    [InlineData("v1.2.3-ALPHA", false)]
    [InlineData("v1.2.3-AL@HA", false)]
    public void IsValue_MixedInput_ReturnsExpected(string? input, bool expected) =>
        input!.IsSemVerInput().Should().Be(expected);

    [Theory]
    [InlineData("v1.2.3-alpha", true)]
    [InlineData("v1.2.3", true)]
    [InlineData(null!, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("1.2.3-alpha", false)]
    [InlineData("1.2.3", false)]
    [InlineData("vX.2.3-alpha", false)]
    [InlineData("v1.X.3-alpha", false)]
    [InlineData("v1.2.X-alpha", false)]
    [InlineData("vX.2.3", false)]
    [InlineData("v1.X.3", false)]
    [InlineData("v1.2.X", false)]
    [InlineData("v 1.2.3-alpha", false)]
    [InlineData("v1 .2.3-alpha", false)]
    [InlineData("v1. 2.3-alpha", false)]
    [InlineData("v1.2 .3-alpha", false)]
    [InlineData("v1.2. 3-alpha", false)]
    [InlineData("v1.2.3 -alpha", false)]
    [InlineData("v1.2.3- alpha", false)]
    [InlineData("v1.2.3-alpha ", false)]
    [InlineData("v 1.2.3", false)]
    [InlineData("v1 .2.3", false)]
    [InlineData("v1. 2.3", false)]
    [InlineData("v1.2 .3", false)]
    [InlineData("v1.2. 3", false)]
    [InlineData("v1.2.3 ", false)]
    [InlineData("v1.2.3- ", false)]
    [InlineData("v1.2.3 -", false)]
    [InlineData("v0.2.3-alpha", false)]
    [InlineData("v1.0.3-alpha", false)]
    [InlineData("v1.2.0-alpha", false)]
    [InlineData("v1.2.3-", false)]
    [InlineData("v1.2.3-abcdefghijk", false)]
    [InlineData("v0.2.3", false)]
    [InlineData("v1.0.3", false)]
    [InlineData("v1.2.0", false)]
    [InlineData("v1.2.3-ALPHA", false)]
    [InlineData("v1.2.3-AL@HA", false)]
    public void TryCreate_MixedInput_ReturnsExpected(string? input, bool expected) =>
        SemVer.TryCreate(input!, out var _).Should().Be(expected);

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
        ((SemVer)null! == null!).Should().BeTrue();
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
        ((SemVer)null! != null!).Should().BeFalse();
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
        var actorId = SemVer.Create("v1.2.3-alpha");

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

    private static (SemVer A1, SemVer A2, SemVer B) GetInstances()
    {
        var a1 = SemVer.Create("v1.2.3-alpha");
        var a2 = SemVer.Create("v1.2.3-alpha");
        var b = SemVer.Create("v1.2.4-alpha");

        return (a1, a2, b);
    }
}