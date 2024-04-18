using MELCloudApiClient.Enums;
using System.ComponentModel;
using System.Reflection;
using Telegram.Bot.Types.ReplyMarkups;

namespace MELCloudTelegramBot.Extensions;
public static class KeyboardExtensions
{
    public static InlineKeyboardMarkup DeviceKeyboards(MELCloudApiClient.Models.DevicePreview device, Enums.DeviceKeyboards keyboardType, bool devicePower, string token, string deviceID, string buildingID)
    {
        switch (keyboardType)
        {
            case Enums.DeviceKeyboards.Main:
                {
                    return MainDeviceKeyboard(devicePower, token, deviceID, buildingID);
                }
            case Enums.DeviceKeyboards.Actions:
                {
                    throw new NotImplementedException();
                }
            case Enums.DeviceKeyboards.Mode:
                {
                    return CreateEnumKeyboard<OperationMode>(token, deviceID, buildingID, "DeviceMode", isReturnButton: true);
                }
            case Enums.DeviceKeyboards.FanSpeed:
                {
                    return CreateEnumKeyboard<FanSpeed>(token, deviceID, buildingID, "DeviceFanSpeed", isReturnButton: true);
                }
            case Enums.DeviceKeyboards.VaneHorizontal:
                {
                    return CreateEnumKeyboard<HorizontalVanePosition>(token, deviceID, buildingID, "DeviceVaneHorz", isReturnButton: true);
                }
            case Enums.DeviceKeyboards.VaneVertical:
                {
                    return CreateEnumKeyboard<HorizontalVanePosition>(token, deviceID, buildingID, "DeviceVaneVert", isReturnButton: true);
                }
            case Enums.DeviceKeyboards.Temperature:
                {
                    return DeviceTemperatureKeyboard(device.SetTemperature, token, deviceID, buildingID);
                }
            case Enums.DeviceKeyboards.None:
            default: return null;
        }
    }
    private static InlineKeyboardMarkup MainDeviceKeyboard(bool devicePower, string token, string deviceID, string buildingID) => new(
    new[]
    {
        new []
        {
            InlineKeyboardButton.WithCallbackData("Refresh", $"GetDevice+{token}+{deviceID}+{buildingID}")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData($"{(devicePower ? "Turn OFF" : "Turn ON")}", $"DevicePower+{token}+{deviceID}+{buildingID}"),
            InlineKeyboardButton.WithCallbackData("Actions", $"DeviceActions+{token}+{deviceID}+{buildingID}")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData("Operation Mode", $"DeviceMode+{token}+{deviceID}+{buildingID}"),
            InlineKeyboardButton.WithCallbackData("🌀Fan Speed", $"DeviceFanSpeed+{token}")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData("🔃Vane Horizontal", $"DeviceVaneHorz+{token}+{deviceID}+{buildingID}"),
            InlineKeyboardButton.WithCallbackData("🔄Vane Vertical", $"DeviceVaneVert+{token}+{deviceID}+{buildingID}")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData("🎚Temperature", $"DeviceTemp+{token}+{deviceID}+{buildingID}")
        },
        new []
        {
            InlineKeyboardButton.WithCallbackData("🗂Dashboard", $"BuildList+{token}+{deviceID}+{buildingID}")
        }
    });

    private static InlineKeyboardMarkup DeviceTemperatureKeyboard(double? currentTemperature, string token, string deviceID, string buildingID)
    {
        const double step = 0.5;
        const int buttonsPerRow = 3;

        var buttons = new List<InlineKeyboardButton[]>();

        // Add previous button
        buttons.Add(new[]
        {
            InlineKeyboardButton.WithCallbackData("<", $"DeviceTemp+{token}+{deviceID}+{buildingID}+{currentTemperature - step * buttonsPerRow}")
        });

        // Add temperature buttons
        for (int i = 0; i < buttonsPerRow; i++)
        {
            var temperature = currentTemperature + i * step;
            buttons.Add(new[]
            {
                InlineKeyboardButton.WithCallbackData(temperature.Value.ToString("0.0"), $"DeviceTemp+{token}+{deviceID}+{buildingID}+{temperature}")
            });
        }

        // Add next button
        buttons.Add(new[]
        {
            InlineKeyboardButton.WithCallbackData(">", $"DeviceTemp+{token}+{deviceID}+{buildingID}+{currentTemperature + step * buttonsPerRow}")
        });

        return new InlineKeyboardMarkup(buttons);
    }


    private static InlineKeyboardMarkup CreateEnumKeyboard<TEnum>(string token, string deviceID, string buildingID, string propertyName, bool isReturnButton = false) where TEnum : Enum
    {
        var enumButtons = Enum.GetValues(typeof(TEnum))
                              .OfType<TEnum>()
                              .Select(enumValue => CreateButtonForEnum(enumValue, token, deviceID, buildingID, propertyName))
                              .ToArray();
        if (isReturnButton)
        {
            var returnButton = InlineKeyboardButton.WithCallbackData("Return", $"GetDevice+{token}+{deviceID}+{buildingID}");
            enumButtons.Append(new[] { returnButton }).ToArray();
        }
        return new InlineKeyboardMarkup(enumButtons);
    }
    private static InlineKeyboardButton[] CreateButtonForEnum<TEnum>(TEnum enumValue, string token, string deviceID, string buildingID, string propertyName) where TEnum : Enum
    {
        var buttonName = enumValue.GetType()
                                  .GetField(enumValue.ToString())
                                  .GetCustomAttribute<DescriptionAttribute>()?.Description ?? enumValue.ToString();

        var callbackData = $"{propertyName}+{token}+{deviceID}+{buildingID}+{Convert.ToInt32(enumValue)}";
        return new[] { InlineKeyboardButton.WithCallbackData(buttonName, callbackData) };
    }
}