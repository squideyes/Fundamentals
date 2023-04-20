// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using static SquidEyes.Fundamentals.LogLevel;

namespace SquidEyes.Fundamentals;

using IV = Dictionary<Identifier, object>;

public class MiscEvent : LogItemBase
{
    public Dictionary<Identifier, string> idValues = new();

    public readonly Identifier eventKind;

    public MiscEvent(Identifier eventKind,
        string message = null!, LogLevel logLevel = Info)
        : this(eventKind, GetIV(message), logLevel)
    {
    }

    public MiscEvent(Identifier eventKind, IV idValues, LogLevel logLevel = Info)
        : base(logLevel, nameof(MiscEvent))
    {
        if (eventKind == default)
            throw new ArgumentOutOfRangeException(nameof(eventKind));

        if (idValues.Keys.Any(iv => iv == default))
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
            tuples.Add((key, idValues[key]));

        return tuples;
    }

    private static IV GetIV(string message)
    {
        var idValues = new IV();

        if (!string.IsNullOrWhiteSpace(message))
            idValues.Add("Message", message);

        return idValues;
    }
}