// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;
using System.Text.Json;

namespace SquidEyes.UnitTests;

public class CreditCardHelperTests
{
    public class ValidCards
    {
        public required string[] Amex { get; init; }
        public required string[] Discover { get; init; }
        public required string[] MasterCard { get; init; }
        public required string[] Visa { get; init; }
    }

    [Fact]
    public void From_ValidInput_Constructs()
    {
        static void Validate(string[] cardNumbers)
        {
            foreach (var cardNumber in cardNumbers)
                CreditCardHelper.IsValid(cardNumber).Should().BeTrue();
        }

        var validCards = JsonSerializer.Deserialize<ValidCards>(
            Properties.Resources.ValidCards);

        Validate(validCards!.Amex);
        Validate(validCards!.Discover);
        Validate(validCards!.MasterCard);
        Validate(validCards!.Visa);
    }

    [Theory]
    [InlineData("4922986976940144")]
    [InlineData("4658926229496179")]
    [InlineData("4891859735467637")]
    [InlineData("4869921224781795")]
    [InlineData("5593668189576157")]
    [InlineData("5452791180744634")]
    [InlineData("5421541287547014")]
    [InlineData("5446017899742218")]
    [InlineData("3478454299366")]
    [InlineData("3417044433242")]
    [InlineData("3446411637273")]
    [InlineData("3443105457366")]
    [InlineData("6011208963375840")]
    [InlineData("6011237243064506")]
    [InlineData("6011916637568478")]
    [InlineData("6011606375516779")]
    [InlineData("")]
    [InlineData(null)]
    public void From_InvalidInput_ReturnsFalse(string cardNumber) =>
        CreditCardHelper.IsValid(cardNumber).Should().BeFalse();
}