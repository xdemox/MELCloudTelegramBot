using System.ComponentModel;
namespace MELCloudApiClient.Enums;
public enum HorizontalVanePosition
{
    [Description("Automatically adjusts the vane position.")]
    Auto = 0,               // Auto button

    [Description("Swings the vane up and down.")]
    Swing = 1,              // Swing button

    [Description("Almost vertical in the opposite direction.")]
    LeftMost = 2,     // -75° or -80°

    [Description("A steeper angle from the horizontal in the opposite direction.")]
    SecondFromLeft = 3,   // ~-60°

    [Description("Directs airflow straight out.")]
    Middle = 4,         // 0°

    [Description("A steep angle downward.")]
    FourthFromLeft = 5,    // ~60°

    [Description("Almost vertical directing airflow downward.")]
    RightMost = 6      // ~75° or 80°
}