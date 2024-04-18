using MELCloudApiClient.Converters;
using MELCloudApiClient.Enums;
using System.Text.Json.Serialization;

namespace MELCloudApiClient.Models;
public class DevicePreview
{
    [JsonPropertyName(nameof(EffectiveFlags))]
    public int EffectiveFlags { get; set; }

    [JsonPropertyName(nameof(LocalIPAddress))]
    public string? LocalIPAddress { get; set; }

    [JsonPropertyName(nameof(RoomTemperature))]
    public double? RoomTemperature { get; set; }

    [JsonPropertyName(nameof(SetTemperature))]
    public double? SetTemperature { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverterWithNumberSupport))]
    [JsonPropertyName(nameof(SetFanSpeed))]
    public FanSpeed SetFanSpeed { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverterWithNumberSupport))]
    [JsonPropertyName(nameof(OperationMode))]
    public Enums.OperationMode OperationMode { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverterWithNumberSupport))]
    [JsonPropertyName(nameof(VaneHorizontal))]
    public Enums.HorizontalVanePosition VaneHorizontal { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverterWithNumberSupport))]
    [JsonPropertyName(nameof(VaneVertical))]
    public Enums.VerticalVanePosition VaneVertical { get; set; }

    [JsonPropertyName(nameof(Name))]
    public string? Name { get; set; }

    [JsonPropertyName(nameof(NumberOfFanSpeeds))]
    public int NumberOfFanSpeeds { get; set; }

    [JsonPropertyName(nameof(WeatherObservations))]
    public List<WeatherForecast> WeatherObservations { get; set; }

    [JsonPropertyName(nameof(ErrorMessage))]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName(nameof(ErrorCode))]
    public int? ErrorCode { get; set; }

    [JsonPropertyName(nameof(DefaultHeatingSetTemperature))]
    public double? DefaultHeatingSetTemperature { get; set; }

    [JsonPropertyName(nameof(DefaultCoolingSetTemperature))]
    public double? DefaultCoolingSetTemperature { get; set; }

    [JsonPropertyName(nameof(HideVaneControls))]
    public bool HideVaneControls { get; set; }

    [JsonPropertyName(nameof(HideDryModeControl))]
    public bool HideDryModeControl { get; set; }

    [JsonPropertyName(nameof(RoomTemperatureLabel))]
    public int RoomTemperatureLabel { get; set; }

    [JsonPropertyName(nameof(InStandbyMode))]
    public bool InStandbyMode { get; set; }

    [JsonPropertyName(nameof(TemperatureIncrementOverride))]
    public int TemperatureIncrementOverride { get; set; }

    [JsonPropertyName(nameof(ProhibitSetTemperature))]
    public bool ProhibitSetTemperature { get; set; }

    [JsonPropertyName(nameof(ProhibitOperationMode))]
    public bool ProhibitOperationMode { get; set; }

    [JsonPropertyName(nameof(ProhibitPower))]
    public bool ProhibitPower { get; set; }

    [JsonPropertyName(nameof(DemandPercentage))]
    public int? DemandPercentage { get; set; }

    [JsonPropertyName(nameof(DeviceID))]
    public int? DeviceID { get; set; }

    [JsonPropertyName(nameof(DeviceType))]
    public int? DeviceType { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName(nameof(LastCommunication))]
    public DateTime? LastCommunication { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName(nameof(NextCommunication))]
    public DateTime? NextCommunication { get; set; }

    [JsonPropertyName(nameof(Power))]
    public bool Power { get; set; }

    [JsonPropertyName(nameof(HasPendingCommand))]
    public bool HasPendingCommand { get; set; }

    [JsonPropertyName(nameof(Offline))]
    public bool Offline { get; set; }

    [JsonPropertyName(nameof(Scene))]
    public string? Scene { get; set; }

    [JsonPropertyName(nameof(SceneOwner))]
    public string? SceneOwner { get; set; }
}