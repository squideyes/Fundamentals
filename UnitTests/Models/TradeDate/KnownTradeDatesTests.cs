//using SquidEyes.Fundamentals;
//using FluentAssertions;

//namespace SquidEyes.UnitTests;

//public class KnownTradeDatesTests
//{
//    [Fact]
//    public void Count_NoArgs_ReturnsExpected() =>
//        KnownTradeDates.Count.Should().Be(2502);

//    [Fact]
//    public void GetAll_NoArgs_ReturnsExpected() =>
//        KnownTradeDates.GetAll().Length.Should().Be(2502);

//    [Fact]
//    public void GetFiltered_NoArgs_ReturnsExpected() =>
//        KnownTradeDates.GetFiltered().Length.Should().Be(2502);

//    [Theory]
//    [InlineData(1, 20)]
//    [InlineData(2, 19)]
//    [InlineData(3, 23)]
//    [InlineData(4, 20)]
//    [InlineData(5, 21)]
//    [InlineData(6, 21)]
//    [InlineData(7, 20)]
//    [InlineData(8, 23)]
//    [InlineData(9, 21)]
//    [InlineData(10, 21)]
//    [InlineData(11, 20)]
//    [InlineData(12, 21)]
//    public void GetFiltered_OneMonth_ReturnsExpected(int month, int expected)
//    {
//        var from = new DateOnly(2022, month, 1);
//        var until = new DateOnly(2022, month, DateTime.DaysInMonth(2022, month));

//        KnownTradeDates.GetFiltered(from, until).Length.Should().Be(expected);
//    }

//    [Fact]
//    public void GetFiltered_WithoutUntil_ReturnsExpected()
//    {
//        var from = new DateOnly(2022, 1, 1);

//        KnownTradeDates.GetFiltered(from).Length.Should().Be(1989);
//    }

//    [Fact]
//    public void GetFiltered_WithoutFrom_ReturnsExpected()
//    {
//        var until = new DateOnly(2021, 12, 31);

//        KnownTradeDates.GetFiltered(null, until).Length.Should().Be(513);
//    }

//    [Fact]
//    public void From_GoodValue_ReturnsExpected()
//    {
//        var date = new DateOnly(2023, 7, 10);

//        KnownTradeDates.From(date).AsDateOnly().Should().Be(date);
//    }

//    [Theory]
//    [InlineData(28)]
//    [InlineData(29)]
//    public void From_BadValue_ThrowsError(int day)
//    {
//        var date = new DateOnly(2023, 5, day);

//        FluentActions.Invoking(() => KnownTradeDates.From(date))
//            .Should().Throw<ArgumentOutOfRangeException>();
//    }

//    [Theory]
//    [InlineData(26, true)]
//    [InlineData(27, false)]
//    [InlineData(28, false)]
//    [InlineData(29, false)]
//    [InlineData(30, true)]
//    public void TryGetValue_MixedArgs_ReturnsExpected(int day, bool expected)
//    {
//        var date = new DateOnly(2023, 5, day);

//        KnownTradeDates.TryGetValue(
//            date, out TradeDate tradeDate).Should().Be(expected);

//        if(expected)
//            tradeDate.AsDateOnly().Should().Be(date);
//    }

//    [Theory]
//    [InlineData(11, 10)]
//    [InlineData(10, 7)]
//    [InlineData(7, 6)]
//    [InlineData(6, 5)]
//    [InlineData(5, 3)]
//    public void TryGetPriorTradeDate_MixedArg_ReturnsExpected(
//        int currentDay, int priorDay)
//    {
//        var current = KnownTradeDates.From(new DateOnly(2023, 7, currentDay));

//        KnownTradeDates.TryGetPriorTradeDate(
//            current, out TradeDate prior).Should().Be(true);

//        prior.AsDateOnly().Day.Should().Be(priorDay);
//    }

//    [Fact]
//    public void TryGetPriorTradeDate_MinTradeDate_ReturnsFalse()
//    {
//        var current = KnownTradeDates.From(TradeDate.MinValue);

//        KnownTradeDates.TryGetPriorTradeDate(
//            current, out TradeDate _).Should().BeFalse();
//    }

//    [Fact]
//    public void TryGet_GoodArg_ReturnsExpected()
//    {
//        var tradeDate = KnownTradeDates.From(new DateOnly(2023, 7, 7));

//        KnownTradeDates.TryGetPreloadDates(
//            tradeDate, 5, out var tradeDates).Should().BeTrue();

//        tradeDates.Length.Should().Be(5);

//        tradeDates[0].Should().Be(KnownTradeDates.From(new DateOnly(2023, 7, 6)));
//        tradeDates[1].Should().Be(KnownTradeDates.From(new DateOnly(2023, 7, 5)));
//        tradeDates[2].Should().Be(KnownTradeDates.From(new DateOnly(2023, 7, 3)));
//        tradeDates[3].Should().Be(KnownTradeDates.From(new DateOnly(2023, 6, 30)));
//        tradeDates[4].Should().Be(KnownTradeDates.From(new DateOnly(2023, 6, 29)));
//    }

//    [Theory]
//    [InlineData(26, true)]
//    [InlineData(27, false)]
//    [InlineData(28, false)]
//    [InlineData(29, false)]
//    [InlineData(30, true)]
//    public void ContainsDateOnly_MixedArgs_ReturnsExpected(int day, bool expected)
//    {
//        var date = new DateOnly(2023, 5, day);

//        KnownTradeDates.Contains(date).Should().Be(expected);
//    }

//    [Theory]
//    [InlineData(26, true)]
//    [InlineData(30, true)]
//    public void ContainsTradeDate_MixedArgs_ReturnsExpected(int day, bool expected)
//    {
//        var tradeDate = KnownTradeDates.From(new DateOnly(2023, 5, day));

//        KnownTradeDates.Contains(tradeDate).Should().Be(expected);
//    }
//}
