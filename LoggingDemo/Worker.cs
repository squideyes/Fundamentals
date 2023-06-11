using SquidEyes.Fundamentals;

namespace LoggingDemo;

internal class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly IHostApplicationLifetime lifeTime;

    public Worker(ILogger<Worker> logger, IHostApplicationLifetime lifeTime)
    {
        this.logger = logger;
        this.lifeTime = lifeTime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
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

        lifeTime.StopApplication();

        while (!stoppingToken.IsCancellationRequested)
            await Task.Delay(100);
    }
}
