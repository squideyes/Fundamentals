using SquidEyes.Fundamentals;

namespace LoggingDemo;

internal static class CustomEventIds
{
    public const int LogonSucceeded = EventIds.CustomEvent;
    public const int LogonFailed = EventIds.CustomEvent + 1;
}
