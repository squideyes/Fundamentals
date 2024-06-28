// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ExceptionInfo
{
    internal ExceptionInfo(Exception ex)
    {
        ErrorType = ex.GetType().ToString();
        Message = ex.Message;
        Source = ex.Source!;
        TargetSite = ex.TargetSite!.ToString()!;
        StackTrace = ex.GetStackTrace();

        if (ex.InnerException is not null)
            InnerException = new ExceptionInfo(ex.InnerException);
    }

    public string ErrorType { get; }
    public string Message { get; }
    public string Source { get; }
    public string TargetSite { get; }
    public string[] StackTrace { get; }
    public ExceptionInfo? InnerException { get; }
}