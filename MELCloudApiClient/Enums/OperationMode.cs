using System.ComponentModel;

namespace MELCloudApiClient.Enums;
public enum OperationMode
{
    [Description("Cooling")]
    Cooling = 3,

    [Description("Drying")]
    Drying = 2,

    [Description("Fan")]
    Fan = 7,

    [Description("Auto")]
    Auto = 8,

    [Description("Heating")]
    Heating = 1
}