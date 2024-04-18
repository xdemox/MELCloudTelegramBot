using System.Text;
namespace MELCloudApiClient;
public class Client
{
    static Dictionary<string, string> GetHeaders(string token)
    {
        return new Dictionary<string, string>
        {
            ["User-Agent"] = "Mozilla/5.0 (X11; Linux x86_64; rv:73.0) Gecko/20100101 Firefox/73.0",
            ["Accept"] = "application/json, text/javascript, */*; q=0.01",
            ["Accept-Language"] = "en-US,en;q=0.5",
            ["Accept-Encoding"] = "gzip, deflate, br",
            ["X-MitsContextKey"] = token,
            ["X-Requested-With"] = "XMLHttpRequest",
            ["Cookie"] = "policyaccepted=true",
        };
    }
    public async Task SendRequest()
    {
        var url = "https://app.melcloud.com/Mitsubishi.Wifi.Client/Support/List?language=0";

        using var httpClient = new HttpClient();
        var token = "your_token_here";
        var headers = GetHeaders(token);
        // Set up the headers
        foreach (var header in headers)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }

        try
        {
            // Send the GET request
            var response = await httpClient.GetAsync(url);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read and output the response content
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
    }

    public async Task GetList()
    {
        var url = "https://app.melcloud.com/Mitsubishi.Wifi.Client/Support/List?language=0";

        using var httpClient = new HttpClient();
        var token = "your_token_here";
        var headers = GetHeaders(token);
        // Set up the headers
        foreach (var header in headers)
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }

        try
        {
            // Send the GET request
            var response = await httpClient.GetAsync(url);

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read and output the response content
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
    }

    public async Task<string> SendLoginRequest(string jsonContent)
    {
        var url = "https://app.melcloud.com/Mitsubishi.Wifi.Client/Login/ClientLogin";

        using var httpClient = new HttpClient();

        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(url, httpContent);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (HttpRequestException e)
        {
            return e.Message;
        }
    }
}