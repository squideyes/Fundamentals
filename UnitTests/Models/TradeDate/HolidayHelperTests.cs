//using SquidEyes.Fundamentals;

//namespace SquidEyes.UnitTests;

//public class HolidayHelperTests: IClassFixture<HolidayHelperTests.Fixture>
//{
//    public class Fixture : IDisposable
//    {
//        public Fixture()
//        {
//            Holidays = HolidayHelper.GetHolidays();
//        }

//        public void Dispose()
//        {
//        }

//        public HashSet<DateOnly> Holidays { get; }
//    }

//    private readonly Fixture fixture;

//    public HolidayHelperTests(Fixture fixture)
//    {
//        this.fixture = fixture;
//    }

//    [Theory]
//    [InlineData(2019, 12, 25)]
//    [InlineData(2020, 01, 01)]
//    [InlineData(2020, 01, 20)]
//    [InlineData(2020, 02, 17)]
//    [InlineData(2020, 04, 10)]
//    [InlineData(2020, 05, 25)]
//    [InlineData(2020, 07, 03)]
//    [InlineData(2020, 09, 07)]
//    [InlineData(2020, 11, 26)]
//    [InlineData(2020, 11, 27)]
//    [InlineData(2020, 12, 25)]
//    [InlineData(2021, 01, 01)]
//    [InlineData(2021, 01, 18)]
//    [InlineData(2021, 02, 15)]
//    [InlineData(2021, 04, 02)]
//    [InlineData(2021, 05, 31)]
//    [InlineData(2021, 07, 05)]
//    [InlineData(2021, 09, 06)]
//    [InlineData(2021, 11, 25)]
//    [InlineData(2021, 11, 26)]
//    [InlineData(2021, 12, 24)]
//    [InlineData(2021, 12, 31)]
//    [InlineData(2022, 01, 17)]
//    [InlineData(2022, 02, 21)]
//    [InlineData(2022, 04, 15)]
//    [InlineData(2022, 05, 30)]
//    [InlineData(2022, 06, 20)]
//    [InlineData(2022, 07, 04)]
//    [InlineData(2022, 09, 05)]
//    [InlineData(2022, 11, 24)]
//    [InlineData(2022, 11, 25)]
//    [InlineData(2022, 12, 26)]
//    [InlineData(2023, 01, 02)]
//    [InlineData(2023, 01, 16)]
//    [InlineData(2023, 02, 20)]
//    [InlineData(2023, 04, 07)]
//    [InlineData(2023, 05, 29)]
//    [InlineData(2023, 06, 19)]
//    [InlineData(2023, 07, 04)]
//    [InlineData(2023, 09, 04)]
//    [InlineData(2023, 11, 23)]
//    [InlineData(2023, 11, 24)]
//    [InlineData(2023, 12, 25)]
//    [InlineData(2024, 01, 01)]
//    [InlineData(2024, 01, 15)]
//    [InlineData(2024, 02, 19)]
//    [InlineData(2024, 03, 29)]
//    [InlineData(2024, 05, 27)]
//    [InlineData(2024, 06, 19)]
//    [InlineData(2024, 07, 04)]
//    [InlineData(2024, 09, 02)]
//    [InlineData(2024, 11, 28)]
//    [InlineData(2024, 11, 29)]
//    [InlineData(2024, 12, 25)]
//    [InlineData(2025, 01, 01)]
//    [InlineData(2025, 01, 20)]
//    [InlineData(2025, 02, 17)]
//    [InlineData(2025, 04, 18)]
//    [InlineData(2025, 05, 26)]
//    [InlineData(2025, 06, 19)]
//    [InlineData(2025, 07, 04)]
//    [InlineData(2025, 09, 01)]
//    [InlineData(2025, 11, 27)]
//    [InlineData(2025, 11, 28)]
//    [InlineData(2025, 12, 25)]
//    [InlineData(2026, 01, 01)]
//    [InlineData(2026, 01, 19)]
//    [InlineData(2026, 02, 16)]
//    [InlineData(2026, 04, 03)]
//    [InlineData(2026, 05, 25)]
//    [InlineData(2026, 06, 19)]
//    [InlineData(2026, 07, 03)]
//    [InlineData(2026, 09, 07)]
//    [InlineData(2026, 11, 26)]
//    [InlineData(2026, 11, 27)]
//    [InlineData(2026, 12, 25)]
//    [InlineData(2027, 01, 01)]
//    [InlineData(2027, 01, 18)]
//    [InlineData(2027, 02, 15)]
//    [InlineData(2027, 03, 26)]
//    [InlineData(2027, 05, 31)]
//    [InlineData(2027, 06, 18)]
//    [InlineData(2027, 07, 05)]
//    [InlineData(2027, 09, 06)]
//    [InlineData(2027, 11, 25)]
//    [InlineData(2027, 11, 26)]
//    [InlineData(2027, 12, 24)]
//    [InlineData(2027, 12, 31)]
//    [InlineData(2028, 01, 17)]
//    [InlineData(2028, 02, 21)]
//    [InlineData(2028, 04, 14)]
//    [InlineData(2028, 05, 29)]
//    [InlineData(2028, 06, 19)]
//    [InlineData(2028, 07, 04)]
//    [InlineData(2028, 09, 04)]
//    [InlineData(2028, 11, 23)]
//    [InlineData(2028, 11, 24)]
//    [InlineData(2028, 12, 25)]
//    [InlineData(2029, 01, 01)]
//    [InlineData(2029, 01, 15)]
//    [InlineData(2029, 02, 19)]
//    [InlineData(2029, 03, 30)]
//    [InlineData(2029, 05, 28)]
//    [InlineData(2029, 06, 19)]
//    [InlineData(2029, 07, 04)]
//    [InlineData(2029, 09, 03)]
//    [InlineData(2029, 11, 22)]
//    [InlineData(2029, 11, 23)]
//    [InlineData(2029, 12, 25)]
//    public void GetHolidays_Returns_ExpectedValues(int year, int month, int day)=>
//        fixture.Holidays.Contains(new DateOnly(year, month, day));
//}
