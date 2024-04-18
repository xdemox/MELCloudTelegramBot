using MELCloudApiClient.Enums;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;

namespace MELCloudTelegramBot.Services;

public class UpdateHandler : IUpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<UpdateHandler> _logger;

    public UpdateHandler(ITelegramBotClient botClient, ILogger<UpdateHandler> logger)
    {
        _botClient = botClient;
        _logger = logger;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient _, Update update, CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            // UpdateType.Unknown:
            // UpdateType.ChannelPost:
            // UpdateType.EditedChannelPost:
            // UpdateType.ShippingQuery:
            // UpdateType.PreCheckoutQuery:
            // UpdateType.Poll:
            { Message: { } message } => BotOnMessageReceived(message, cancellationToken),
            { EditedMessage: { } message } => BotOnMessageReceived(message, cancellationToken),
            { CallbackQuery: { } callbackQuery } => BotOnCallbackQueryReceived(callbackQuery, cancellationToken),
            { InlineQuery: { } inlineQuery } => BotOnInlineQueryReceived(inlineQuery, cancellationToken),
            { ChosenInlineResult: { } chosenInlineResult } => BotOnChosenInlineResultReceived(chosenInlineResult, cancellationToken),
            _ => UnknownUpdateHandlerAsync(update, cancellationToken)
        };

        await handler;
    }

    private async Task BotOnMessageReceived(Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Receive message type: {MessageType}", message.Type);
        if (message.Text is not { } messageText)
            return;

        var parts = messageText.Split(' ');
        var action = parts[0] switch
        {
            "/login" => HandleLogin(_botClient, message, parts, cancellationToken),
            _ => Usage(_botClient, message, cancellationToken)
        };
        Message sentMessage = await action;
        _logger.LogInformation("The message was sent with id: {SentMessageId}", sentMessage.MessageId);

        static async Task<Message> HandleLogin(ITelegramBotClient botClient, Message message, string[] parts, CancellationToken cancellationToken)
        {
            if (parts.Length < 3)
            {
                return await botClient.SendTextMessageAsync(message.Chat.Id, "Usage: /login <username> <password>", cancellationToken: cancellationToken);
            }

            var username = parts[1];
            var password = parts[2];

            // Delete the message containing the username and password
            await botClient.DeleteMessageAsync(message.Chat.Id, message.MessageId, cancellationToken);

            return await MainLogin(botClient, message, username, password, cancellationToken);
        }

        static async Task<Message> MainLogin(ITelegramBotClient botClient, Message message, string username, string password, CancellationToken cancellationToken)
        {
            MELCloudApiClient.Models.ClientLoginRequest loginRequest = new()
            {
                AppVersion = "1.31.1.0",
                CaptchaResponse = null,
                Email = username,
                Language = Language.English,
                Password = password,
                Persist = false
            };

            var client = new MELCloudApiClient.Services.MELCloudAPI();
            var clientLogin = await client.ClientLoginAsync(loginRequest);

            if (clientLogin is not null)
            {
                if (clientLogin.ErrorId is not null)
                {
                    return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: clientLogin.ErrorMessage ?? $"Server returned error: {clientLogin.ErrorId}, but no error message.",
                    cancellationToken: cancellationToken);
                }

                InlineKeyboardMarkup inlineKeyboard = new(
                new[]
                {
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("Open Menu", $"BuildList+{clientLogin.LoginData.ContextKey}")
                    }
                });

                var botResponse =
                $"""
                {(string.IsNullOrWhiteSpace(clientLogin.AppVersionAnnouncement) ? string.Empty : $"Version Announcement: {clientLogin.AppVersionAnnouncement}")}
                Welcome to MELCloud Telegram bot, {clientLogin.LoginData.Name} 🤠
                """;

                return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: botResponse,
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken);
            }
            else
            {
                return await botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Server returned null. Please try again!",
                    cancellationToken: cancellationToken);
            }
        }

        static async Task<Message> Usage(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            const string usage = "Usage:\n" +
                                 "Usage: /login <username> <password>";

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: usage,
                replyMarkup: new ReplyKeyboardRemove(),
                cancellationToken: cancellationToken);
        }

        static async Task<Message> StartInlineQuery(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboard = new(
                InlineKeyboardButton.WithSwitchInlineQueryCurrentChat("Inline Mode"));

            return await botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: "Press the button to start Inline Query",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }

#pragma warning disable RCS1163 // Unused parameter.
#pragma warning disable IDE0060 // Remove unused parameter
        static Task<Message> FailingHandler(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
        {
            throw new IndexOutOfRangeException();
        }
#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore RCS1163 // Unused parameter.
    }

    // Process Inline Keyboard callback data
    private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline keyboard callback from: {CallbackQueryId}", callbackQuery.Id);

        if (string.IsNullOrWhiteSpace(callbackQuery.Data) is false)
        {
            var parts = callbackQuery.Data.Split('+');
            switch (parts[0])
            {
                case "BuildList":
                {
                    var client = new MELCloudApiClient.Services.MELCloudAPI();
                    var result = await client.GetListDevicesAsync(parts[1]);
                    if (result is null)
                    {
                        await _botClient.SendTextMessageAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            text:$"Server returned empty list, but no error message. Try to login again", 
                            cancellationToken: cancellationToken);
                        break;
                    }

                    var InlineKeyboardList = new List<InlineKeyboardButton>();
                    var devices = result.First();
                    foreach (var device in devices.Structure.Devices)
                    {
                        var deviceInfo = $"{device.DeviceName} [{(device.DeviceDetails.Power ? "ON" : "OFF")} {device.DeviceDetails.RoomTemperature}C]";
                        InlineKeyboardList.Add(new InlineKeyboardButton(deviceInfo) { Text = deviceInfo, CallbackData = $"GetDevice+{parts[1]}+{device.DeviceID}+{device.BuildingID}" });
                    }

                    InlineKeyboardList.Add(new InlineKeyboardButton("Scenes") { Text = "Scenes", CallbackData = $"Scenes+{parts[1]}+{devices.Structure.Devices.First().DeviceID}+{devices.Structure.Devices.First().BuildingID}" });
                    InlineKeyboardList.Add(new InlineKeyboardButton("Reports") { Text = "Reports", CallbackData = $"Reports+{parts[1]}+{devices.Structure.Devices.First().DeviceID}+{devices.Structure.Devices.First().BuildingID}" });
                    InlineKeyboardList.Add(new InlineKeyboardButton("Settings") { Text = "Settings", CallbackData = $"Settings+{parts[1]}" });
                    InlineKeyboardMarkup inlineKeyboard = new(InlineKeyboardList);

                    var botResponse =
                    $"""
                    🗂Dashboard:

                    🏢 Name - {devices.Name}
                    📍Address - {devices.AddressLine1}, {devices.AddressLine2}, {devices.City}, {devices.Country}
                    🛠Devices:
                    """;

                    await _botClient.EditMessageTextAsync(
                        chatId: callbackQuery.Message!.Chat.Id,
                        messageId: callbackQuery.Message!.MessageId,
                        text: botResponse,
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);

                    break;
                }
                case "GetDevice":
                {
                    var token = parts[1];
                    var deviceID = parts[2];
                    var buildingID = parts[3];

                    var client = new MELCloudApiClient.Services.MELCloudAPI();
                    var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                    if (device is null)
                    {
                        await _botClient.SendTextMessageAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            text: $"Server returned empty list, but no error message. Try to login again",
                            cancellationToken: cancellationToken);
                        break;
                    }

                    var deviceMenu = Utilities.MenuUtils.DeviceMenu(device, Enums.DeviceKeyboards.Main, token, deviceID, buildingID);

                    await _botClient.EditMessageTextAsync(
                        chatId: callbackQuery.Message!.Chat.Id,
                        messageId: callbackQuery.Message.MessageId,
                        text: deviceMenu.Text,
                        parseMode: deviceMenu.ParseMode,
                        replyMarkup: deviceMenu.Keyboard,
                        cancellationToken: cancellationToken);

                        break;
                }
                case "DevicePower":
                {
                    var token = parts[1];
                    var deviceID = parts[2];
                    var buildingID = parts[3];

                    var client = new MELCloudApiClient.Services.MELCloudAPI();

                    var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                    if (device is null)
                    {
                        await _botClient.SendTextMessageAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            text: $"Server returned empty list, but no error message. Try to login again",
                            cancellationToken: cancellationToken);
                        break;
                    }

                    device.Power = !device.Power;
                    device.Offline = !device.Offline;
                    device.EffectiveFlags = 1;

                    var updatedDevice = await client.SetAttributeAsync(token, device);

                    var deviceMenu = Utilities.MenuUtils.DeviceMenu(updatedDevice, Enums.DeviceKeyboards.Main, token, deviceID, buildingID);
                    
                    await _botClient.AnswerCallbackQueryAsync(
                        callbackQueryId: callbackQuery.Id,
                        text: "Done",
                        cancellationToken: cancellationToken);

                    await _botClient.EditMessageTextAsync(
                        chatId: callbackQuery.Message!.Chat.Id,
                        messageId: callbackQuery.Message.MessageId,
                        text: deviceMenu.Text,
                        parseMode: deviceMenu.ParseMode,
                        replyMarkup: deviceMenu.Keyboard,
                        cancellationToken: cancellationToken);

                    break;
                }
                case "DeviceActions":
                {
                    var token = parts[1];
                    var deviceID = parts[2];
                    var buildingID = parts[3];

                    var client = new MELCloudApiClient.Services.MELCloudAPI();

                    var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                    if (device is null)
                    {
                        await _botClient.SendTextMessageAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            text: $"Server returned empty list, but no error message. Try to login again",
                            cancellationToken: cancellationToken);
                        break;
                    }

                    var deviceMenu = Utilities.MenuUtils.DeviceMenu(device, Enums.DeviceKeyboards.Main, token, deviceID, buildingID);

                    await _botClient.EditMessageTextAsync(
                        chatId: callbackQuery.Message!.Chat.Id,
                        messageId: callbackQuery.Message.MessageId,
                        text: deviceMenu.Text,
                        parseMode: deviceMenu.ParseMode,
                        replyMarkup: deviceMenu.Keyboard,
                        cancellationToken: cancellationToken);

                    break;
                }
                case "DeviceMode":
                {
                    var token = parts[1];
                    var deviceID = parts[2];
                    var buildingID = parts[3];
                    if (parts.Length > 4)
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        OperationMode mode = (OperationMode)Enum.Parse(typeof(OperationMode), parts[4]);
                        device.OperationMode = mode;

                        var updatedDevice = await client.SetAttributeAsync(parts[1], device);

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(updatedDevice, Enums.DeviceKeyboards.Main, token, deviceID, buildingID);

                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQueryId: callbackQuery.Id,
                            text: "Done",
                            cancellationToken: cancellationToken);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(device, Enums.DeviceKeyboards.Mode, token, deviceID, buildingID);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }

                    break;
                }
                case "DeviceFanSpeed":
                {
                    var token = parts[1];
                    var deviceID = parts[2];
                    var buildingID = parts[3];
                    if (parts.Length > 4)
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        FanSpeed mode = (FanSpeed)Enum.Parse(typeof(FanSpeed), parts[4]);
                        device.SetFanSpeed = mode;

                        var updatedDevice = await client.SetAttributeAsync(parts[1], device);

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(updatedDevice, Enums.DeviceKeyboards.Main, token, deviceID, buildingID);
                        
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQueryId: callbackQuery.Id,
                            text: "Done",
                            cancellationToken: cancellationToken);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(device, Enums.DeviceKeyboards.FanSpeed, token, deviceID, buildingID);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    break;
                }
                case "DeviceVaneHorz":
                {
                    var token = parts[1];
                    var deviceID = parts[2];
                    var buildingID = parts[3];
                    if (parts.Length > 4)
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        HorizontalVanePosition mode = (HorizontalVanePosition)Enum.Parse(typeof(HorizontalVanePosition), parts[4]);
                        device.VaneHorizontal = mode;

                        var updatedDevice = await client.SetAttributeAsync(parts[1], device);

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(updatedDevice, Enums.DeviceKeyboards.Main, token, deviceID, buildingID);
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQueryId: callbackQuery.Id,
                            text: "Done",
                            cancellationToken: cancellationToken);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(device, Enums.DeviceKeyboards.VaneHorizontal, token, deviceID, buildingID);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    break;
                }
                case "DeviceVaneVert":
                {
                    var token = parts[1];
                    var deviceID = parts[2];
                    var buildingID = parts[3];
                    if (parts.Length > 4)
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        VerticalVanePosition mode = (VerticalVanePosition)Enum.Parse(typeof(VerticalVanePosition), parts[4]);
                        device.VaneVertical = mode;

                        var updatedDevice = await client.SetAttributeAsync(parts[1], device);

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(updatedDevice, Enums.DeviceKeyboards.Main, token, deviceID, buildingID);
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQueryId: callbackQuery.Id,
                            text: "Done",
                            cancellationToken: cancellationToken);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(device, Enums.DeviceKeyboards.VaneVertical, token, deviceID, buildingID);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    break;                
                }
                case "DeviceTemp":
                {
                    var token = parts[1];
                    var deviceID = parts[2];
                    var buildingID = parts[3];
                    if (parts.Length > 4)
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        if (double.TryParse(parts[4], out var temperature))
                        {
                            device.SetTemperature = temperature;
                        }
                        else
                        {
                            await _botClient.AnswerCallbackQueryAsync(
                                callbackQueryId: callbackQuery.Id,
                                text: "Invalid temperature value: " + parts[4],
                                cancellationToken: cancellationToken);
                        }

                        var updatedDevice = await client.SetAttributeAsync(parts[1], device);

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(updatedDevice, Enums.DeviceKeyboards.Main, token, deviceID, buildingID);
                        
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQueryId: callbackQuery.Id,
                            text: "Done",
                            cancellationToken: cancellationToken);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    else
                    {
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        var deviceMenu = Utilities.MenuUtils.DeviceMenu(device, Enums.DeviceKeyboards.Temperature, token, deviceID, buildingID);

                        await _botClient.EditMessageTextAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            messageId: callbackQuery.Message.MessageId,
                            text: deviceMenu.Text,
                            parseMode: deviceMenu.ParseMode,
                            replyMarkup: deviceMenu.Keyboard,
                            cancellationToken: cancellationToken);
                    }
                    break;                   
                }
                case "Reports":
                    {
                        var token = parts[1];
                        var deviceID = parts[2];
                        var buildingID = parts[3];
                        var client = new MELCloudApiClient.Services.MELCloudAPI();

                        var device = await client.GetDeviceAsync(token, deviceID, buildingID);
                        if (device is null)
                        {
                            await _botClient.SendTextMessageAsync(
                                chatId: callbackQuery.Message!.Chat.Id,
                                text: $"Server returned empty list, but no error message. Try to login again",
                                cancellationToken: cancellationToken);
                            break;
                        }

                        MELCloudApiClient.Models.Reports.TemperatureLogRequest reportRequest = new()
                        {
                            DeviceID = Convert.ToInt32(deviceID),
                            Duration = 31,
                            FromDate = DateTime.Now,
                            ToDate = DateTime.Now,
                            Location = "?",
                        };

                        var report = await client.GetTemperatureReport(token, reportRequest);
                        var image = Utilities.ReportsUtils.GenerateImageReportForTemperature(report);
                        image.Position = 0;

                        await _botClient.SendChatActionAsync(
                            callbackQuery.Message!.Chat.Id,
                            ChatAction.UploadPhoto,
                            cancellationToken: cancellationToken);

                        await _botClient.SendPhotoAsync(
                            chatId: callbackQuery.Message!.Chat.Id,
                            photo: new InputFileStream(image),
                            caption: $"Temperature report from {reportRequest.FromDate} to {reportRequest.ToDate}",
                            cancellationToken: cancellationToken);
                        break;
                    }
                default: break;
            }
        }
    }

    #region Inline Mode

    private async Task BotOnInlineQueryReceived(InlineQuery inlineQuery, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline query from: {InlineQueryFromId}", inlineQuery.From.Id);

        InlineQueryResult[] results = {
            // displayed result
            new InlineQueryResultArticle(
                id: "1",
                title: "TgBots",
                inputMessageContent: new InputTextMessageContent("hello"))
        };

        await _botClient.AnswerInlineQueryAsync(
            inlineQueryId: inlineQuery.Id,
            results: results,
            cacheTime: 0,
            isPersonal: true,
            cancellationToken: cancellationToken);
    }

    private async Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received inline result: {ChosenInlineResultId}", chosenInlineResult.ResultId);

        await _botClient.SendTextMessageAsync(
            chatId: chosenInlineResult.From.Id,
            text: $"You chose result with Id: {chosenInlineResult.ResultId}",
            cancellationToken: cancellationToken);
    }

    #endregion

#pragma warning disable IDE0060 // Remove unused parameter
#pragma warning disable RCS1163 // Unused parameter.
    private Task UnknownUpdateHandlerAsync(Update update, CancellationToken cancellationToken)
#pragma warning restore RCS1163 // Unused parameter.
#pragma warning restore IDE0060 // Remove unused parameter
    {
        _logger.LogInformation("Unknown update type: {UpdateType}", update.Type);
        return Task.CompletedTask;
    }

    public async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", ErrorMessage);

        // Cooldown in case of network connection error
        if (exception is RequestException)
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
    }
}