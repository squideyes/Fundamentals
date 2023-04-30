// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;

namespace SquidEyes.Fundamentals;

public class ErrorCaught : LogItemBase
{
    private readonly Error error;

    public ErrorCaught(Exception error, bool? withFileInfo = null)
        : base(LogLevel.Error, Tag.From(nameof(ErrorCaught)))
    {
        ArgumentNullException.ThrowIfNull(error, nameof(error));

        if (!withFileInfo.HasValue)
            withFileInfo = Debugger.IsAttached;

        this.error = new Error(error, withFileInfo.Value);
    }

    internal override List<(string, object)> GetIdValues()
    {
        var keyValues = new List<(string, object)> {
            ("Type", error.Type.ToString()) };

        foreach (var message in error.Messages)
            keyValues.Add(("Message", message));

        keyValues.Add(("Details", error));

        return keyValues;
    }
}