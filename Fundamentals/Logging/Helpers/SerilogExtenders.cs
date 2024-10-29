using Serilog;

namespace SquidEyes.Fundamentals;

public static class SerilogExtenders
{
    public static LoggerConfiguration OmitTypeField<T>(
        this LoggerConfiguration loggerConfiguration)
    {
        return loggerConfiguration.Destructure.ByTransforming<T>(instance =>
        {
            var properties = instance!.GetType()
                .GetProperties().Where(p => p.Name != "$type")
                .ToDictionary(p => p.Name, p => p.GetValue(instance));

            return properties;
        });
    }
}