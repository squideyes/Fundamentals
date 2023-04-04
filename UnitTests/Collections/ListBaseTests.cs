// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ListBaseTests
{
    private class Codes : ListBase<string>
    {
        public void Add(string code)
        {
            Items.Add(code);
        }
    }

    [Fact]
    public void FullCoverageTestShouldWork()
    {
        var codes = new Codes();

        codes.Count.Should().Be(0);
        codes.FirstOrDefault().Should().Be(null);
        codes.LastOrDefault().Should().Be(null);

        codes.Add("AAA");
        codes.Add("BBB");
        codes.Add("CCC");

        codes.Count.Should().Be(3);

        codes.First().Should().Be("AAA");
        codes.FirstOrDefault().Should().Be("AAA");
        codes.Last().Should().Be("CCC");
        codes.LastOrDefault().Should().Be("CCC");

        int count = 0;

        codes.ForEach(c => count++);

        count.Should().Be(3);

        string.Join(",", codes).Should().Be("AAA,BBB,CCC");
    }
}