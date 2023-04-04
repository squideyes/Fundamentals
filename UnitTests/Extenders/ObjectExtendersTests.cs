// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ObjectExtendersTests
{
    [Fact]
    public void AsListExtenderWorksWithSingleArg()
    {
        var list = "AAA".AsList();

        list.Count.Should().Be(1);

        list.Should().ContainInOrder("AAA");
    }

    [Fact]
    public void AsHashSetExtenderWorksWithSingleArg()
    {
        var list = "AAA".AsHashSet();

        list.Count.Should().Be(1);

        list.Should().ContainInOrder("AAA");
    }
}