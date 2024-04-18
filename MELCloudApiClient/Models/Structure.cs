using System.Text.Json.Serialization;

namespace MELCloudApiClient.Models;
public class Structure
{
    [JsonPropertyName(nameof(Floors))]
    public List<object> Floors { get; set; }

    [JsonPropertyName(nameof(Areas))]
    public List<object> Areas { get; set; }

    [JsonPropertyName(nameof(Devices))]
    public List<Device> Devices { get; set; }

    [JsonPropertyName(nameof(Clients))]
    public List<object> Clients { get; set; }
}