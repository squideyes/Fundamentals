// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public abstract class LogItemBase
{
    private static int ordinal = -1;

    public LogItemBase(Severity severity, Tag? activity = null)
    {
        Ordinal = Interlocked.Increment(ref ordinal);

        Severity = severity.Must().BeEnumValue();

        Activity = !activity.HasValue ? Tag.From(GetType().Name) 
            : activity.Value.MayNot().BeDefault();
    }

    public int Ordinal { get; }
    public Severity Severity { get; }
    public Tag Activity { get; }

    public abstract (Tag, object)[] GetTagValues();

    public virtual void Validate()
    {
    }
}