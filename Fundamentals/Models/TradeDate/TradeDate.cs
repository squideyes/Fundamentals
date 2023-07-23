//namespace SquidEyes.Fundamentals;

//public readonly struct TradeDate : IEquatable<TradeDate>, IComparable<TradeDate>
//{
//    public static readonly DateOnly MinValue = new(2019, 12, 16);
//    public static readonly DateOnly MaxValue = new(2029, 12, 14);

//    internal TradeDate(DateOnly value)
//    {
//        Value = value;
//    }

//    public int Month => Value.Month;
//    public int Day => Value.Day;
//    public int Year => Value.Year;
//    public int DayNumber => Value.DayNumber;
//    public int DayOfYear => Value.DayOfYear;
//    public DayOfWeek DayOfWeek => Value.DayOfWeek;

//    private DateOnly Value { get; }

//    public DateOnly AsDateOnly() => Value;

//    public DateTime AsDateTime() => Value.ToDateTime(TimeOnly.MinValue);

//    public bool Equals(TradeDate other) => Value.Equals(other.Value);

//    public override bool Equals(object? other) =>
//        other is TradeDate tradeDate && Equals(tradeDate);

//    public int CompareTo(TradeDate other) => Value.CompareTo(other.Value);

//    public override int GetHashCode() => Value.GetHashCode();

//    public override string ToString() => Value.ToString("MM/dd/yyyy");

//    public static bool operator ==(TradeDate left, TradeDate right) =>
//        left.Equals(right);

//    public static bool operator !=(TradeDate left, TradeDate right) =>
//        !(left == right);

//    public static bool operator <(TradeDate left, TradeDate right) =>
//        left.CompareTo(right) < 0;

//    public static bool operator <=(TradeDate left, TradeDate right) =>
//        left.CompareTo(right) <= 0;

//    public static bool operator >(TradeDate left, TradeDate right) =>
//        left.CompareTo(right) > 0;

//    public static bool operator >=(TradeDate left, TradeDate right) =>
//        left.CompareTo(right) >= 0;
//}