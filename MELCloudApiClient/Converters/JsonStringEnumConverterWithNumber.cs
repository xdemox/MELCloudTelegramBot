using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MELCloudApiClient.Converters;
public class JsonStringEnumConverterWithNumberSupport : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsEnum;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        return (JsonConverter)Activator.CreateInstance(
            typeof(JsonStringEnumConverterWithNumberSupportInner<>).MakeGenericType(new Type[] { typeToConvert }),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: null,
            culture: null)!;
    }

    private class JsonStringEnumConverterWithNumberSupportInner<TEnum> : JsonConverter<TEnum> where TEnum : Enum
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                var number = reader.GetInt32();
                if (Enum.IsDefined(typeof(TEnum), number))
                {
                    return (TEnum)Enum.ToObject(typeof(TEnum), number);
                }
            }
            throw new JsonException($"Unable to convert the JSON number {reader.GetInt32()} to an enum of type {typeof(TEnum)}.");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(Convert.ToInt32(value));
        }
    }
}