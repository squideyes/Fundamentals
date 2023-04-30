// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public abstract class LogItemBase
{
    private static int ordinal = -1;

    public LogItemBase(LogLevel logLevel, Tag activity)
    {
        LogLevel = logLevel;
        Activity = activity;
        Ordinal = Interlocked.Increment(ref ordinal);
    }

    public Tag Activity { get; }
    public LogLevel LogLevel { get; }
    public int Ordinal { get; }

    internal abstract List<(string, object)> GetIdValues();
}