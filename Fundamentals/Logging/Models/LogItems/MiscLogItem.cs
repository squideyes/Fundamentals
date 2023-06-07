// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class MiscLogItem : LogItemBase
{
    private readonly TagValueSet? tagValues;

    public MiscLogItem(Severity severity, Tag activity, TagValueSet tagValues = null!)
        : base(severity, activity)
    {
        this.tagValues = tagValues.Must().Be(v => v is null || !v.IsEmpty);
    }

    public override (Tag, object)[] GetTagValues()
    {
        if (tagValues == null)
            return Array.Empty<(Tag, object)>();
        else
            return tagValues!.Select(kv => (kv.Key, kv.Value)).ToArray();
    }
}