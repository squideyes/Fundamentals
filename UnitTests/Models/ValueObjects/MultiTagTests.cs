// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class MultiTagTests
{
    [Theory]
    [InlineData("Tag1", 1)]
    [InlineData("Tag1:Tag2", 2)]
    public void Create_GoodInput_Contructs(string input, int tagCount)
    {
        var multiTag = MultiTag.Create(input);

        multiTag.Tags!.Length.Should().Be(tagCount);
        multiTag.Input.Should().Be(input);
        multiTag.ToString().Should().Be(input);

        var tagValues = input.Split(':');

        for (var i = 0; i < tagCount; i++)
            multiTag.Tags[i].Value.Should().Be(tagValues[i]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("Tag1:Tag2:Tag3:Tag4:Tag5:Tag6:Tag7:Tag8:Tag9:Tag10:Tag11")]
    [InlineData("badTag1")]
    [InlineData("0adTag1")]
    [InlineData(" Tag1:Tag2")]
    [InlineData("Tag1:Tag2 ")]
    [InlineData("Tag1:TagLongerThanMaxLengthTag")]
    [InlineData("Tag1:Bad@Tag")]
    [InlineData("Tag1:Bad Tag")]
    [InlineData("Tag1:")]
    [InlineData(":Tag2")]
    public void Create_BadInput_ThrowsError(string input)
    {
        FluentActions.Invoking(() => MultiTag.Create(input))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("Tag1", true)]
    [InlineData("Tag1:Tag2", true)]
    [InlineData("Tag1:Tag2:Tag3:Tag4:Tag5:Tag6:Tag7:Tag8:Tag9:Tag10", true)]
    [InlineData("Tag1:Tag2:Tag3:Tag4:Tag5:Tag6:Tag7:Tag8:Tag9:Tag10:Tag11", false)]
    [InlineData("badTag1", false)]
    [InlineData("0adTag1", false)]
    [InlineData(" Tag1:Tag2", false)]
    [InlineData("Tag1:Tag2 ", false)]
    [InlineData("Tag1:TagLongerThanMaxLengthTag", false)]
    [InlineData("Tag1:Bad@Tag", false)]
    [InlineData("Tag1:Bad Tag", false)]
    [InlineData("Tag1:", false)]
    [InlineData(":Tag2", false)]
    public void IsValue_MixedInput_ReturnsExpected(string input, bool expected) =>
        input.IsMultiTagInput().Should().Be(expected);

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("Tag1", true)]
    [InlineData("Tag1:Tag2", true)]
    [InlineData("Tag1:Tag2:Tag3:Tag4:Tag5:Tag6:Tag7:Tag8:Tag9:Tag10", true)]
    [InlineData("Tag1:Tag2:Tag3:Tag4:Tag5:Tag6:Tag7:Tag8:Tag9:Tag10:Tag11", false)]
    [InlineData("badTag1", false)]
    [InlineData("0adTag1", false)]
    [InlineData(" Tag1:Tag2", false)]
    [InlineData("Tag1:Tag2 ", false)]
    [InlineData("Tag1:TagLongerThanMaxLengthTag", false)]
    [InlineData("Tag1:Bad@Tag", false)]
    [InlineData("Tag1:Bad Tag", false)]
    [InlineData("Tag1:", false)]
    [InlineData(":Tag2", false)]
    public void TryCreate_MixedInput_ReturnsExpected(string input, bool expected) =>
        MultiTag.TryCreate(input, out var _).Should().Be(expected);

    [Fact]
    public void TypeEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.Equals(a2).Should().BeTrue();
        a1.Equals(b).Should().BeFalse();
        a2.Equals(a1).Should().BeTrue();
        a2.Equals(b).Should().BeFalse();
        b.Equals(a1).Should().BeFalse();
        b.Equals(a2).Should().BeFalse();
        a1.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void OperatorEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 == a2).Should().BeTrue();
        (a1 == b).Should().BeFalse();
        (a2 == a1).Should().BeTrue();
        (a2 == b).Should().BeFalse();
        (b == a1).Should().BeFalse();
        (b == a2).Should().BeFalse();
        (a1 == null).Should().BeFalse();
        ((MultiTag)null! == null!).Should().BeTrue();
        (null! == a1).Should().BeFalse();
        (null! == a2).Should().BeFalse();
        (null! == b).Should().BeFalse();
    }

    [Fact]
    public void OperatorNotEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 != a2).Should().BeFalse();
        (a1 != b).Should().BeTrue();
        (a2 != a1).Should().BeFalse();
        (a2 != b).Should().BeTrue();
        (b != a1).Should().BeTrue();
        (b != a2).Should().BeTrue();
        (a1 != null).Should().BeTrue();
        ((MultiTag)null! != null!).Should().BeFalse();
        (null! != a1).Should().BeTrue();
        (null! != a2).Should().BeTrue();
        (null! != b).Should().BeTrue();
    }

    [Fact]
    public void ObjectEquals_GoodInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.Equals((object)a2).Should().BeTrue();
        a1.Equals((object)b).Should().BeFalse();
        a2.Equals((object)a1).Should().BeTrue();
        a2.Equals((object)b).Should().BeFalse();
        b.Equals((object)a1).Should().BeFalse();
        b.Equals((object)a2).Should().BeFalse();
        a1.Equals((object)null!).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_GoodInput_EqualsInputGetHashCode()
    {
        var multiTag = MultiTag.Create("Tag1:Tag2");

        multiTag.GetHashCode().Should().Be(multiTag.Input!.GetHashCode());
    }

    [Fact]
    public void CompareTo_MixedInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        a1.CompareTo(a2).Should().Be(0);
        a1.CompareTo(null).Should().Be(1);
        a1.CompareTo(b).Should().Be(-1);
        b.CompareTo(a1).Should().Be(1);
    }

    [Fact]
    public void OperatorCompare_MixedInput_ReturnsExpected()
    {
        var (a1, a2, b) = GetInstances();

        (a1 < a2).Should().BeFalse();
        (a1 <= a2).Should().BeTrue();
        (a1 < b).Should().BeTrue();
        (a1 <= b).Should().BeTrue();
        (a1 > a2).Should().BeFalse();
        (a1 >= a2).Should().BeTrue();
        (b > a1).Should().BeTrue();
        (b >= a1).Should().BeTrue();
    }

    private static (MultiTag A1, MultiTag A2, MultiTag B) GetInstances()
    {
        var a1 = MultiTag.Create("Tag1:Tag2");
        var a2 = MultiTag.Create("Tag1:Tag2");
        var b = MultiTag.Create("Tag2:Tag3");

        return (a1, a2, b);
    }
}