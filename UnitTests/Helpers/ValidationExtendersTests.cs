using FluentAssertions;
using FluentValidation;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ValidationExtendersTests
{
    private class Data
    {
        public class Validator : AbstractValidator<Data>
        {
            public Validator()
            {
                RuleFor(x => x.KeyValues!)
                    .IsNonNullAndHasItems();
            }
        }

        public string? Code { get; set; }
        public Dictionary<string, string>? KeyValues { get; set; }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void IsNonNullAndHasItems_MixedArgs_ReturnExpected(bool hasKeyValues)
    {
        var data = new Data();

        if (hasKeyValues)
        {
            data!.KeyValues = new Dictionary<string, string>()
            {
                { "UserId", "ABC123" }
            };
        }

        var validator = new Data.Validator();

        var result = validator.Validate(data!);

        result.IsValid.Should().Be(hasKeyValues);
    }
}
