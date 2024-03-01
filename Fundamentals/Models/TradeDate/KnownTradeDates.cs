// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

//public static class KnownTradeDates
//{
//    private static readonly SortedDictionary<DateOnly, TradeDate> dict = new();

//    static KnownTradeDates()
//    {
//        var holidays = HolidayHelper.GetHolidays();

//        for (var date = TradeDate.MinValue;
//            date <= TradeDate.MaxValue; date = date.AddDays(1))
//        {
//            if (date.IsWeekday() && !holidays.Contains(date))
//                dict.Add(date, new TradeDate(date));
//        }
//    }

//    public static int Count => dict.Count;

//    public static TradeDate From(DateOnly value)
//    {
//        if (dict.ContainsKey(value))
//            return dict[value];

//        throw new ArgumentOutOfRangeException(nameof(value));
//    }

//    public static bool TryGetValue(DateOnly date, out TradeDate tradeDate) =>
//        dict.TryGetValue(date, out tradeDate);

//    public static bool TryGetPreloadDates(
//        TradeDate tradeDate, int daysBack, out TradeDate[] preloadDates)
//    {
//        tradeDate.MayNotBe().Default();
//        daysBack.MustBe().Between(1, 5);

//        var tradeDates = new List<TradeDate>();

//        while (tradeDates.Count < daysBack)
//        {
//            if (TryGetPriorTradeDate(tradeDate, out tradeDate))
//                tradeDates.Add(tradeDate);
//            else
//                break;
//        }

//        preloadDates = tradeDates.ToArray();

//        return preloadDates.Length > 0;
//    }

//    public static bool TryGetPriorTradeDate(
//        TradeDate tradeDate, out TradeDate value)
//    {
//        tradeDate.MayNotBe().Default();

//        var date = tradeDate.AsDateOnly();

//        if (date == TradeDate.MinValue)
//        {
//            value = default;

//            return false;
//        }

//        date = date.AddDays(-1);

//        while (!Contains(date))
//            date = date.AddDays(-1);

//        value = new TradeDate(date);

//        return true;
//    }

//    public static TradeDate[] GetAll() => dict.Values.ToArray();

//    public static TradeDate[] GetFiltered(
//        DateOnly? from = null, DateOnly? until = null)
//    {
//        if (from == null)
//            from = TradeDate.MinValue;

//        if (until == null)
//            until = TradeDate.MaxValue;

//        return dict.Values
//            .Where(v => v.AsDateOnly() >= from && v.AsDateOnly() <= until)
//            .ToArray();
//    }

//    public static bool Contains(TradeDate value) =>
//        dict.ContainsKey(value.AsDateOnly());

//    public static bool Contains(DateOnly value) => dict.ContainsKey(value);
//}