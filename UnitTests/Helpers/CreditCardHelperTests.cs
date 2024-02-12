// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

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

    [Theory]
    [InlineData("4012 8888 8888 1881")]
    [InlineData("4111 1111 1111 1111")]
    [InlineData("4917 6100 0000 0000")]
    [InlineData("5274 5763 9425 9961")]
    [InlineData("5555 5555 5555 4444")]
    [InlineData("5105 1051 0510 5100")]
    [InlineData("3714 496353 98431")]
    [InlineData("3787 344936 71000")]
    [InlineData("3400 000000 00009")]
    [InlineData("3411 111111 11111")]
    [InlineData("3434 343434 34343")]
    [InlineData("3468 276304 35344")]
    [InlineData("3700 000000 00002")]
    [InlineData("3700 002000 00000")]
    [InlineData("3704 072699 09809")]
    [InlineData("3705 560193 09221")]
    [InlineData("3742 000000 00004")]
    [InlineData("3764 622809 21451")]
    [InlineData("3777 527498 96404")]
    [InlineData("3782 822463 10005")]
    [InlineData("6011 1111 1111 1117")]
    [InlineData("6011 0009 9013 9424")]
    [InlineData("6011 0000 0000 0004")]
    [InlineData("6011 0004 0000 0000")]
    [InlineData("6011 1532 1637 1980")]
    [InlineData("6011 6011 6011 6611")]
    [InlineData("6011 6874 8256 4166")]
    [InlineData("6011 8148 3690 5651")]
    public void From_ValidInput_Constructs(string cardNumber) =>
        CreditCardValidator.IsNumber(cardNumber).Should().BeTrue();

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
    [InlineData(null!)]
    public void From_InvalidInput_ReturnsFalse(string? cardNumber) =>
        CreditCardValidator.IsNumber(cardNumber!).Should().BeFalse();
}