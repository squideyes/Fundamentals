// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class TokenTests
{
    [Fact]
    public void From_ValidInput_Constructs() =>
        _ = Token.From("AAAAAAAA");

    [Theory]
    [InlineData("A", true)]
    [InlineData("Aa", true)]
    [InlineData(" Aa", false)]
    [InlineData("Aa ", false)]
    [InlineData("A a", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void IsValue_ValidInput_ReturnsExpected(string value, bool expected) =>
        Token.IsValue(value).Should().Be(expected);
}