// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;
using System.Reflection;

namespace SquidEyes.UnitTests;

public class AssemblyExtendersTests
{
    [Fact]
    public void FindsExistingAttribute()
    {
        var assembly = typeof(AssemblyExtendersTests).Assembly;

        assembly.GetAttribute<AssemblyTitleAttribute>()?.Title
            .Should().Be(assembly.GetName().Name);
    }

    [Fact]
    public void ReturnsNullForMissingAttribute()
    {
        var assembly = typeof(AssemblyExtendersTests).Assembly;

        assembly.GetAttribute<AssemblyTrademarkAttribute>().Should().BeNull();
    }
}