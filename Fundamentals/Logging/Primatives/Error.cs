// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class Error
{
    internal Error(Exception error, bool withFileInfo)
    {
        Type = error.GetType().ToString();
        Messages = new string[] { error.Message };
        Source = error.Source!;
        TargetSite = error.TargetSite!.ToString()!;
        StackTrace = error.GetStackTrace(withFileInfo);

        if (error.InnerException is not null)
            InnerError = new Error(error.InnerException, withFileInfo);
    }

    public string Type { get; }
    public string[] Messages { get; }
    public string Source { get; }
    public string TargetSite { get; }
    public string[] StackTrace { get; }
    public Error? InnerError { get; }
}