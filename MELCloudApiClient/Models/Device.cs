using MELCloudApiClient.Converters;
using System.Text.Json.Serialization;

namespace MELCloudApiClient.Models;

public class Device
{
    [JsonPropertyName("DeviceID")]
    public int DeviceID { get; set; }

    [JsonPropertyName("DeviceName")]
    public string DeviceName { get; set; }

    [JsonPropertyName("BuildingID")]
    public int BuildingID { get; set; }

    [JsonPropertyName("BuildingName")]
    public string BuildingName { get; set; }

    [JsonPropertyName("FloorID")]
    public int? FloorID { get; set; }

    [JsonPropertyName("FloorName")]
    public string FloorName { get; set; }

    [JsonPropertyName("AreaID")]
    public int? AreaID { get; set; }

    [JsonPropertyName("AreaName")]
    public string AreaName { get; set; }

    [JsonPropertyName("ImageID")]
    public int ImageID { get; set; }

    [JsonPropertyName("InstallationDate")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? InstallationDate { get; set; }

    [JsonPropertyName("LastServiceDate")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? LastServiceDate { get; set; }

    [JsonPropertyName("Presets")]
    public List<object> Presets { get; set; }

    [JsonPropertyName("OwnerID")]
    public int OwnerID { get; set; }

    [JsonPropertyName("OwnerName")]
    public string OwnerName { get; set; }

    [JsonPropertyName("OwnerEmail")]
    public string OwnerEmail { get; set; }

    [JsonPropertyName("AccessLevel")]
    public int AccessLevel { get; set; }

    [JsonPropertyName("DirectAccess")]
    public bool DirectAccess { get; set; }

    [JsonPropertyName("EndDate")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName("Zone1Name")]
    public string Zone1Name { get; set; }

    [JsonPropertyName("Zone2Name")]
    public string Zone2Name { get; set; }

    [JsonPropertyName("MinTemperature")]
    public int MinTemperature { get; set; }

    [JsonPropertyName("MaxTemperature")]
    public int MaxTemperature { get; set; }

    [JsonPropertyName("HideVaneControls")]
    public bool HideVaneControls { get; set; }

    [JsonPropertyName("HideDryModeControl")]
    public bool HideDryModeControl { get; set; }

    [JsonPropertyName("HideRoomTemperature")]
    public bool HideRoomTemperature { get; set; }

    [JsonPropertyName("HideSupplyTemperature")]
    public bool HideSupplyTemperature { get; set; }

    [JsonPropertyName("HideOutdoorTemperature")]
    public bool HideOutdoorTemperature { get; set; }

    [JsonPropertyName("EstimateAtaEnergyProductionOptIn")]
    public bool EstimateAtaEnergyProductionOptIn { get; set; }

    [JsonPropertyName("EstimateAtaEnergyProduction")]
    public bool EstimateAtaEnergyProduction { get; set; }

    [JsonPropertyName("BuildingCountry")]
    public string BuildingCountry { get; set; }

    [JsonPropertyName("OwnerCountry")]
    public string OwnerCountry { get; set; }

    [JsonPropertyName("AdaptorType")]
    public int AdaptorType { get; set; }

    [JsonPropertyName("LinkedDevice")]
    public object LinkedDevice { get; set; }

    [JsonPropertyName("Type")]
    public int Type { get; set; }

    [JsonPropertyName("MacAddress")]
    public string MacAddress { get; set; }

    [JsonPropertyName("SerialNumber")]
    public string SerialNumber { get; set; }

    [JsonPropertyName("Device")]
    public DeviceDetails DeviceDetails { get; set; }

    [JsonPropertyName("DiagnosticMode")]
    public int DiagnosticMode { get; set; }

    [JsonPropertyName("DiagnosticEndDate")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? DiagnosticEndDate { get; set; }

    [JsonPropertyName("Location")]
    public int Location { get; set; }

    [JsonPropertyName("DetectedCountry")]
    public string DetectedCountry { get; set; }

    [JsonPropertyName("Registrations")]
    public int Registrations { get; set; }

    [JsonPropertyName("LocalIPAddress")]
    public string LocalIPAddress { get; set; }

    [JsonPropertyName("TimeZone")]
    public int TimeZone { get; set; }

    [JsonPropertyName("RegistReason")]
    public string RegistReason { get; set; }

    [JsonPropertyName("ExpectedCommand")]
    public int ExpectedCommand { get; set; }

    [JsonPropertyName("RegistRetry")]
    public int RegistRetry { get; set; }

    [JsonPropertyName("DateCreated")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? DateCreated { get; set; }

    [JsonPropertyName("FirmwareDeployment")]
    public object FirmwareDeployment { get; set; }

    [JsonPropertyName("FirmwareUpdateAborted")]
    public bool FirmwareUpdateAborted { get; set; }

    [JsonPropertyName("Permissions")]
    public Permissions Permissions { get; set; }
}

public class Unit
{
    /// <summary>
    /// Gets or sets the unique identifier for the unit.
    /// </summary>
    [JsonPropertyName("ID")]
    public int ID { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the device associated with this unit.
    /// </summary>
    [JsonPropertyName("Device")]
    public int Device { get; set; }

    /// <summary>
    /// Gets or sets the serial number of the unit.
    /// </summary>
    [JsonPropertyName("SerialNumber")]
    public string SerialNumber { get; set; }

    /// <summary>
    /// Gets or sets the model number of the unit.
    /// </summary>
    [JsonPropertyName("ModelNumber")]
    public int? ModelNumber { get; set; }

    /// <summary>
    /// Gets or sets the model name or description of the unit.
    /// </summary>
    [JsonPropertyName("Model")]
    public string Model { get; set; }

    /// <summary>
    /// Gets or sets the type of the unit. The specific meanings of the values are not provided in the example.
    /// </summary>
    [JsonPropertyName("UnitType")]
    public int UnitType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the unit is located indoors.
    /// </summary>
    [JsonPropertyName("IsIndoor")]
    public bool IsIndoor { get; set; }
}

public class DeviceDetails
{
    /// <summary>
    /// Gets or sets the actual power consumption cycle of the device.
    /// </summary>
    [JsonPropertyName("PCycleActual")]
    public int PCycleActual { get; set; }

    /// <summary>
    /// Gets or sets any error messages reported by the device.
    /// </summary>
    [JsonPropertyName("ErrorMessages")]
    public string ErrorMessages { get; set; }

    /// <summary>
    /// Gets or sets the type of the device.
    /// </summary>
    [JsonPropertyName("DeviceType")]
    public int DeviceType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the device can cool the space.
    /// </summary>
    [JsonPropertyName("CanCool")]
    public bool CanCool { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the device can heat the space.
    /// </summary>
    [JsonPropertyName("CanHeat")]
    public bool CanHeat { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the device has a dry function to reduce humidity.
    /// </summary>
    [JsonPropertyName("CanDry")]
    public bool CanDry { get; set; }

    [JsonPropertyName("HasAutomaticFanSpeed")]
    public bool HasAutomaticFanSpeed { get; set; }

    [JsonPropertyName("AirDirectionFunction")]
    public bool AirDirectionFunction { get; set; }

    [JsonPropertyName("SwingFunction")]
    public bool SwingFunction { get; set; }

    /// <summary>
    /// Gets or sets the number of fan speeds supported by the device.
    /// </summary>
    [JsonPropertyName("NumberOfFanSpeeds")]
    public int NumberOfFanSpeeds { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the device is using temperature sensor A.
    /// </summary>
    [JsonPropertyName("UseTemperatureA")]
    public bool UseTemperatureA { get; set; }

    [JsonPropertyName("TemperatureIncrementOverride")]
    public double TemperatureIncrementOverride { get; set; }

    [JsonPropertyName("TemperatureIncrement")]
    public double TemperatureIncrement { get; set; }

    [JsonPropertyName("MinTempCoolDry")]
    public double MinTempCoolDry { get; set; }

    [JsonPropertyName("MaxTempCoolDry")]
    public double MaxTempCoolDry { get; set; }

    [JsonPropertyName("MinTempHeat")]
    public double MinTempHeat { get; set; }

    [JsonPropertyName("MaxTempHeat")]
    public double MaxTempHeat { get; set; }

    [JsonPropertyName("MinTempAutomatic")]
    public double MinTempAutomatic { get; set; }

    [JsonPropertyName("MaxTempAutomatic")]
    public double MaxTempAutomatic { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the device is a legacy device.
    /// </summary>
    [JsonPropertyName("LegacyDevice")]
    public bool LegacyDevice { get; set; }

    [JsonPropertyName("UnitSupportsStandbyMode")]
    public bool UnitSupportsStandbyMode { get; set; }

    [JsonPropertyName("IsSplitSystem")]
    public bool IsSplitSystem { get; set; }

    [JsonPropertyName("HasOutdoorTemperature")]
    public bool HasOutdoorTemperature { get; set; }

    [JsonPropertyName("ModelIsAirCurtain")]
    public bool ModelIsAirCurtain { get; set; }

    [JsonPropertyName("ModelSupportsFanSpeed")]
    public bool ModelSupportsFanSpeed { get; set; }

    [JsonPropertyName("ModelSupportsAuto")]
    public bool ModelSupportsAuto { get; set; }

    [JsonPropertyName("ModelSupportsHeat")]
    public bool ModelSupportsHeat { get; set; }

    [JsonPropertyName("ModelSupportsDry")]
    public bool ModelSupportsDry { get; set; }

    [JsonPropertyName("ModelSupportsVaneVertical")]
    public bool ModelSupportsVaneVertical { get; set; }

    [JsonPropertyName("ModelSupportsVaneHorizontal")]
    public bool ModelSupportsVaneHorizontal { get; set; }

    [JsonPropertyName("ModelSupportsWideVane")]
    public bool ModelSupportsWideVane { get; set; }

    [JsonPropertyName("ModelDisableEnergyReport")]
    public bool ModelDisableEnergyReport { get; set; }

    [JsonPropertyName("ModelSupportsStandbyMode")]
    public bool ModelSupportsStandbyMode { get; set; }

    [JsonPropertyName("ModelSupportsEnergyReporting")]
    public bool ModelSupportsEnergyReporting { get; set; }

    [JsonPropertyName("ProhibitSetTemperature")]
    public bool ProhibitSetTemperature { get; set; }

    [JsonPropertyName("ProhibitOperationMode")]
    public bool ProhibitOperationMode { get; set; }

    [JsonPropertyName("ProhibitPower")]
    public bool ProhibitPower { get; set; }

    /// <summary>
    /// Gets or sets the power status of the device (true for on, false for off).
    /// </summary>
    [JsonPropertyName("Power")]
    public bool Power { get; set; }

    /// <summary>
    /// Gets or sets the current room temperature as measured by the device.
    /// </summary>
    [JsonPropertyName("RoomTemperature")]
    public double RoomTemperature { get; set; }

    /// <summary>
    /// Gets or sets the current outdoor temperature as measured by the device or associated sensor.
    /// </summary>
    [JsonPropertyName("OutdoorTemperature")]
    public double? OutdoorTemperature { get; set; }

    [JsonPropertyName("ActualFanSpeed")]
    public int ActualFanSpeed { get; set; }

    [JsonPropertyName("FanSpeed")]
    public int FanSpeed { get; set; }

    [JsonPropertyName("AutomaticFanSpeed")]
    public bool AutomaticFanSpeed { get; set; }

    [JsonPropertyName("VaneVerticalDirection")]
    public int VaneVerticalDirection { get; set; }

    [JsonPropertyName("VaneVerticalSwing")]
    public bool VaneVerticalSwing { get; set; }

    [JsonPropertyName("VaneHorizontalDirection")]
    public int VaneHorizontalDirection { get; set; }

    [JsonPropertyName("VaneHorizontalSwing")]
    public bool VaneHorizontalSwing { get; set; }

    [JsonPropertyName("OperationMode")]
    public int OperationMode { get; set; }

    [JsonPropertyName("EffectiveFlags")]
    public int EffectiveFlags { get; set; }

    [JsonPropertyName("LastEffectiveFlags")]
    public int LastEffectiveFlags { get; set; }

    [JsonPropertyName("InStandbyMode")]
    public bool InStandbyMode { get; set; }

    [JsonPropertyName("DemandPercentage")]
    public int DemandPercentage { get; set; }

    /// <summary>
    /// Gets or sets the configured demand percentage for power consumption. Null if not configured.
    /// </summary>
    [JsonPropertyName("ConfiguredDemandPercentage")]
    public object ConfiguredDemandPercentage { get; set; }

    [JsonPropertyName("HasDemandSideControl")]
    public bool HasDemandSideControl { get; set; }

    [JsonPropertyName("DefaultCoolingSetTemperature")]
    public double DefaultCoolingSetTemperature { get; set; }

    [JsonPropertyName("DefaultHeatingSetTemperature")]
    public double DefaultHeatingSetTemperature { get; set; }

    [JsonPropertyName("RoomTemperatureLabel")]
    public int RoomTemperatureLabel { get; set; }

    [JsonPropertyName("HeatingEnergyConsumedRate1")]
    public int HeatingEnergyConsumedRate1 { get; set; }

    [JsonPropertyName("HeatingEnergyConsumedRate2")]
    public int HeatingEnergyConsumedRate2 { get; set; }

    [JsonPropertyName("CoolingEnergyConsumedRate1")]
    public int CoolingEnergyConsumedRate1 { get; set; }

    [JsonPropertyName("CoolingEnergyConsumedRate2")]
    public int CoolingEnergyConsumedRate2 { get; set; }

    [JsonPropertyName("AutoEnergyConsumedRate1")]
    public int AutoEnergyConsumedRate1 { get; set; }

    [JsonPropertyName("AutoEnergyConsumedRate2")]
    public int AutoEnergyConsumedRate2 { get; set; }

    [JsonPropertyName("DryEnergyConsumedRate1")]
    public int DryEnergyConsumedRate1 { get; set; }

    [JsonPropertyName("DryEnergyConsumedRate2")]
    public int DryEnergyConsumedRate2 { get; set; }

    [JsonPropertyName("FanEnergyConsumedRate1")]
    public int FanEnergyConsumedRate1 { get; set; }

    [JsonPropertyName("FanEnergyConsumedRate2")]
    public int FanEnergyConsumedRate2 { get; set; }

    [JsonPropertyName("OtherEnergyConsumedRate1")]
    public int OtherEnergyConsumedRate1 { get; set; }

    [JsonPropertyName("OtherEnergyConsumedRate2")]
    public int OtherEnergyConsumedRate2 { get; set; }

    [JsonPropertyName("EstimateAtaEnergyProduction")]
    public bool EstimateAtaEnergyProduction { get; set; }

    [JsonPropertyName("EstimateAtaEnergyProductionOptIn")]
    public bool EstimateAtaEnergyProductionOptIn { get; set; }

    [JsonPropertyName("EstimateAtaEnergyProductionOptInTimestamp")]
    public object EstimateAtaEnergyProductionOptInTimestamp { get; set; }

    [JsonPropertyName("WeatherForecast")]
    public List<WeatherForecast> WeatherForecast { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the device has an energy consumed meter.
    /// </summary>
    [JsonPropertyName("HasEnergyConsumedMeter")]
    public bool HasEnergyConsumedMeter { get; set; }

    /// <summary>
    /// Gets or sets the current energy consumed by the device.
    /// </summary>
    [JsonPropertyName("CurrentEnergyConsumed")]
    public int CurrentEnergyConsumed { get; set; }

    /// <summary>
    /// Gets or sets the current energy mode of the device.
    /// </summary>
    [JsonPropertyName("CurrentEnergyMode")]
    public int CurrentEnergyMode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether cooling is disabled on the device.
    /// </summary>
    [JsonPropertyName("CoolingDisabled")]
    public bool CoolingDisabled { get; set; }

    /// <summary>
    /// Gets or sets the model for energy correction. Null if not applicable.
    /// </summary>
    [JsonPropertyName("EnergyCorrectionModel")]
    public object EnergyCorrectionModel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether energy correction is active.
    /// </summary>
    [JsonPropertyName("EnergyCorrectionActive")]
    public bool EnergyCorrectionActive { get; set; }

    [JsonPropertyName("MinPcycle")]
    public int MinPcycle { get; set; }

    [JsonPropertyName("MaxPcycle")]
    public int MaxPcycle { get; set; }

    [JsonPropertyName("EffectivePCycle")]
    public int EffectivePCycle { get; set; }

    [JsonPropertyName("MaxOutdoorUnits")]
    public int MaxOutdoorUnits { get; set; }

    [JsonPropertyName("MaxIndoorUnits")]
    public int MaxIndoorUnits { get; set; }

    [JsonPropertyName("MaxTemperatureControlUnits")]
    public int MaxTemperatureControlUnits { get; set; }

    /// <summary>
    /// Gets or sets the model code of the device.
    /// </summary>
    [JsonPropertyName("ModelCode")]
    public string ModelCode { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the device.
    /// </summary>
    [JsonPropertyName("DeviceID")]
    public int DeviceID { get; set; }

    /// <summary>
    /// Gets or sets the MAC address of the device.
    /// </summary>
    [JsonPropertyName("MacAddress")]
    public string MacAddress { get; set; }

    [JsonPropertyName("SerialNumber")]
    public string SerialNumber { get; set; }

    [JsonPropertyName("TimeZoneID")]
    public int TimeZoneID { get; set; }

    [JsonPropertyName("DiagnosticMode")]
    public int DiagnosticMode { get; set; }

    [JsonPropertyName("DiagnosticEndDate")]
    public object DiagnosticEndDate { get; set; }

    [JsonPropertyName("ExpectedCommand")]
    public int ExpectedCommand { get; set; }

    [JsonPropertyName("Owner")]
    public int Owner { get; set; }

    [JsonPropertyName("DetectedCountry")]
    public object DetectedCountry { get; set; }

    [JsonPropertyName("AdaptorType")]
    public int AdaptorType { get; set; }

    [JsonPropertyName("FirmwareDeployment")]
    public object FirmwareDeployment { get; set; }

    [JsonPropertyName("FirmwareUpdateAborted")]
    public bool FirmwareUpdateAborted { get; set; }

    [JsonPropertyName("LinkedDevice")]
    public object LinkedDevice { get; set; }

    /// <summary>
    /// Gets or sets the WiFi signal strength of the device.
    /// </summary>
    [JsonPropertyName("WifiSignalStrength")]
    public int WifiSignalStrength { get; set; }

    /// <summary>
    /// Gets or sets the status of the WiFi adapter in the device.
    /// </summary>
    [JsonPropertyName("WifiAdapterStatus")]
    public string WifiAdapterStatus { get; set; }

    /// <summary>
    /// Gets or sets the physical or logical position of the device.
    /// </summary>
    [JsonPropertyName("Position")]
    public string Position { get; set; }

    /// <summary>
    /// Gets or sets the power consumption cycle setting of the device.
    /// </summary>
    [JsonPropertyName("PCycle")]
    public int PCycle { get; set; }

    /// <summary>
    /// Gets or sets the configured power consumption cycle. Null if not configured.
    /// </summary>
    [JsonPropertyName("PCycleConfigured")]
    public object PCycleConfigured { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of records the device can hold.
    /// </summary>
    [JsonPropertyName("RecordNumMax")]
    public int RecordNumMax { get; set; }

    [JsonPropertyName("LastTimeStamp")]
    public string LastTimeStamp { get; set; }

    [JsonPropertyName("ErrorCode")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("HasError")]
    public bool HasError { get; set; }

    [JsonPropertyName("LastReset")]
    public string LastReset { get; set; }

    [JsonPropertyName("FlashWrites")]
    public int FlashWrites { get; set; }

    [JsonPropertyName("Scene")]
    public object Scene { get; set; }

    [JsonPropertyName("SSLExpirationDate")]
    public string SSLExpirationDate { get; set; }

    [JsonPropertyName("SPTimeout")]
    public int SPTimeout { get; set; }

    [JsonPropertyName("Passcode")]
    public object Passcode { get; set; }

    [JsonPropertyName("ServerCommunicationDisabled")]
    public bool ServerCommunicationDisabled { get; set; }

    [JsonPropertyName("ConsecutiveUploadErrors")]
    public int ConsecutiveUploadErrors { get; set; }

    [JsonPropertyName("DoNotRespondAfter")]
    public object DoNotRespondAfter { get; set; }

    [JsonPropertyName("OwnerRoleAccessLevel")]
    public int OwnerRoleAccessLevel { get; set; }

    [JsonPropertyName("OwnerCountry")]
    public int OwnerCountry { get; set; }

    [JsonPropertyName("HideEnergyReport")]
    public bool HideEnergyReport { get; set; }

    [JsonPropertyName("ExceptionHash")]
    public object ExceptionHash { get; set; }

    [JsonPropertyName("ExceptionDate")]
    public object ExceptionDate { get; set; }

    [JsonPropertyName("ExceptionCount")]
    public object ExceptionCount { get; set; }

    [JsonPropertyName("Rate1StartTime")]
    public object Rate1StartTime { get; set; }

    [JsonPropertyName("Rate2StartTime")]
    public object Rate2StartTime { get; set; }

    [JsonPropertyName("ProtocolVersion")]
    public int ProtocolVersion { get; set; }

    [JsonPropertyName("UnitVersion")]
    public int UnitVersion { get; set; }

    [JsonPropertyName("FirmwareAppVersion")]
    public int FirmwareAppVersion { get; set; }

    [JsonPropertyName("FirmwareWebVersion")]
    public int FirmwareWebVersion { get; set; }

    [JsonPropertyName("FirmwareWlanVersion")]
    public int FirmwareWlanVersion { get; set; }

    [JsonPropertyName("MqttFlags")]
    public int MqttFlags { get; set; }

    [JsonPropertyName("HasErrorMessages")]
    public bool HasErrorMessages { get; set; }

    [JsonPropertyName("HasZone2")]
    public bool HasZone2 { get; set; }

    [JsonPropertyName("Offline")]
    public bool Offline { get; set; }

    [JsonPropertyName("SupportsHourlyEnergyReport")]
    public bool SupportsHourlyEnergyReport { get; set; }

    [JsonPropertyName("Units")]
    public List<Unit> Units { get; set; }
}

public class Permissions
{
    /// <summary>
    /// Gets or sets a value indicating whether the user has permission to set the operation mode of the device.
    /// </summary>
    [JsonPropertyName("CanSetOperationMode")]
    public bool CanSetOperationMode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has permission to set the fan speed of the device.
    /// </summary>
    [JsonPropertyName("CanSetFanSpeed")]
    public bool CanSetFanSpeed { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has permission to set the vane direction of the device.
    /// </summary>
    [JsonPropertyName("CanSetVaneDirection")]
    public bool CanSetVaneDirection { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has permission to turn the device on or off.
    /// </summary>
    [JsonPropertyName("CanSetPower")]
    public bool CanSetPower { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has permission to override the temperature increment settings on the device.
    /// </summary>
    [JsonPropertyName("CanSetTemperatureIncrementOverride")]
    public bool CanSetTemperatureIncrementOverride { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has permission to disable the local controller of the device.
    /// </summary>
    [JsonPropertyName("CanDisableLocalController")]
    public bool CanDisableLocalController { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user has permission to set the demand side control of the device.
    /// </summary>
    [JsonPropertyName("CanSetDemandSideControl")]
    public bool CanSetDemandSideControl { get; set; }
}

public class WeatherForecast
{
    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName("Date")]
    public DateTime? Date { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName("Sunrise")]
    public DateTime? Sunrise { get; set; }

    [JsonConverter(typeof(DateTimeConverter))]
    [JsonPropertyName("Sunset")]
    public DateTime? Sunset { get; set; }

    [JsonPropertyName("Condition")]
    public int? Condition { get; set; }

    [JsonPropertyName("ID")]
    public float? ID { get; set; }

    [JsonPropertyName("Humidity")]
    public int? Humidity { get; set; }

    [JsonPropertyName("Temperature")]
    public int? Temperature { get; set; }

    [JsonPropertyName("Icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("ConditionName")]
    public string? ConditionName { get; set; }

    [JsonPropertyName("Day")]
    public int? Day { get; set; }

    [JsonPropertyName("WeatherType")]
    public int? WeatherType { get; set; }
}