using Refit;

namespace MELCloudApiClient.Interfaces;
public interface IMELCloudAPI
{
    [Post("/Mitsubishi.Wifi.Client/Login/ClientLogin")]
    Task<Models.ClientLogin> ClientLoginAsync([Body] Models.ClientLoginRequest request);

    [Get("/Mitsubishi.Wifi.Client/User/ListDevices")]
    Task<List<Models.ListDevices>> GetListDevicesAsync([Header("X-MitsContextKey")] string contextKey, int language = 0);

    [Post("/Mitsubishi.Wifi.Client/Device/SetAta")]
    Task<Models.DevicePreview> SetAttributeAsync([Header("X-MitsContextKey")] string contextKey, [Body] Models.DevicePreview request);

    [Get("/Mitsubishi.Wifi.Client/Device/Get?id={device}&buildingID={building}")]
    Task<Models.DevicePreview> GetDeviceAsync([Header("X-MitsContextKey")] string contextKey, [AliasAs("device")] string device, [AliasAs("building")] string building);
    
    [Post("/Mitsubishi.Wifi.Client/Report/GetTemperatureLog2")]
    Task<Models.Reports.TemperatureLog> GetTemperatureReport([Header("X-MitsContextKey")] string contextKey, [Body] Models.Reports.TemperatureLogRequest request);
}