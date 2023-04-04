// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ErrorArgs
{
    public ErrorArgs(Exception error)
    {
        Error = error;
    }

    public Exception Error { get; }
}