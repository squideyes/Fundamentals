// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using System.Text;

namespace SquidEyes.Fundamentals;

public static class ILoggerExtenders
{
    public static void Log<T>(this ILogger logger, T logItem)
        where T : LogItemBase
    {
        logger!.MayNotBe().Null();
        logItem.MayNotBe().Null();

        logItem.Validate();

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

        foreach (var (tag, value) in logItem.GetTagValues())
        {
            tag.MayNotBe().Default();

            Append(tag.ToString(), value);
        }

        logger.Log(logItem.Severity.ToLogLevel(),
            sb.ToString(), datas.ToArray());
    }

    public static void Log<T>(this Serilog.ILogger logger, T logItem)
        where T : LogItemBase
    {
        logger!.MayNotBe().Null();
        logItem.MayNotBe().Null();

        logItem.Validate();

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

        foreach (var (tag, value) in logItem.GetTagValues())
        {
            tag.MayNotBe().Default();

            Append(tag.ToString(), value);
        }

        logger.Write(logItem.Severity.ToLogEventLevel(),
            sb.ToString(), datas.ToArray());
    }
}