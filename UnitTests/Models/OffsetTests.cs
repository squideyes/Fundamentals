// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;
using Vogen;

namespace SquidEyes.UnitTests;

public class OffsetTests
{
    [Theory]
    [InlineData(Vector.Gain, 0.1, true)]
    [InlineData(Vector.Loss, 0.1, true)]
    [InlineData(Vector.Gain, 999.999999, true)]
    [InlineData(Vector.Loss, 999.999999, true)]
    [InlineData((Vector)0, 1.0, false)]
    [InlineData(Vector.Gain, 0.0, false)]
    [InlineData(Vector.Loss, 0.0, false)]
    [InlineData(Vector.Gain, 1000.0, false)]
    [InlineData(Vector.Loss, 1000.0, false)]
    public void IsValue_MixedInput_ReturnsExpected(
        Vector vector, double delta, bool expected)
    {
        var value = $"{vector}={delta}";
        
        Offset.IsValue(value).Should().Be(expected);
    }

    [Fact]
    public void From_ValidInput_Constructs() =>
        Offset.From("Loss=1.3");

    [Fact]
    public void CustomFrom_ValidInput_Constructs() =>
        Offset.From(Vector.Gain, 9.7);

    [Fact]
    public void GetDerivedValues_ValidInput_ReturnsExpected()
    {
        var offset = Offset.From(Vector.Gain, 4.2);

        offset.GetVector().Should().Be(Vector.Gain);
        offset.GetDelta().Should().Be(4.2);
    }

    [Fact]
    public void ToString_ValidInput_ReturnsFormattedString()=>
        Offset.From(Vector.Gain, 3.5).ToString().Should().Be("Gain=3.5");

    [Theory]
    [InlineData("BAD_DATA")]
    [InlineData("")]
    [InlineData(null)]
    public void From_InvalidInput_ThrowsValueObjectValidationException(string input)
    {
        FluentActions.Invoking(() => AccountId.From(input))
            .Should().Throw<ValueObjectValidationException>();
    }
}