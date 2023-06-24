// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;
using System.Text;

namespace SquidEyes.UnitTests;

public class StringBuilderExtendersTests
{
    [Theory]
    [InlineData(',', "1,2")]
    [InlineData('|', "1|2")]
    public void AppendDelimitedWithGoodArgs(char delimiter, string result2)
    {
        var sb = new StringBuilder();

        sb.AppendDelimited(1, delimiter);
        sb.ToString().Should().Be("1");

        sb.AppendDelimited(2, delimiter);
        sb.ToString().Should().Be(result2);
    }

    [Fact]
    public void ToAndFromBase64WithGoodArgs()
    {
        var a = "ABC123";
        var b = a.ToBase64();
        var c = b.FromBase64();

        c.Should().Be(a);
    }
}