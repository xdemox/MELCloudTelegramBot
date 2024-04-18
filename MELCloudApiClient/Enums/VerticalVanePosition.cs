using System.ComponentModel;
namespace MELCloudApiClient.Enums;

public enum VerticalVanePosition
{
    [Description("Automatically adjusts the vane position.")]
    Auto = 0,               // Auto button

    [Description("Swings the vane up and down.")]
    Swing = 1,              // Swing button

    [Description("Directs airflow straight out or slightly to the side.")]
    StraightOut = 2,    // 0°

    [Description("Slight angle to the side.")]
    SlightAngle = 3,   // ~15°

    [Description("Moderate angle to the side.")]
    ModerateAngle = 4, // ~30°

    [Description("Directs airflow diagonally to the side.")]
    Diagonal = 5,      // ~45°

    [Description("Steeper angle to the side.")]
    SteepAngle = 6,    // ~60°

    [Description("Almost vertically to the side.")]
    NearlyVertical = 7, // ~75°

    [Description("Completely vertical, directing airflow straight down or to the side.")]
    Vertical = 8       // ~90°
}
