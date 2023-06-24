using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class DeltaTests
{
    [Theory]
    [InlineData(Vector.Gain, 0.25)]
    [InlineData(Vector.Loss, 1.5)]
    public void Create_GoodInput_Contructs(Vector vector, double value)
    {
        var input = $"{vector}={value}";

        var clientId = Delta.Create(input);

        clientId.Vector.Should().Be(vector);
        clientId.Value.Should().Be(value);
        clientId.Input.Should().Be(input);
        clientId.ToString().Should().Be(input);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(" Gain=0.25")]
    [InlineData("Gain=0.25 ")]
    [InlineData("Gain =0.25")]
    [InlineData("Gain= 0.25")]
    [InlineData("Gain = 0.25")]
    [InlineData("=0.25")]
    [InlineData("Gain=")]
    [InlineData("Gain=XXX")]
    [InlineData("XXX=0.25")]
    [InlineData("Gain0.25")]
    public void Create_BadInput_ThrowsError(string input)
    {
        FluentActions.Invoking(() => Delta.Create(input))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("Gain=0.25", true)]
    [InlineData("Loss=1", true)]
    [InlineData("gain=0.25", true)]
    [InlineData("loss=1", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData(" Gain=0.25", false)]
    [InlineData("Gain=0.25 ", false)]
    [InlineData("Gain =0.25", false)]
    [InlineData("Gain= 0.25", false)]
    [InlineData("Gain = 0.25", false)]
    [InlineData("=0.25", false)]
    [InlineData("Gain=", false)]
    [InlineData("Gain=XXX", false)]
    [InlineData("XXX=0.25", false)]
    [InlineData("Gain0.25", false)]
    public void IsValue_MixedInput_ReturnsExpected(string input, bool expected) =>
        input.IsOffsetInput().Should().Be(expected);

    [Theory]
    [InlineData("Gain=0.25", true)]
    [InlineData("Loss=1", true)]
    [InlineData("gain=0.25", true)]
    [InlineData("loss=1", true)]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData(" Gain=0.25", false)]
    [InlineData("Gain=0.25 ", false)]
    [InlineData("Gain =0.25", false)]
    [InlineData("Gain= 0.25", false)]
    [InlineData("Gain = 0.25", false)]
    [InlineData("=0.25", false)]
    [InlineData("Gain=", false)]
    [InlineData("Gain=XXX", false)]
    [InlineData("XXX=0.25", false)]
    [InlineData("Gain0.25", false)]
    public void TryCreate_MixedInput_ReturnsExpected(string input, bool expected) =>
        Delta.TryCreate(input, out var _).Should().Be(expected);

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
        ((Delta)null! == null!).Should().BeTrue();
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
        ((Delta)null! != null!).Should().BeFalse();
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
        var clientId = Delta.Create("Gain=0.25");

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

    private static (Delta A1, Delta A2, Delta B) GetInstances()
    {
        var a1 = Delta.Create("Gain=0.25");
        var a2 = Delta.Create("Gain=0.25");
        var b = Delta.Create("Loss=1.5");

        return (a1, a2, b);
    }
}
