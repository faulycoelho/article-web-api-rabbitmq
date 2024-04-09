using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentWorker(IQueueService queueService, IServiceScopeFactory _scopeFactory, ILoggerFactory loggerFactory) : BackgroundService
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<PaymentWorker>();

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();


            await queueService.ReadMessages(async (string message) =>
            {
                _logger.LogInformation($"processing new payment: {message}");
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var paymentUseCase = scope.ServiceProvider.GetRequiredService<IPaymentUseCase>();
                        await paymentUseCase.ProcessPaymentAsync(int.Parse(message));
                        _logger.LogInformation($"success!");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "error while processing the payment");
                }
            });

            await Task.CompletedTask;
        }
    }
}
