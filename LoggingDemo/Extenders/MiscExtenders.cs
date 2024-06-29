using SquidEyes.Fundamentals.LoggingDemo;

namespace LoggingDemo;

internal static class MiscExtenders
{
    public static string ToCode(this Broker broker)
    {
        return broker switch
        {
            Broker.CanonTrading => "CANON",
            Broker.DiscountTrading => "DT",
            Broker.AmpFutures => "AMP",
            Broker.OptimusFutures => "OPTIMUS",
            _ => throw new ArgumentOutOfRangeException(nameof(broker))
        };
    }
}
