//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using FluentAssertions;
//using SquidEyes.Fundamentals;

//namespace SquidEyes.UnitTests;

//public class MiscExtendersTests
//{
//    [Fact]
//    public void AsFuncWorksAsExpected() =>
//        "XXX".Convert(s => s).Should().Be("XXX");

//    [Fact]
//    public void AsActionWorksAsExpected() =>
//        "XXX".Do(s => s.Should().Be("XXX"));

//    [Theory]
//    [InlineData(true, 1, 1)]
//    [InlineData(true, 0, 0)]
//    [InlineData(false, 1, 0)]
//    [InlineData(false, 0, 0)]
//    public void DoIfCanDoWorksAsExpected(bool canDo, int value, int expected)
//    {
//        int count = 0;

//        value.DoIfCanDo(v => canDo, v => { count += v; });

//        count.Should().Be(expected);
//    }
//}