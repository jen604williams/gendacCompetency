using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNGWooManagement.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly OrdersService _ordersService;
    public string AllOrders { get; private set; }
    

    public List<Item> Orders { get; private set; }

    public IndexModel(OrdersService ordersService, ILogger<IndexModel> logger)
    {
        _ordersService = ordersService;
        _logger = logger;
        AllOrders = string.Empty;
        Orders = new List<Item>();
    }

    public async Task OnGetAsync()
    {
        try
        {
            AllOrders = await _ordersService.GetAllOrders();
            Orders = JsonSerializer.Deserialize<List<Item>>(AllOrders) ?? new List<Item>();
        }
        catch
        {
            _logger.LogInformation("Deserialisation of orders failed");
        }
    } 
}
