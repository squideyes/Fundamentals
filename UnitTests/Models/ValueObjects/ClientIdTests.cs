// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ClientIdTests
{
    [Fact]
    public void Create_GoodInput_Contructs()
    {
        const string INPUT = "AAAAAAAA";

        var clientId = ClientId.Create(INPUT);

        clientId.Value.Should().Be(INPUT);
        clientId.Input.Should().Be(INPUT);
        clientId.ToString().Should().Be(INPUT);
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
        FluentActions.Invoking(() => ClientId.Create(input))
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
        input.IsClientIdInput().Should().Be(expected);

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
        ClientId.TryCreate(input, out var _).Should().Be(expected);

    [Fact]
    public void Next_NoInput_Constructs()
    {
        var clientId = ClientId.Next();

        clientId.Value.Should().Be(clientId.Input);
        clientId.ToString().Should().Be(clientId.Input);
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
        ((ClientId)null! == null!).Should().BeTrue();
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
        ((ClientId)null! != null!).Should().BeFalse();
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
        var clientId = ClientId.Create("AAAAAAAA");

        clientId.GetHashCode().Should().Be(clientId.Input!.GetHashCode());
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

    private static (ClientId A1, ClientId A2, ClientId B) GetInstances()
    {
        var a1 = ClientId.Create("AAAAAAAA");
        var a2 = ClientId.Create("AAAAAAAA");
        var b = ClientId.Create("BBBBBBBB");

        return (a1, a2, b);
    }
}