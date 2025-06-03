using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
public enum OrderStatusOptions
{
    [Display(Name = "None")]
    None = 0,
    [Display(Name = "Pending Payment")]
    PendingPayment = 1,
    [Display(Name = "Paid")]
    Paid = 2,
    [Display(Name = "Shipped")]
    Shipped = 3,
    [Display(Name = "Delivered")]
    Delivered = 4
}

