using System.Text.Json.Serialization;
using System.Text.Json;

namespace MELCloudApiClient.Converters;
public class DateTimeConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (DateTime.TryParse(reader.GetString(), out DateTime result))
            {
                return result;
            }
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
        {
            if (value.Value.Year < 0001 || value.Value.Year > 9999)
            {
                // Handle out-of-range dates by converting to DateTimeOffset
                var dateTimeOffset = new DateTimeOffset(value.Value);
                writer.WriteStringValue(dateTimeOffset.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
            }
            else
            {
                writer.WriteStringValue(value.Value.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
            }
        }
        else
        {
            writer.WriteNullValue();
        }
    }

}