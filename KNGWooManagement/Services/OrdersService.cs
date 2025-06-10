
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class OrdersService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
  

    public OrdersService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        _apiKey = configuration["ApiSettings:ApiKey"] ?? "";
    }

    public Task<string> GetPageOfOrders(int pageNumber, int pageSize)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/orders?pageNumber={pageNumber}&pageSize={pageSize}");
        return GetDataAsync(request);
    }

    public Task<string> GetAllOrders()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/orders");
        return GetDataAsync(request);
    }
    
        public Task<string> GetOrder(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"/api/orders/{id}");
        return GetDataAsync(request);
    }

        public Task<string> DeleteOrder(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/orders/{id}");
        return GetDataAsync(request);
    }

            public Task<string> UpdateOrder(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"/api/orders/{id}");
        return GetDataAsync(request);
    }

        public Task<string> UpdateStatusOfOrder(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Patch, $"/api/orders/{id}");
        return GetDataAsync(request);
    }

        public Task<string> AddOrder(string order)
    {
        var content = new StringContent(order, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/orders/")
        {
            Content = content
        };
        return GetDataAsync(request);
    }

    private async Task<string> GetDataAsync(HttpRequestMessage request)
    {

        request.Headers.Add("x-api-key", _apiKey);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        return $"Error: {response.StatusCode}";
    }
}