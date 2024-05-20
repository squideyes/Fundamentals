using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class AddressTests
{
    [Theory]
    [InlineData("Second Address Line", true)]
    [InlineData("", false)]
    [InlineData(null!, true)]
    public void Address2_WithMixedValues_ShouldValidate(
        string? address2, bool expected)
    {
        var address = new Address()
        {
            Address1 = "123 Main Street",
            Address2 = address2,
            Country = "US",
            Locality = "Some Town",
            Region = "Somewhere",
            PostalCode ="12345"
        };

        var validator = new Address.Validator();

        var result = validator.Validate(address);

        result.IsValid.Should().Be(expected);
    }
}
