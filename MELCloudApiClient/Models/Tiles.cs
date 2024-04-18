using MELCloudApiClient.Converters;
using System.Text.Json.Serialization;

namespace MELCloudApiClient.Models;

public class Tiles
{
    [JsonPropertyName("Tiles")]
    public List<Tiles>? TilesList { get; set; }

    [JsonPropertyName("SelectedDevice")]
    public Device? SelectedDevice { get; set; }
}
public class Tile
{
    [JsonPropertyName("Device")]
    public long Device { get; set; }

    [JsonPropertyName("Power")]
    public bool Power { get; set; }

    [JsonPropertyName("RoomTemperature")]
    public double? RoomTemperature { get; set; }

    [JsonPropertyName("RoomTemperature2")]
    public double? RoomTemperature2 { get; set; }

    [JsonPropertyName("ErrorCode")]
    public int? ErrorCode { get; set; }

    [JsonPropertyName("LastCommunication")]
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? LastCommunication { get; set; }

    [JsonPropertyName("Offline")]
    public bool Offline { get; set; }

    [JsonPropertyName("TankWaterTemperature")]
    public double? TankWaterTemperature { get; set; }

    [JsonPropertyName("Scene")]
    public object Scene { get; set; }

    [JsonPropertyName("SceneOwner")]
    public object SceneOwner { get; set; }
}

public class TileRequest
{
    [JsonPropertyName("DeviceIDs")]
    public List<long> DeviceIDs { get; set; }

    [JsonPropertyName("SelectedDevice")]
    public long? SelectedDevice { get; set; }

    [JsonPropertyName("SelectedBuilding")]
    public long? SelectedBuilding { get; set; }
}
