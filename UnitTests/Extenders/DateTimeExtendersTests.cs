//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using FluentAssertions;
//using SquidEyes.Fundamentals;

//namespace SquidEyes.UnitTests;

//public class DateTimeExtendersTests
//{
//    [Fact]
//    public void EasternRoundtripWorks() =>
//        Roundtrip(d => d.ToEasternFromUtc(), d => d.ToUtcFromEastern());

//    [Fact]
//    public void CentralRoundtripWorks() =>
//        Roundtrip(d => d.ToCentralFromUtc(), d => d.ToUtcFromCentral());

//    [Fact]
//    public void MountainRoundtripWorks() =>
//        Roundtrip(d => d.ToMountainFromUtc(), d => d.ToUtcFromMountain());

//    [Fact]
//    public void PacificRoundtripWorks() =>
//        Roundtrip(d => d.ToPacificFromUtc(), d => d.ToUtcFromPacific());

//    [Theory]
//    [InlineData("01/02/2021 03:04:05.006", false)]
//    [InlineData("01/02/2021", true)]
//    [InlineData("01/02/2021 00:00:00.000", true)]
//    public void IsDateWorks(string dateTimeString, bool expected) =>
//        DateTime.Parse(dateTimeString).IsDate().Should().Be(expected);

//    [Theory]
//    [InlineData("10/10/2021", false)]
//    [InlineData("10/11/2021", true)]
//    [InlineData("10/12/2021", true)]
//    [InlineData("10/13/2021", true)]
//    [InlineData("10/14/2021", true)]
//    [InlineData("10/15/2021", true)]
//    [InlineData("10/16/2021", false)]
//    public void IsWeekdayShouldWork(string dateTimeString, bool expected) =>
//        DateTime.Parse(dateTimeString).IsWeekday().Should().Be(expected);

//    [Theory]
//    [InlineData(0, true)]
//    [InlineData(60, true)]
//    [InlineData(1, false)]
//    [InlineData(59, false)]
//    [InlineData(-1, false)]
//    [InlineData(61, false)]
//    public void OnIntervalShouldWork(int seconds, bool expected) =>
//        new DateTime(2021, 10, 10).AddSeconds(seconds).OnInterval(60).Should().Be(expected);

//    private static void Roundtrip(
//        Func<DateTime, DateTime> getUnspecified, Func<DateTime, DateTime> getUtc)
//    {
//        var utcNow = DateTime.UtcNow;

//        var unspecified = getUnspecified(utcNow);

//        getUtc(unspecified).Should().Be(utcNow);
//    }
//}