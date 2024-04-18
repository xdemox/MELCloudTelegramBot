using MELCloudApiClient.Converters;
using System.Text.Json.Serialization;

namespace MELCloudApiClient.Models;

public class ListDevices
{
    [JsonPropertyName(nameof(ID))]
    public int ID { get; set; }

    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; }

    [JsonPropertyName(nameof(AddressLine1))]
    public string AddressLine1 { get; set; }

    [JsonPropertyName(nameof(AddressLine2))]
    public string? AddressLine2 { get; set; }

    [JsonPropertyName(nameof(City))]
    public string City { get; set; }

    [JsonPropertyName(nameof(Postcode))]
    public string Postcode { get; set; }

    [JsonPropertyName(nameof(Latitude))]
    public double Latitude { get; set; }

    [JsonPropertyName(nameof(Longitude))]
    public double Longitude { get; set; }

    [JsonPropertyName(nameof(District))]
    public string? District { get; set; }

    [JsonPropertyName(nameof(FPDefined))]
    public bool FPDefined { get; set; }

    [JsonPropertyName(nameof(FPEnabled))]
    public bool FPEnabled { get; set; }

    [JsonPropertyName(nameof(FPMinTemperature))]
    public int FPMinTemperature { get; set; }

    [JsonPropertyName(nameof(FPMaxTemperature))]
    public int FPMaxTemperature { get; set; }

    [JsonPropertyName(nameof(HMDefined))]
    public bool HMDefined { get; set; }

    [JsonPropertyName(nameof(HMEnabled))]
    public bool HMEnabled { get; set; }

    [JsonPropertyName(nameof(HMStartDate))]
    public string? HMStartDate { get; set; }

    [JsonPropertyName(nameof(HMEndDate))]
    public string? HMEndDate { get; set; }

    [JsonPropertyName(nameof(BuildingType))]
    public int BuildingType { get; set; }

    [JsonPropertyName(nameof(PropertyType))]
    public int PropertyType { get; set; }

    [JsonPropertyName(nameof(DateBuilt))]
    public string? DateBuilt { get; set; }

    [JsonPropertyName(nameof(HasGasSupply))]
    public bool HasGasSupply { get; set; }

    [JsonPropertyName(nameof(LocationLookupDate))]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? LocationLookupDate { get; set; }

    [JsonPropertyName(nameof(Country))]
    public int Country { get; set; }

    [JsonPropertyName(nameof(TimeZoneContinent))]
    public int TimeZoneContinent { get; set; }

    [JsonPropertyName(nameof(TimeZoneCity))]
    public int TimeZoneCity { get; set; }

    [JsonPropertyName(nameof(TimeZone))]
    public int TimeZone { get; set; }

    [JsonPropertyName(nameof(Location))]
    public int Location { get; set; }

    [JsonPropertyName(nameof(CoolingDisabled))]
    public bool CoolingDisabled { get; set; }

    [JsonPropertyName(nameof(Expanded))]
    public bool Expanded { get; set; }

    [JsonPropertyName(nameof(Structure))]
    public Structure Structure { get; set; }

    [JsonPropertyName(nameof(AccessLevel))]
    public int AccessLevel { get; set; }

    [JsonPropertyName(nameof(DirectAccess))]
    public bool DirectAccess { get; set; }

    [JsonPropertyName(nameof(MinTemperature))]
    public int MinTemperature { get; set; }

    [JsonPropertyName(nameof(MaxTemperature))]
    public int MaxTemperature { get; set; }

    [JsonPropertyName(nameof(Owner))]
    public string? Owner { get; set; }

    [JsonPropertyName(nameof(EndDate))]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? EndDate { get; set; }

    [JsonPropertyName(nameof(iDateBuilt))]
    public string? iDateBuilt { get; set; }

    [JsonPropertyName(nameof(QuantizedCoordinates))]
    public QuantizedCoordinates? QuantizedCoordinates { get; set; }
}