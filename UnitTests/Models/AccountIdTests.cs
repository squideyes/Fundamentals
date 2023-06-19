//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using FluentAssertions;
//using SquidEyes.Fundamentals;
//using Vogen;

//namespace SquidEyes.UnitTests;

//public class AccountIdTests
//{
//    [Fact]
//    public void From_ValidInput_Constructs() =>
//        AccountId.From("ABCDEFGHT092");

//    [Fact]
//    public void CustomFrom_ValidInput_Constructs() =>
//        AccountId.From(ClientId.From("ABCDEFGH"), 'B', 333);

//    [Fact]
//    public void GetDerivedValues_ValidInput_ReturnsExpected()
//    {
//        var clientId = ClientId.Next();
//        char code = 'X';
//        var ordinal = 475;

//        var accountId = AccountId.From(clientId, code, ordinal);

//        accountId.GetClientId().Should().Be(clientId);
//        accountId.GetCode().Should().Be(code);
//        accountId.GetOrdinal().Should().Be(ordinal);
//    }

//    [Fact]
//    public void ToString_ValidInput_ReturnsFormattedString()
//    {
//        AccountId.From(ClientId.From("ABCDEFGH"), 'R', 14)
//            .ToString().Should().Be("ABCDEFGHR014");
//    }

//    [Theory]
//    [InlineData('A', 1, true)]
//    [InlineData('Z', 1, true)]
//    [InlineData('A', 999, true)]
//    [InlineData('Z', 999, true)]
//    [InlineData('0', 1, false)]
//    [InlineData('A', 0, false)]
//    [InlineData('A', 1000, false)]
//    public void IsValue_MixedInput_ReturnsExpected(
//        char letter, int ordinal, bool expected)
//    {
//        var value = $"ABCDEFGH{letter}{ordinal:000}";

//        AccountId.IsValue(value).Should().Be(expected);
//    }

//    [Theory]
//    [InlineData("BAD_DATA")]
//    [InlineData("")]
//    [InlineData(null)]
//    public void From_InvalidInput_ThrowsValueObjectValidationException(string input)
//    {
//        FluentActions.Invoking(() => AccountId.From(input))
//            .Should().Throw<ValueObjectValidationException>();
//    }
//}