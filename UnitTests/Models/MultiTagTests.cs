// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class MultiTagTests
{
    [Theory]
    [InlineData("A")]
    [InlineData("A:B")]
    [InlineData("A:B:C")]
    [InlineData("A:B:C:D")]
    public void From_ValidInput_Constructs(string value) =>
        _ = MultiTag.From(value);

    [Fact]
    public void From_ValidInput_ReturnsCount() =>
        MultiTag.From("A:B").Count.Should().Be(2);

    [Theory]
    [InlineData("Aa", true)]
    [InlineData("Aa:Bb", true)]
    [InlineData("Aa:Bb:Cc", true)]
    [InlineData("Aa:Bb:Cc:Dd", true)]
    [InlineData("A", true)]
    [InlineData("A:B", true)]
    [InlineData("A:B:C", true)]
    [InlineData("A:B:C:D", true)]
    [InlineData("A:B:C:D:", false)]
    [InlineData(" A:B:C:D", false)]
    [InlineData("A:B:C:D ", false)]
    [InlineData("A :B:C:D", false)]
    [InlineData("A: B:C:D", false)]
    [InlineData("a:b:c:d", false)]
    [InlineData("A:B:C:D:E:F:G:H:I:J", true)]
    [InlineData("A:B:C:D:E:F:G:H:I:J:K", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValue_ValidInput_ReturnsExpected(string value, bool expected) =>
        MultiTag.IsValue(value).Should().Be(expected);
}