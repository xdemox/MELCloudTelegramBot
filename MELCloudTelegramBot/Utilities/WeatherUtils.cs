namespace MELCloudTelegramBot.Utilities;
public static class WeatherUtils
{
    public static string GetWeatherEmoji(string conditionName)
    {
        switch (conditionName.ToLower())
        {
            case "clear/sunny":
                return "☀️";
            case "partly cloudy":
                return "⛅";
            case "cloudy":
                return "☁️";
            case "rain":
            case "Light rain shower":
                return "🌧️";
            case "thunderstorm":
                return "⛈️";
            case "snow":
                return "❄️";
            case "mist":
            case "fog":
                return "🌫️";
            default:
                return "🌈"; // default emoji for unknown conditions
        }
    }
}