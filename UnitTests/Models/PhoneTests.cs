// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class PhoneTests
{
    [Theory]
    [InlineData("+442079476330", "+442079476330")]
    [InlineData("+44 207 947 6330", "+442079476330")]
    [InlineData("215-316-5555", "+12153165555")]
    [InlineData("1215-316-5555", "+12153165555")]
    [InlineData("12153165555", "+12153165555")]
    [InlineData("+1 215-316-5555", "+12153165555")]
    [InlineData("+12153165555", "+12153165555")]
    [InlineData("+1 (215) 316-5555", "+12153165555")]
    [InlineData("+1 (215) 3165555", "+12153165555")]
    public void From_ValidInput_ConstructsAndNormalizes(
        string original, string normalized)
    {
        Phone.From(original).Value.Should().Be(normalized);
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
    public void IsValue_ValidInput_ReturnsExpected(string value, bool expected) =>
        Phone.IsValue(value).Should().Be(expected);
}