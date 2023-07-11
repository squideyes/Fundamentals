// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ActorIdTests
{
    [Fact]
    public void Create_GoodInput_Contructs()
    {
        const string INPUT = "AAAAAAAA";

        var actorId = ActorId.Create(INPUT);

        actorId.Value.Should().Be(INPUT);
        actorId.Input.Should().Be(INPUT);
        actorId.ToString().Should().Be(INPUT);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("AAAAAAA")]
    [InlineData("AAAAAAAa")]
    [InlineData("AAAAAAAA ")]
    [InlineData(" AAAAAAAA")]
    [InlineData("AAAAAAAI")]
    [InlineData("AAAAAAA1")]
    [InlineData("AAAAAAAO")]
    [InlineData("AAAAAAA0")]
    public void Create_BadInput_ThrowsError(string input)
    {
        FluentActions.Invoking(() => ActorId.Create(input))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("AAAAAAAA", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("AAAAAAA", false)]
    [InlineData("AAAAAAAa", false)]
    [InlineData("AAAAAAAA ", false)]
    [InlineData(" AAAAAAAA", false)]
    [InlineData("AAAAAAAI", false)]
    [InlineData("AAAAAAA1", false)]
    [InlineData("AAAAAAAO", false)]
    [InlineData("AAAAAAA0", false)]
    public void IsValue_MixedInput_ReturnsExpected(string input, bool expected) =>
        input.IsActorIdInput().Should().Be(expected);

    [Theory]
    [InlineData("AAAAAAAA", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("AAAAAAA", false)]
    [InlineData("AAAAAAAa", false)]
    [InlineData("AAAAAAAA ", false)]
    [InlineData(" AAAAAAAA", false)]
    [InlineData("AAAAAAAI", false)]
    [InlineData("AAAAAAA1", false)]
    [InlineData("AAAAAAAO", false)]
    [InlineData("AAAAAAA0", false)]
    public void TryCreate_MixedInput_ReturnsExpected(string input, bool expected) =>
        ActorId.TryCreate(input, out var _).Should().Be(expected);

    [Fact]
    public void Next_NoInput_Constructs()
    {
        var actorId = ActorId.Next();

        actorId.Value.Should().Be(actorId.Input);
        actorId.ToString().Should().Be(actorId.Input);
    }

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
        ((ActorId)null! == null!).Should().BeTrue();
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
        ((ActorId)null! != null!).Should().BeFalse();
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
        var actorId = ActorId.Create("AAAAAAAA");

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

    private static (ActorId A1, ActorId A2, ActorId B) GetInstances()
    {
        var a1 = ActorId.Create("AAAAAAAA");
        var a2 = ActorId.Create("AAAAAAAA");
        var b = ActorId.Create("BBBBBBBB");

        return (a1, a2, b);
    }
}