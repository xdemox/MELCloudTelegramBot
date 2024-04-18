using MELCloudApiClient.Models.Reports;
using System.Drawing;

namespace MELCloudTelegramBot.Utilities;
public static class ReportsUtils
{
    public static MemoryStream GenerateImageReportForTemperature(TemperatureLog temperatureData)
    {
        var plt = new ScottPlot.Plot(800, 400);

        double[] days = Array.ConvertAll(temperatureData.Labels.ToArray(), Double.Parse);
        var colors = new List<Color> { Color.Blue, Color.Green, Color.Black };
        var labels = new List<string> { "Set Room Temperature", "Room Temperature", "Weather Forecast" };

        for (int i = 0; i < temperatureData.Data.Length; i++)
        {
            plt.AddScatter(days, temperatureData.Data[i].ToArray(), label: labels[i], color: colors[i]);
        }
        // Get current limits
        var limits = plt.GetAxisLimits();

        plt.Title("Monthly Temperature");
        plt.XLabel("Day of the Month");
        plt.YLabel("Temperature (°C)");
        // Set the X and Y axis limits
        plt.SetAxisLimits(xMin: 1, xMax: 31, yMin: 10, yMax: 40);

        // Legend
        plt.Legend(location: ScottPlot.Alignment.UpperLeft);

        return ConvertBitmapToMemoryStream(plt.Render());
    }

    private static MemoryStream ConvertBitmapToMemoryStream(Bitmap bmp)
    {
        MemoryStream memoryStream = new MemoryStream();

        bmp.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);

        return memoryStream;
    }
}