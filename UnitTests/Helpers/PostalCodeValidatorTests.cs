using FluentAssertions;
using static SquidEyes.Fundamentals.PostalCodeValidator;

namespace SquidEyes.UnitTests;

public class PostalCodeValidatorTests
{
    [Theory]
    [InlineData("12345", "US", true)]
    [InlineData("A", "US", false)]
    [InlineData("12345", "XX", false)]
    public void IsPostalCode_InvalidInput_ReturnsFalse(
        string code, string country, bool expected)
    {
        IsPostalCode(code, country).Should().Be(expected);
    }
}
