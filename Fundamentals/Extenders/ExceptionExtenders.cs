// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;
using System.Text;

namespace SquidEyes.Fundamentals;

public static class ExceptionExtenders
{
    public static string[] GetStackTrace(
        this Exception error, bool withFileInfo)
    {
        var result = new List<string>();

        var stackTrace = new StackTrace(error, withFileInfo);

        foreach (var f in stackTrace.GetFrames())
        {
            var sb = new StringBuilder();

            var method = f.GetMethod();

            if (method is null)
                continue;

            sb.Append(method);

            var fileName = f.GetFileName();

            if (withFileInfo && !string.IsNullOrWhiteSpace(fileName))
            {
                var line = f.GetFileLineNumber();
                var column = f.GetFileColumnNumber();

                sb.Append($", {fileName} (Line {line}, Column {column})");
            }

            result.Add(sb.ToString());
        }

        return result.ToArray();
    }
}