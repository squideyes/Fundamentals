// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentAssertions;
using SquidEyes.Fundamentals;

namespace SquidEyes.UnitTests;

public class IEnumerableExtendersTests
{
    public enum Validation
    {
        None,
        Success,
        Failure,
        Between
    }

    [Theory]
    [InlineData("")]
    [InlineData("1,2,3")]
    public void ForEachIteratesAsExpected(string values)
    {
        int index = 0;

        var list = SplitIntoInts(values);

        list.ForEach(
            number =>
            {
                index++;

                number.Should().Be(index);
            });

        index.Should().Be(list.Count);
    }

    [Fact]
    public void IsUniqueWorksAsExpected()
    {
        var items = new List<int>();

        items.IsUnique().Should().BeTrue();

        items.Add(1);
        items.IsUnique().Should().BeTrue();

        items.Add(2);
        items.IsUnique().Should().BeTrue();

        items.Add(1);
        items.IsUnique().Should().BeFalse();
    }

    [Theory]
    [InlineData("", true, true, false)]
    [InlineData("", true, false, false)]
    [InlineData("1", true, true, true)]
    [InlineData("0", true, false, true)]
    [InlineData("0", true, true, false)]
    [InlineData("", false, true, false)]
    [InlineData("1", false, true, true)]
    [InlineData("0", false, true, false)]
    public void HasItemsNonDefaultWorksWithGoodArgs(string value,
        bool includeNonDefault, bool nonDefault, bool result)
    {
        var items = SplitIntoInts(value).ToHashSet();

        if (includeNonDefault)
            items.HasItems(nonDefault).Should().Be(result);
        else
            items.HasItems().Should().Be(result);
    }

    [Theory]
    [InlineData("", Validation.None, false)]
    [InlineData("", Validation.Success, false)]
    [InlineData("", Validation.Failure, false)]
    [InlineData("1", Validation.None, true)]
    [InlineData("1", Validation.Success, true)]
    [InlineData("1", Validation.Failure, false)]
    [InlineData("4,5", Validation.Between, false)]
    [InlineData("5,7", Validation.Between, true)]
    [InlineData("5,6,7", Validation.Between, true)]
    [InlineData("5,6,7,8", Validation.Between, true)]
    [InlineData("5,6,7,9", Validation.Between, false)]
    public void HasItemsValidatorWorksWithGoodArgs(string values,
        Validation validation, bool result)
    {
        var items = SplitIntoInts(values).ToHashSet();

        var isValid = GetIsValid(validation);

        items.HasItems(isValid).Should().Be(result);
    }

    [Theory]
    [InlineData("1", 2, 4, true, false)]
    [InlineData("1,2", 2, 4, true, true)]
    [InlineData("1,2,3", 2, 4, true, true)]
    [InlineData("1,2,3,4", 2, 4, true, true)]
    [InlineData("1,2,3,4,5", 2, 4, true, false)]
    [InlineData("1", 2, 4, false, false)]
    [InlineData("1,2", 2, 4, false, true)]
    [InlineData("1,2,3", 2, 4, false, true)]
    [InlineData("1,2,3,4", 2, 4, false, true)]
    [InlineData("1,2,3,4,5", 2, 4, false, false)]
    [InlineData("0", 1, 4, true, false)]
    [InlineData("0", 1, 4, false, true)]
    public void HasItemsMinMaxWorksWithGoodArgs(string values,
        int minItems, int maxItems, bool nonDefault, bool result)
    {
        var items = values.Split(',').Select(int.Parse).ToHashSet();

        items.HasItems(minItems, maxItems, nonDefault)
            .Should().Be(result);
    }

    [Theory]
    [InlineData("1", 2, 4, Validation.None, false)]
    [InlineData("1,2", 2, 4, Validation.None, true)]
    [InlineData("1,2,3", 2, 4, Validation.None, true)]
    [InlineData("1,2,3,4", 2, 4, Validation.None, true)]
    [InlineData("1,2,3,4,5", 2, 4, Validation.None, false)]
    [InlineData("1", 2, 4, Validation.Success, false)]
    [InlineData("1,2", 2, 4, Validation.Success, true)]
    [InlineData("1,2,3", 2, 4, Validation.Success, true)]
    [InlineData("1,2,3,4", 2, 4, Validation.Success, true)]
    [InlineData("1,2,3,4,5", 2, 4, Validation.Success, false)]
    [InlineData("1", 2, 4, Validation.Failure, false)]
    [InlineData("1,2", 2, 4, Validation.Failure, false)]
    [InlineData("1,2,3", 2, 4, Validation.Failure, false)]
    [InlineData("1,2,3,4", 2, 4, Validation.Failure, false)]
    [InlineData("1,2,3,4,5", 2, 4, Validation.Failure, false)]
    [InlineData("4,5", 2, 4, Validation.Between, false)]
    [InlineData("5,7", 2, 4, Validation.Between, true)]
    [InlineData("5,6,7", 2, 4, Validation.Between, true)]
    [InlineData("5,6,7,8", 2, 4, Validation.Between, true)]
    [InlineData("5,6,7,9", 2, 4, Validation.Between, false)]
    public void HasItemsMinMaxValidatorWorksWithGoodArgs(string values,
        int minItems, int maxItems, Validation validation, bool result)
    {
        var items = values.Split(',').Select(int.Parse).ToHashSet();

        var isValid = GetIsValid(validation);

        items.HasItems(minItems, maxItems, isValid).Should().Be(result);
    }

    [Theory]
    [InlineData(1, 3)]
    public void HasNonDefaultItemsThrowsErrorOnBadArgs(
        int minItems, int maxItems)
    {
        var items = "1,2,3".Split(',').Select(int.Parse).ToHashSet();

        items.HasItems(minItems, maxItems);
    }

    [Theory]
    [InlineData(0, 2, true)]
    [InlineData(3, 1, true)]
    [InlineData(0, 2, false)]
    [InlineData(3, 1, false)]
    public void HasItemsMinMaxThrowsErrorOnBadArgs(
        int minItems, int maxItems, bool nonDefault)
    {
        var items = new HashSet<int> { 1, 2, 3 };

        FluentActions.Invoking(() =>
            items.HasItems(minItems, maxItems, nonDefault))
            .Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(3, 1)]
    public void HasItemsMinMaxValidatorThrowsErrorOnBadArgs(
        int minItems, int maxItems)
    {
        var items = new HashSet<int> { 1, 2, 3 };

        FluentActions.Invoking(() => items.HasItems(minItems, maxItems,
            null)).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData(0, 10, 0, 0)]
    [InlineData(100, 10, 10, 10)]
    [InlineData(91, 10, 10, 1)]
    public void ChunkedWorksWithGoodArgs(
        int itemCount, int chunkSize, int chunkCount, int lastChunkSize)
    {
        var items = Enumerable.Range(1, itemCount);

        var chunks = items.Chunked(chunkSize).ToList();

        chunks.Count.Should().Be(chunkCount);

        foreach (var chunk in chunks.Take(chunkCount - 1))
            chunk.Count.Should().Be(chunkSize);

        if (itemCount > 0)
            chunks[^1].Count.Should().Be(lastChunkSize);
    }

    private static Func<int, bool>? GetIsValid(Validation validation)
    {
        return validation switch
        {
            Validation.None => null,
            Validation.Success => v => true,
            Validation.Failure => v => false,
            Validation.Between => v => v.IsBetween(5, 8),
            _ => throw new ArgumentOutOfRangeException(nameof(validation))
        };
    }

    private static List<int> SplitIntoInts(string value)
    {
        if (string.IsNullOrEmpty(value))
            return new List<int>();
        else
            return value.Split(',').Select(int.Parse).ToList();
    }
}