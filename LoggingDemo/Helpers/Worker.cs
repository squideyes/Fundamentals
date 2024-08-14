// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Fundamentals;

namespace LoggingDemo;

internal class Worker(
    ILogger<Worker> logger, IHostApplicationLifetime lifeTime) : BackgroundService
{
    private readonly ILogger<Worker> logger = logger;
    private readonly IHostApplicationLifetime lifeTime = lifeTime;

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
            logger.LogExceptionCaught(outer);
        }

        lifeTime.StopApplication();

        while (!stoppingToken.IsCancellationRequested)
            await Task.Delay(100, stoppingToken);
    }
}