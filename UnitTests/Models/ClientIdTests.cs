//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using FluentAssertions;
//using SquidEyes.Fundamentals;

//namespace SquidEyes.UnitTests;

//public class ClientIdTests
//{
//    [Fact]
//    public void ClientId_WithGoodValue_Constructs() =>
//        _ = ClientId.From("AAAAAAAA");

//    [Fact]
//    public void ClientId_Next_ReturnsValidClientId() =>
//        _ = ClientId.From(ClientId.Next().Value);

//    [Theory]
//    [InlineData("ABCDEFGH", true)]
//    [InlineData("JKLMNPQR", true)]
//    [InlineData("STUVWXYZ", true)]
//    [InlineData("23456789", true)]
//    [InlineData(" AAAAAAAA", false)]
//    [InlineData("AAAAAAAA ", false)]
//    [InlineData("AAAA AAAA", false)]
//    [InlineData("AAAAAAA", false)]
//    [InlineData("IAAAAAAA", false)]
//    [InlineData("1AAAAAAA", false)]
//    [InlineData("OAAAAAAA", false)]
//    [InlineData("0AAAAAAA", false)]
//    [InlineData("", false)]
//    [InlineData(null, false)]
//    public void ClientId_IsValue_ReturnsExpected(string value, bool expected) =>
//        ClientId.IsValue(value).Should().Be(expected);
//}