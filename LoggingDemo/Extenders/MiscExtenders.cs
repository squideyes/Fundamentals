// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

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