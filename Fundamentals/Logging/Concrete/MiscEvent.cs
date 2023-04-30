// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using static SquidEyes.Fundamentals.LogLevel;

namespace SquidEyes.Fundamentals;

using IV = Dictionary<Tag, object>;

public class MiscEvent : LogItemBase
{
    public Dictionary<Tag, string> idValues = new();

    public readonly Tag eventKind;

    public MiscEvent(Tag eventKind,
        string message = null!, LogLevel logLevel = Info)
        : this(eventKind, GetIV(message), logLevel)
    {
    }

    public MiscEvent(Tag eventKind, IV idValues, LogLevel logLevel = Info)
        : base(logLevel, Tag.From(nameof(MiscEvent)))
    {
        if (eventKind.IsDefault())
            throw new ArgumentOutOfRangeException(nameof(eventKind));

        if (idValues.Keys.Any(iv => iv.IsDefault()))
            throw new ArgumentException(nameof(idValues));

        ArgumentNullException.ThrowIfNull(nameof(idValues));

        this.eventKind = eventKind;

        foreach (var key in idValues.Keys)
            this.idValues[key] = idValues[key].ToString()!;
    }

    internal override List<(string, object)> GetIdValues()
    {
        var tuples = new List<(string, object)> {
            ("EventKind", eventKind.ToString()) };

        foreach (var key in idValues.Keys)
            tuples.Add((key.ToString(), idValues[key]));

        return tuples;
    }

    private static IV GetIV(string message)
    {
        var idValues = new IV();

        if (!string.IsNullOrWhiteSpace(message))
            idValues.Add(Tag.From("Message"), message);

        return idValues;
    }
}