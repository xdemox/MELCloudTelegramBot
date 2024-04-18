using MELCloudTelegramBot.Abstract;
using MELCloudTelegramBot.Services;
using Microsoft.Extensions.Logging;

namespace MELCloudTelegramBot.Services;

// Compose Polling and ReceiverService implementations
public class PollingService : PollingServiceBase<ReceiverService>
{
    public PollingService(IServiceProvider serviceProvider, ILogger<PollingService> logger)
        : base(serviceProvider, logger)
    {
    }
}