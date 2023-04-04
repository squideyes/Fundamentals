// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class EnumExtendersTests
{
    private enum Color
    {
        Red = 1,
        Green,
        Blue
    }

    [Flags]
    public enum Flags
    {
        One = 1,
        Two = 2
    }

    [Theory]
    [InlineData(Color.Red, true)]
    [InlineData((Color)0, false)]
    [InlineData(Flags.One, true)]
    [InlineData(Flags.Two, true)]
    [InlineData(Flags.One | Flags.Two, false)]
    public void IsEnumValueWorksAsExpected<T>(T enumeration, bool expected)
        where T : struct, Enum
    {
        enumeration.IsEnumValue().Should().Be(expected);
    }

    [Theory]
    [InlineData(Flags.One, true)]
    [InlineData(Flags.Two, true)]
    [InlineData(Flags.One | Flags.Two, true)]
    [InlineData((Flags)0, false)]
    [InlineData((Flags)4, false)]
    public void IsFlagsValueWorksAsExpected(Flags flags, bool expected) =>
        flags.IsFlagsValue().Should().Be(expected);
}