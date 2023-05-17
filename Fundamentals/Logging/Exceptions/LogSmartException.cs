// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class LogSmartException : Exception
{
    public LogSmartException(string message)
        : base(message)
    {
    }
}