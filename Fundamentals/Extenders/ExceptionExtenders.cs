// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;

namespace SquidEyes.Fundamentals;

public static class ExceptionExtenders
{
    public static string[] GetStackTrace(this Exception error)
    {
        var result = new List<string>();

        var stackTrace = new StackTrace(error);

        foreach (var f in stackTrace.GetFrames())
        {
            var method = f.GetMethod();

            if (method is not null)
                result.Add(method.ToString()!);
        }

        return [.. result];
    }
}