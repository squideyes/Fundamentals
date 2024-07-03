// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class TagArgExtendersTests
{
    [Theory]
    [InlineData("255", TagArgState.Valid, true)]
    [InlineData("255", TagArgState.Valid, null!)]
    [InlineData("255", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void Byte_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<byte>(input, TagArgArgKind.Byte,
            expectedIsValid, expectedState, (t, a, f) => a.ToByteTagArg(t, f));
    }

    [Theory]
    [InlineData("Z", TagArgState.Valid, true)]
    [InlineData("Z", TagArgState.Valid, null!)]
    [InlineData("Z", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void Char_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<char>(input, TagArgArgKind.Char,
            expectedIsValid, expectedState, (t, a, f) => a.ToCharTagArg(t, f));
    }

    [Theory]
    [InlineData("32767", TagArgState.Valid, true)]
    [InlineData("32767", TagArgState.Valid, null!)]
    [InlineData("32767", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void Int16_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<short>(input, TagArgArgKind.Int16,
            expectedIsValid, expectedState, (t, a, f) => a.ToInt16TagArg(t, f));
    }

    [Theory]
    [InlineData("2147483647", TagArgState.Valid, true)]
    [InlineData("2147483647", TagArgState.Valid, null!)]
    [InlineData("2147483647", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void Int32_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<int>(input, TagArgArgKind.Int32,
            expectedIsValid, expectedState, (t, a, f) => a.ToInt32TagArg(t, f));
    }

    [Theory]
    [InlineData("9223372036854775807", TagArgState.Valid, true)]
    [InlineData("9223372036854775807", TagArgState.Valid, null!)]
    [InlineData("9223372036854775807", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void Int64_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<long>(input, TagArgArgKind.Int64,
            expectedIsValid, expectedState, (t, a, f) => a.ToInt64TagArg(t, f));
    }

    [Theory]
    [InlineData("3.40282347E+38", TagArgState.Valid, true)]
    [InlineData("3.40282347E+38", TagArgState.Valid, null!)]
    [InlineData("3.40282347E+38", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void Float_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<float>(input, TagArgArgKind.Float,
            expectedIsValid, expectedState, (t, a, f) => a.ToFloatTagArg(t, f));
    }

    [Theory]
    [InlineData("1.7976931348623157E+308", TagArgState.Valid, true)]
    [InlineData("1.7976931348623157E+308", TagArgState.Valid, null!)]
    [InlineData("1.7976931348623157E+308", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void Double_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<double>(input, TagArgArgKind.Double,
            expectedIsValid, expectedState, (t, a, f) => a.ToDoubleTagArg(t, f));
    }

    [Theory]
    [InlineData("12/31/9999 23:59:59.9999999", TagArgState.Valid, true)]
    [InlineData("12/31/9999 23:59:59.9999999", TagArgState.Valid, null!)]
    [InlineData("12/31/9999 23:59:59.9999999", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void DateTime_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<DateTime>(input, TagArgArgKind.DateTime,
            expectedIsValid, expectedState, (t, a, f) => a.ToDateTimeTagArg(t, f));
    }


    [Theory]
    [InlineData("12/31/9999", TagArgState.Valid, true)]
    [InlineData("12/31/9999", TagArgState.Valid, null!)]
    [InlineData("12/31/9999", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void DateOnly_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<DateOnly>(input, TagArgArgKind.DateOnly,
            expectedIsValid, expectedState, (t, a, f) => a.ToDateOnlyTagArg(t, f));
    }

    [Theory]
    [InlineData("23:59:59.9999999", TagArgState.Valid, true)]
    [InlineData("23:59:59.9999999", TagArgState.Valid, null!)]
    [InlineData("23:59:59.9999999", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void TimeOnly_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<TimeOnly>(input, TagArgArgKind.TimeOnly,
            expectedIsValid, expectedState, (t, a, f) => a.ToTimeOnlyTagArg(t, f));
    }

    [Theory]
    [InlineData("10675199.02:48:05.4775807", TagArgState.Valid, true)]
    [InlineData("10675199.02:48:05.4775807", TagArgState.Valid, null!)]
    [InlineData("10675199.02:48:05.4775807", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void TimeSpan_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<TimeSpan>(input, TagArgArgKind.TimeSpan,
            expectedIsValid, expectedState, (t, a, f) => a.ToTimeSpanTagArg(t, f));
    }

    [Theory]
    [InlineData("98689ce5ee504fbca646b5eabc64b332", TagArgState.Valid, true)]
    [InlineData("98689ce5ee504fbca646b5eabc64b332", TagArgState.Valid, null!)]
    [InlineData("98689ce5ee504fbca646b5eabc64b332", TagArgState.Invalid, false)]
    [InlineData(null, TagArgState.NullOrWhitespace, null!)]
    [InlineData("", TagArgState.NullOrWhitespace, null!)]
    [InlineData(" ", TagArgState.NullOrWhitespace, null!)]
    public void Guid_Should_ConvertToTagArg(
        string? input, TagArgState expectedState, bool? expectedIsValid)
    {
        BasicArgTest<Guid>(input, TagArgArgKind.Guid,
            expectedIsValid, expectedState, (t, a, f) => a.ToGuidTagArg(t, f));
    }

    ///////////////////////////////////

    private static void BasicArgTest<T>(string? input, TagArgArgKind argKind, 
        bool? expectedIsValid, TagArgState expectedState,
            Func<Tag, string, Func<T, bool>, TagArg<T>> getTagArg)
                where T : struct, IComparable<T>, IParsable<T>
    {
        var tag = Tag.Create("X");

        TagArg<T> tagArg;

        if (expectedIsValid == true)
            tagArg = getTagArg(tag, input!, v => true);
        else if (expectedIsValid == false)
            tagArg = getTagArg(tag, input!, v => false);
        else
            tagArg = getTagArg(tag, input!, null!);

        tagArg.Tag.Should().Be(tag);

        tagArg.State.Should().Be(expectedState);

        if (expectedState == TagArgState.Valid)
        {
            tagArg.Kind.Should().Be(argKind);
            tagArg.Arg.Should().Be(T.Parse(input!, null!));
            tagArg.TypeName.Should().Be(typeof(T).FullName);
            tagArg.IsValid.Should().BeTrue();
            tagArg.Message.Should().BeNull();
        }
        else
        {
            tagArg.Kind.Should().Be(default);
            tagArg.Arg.Should().Be(default);
            tagArg.TypeName.Should().BeNull();
            tagArg.IsValid.Should().BeFalse();
            tagArg.Message.Should().NotBeNull();
        }
    }






    //    [Fact]
    //    public void GoodFloat_Should_ConvertToTagArg()
    //    {
    //        ParseableArgTest(float.E.ToString(), float.E);
    //        ParseableArgTest(float.Epsilon.ToString(), float.Epsilon);
    //        ParseableArgTest(float.MaxValue.ToString(), float.MaxValue);
    //        ParseableArgTest(float.MinValue.ToString(), float.MinValue);
    //        ParseableArgTest(float.NaN.ToString(), float.NaN);
    //        ParseableArgTest(float.NegativeInfinity.ToString(), float.NegativeInfinity);
    //        ParseableArgTest(float.NegativeZero.ToString(), float.NegativeZero);
    //        ParseableArgTest(float.Pi.ToString(), float.Pi);
    //        ParseableArgTest(float.PositiveInfinity.ToString(), float.PositiveInfinity);
    //        ParseableArgTest(float.Tau.ToString(), float.Tau);
    //    }

    //    [Fact]
    //    public void GoodDouble_Should_ConvertToTagArg()
    //    {
    //        ParseableArgTest(double.E.ToString(), double.E);
    //        ParseableArgTest(double.Epsilon.ToString(), double.Epsilon);
    //        ParseableArgTest(double.MaxValue.ToString(), double.MaxValue);
    //        ParseableArgTest(double.MinValue.ToString(), double.MinValue);
    //        ParseableArgTest(double.NaN.ToString(), double.NaN);
    //        ParseableArgTest(double.NegativeInfinity.ToString(), double.NegativeInfinity);
    //        ParseableArgTest(double.NegativeZero.ToString(), double.NegativeZero);
    //        ParseableArgTest(double.Pi.ToString(), double.Pi);
    //        ParseableArgTest(double.PositiveInfinity.ToString(), double.PositiveInfinity);
    //        ParseableArgTest(double.Tau.ToString(), double.Tau);
    //    }



    //    [Fact]
    //    public void GoodUri_Should_ConvertToConfigUri()
    //    {
    //        const string URI = "https://cnn.com";

    //        URI.ToUriArg("X").Arg.Should().Be(new Uri(URI));
    //        "".ToUriArg("X", UriKind.Absolute).Arg.Should().BeNull();
    //        URI.ToUriArg("X").Arg.Should().Be(new Uri(URI));
    //    }

    //    [Fact]
    //    public void GoodEmail_Should_ConvertToConfigEmail()
    //    {
    //        const string EMAIL = "dude@someco.com";

    //        EMAIL.ToEmailArg("X").Arg.Should().Be(Email.Create(EMAIL));
    //        "".ToEmailArg("X").Arg.Should().BeNull();
    //        EMAIL.ToEmailArg("X").Arg.Should().Be(Email.Create(EMAIL));
    //    }

    //    [Fact]
    //    public void GoodPhone_Should_ConvertToConfigPhone()
    //    {
    //        const string PHONE = "+1 (234) 567-8901";

    //        PHONE.ToPhoneTagArg("X").Arg.Should().Be(Phone.Create(PHONE));
    //        "".ToPhoneTagArg("X").Arg.Should().BeNull();
    //        PHONE.ToPhoneTagArg("X").Arg.Should().Be(Phone.Create(PHONE));
    //    }

    //    [Fact]
    //    public void GoodEnum_Should_ConvertToConfigEnum()
    //    {
    //        "Absolute".ToEnumArg<UriKind>("X").Arg.Should().Be(UriKind.Absolute);
    //        "".ToEnumArg<UriKind>("X", true).Arg.Should().Be(default);
    //        "Absolute".ToEnumArg<UriKind>("X").Arg.Should().Be(UriKind.Absolute);
    //    }

    //    [Fact]
    //    public void GoodTag_Should_ConvertToConfigTag()
    //    {
    //        const string TAG = "Tag1";

    //        TAG.ToTagTagArg("X").Arg.Should().Be(Tag.Create(TAG));
    //        "".ToTagTagArg("X").Arg.Should().BeNull();
    //        TAG.ToTagTagArg("X").Arg.Should().Be(Tag.Create(TAG));
    //    }

    //    [Fact]
    //    public void GoodMultiTag_Should_ConvertToConfigMultiTag()
    //    {
    //        const string MT = "Tag1:Tag2:Tag3";

    //        MT.ToMultiTagArg("X").Arg.Should().Be(MultiTag.Create(MT));
    //        "".ToMultiTagArg("X").Arg.Should().BeNull();
    //        MT.ToMultiTagArg("X").Arg.Should().Be(MultiTag.Create(MT));
    //    }

    //    private static void ParseableArgTest<T>(string input, T expected)
    //        where T : struct, IParsable<T>
    //    {
    //        var requiredArg = input!.ToParseableTagArg<T>("X");
    //        var nullArg = "".ToParseableTagArg<T>("X", true);
    //        var nonNullArg = input!.ToParseableTagArg<T>("X", true);

    //        requiredArg.Tag.Should().Be(Tag.Create("X"));
    //        nullArg.Tag.Should().Be(Tag.Create("X"));
    //        nonNullArg.Tag.Should().Be(Tag.Create("X"));

    //        requiredArg.IsValid.Should().BeTrue();
    //        nullArg.IsValid.Should().BeTrue();
    //        nonNullArg.IsValid.Should().BeTrue();

    //        requiredArg.Message.Should().BeNull();
    //        nullArg.Message.Should().BeNull();
    //        nonNullArg.Message.Should().BeNull();

    //        requiredArg.Arg.Should().Be(expected);
    //        nullArg.Arg.Should().Be(default(T));
    //        nonNullArg.Arg.Should().Be(expected);
    //    }
}