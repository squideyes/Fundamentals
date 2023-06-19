//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using FluentAssertions;
//using SquidEyes.Fundamentals;

//namespace SquidEyes.UnitTests;

//public class EmailTests
//{
//    [Theory]
//    [InlineData("somedude@someco.com")]
//    [InlineData("a-b@c-d.com")]
//    public void From_ValidInput_Constructs(string value) =>
//        _ = Email.From(value);

//    [Theory]
//    [InlineData("somedude@someco.com", true)]
//    [InlineData("a-b@c-d.com", true)]
//    [InlineData(" somedude@someco.com", false)]
//    [InlineData("somedude@someco.com ", false)]
//    [InlineData("somedudesomeco.com", false)]
//    [InlineData("some dude@someco.com", false)]
//    [InlineData("somedude@some co.com", false)]
//    [InlineData("somedude@someco.", false)]
//    [InlineData("somedude@someco", false)]
//    [InlineData("somedudesomecocom", false)]
//    [InlineData("some_dude@someco.com", false)]
//    [InlineData("somedude@some_co.com", false)]
//    [InlineData("somedude@10-minute-mail.com", false)]
//    [InlineData("somedude@some_co.scam", false)]
//    [InlineData("", false)]
//    [InlineData(null, false)]
//    public void IsValue_ValidInput_ReturnsExpected(string value, bool expected) =>
//        Email.IsValue(value).Should().Be(expected);
//}