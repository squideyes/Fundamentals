// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public abstract class LogItemBase(
    Severity severity, Tag activity, TagValueSet? metadata)
{
    public Severity Severity { get; } = severity.MustBe().EnumValue();
    public Tag Activity { get; } = activity.MayNotBe().Null();
    public TagValueSet? Metadata { get; } =
        metadata?.MustBe(v => v is not null).True(v => !v!.IsEmpty);

    public TagValueSet GetTagValues()
    {
        var tagValues = Metadata ?? [];

        var customTagValues = GetCustomTagValues();

        if (customTagValues is not null)
            tagValues!.UpsertRange(customTagValues);

        return tagValues;
    }

    protected virtual TagValueSet GetCustomTagValues()
    {
        return null!;
    }
}