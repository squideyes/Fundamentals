// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class ValueSetTests
{
    [Fact]
    public void Get_ValidInput_ReturnsExpected()
    {
        static void Test<T>(ValueSet values, params T[] expected) =>
            values.Get<T>().Should().BeEquivalentTo(expected.ToList());

        Test(ValueSet.From(1, 2, 3), 1, 2, 3);
        Test(ValueSet.From(1.0, 2.0, 3.0), 1.0, 2.0, 3.0);
    }
}