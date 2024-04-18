using MELCloudApiClient.Converters;
using System.Text.Json.Serialization;

namespace MELCloudApiClient.Models.Reports
{
    /// <summary>
    /// Represents a dataset for plotting temperature data.
    /// </summary>
    public class TemperatureLog
    {
        /// <summary>
        /// Gets or sets the labels for the temperature data points.
        /// </summary>
        [JsonPropertyName("Labels")]
        public string[] Labels { get; set; }

        /// <summary>
        /// Gets or sets the temperature data values.
        /// </summary>
        [JsonPropertyName("Data")]
        public double[][] Data { get; set; }

        /// <summary>
        /// Gets or sets the label type.
        /// </summary>
        [JsonPropertyName("LabelType")]
        public int LabelType { get; set; }

        /// <summary>
        /// Gets or sets the start date of the data range.
        /// </summary>
        [JsonPropertyName("FromDate")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the data range.
        /// </summary>
        [JsonPropertyName("ToDate")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the series number.
        /// </summary>
        [JsonPropertyName("Series")]
        public int Series { get; set; }

        /// <summary>
        /// Gets or sets the number of data points.
        /// </summary>
        [JsonPropertyName("Points")]
        public int Points { get; set; }
    }

    public class TemperatureLogRequest
    {
        [JsonPropertyName("DeviceID")]
        public required int DeviceID { get; set; }

        /// <summary>
        /// Gets or sets the temperature data values.
        /// </summary>
        [JsonPropertyName("Data")]
        public required int Duration { get; set; }

        /// <summary>
        /// Gets or sets the start date of the data range.
        /// </summary>
        [JsonPropertyName("FromDate")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the data range.
        /// </summary>
        [JsonPropertyName("ToDate")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? ToDate { get; set; }

        [JsonPropertyName("Location")]
        public string Location { get; set; }
    }
}
