// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class TagTests
{
    [Fact]
    public void Create_GoodInput_Contructs()
    {
        const string INPUT = "Tag1";

        var tag = Tag.Create(INPUT);

        tag.Value.Should().Be(INPUT);
        tag.Input.Should().Be(INPUT);
        tag.ToString().Should().Be(INPUT);
    }

    [Theory]
    [InlineData(null!)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("tag1")]
    [InlineData("0ag1")]
    [InlineData(" Tag1")]
    [InlineData("Tag2 ")]
    [InlineData("TagLongerThanMaxLengthTag")]
    [InlineData("Bad@Tag")]
    [InlineData("Bad Tag")]
    public void Create_BadInput_ThrowsError(string? input)
    {
        FluentActions.Invoking(() => Tag.Create(input!))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(null!, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("Tag1", true)]
    [InlineData("tag1", false)]
    [InlineData("0ag1", false)]
    [InlineData(" Tag1", false)]
    [InlineData("Tag2 ", false)]
    [InlineData("MaxTagLengthTagsAreValid", true)]
    [InlineData("TagLongerThanMaxLengthTag", false)]
    [InlineData("Bad@Tag", false)]
    [InlineData("Bad Tag", false)]
    public void IsValue_MixedInput_ReturnsExpected(string? input, bool expected) =>
        input!.IsTagInput().Should().Be(expected);

    [Theory]
    [InlineData(null!, false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("Tag1", true)]
    [InlineData("tag1", false)]
    [InlineData("0ag1", false)]
    [InlineData(" Tag1", false)]
    [InlineData("Tag2 ", false)]
    [InlineData("MaxTagLengthTagsAreValid", true)]
    [InlineData("TagLongerThanMaxLengthTag", false)]
    [InlineData("Bad@Tag", false)]
    [InlineData("Bad Tag", false)]
    public void TryCreate_MixedInput_ReturnsExpected(string? input, bool expected) =>
        Tag.TryCreate(input!, out var _).Should().Be(expected);

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
        ((Tag)null! == null!).Should().BeTrue();
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
        ((Tag)null! != null!).Should().BeFalse();
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
        var actorId = Tag.Create("Tag1");

        actorId.GetHashCode().Should().Be(actorId.Input!.GetHashCode());
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

    private static (Tag A1, Tag A2, Tag B) GetInstances()
    {
        var a1 = Tag.Create("Tag1");
        var a2 = Tag.Create("Tag1");
        var b = Tag.Create("Tag2");

        return (a1, a2, b);
    }
}