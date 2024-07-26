// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Fundamentals;

namespace LoggingDemo;

internal static class CustomEventIds
{
    public const int LogonSucceeded = EventIds.CustomEvent;
    public const int LogonFailed = EventIds.CustomEvent + 1;
}