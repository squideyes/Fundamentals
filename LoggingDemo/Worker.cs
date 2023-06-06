using SquidEyes.Fundamentals;

namespace LoggingDemo;

internal class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;

    public Worker(ILogger<Worker> logger)
    {
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.CompletedTask;

        try
        {
            try
            {
                throw new ArgumentException(
                    "An \"Inner\" exception was caught!");
            }
            catch (Exception inner)
            {
                throw new Exception(
                    "An \"Outer\" exception was caught!", inner);
            }
        }
        catch (Exception outer)
        {
            // Log "ErrorCaught" (a standard log-item)
            logger.Log(new ErrorCaught(outer, true));
        }
    }
}
