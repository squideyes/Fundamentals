// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;

namespace SquidEyes.Fundamentals;

public class ErrorCaught : LogItemBase
{
    private readonly Error error;
    private readonly Context context;

    public ErrorCaught(Exception error, 
        bool? withFileInfo = null, Context context = null!)
        : base(Severity.Error)
    {
        error.MayNot().BeNull();

        if (!withFileInfo.HasValue)
            withFileInfo = Debugger.IsAttached;

        this.context = context.Must().Be(v => v is null || !v.IsEmpty);

        this.error = new Error(error, withFileInfo.Value);
    }

    public override (Tag, object)[] GetTagValues()
    {
        var tagValues = new List<(Tag, object)> {
            (Tag.From("Type"), error.Type.ToString()) };

        foreach (var message in error.Messages)
            tagValues.Add((Tag.From("Message"), message));

        tagValues.Add((Tag.From("Details"), error));

        if (context != null)
            tagValues.AddRange(context!.Select(kv => (kv.Key, kv.Value)));

        return tagValues.ToArray();
    }
}