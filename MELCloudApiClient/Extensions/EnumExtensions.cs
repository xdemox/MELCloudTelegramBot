using System.ComponentModel;
namespace MELCloudApiClient.Extensions;
public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false).SingleOrDefault() as DescriptionAttribute;
        return attribute?.Description ?? value.ToString();
    }
}