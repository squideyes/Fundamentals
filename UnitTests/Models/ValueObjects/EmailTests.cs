// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class EmailTests
{
    [Fact]
    public void Create_GoodInput_Contructs()
    {
        const string INPUT = "somedude@someco.com";

        var actorId = Email.Create(INPUT);

        actorId.Value.Should().Be(INPUT);
        actorId.Input.Should().Be(INPUT);
        actorId.ToString().Should().Be(INPUT);
    }

    [Theory]
    [InlineData(" somedude@someco.com")]
    [InlineData("somedude@someco.com ")]
    [InlineData("somedudesomeco.com")]
    [InlineData("some dude@someco.com")]
    [InlineData("somedude@some co.com")]
    [InlineData("somedude@someco.")]
    [InlineData("somedude@someco")]
    [InlineData("somedudesomecocom")]
    [InlineData("some_dude@someco.com")]
    [InlineData("somedude@some_co.com")]
    [InlineData("somedude@10-minute-mail.com")]
    [InlineData("somedude@some_co.scam")]
    public void Create_BadInput_ThrowsError(string input)
    {
        FluentActions.Invoking(() => Email.Create(input))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("somedude@someco.com", true)]
    [InlineData("a-b@c-d.com", true)]
    [InlineData(" somedude@someco.com", false)]
    [InlineData("somedude@someco.com ", false)]
    [InlineData("somedudesomeco.com", false)]
    [InlineData("some dude@someco.com", false)]
    [InlineData("somedude@some co.com", false)]
    [InlineData("somedude@someco.", false)]
    [InlineData("somedude@someco", false)]
    [InlineData("somedudesomecocom", false)]
    [InlineData("some_dude@someco.com", false)]
    [InlineData("somedude@some_co.com", false)]
    [InlineData("somedude@10-minute-mail.com", false)]
    [InlineData("somedude@some_co.scam", false)]
    [InlineData("", false)]
    [InlineData(null!, false)]
    public void IsValue_MixedInput_ReturnsExpected(string? input, bool expected) =>
        input!.IsEmailInput().Should().Be(expected);

    [Theory]
    [InlineData("somedude@someco.com", true)]
    [InlineData("a-b@c-d.com", true)]
    [InlineData(" somedude@someco.com", false)]
    [InlineData("somedude@someco.com ", false)]
    [InlineData("somedudesomeco.com", false)]
    [InlineData("some dude@someco.com", false)]
    [InlineData("somedude@some co.com", false)]
    [InlineData("somedude@someco.", false)]
    [InlineData("somedude@someco", false)]
    [InlineData("somedudesomecocom", false)]
    [InlineData("some_dude@someco.com", false)]
    [InlineData("somedude@some_co.com", false)]
    [InlineData("somedude@10-minute-mail.com", false)]
    [InlineData("somedude@some_co.scam", false)]
    [InlineData("", false)]
    [InlineData(null!, false)]
    public void TryCreate_MixedInput_ReturnsExpected(string? input, bool expected) =>
        Email.TryCreate(input!, out var _).Should().Be(expected);

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
        ((Email)null! == null!).Should().BeTrue();
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
        ((Email)null! != null!).Should().BeFalse();
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
        var actorId = Email.Create("somedude@someco.com");

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

    private static (Email A1, Email A2, Email B) GetInstances()
    {
        var a1 = Email.Create("dude1@someco.com");
        var a2 = Email.Create("dude1@someco.com");
        var b = Email.Create("dude2@someco.com");

        return (a1, a2, b);
    }
}