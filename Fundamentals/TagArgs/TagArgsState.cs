// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public enum TagArgState
{
    Valid = 1,
    Invalid,
    ParseFailed,
    NotTrimmed,
    TooLong,
    BadChars,
    NullOrWhitespace
}