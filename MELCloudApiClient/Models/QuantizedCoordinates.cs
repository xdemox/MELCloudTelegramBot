using System.Text.Json.Serialization;

namespace MELCloudApiClient.Models;
public class QuantizedCoordinates
{
    [JsonPropertyName(nameof(Latitude))]
    public double? Latitude { get; set; }

    [JsonPropertyName(nameof(Longitude))]
    public double? Longitude { get; set; }
}