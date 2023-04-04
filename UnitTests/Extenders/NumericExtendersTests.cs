// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class NumericExtendersTests
{
    [Theory]
    [InlineData(0.00000001f, 0.00000001f, true)]
    [InlineData(0.000000011f, 0.00000001f, true)]
    [InlineData(0.00000002f, 0.00000001f, false)]
    [InlineData(0.00000011f, 0.00000001f, false)]
    [InlineData(0.000000111f, 0.00000001f, false)]
    public void ApproximatesWorksForFloatAsExpected(
        float a, float b, bool result)
    {
        a.Approximates(b).Should().Be(result);
    }

    [Theory]
    [InlineData(0.00000001, 0.00000001, true)]
    [InlineData(0.000000011, 0.00000001, true)]
    [InlineData(0.00000002, 0.00000001, false)]
    [InlineData(0.00000011, 0.00000001, false)]
    [InlineData(0.000000111, 0.00000001, false)]
    public void ApproximatesWorksForDoubleAsExpected(
        float a, float b, bool result)
    {
        a.Approximates(b).Should().Be(result);
    }

    [Theory]
    [InlineData(1.0f, 2.0f, true)]
    [InlineData(1.0f, 2.0f, false)]
    public void UpdateWorksForFloatsAsExpected(
        float oldValue, float newValue, bool canUpdate)
    {
        oldValue.Update(newValue, (o, n) =>
        {
            o.Should().Be(oldValue);
            n.Should().Be(newValue);

            return canUpdate;
        })
            .Should().Be(canUpdate ? newValue : oldValue);
    }

    [Theory]
    [InlineData(1.0, 2.0, true)]
    [InlineData(1.0, 2.0, false)]
    public void UpdateWorksForDoublesAsExpected(
        double oldValue, double newValue, bool canUpdate)
    {
        oldValue.Update(newValue, (o, n) =>
        {
            o.Should().Be(oldValue);
            n.Should().Be(newValue);

            return canUpdate;
        })
            .Should().Be(canUpdate ? newValue : oldValue);
    }
}