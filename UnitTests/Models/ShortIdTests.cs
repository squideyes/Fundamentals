//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using FluentAssertions;
//using SquidEyes.Fundamentals;

//namespace SquidEyes.UnitTests;

//public class ShortIdTests
//{
//    [Theory]
//    [InlineData("ABCDEFGHJKLMNPQRSTUVWX")]
//    [InlineData("YZabcdefghijklmnopqrst")]
//    [InlineData("uvwxyz23456789ABCDEFGH")]
//    public void From_ValidInput_Constructs(string value) =>
//        _ = ShortId.From(value);

//    [Fact]
//    public void Next_ValidInput_ReturnsValidShortId() =>
//        _ = ShortId.Next();

//    [Theory]
//    [InlineData("ABCDEFGHJKLMNPQRSTUVWX", true)]
//    [InlineData("YZabcdefghijklmnopqrst", true)]
//    [InlineData("uvwxyz23456789ABCDEFGH", true)]
//    [InlineData("IBCDEFGHJKLMNPQRSTUVWX", false)]
//    [InlineData("1BCDEFGHJKLMNPQRSTUVWX", false)]
//    [InlineData("OBCDEFGHJKLMNPQRSTUVWX", false)]
//    [InlineData("0BCDEFGHJKLMNPQRSTUVWX", false)]
//    [InlineData(" ABCDEFGHJKLMNPQRSTUVWX", false)]
//    [InlineData("ABCDEFGHJKLMNPQRSTUVWX ", false)]
//    [InlineData("ABCDEFGHJKL MNPQRSTUVWX", false)]
//    [InlineData("", false)]
//    [InlineData(null, false)]
//    public void IsValue_ValidInput_ReturnsExpected(string value, bool expected) =>
//        ShortId.IsValue(value).Should().Be(expected);
//}