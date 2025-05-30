using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KNGWooManagement.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> ?_logger;
    private readonly OrdersService _ordersService;
    public string ?Order1 { get; private set; }

    public string ?AllOrders { get; private set; }

    public IndexModel(OrdersService ordersService, ILogger<IndexModel> logger)
    {
        _ordersService = ordersService;
        _logger = logger;
    }

    public async Task OnGetAsync()
    {
        Order1 = await _ordersService.GetOrder(1);
        AllOrders = await _ordersService.GetAllOrders();
    } 
}
