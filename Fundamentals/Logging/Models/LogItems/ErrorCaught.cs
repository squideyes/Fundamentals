// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;

namespace SquidEyes.Fundamentals;

public class ErrorCaught : LogItemBase
{
    private readonly Error error;
    private readonly TagValueSet tagValues;

    public ErrorCaught(Exception error,
        bool? withFileInfo = null, TagValueSet tagValues = null!)
        : base(Severity.Error)
    {
        error.MayNotBe().Null();

        if (!withFileInfo.HasValue)
            withFileInfo = Debugger.IsAttached;

        this.tagValues = tagValues.MustBe().True(v => v is null || !v.IsEmpty);

        this.error = new Error(error, withFileInfo.Value);
    }

    public override (Tag, object)[] GetTagValues()
    {
        var tagValues = new List<(Tag, object)> {
            (Tag.Create("Type"), error.Type.ToString()) };

        foreach (var message in error.Messages)
            tagValues.Add((Tag.Create("Message"), message));

        tagValues.Add((Tag.Create("Details"), error));

        if (this.tagValues != null)
            tagValues.AddRange(this.tagValues!.Select(kv => (kv.Key, kv.Value)));

        return tagValues.ToArray();
    }
}