using System.Text.Json.Serialization;
namespace ItemDataModel
{
    public class Item
    {
        [JsonPropertyName("orderNumber")]
        public int OrderNumber { get; set; }
        [JsonPropertyName("buyerName")]
        public string BuyerName { get; set; } = "";
        [JsonPropertyName("price")]
        public double Price { get; set; }
        [JsonPropertyName("eta")]
        public DateTime Eta { get; set; }
        [JsonPropertyName("orderStatus")]
        public OrderStatusOptions OrderStatus { get; set; }

    }
}