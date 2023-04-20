// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog;
using System.Text;

namespace SquidEyes.Fundamentals;

public static class ILoggerExtenders
{
    public static void Log<T>(this ILogger logger, T logItem)
        where T : LogItemBase
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(logItem, nameof(logItem));

        var sb = new StringBuilder();
        var datas = new List<object>();

        void Append(string key, object value)
        {
            if (sb!.Length > 0)
                sb.Append(',');

            sb!.Append($"{{@{key}}}");
            datas!.Add(value!);
        }

        Append("Ordinal", logItem.Ordinal);
        Append("Activity", logItem.Activity);

        foreach (var (key, value) in logItem.GetIdValues())
            Append(key, value);

        logger.Write(logItem.LogLevel.ToLogEventLevel(),
            sb.ToString(), datas.ToArray());
    }
}