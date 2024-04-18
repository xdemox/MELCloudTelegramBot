using MELCloudApiClient.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace MELCloudTelegramBot.Utilities;
public static class MenuUtils
{
    public static Models.Menu DeviceMenu(MELCloudApiClient.Models.DevicePreview device, Enums.DeviceKeyboards keyboardType, string token, string deviceID, string buildingID)
    {
        InlineKeyboardMarkup inlineKeyboard = new(
        new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData($"{(device.Power ? "Turn OFF" : "Turn ON")}", $"DevicePower+{token}"),
                InlineKeyboardButton.WithCallbackData("Actions", $"DeviceActions+{token}")
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("Operation Mode", $"DeviceMode+{token}"),
                InlineKeyboardButton.WithCallbackData("🌀Fan Speed", $"DeviceFanSpeed+{token}")
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("🔃Vane Horizontal", $"DeviceVaneHorz+{token}"),
                InlineKeyboardButton.WithCallbackData("🔄Vane Vertical", $"DeviceVaneVert+{token}")
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("🎚Temperature", $"DeviceTemp+{token}")
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("🗂Dashboard", $"BuildList+{token}")
            }
        });

        var deviceForecast = "\r\n";
        if (device.WeatherObservations.Count > 0)
        {
            deviceForecast += "🌦 <b>Weather Forecast:</b>\n";
            foreach (var observation in device.WeatherObservations)
            {
                var weatherEmoji = Utilities.WeatherUtils.GetWeatherEmoji(observation.ConditionName);
                deviceForecast += $"📅 <b>{observation.Date:HH:mm dd-MM-yyyy}</b> | {weatherEmoji}{observation.ConditionName} 🌡{observation.Temperature}°C 💧{observation.Humidity}%\n";
            }
        }

        var botResponse =
        $"""
        🔧 <b>Device Overview</b> 🔧
        <b>Name:</b> {device.Name} • <b>ID:</b> {device.DeviceID}

        {(device.Power ? "✅" : "❌")} <b>Power Status:</b> {(device.Power ? "ON" : "OFF")}
        🌡 <b>Operation Mode:</b> {(Enum.IsDefined(typeof(OperationMode), device.OperationMode) ? MELCloudApiClient.Extensions.EnumExtensions.GetDescription(device.OperationMode) : "NONE")}
                    
        🌡 <b>Room Temperature:</b> {device.RoomTemperature}°C
        🎚 <b>Set Temperature:</b> {device.SetTemperature}°C
        🔥 <b>Default Heating Temp:</b> {device.DefaultHeatingSetTemperature}°C
        ❄️ <b>Default Cooling Temp:</b> {device.DefaultCoolingSetTemperature}°C

        🌀 <b>Fan Speed:</b> {device.SetFanSpeed}
        🔃 <b>Vane Horizontal:</b> {MELCloudApiClient.Extensions.EnumExtensions.GetDescription(device.VaneHorizontal)}
        🔄 <b>Vane Vertical:</b> {MELCloudApiClient.Extensions.EnumExtensions.GetDescription(device.VaneVertical)}
        💪 <b>Demand Percentage:</b> {device.DemandPercentage}%

        📡 <b>Last Communication:</b> {device.LastCommunication:HH:mm:ss dd-MM-yyyy}
        {deviceForecast}
        """;

        var deviceMenu = new Models.Menu()
        {
            Keyboard = Extensions.KeyboardExtensions.DeviceKeyboards(device, keyboardType, device.Power, token, deviceID, buildingID),
            Text = botResponse,
            ParseMode = Telegram.Bot.Types.Enums.ParseMode.Html
        };

        return deviceMenu;
    }
}