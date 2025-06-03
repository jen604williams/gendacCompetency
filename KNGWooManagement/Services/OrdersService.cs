
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

    // public Task<List<Item>> GetPageOfOrders(int page)
    // {
    //     // if (_allOrders == null)
    //     // {
    //     //     //TODO: check about the configure await and whether it is worth adding
    //     //     try
    //     //     {
    //     //         var orders = GetAllOrders().ConfigureAwait(false);
    //     //         if (orders.ToString().Contains("Error"))
    //     //         {
    //     //             return null;
    //     //         }
    //     //         _allOrders = JsonSerializer.Deserialize<List<Item>>(orders.ToString());
    //     //     }
    //     //     catch (Exception ex)
    //     //     {

    //     //     }
    //     // }
    //     // //page starts at 0
    //     // int startItem = page * _numberOfItemsOnPage;
    //     // return (Task<List<Item>?>)_allOrders.Skip(startItem).Take(_numberOfItemsOnPage);
    //     var orders = GetAllOrders().ToString;
    //     return orders;
    // }

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

        public Task<string> AddOrder()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/orders/");
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