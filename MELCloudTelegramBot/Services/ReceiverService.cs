using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Abstract;
using Telegram.Bots.Extensions.Polling;

namespace MELCloudTelegramBot.Services;
// Compose Receiver and UpdateHandler implementation
public class ReceiverService : ReceiverServiceBase<UpdateHandler>
{
    public ReceiverService(
    ITelegramBotClient botClient,
        UpdateHandler updateHandler,
        ILogger<ReceiverServiceBase<UpdateHandler>> logger)
        : base(botClient, updateHandler, logger)
    {
    }
}