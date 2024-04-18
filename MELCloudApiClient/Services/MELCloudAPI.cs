using MELCloudApiClient.Interfaces;
using Refit;
using System.Text;
using System.Text.Json;

namespace MELCloudApiClient.Services;
public class MELCloudAPI
{
    private readonly HttpClient _httpClient;
    private readonly IMELCloudAPI _apiClient;

    public MELCloudAPI()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://app.melcloud.com") };
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/118.0.0.0 Safari/537.36");
        _httpClient.DefaultRequestHeaders.Accept.ParseAdd("application/json, text/javascript, */*; q=0.01");
        _httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
        _httpClient.DefaultRequestHeaders.Add("Cookie", "policyaccepted=true");

        _apiClient = RestService.For<IMELCloudAPI>(_httpClient);
    }

    public async Task<Models.ClientLogin> ClientLoginAsync(Models.ClientLoginRequest request)
    {
        var url = "https://app.melcloud.com/Mitsubishi.Wifi.Client/Login/ClientLogin";

        var jsonContent = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        try
        {
            var response = await _httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var location = JsonSerializer.Deserialize<Models.ClientLogin>(responseBody);
            return location;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"HttpRequest Exception: {e.Message}");
            return null;
        }
    }

    public async Task<List<Models.ListDevices>> GetListDevicesAsync(string contextKey)
    {
        try
        {
            var result = await _apiClient.GetListDevicesAsync(contextKey);
            return result ?? throw new ArgumentNullException(nameof(result));
        }
        catch (ApiException apiEx)
        {
            // Log the response content to see the error details from the server
            Console.WriteLine($"API Error: {apiEx.StatusCode}");
            Console.WriteLine(await apiEx.GetContentAsAsync<string>());
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

    public async Task<Models.DevicePreview> GetDeviceAsync(string contextKey, string device, string building)
    {
        try
        {
            var result = await _apiClient.GetDeviceAsync(contextKey, device, building);
            return result ?? throw new ArgumentNullException(nameof(result));
        }
        catch (ApiException apiEx)
        {
            // Log the response content to see the error details from the server
            Console.WriteLine($"API Error: {apiEx.StatusCode}");
            Console.WriteLine(await apiEx.GetContentAsAsync<string>());
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

    public async Task<Models.DevicePreview> SetAttributeAsync(string contextKey, Models.DevicePreview device)
    {
        try
        {
            var result = await _apiClient.SetAttributeAsync(contextKey, device);
            return result ?? throw new ArgumentNullException(nameof(result));
        }
        catch (ApiException apiEx)
        {
            // Log the response content to see the error details from the server
            Console.WriteLine($"API Error: {apiEx.StatusCode}");
            Console.WriteLine(await apiEx.GetContentAsAsync<string>());
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
    public async Task<Models.Reports.TemperatureLog> GetTemperatureReport(string contextKey, Models.Reports.TemperatureLogRequest request)
    {
        try
        {
            var result = await _apiClient.GetTemperatureReport(contextKey, request);
            return result ?? throw new ArgumentNullException(nameof(result));
        }
        catch (ApiException apiEx)
        {
            // Log the response content to see the error details from the server
            Console.WriteLine($"API Error: {apiEx.StatusCode}");
            Console.WriteLine(await apiEx.GetContentAsAsync<string>());
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}