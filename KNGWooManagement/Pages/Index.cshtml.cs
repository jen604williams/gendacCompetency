using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ItemDataModel;

namespace KNGWooManagement.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly OrdersService _ordersService;

    public readonly int _numberOfItemsPerPage = 50;

    private int _pageNumber = 1;
    public string OrdersPerPage { get; private set; } = string.Empty;
    public string AllOrdersJson { get; private set; } = string.Empty;
    [BindProperty]
    public string SearchQuery { get; set; } = string.Empty;
    public List<Item> Orders { get; private set; } = new();
    public List<Item> AllOrders { get; private set; } = new();

    [BindProperty]
    public Item NewOrder { get; set; } = new();

    public List<OrderStats> OrderStats { get; private set; } = new();

    public IndexModel(OrdersService ordersService, ILogger<IndexModel> logger)
    {
        _ordersService = ordersService;
        _logger = logger;
    }

    public async Task OnGetAsync()
    {
        try
        {
            _logger.LogInformation("OnGetAsync");
            //TODO: update the stats to be the actual count: 
            OrderStats = new List<OrderStats>
            {
                new OrderStats { Icon = "Σ", Count = 131, Label = "Total Orders" },
                new OrderStats { Icon = "💳", Count = 45, Label = "Pending Payment" },
                new OrderStats { Icon = "💰", Count = 86, Label = "Paid" },
                new OrderStats { Icon = "🚚", Count = 65, Label = "Shipped" },
                new OrderStats { Icon = "📦", Count = 48, Label = "Delivered" }
            };
            OrdersPerPage = await _ordersService.GetPageOfOrders(_pageNumber, _numberOfItemsPerPage);
            Orders = JsonSerializer.Deserialize<List<Item>>(OrdersPerPage) ?? new List<Item>();
        }
        catch
        {
            _logger.LogInformation("Deserialisation of orders failed");
        }
    }

    public void OnPostSearch(string searchQuery)
    {
        _logger.LogInformation("OnPostSearch for: {@searchQuery}", searchQuery);
        GetAllOrders();
        SearchQuery = searchQuery;
        Orders = AllOrders
            .Where(o => o.BuyerName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
            .ToList();
            //TODO: show list of options found.
        RedirectToPage();
    }

    public async Task<IActionResult> OnPostSubmit()
    {
        // if (!ModelState.IsValid)
        // {
        //     return Page();
        // }
        //TODO: Add validation on date - cannot be before today
        var jsonContent = JsonSerializer.Serialize(NewOrder);
        _logger.LogInformation("OnPost for: {@jsonContent}", jsonContent);
        var response = await _ordersService.AddOrder(jsonContent);
        _logger.LogInformation("OnPost response: {@response}", response);
        if (response.Contains("Error"))
        {
            ModelState.AddModelError("", "Failed to submit order.");
            return Page();
        }
        else
        {
            return RedirectToPage();
        }
}
    private async void GetAllOrders()
    {
        _logger.LogInformation("GetAllOrders");
        AllOrdersJson = await _ordersService.GetAllOrders();
        AllOrders = JsonSerializer.Deserialize<List<Item>>(AllOrdersJson) ?? new List<Item>();
    }
}
