public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int ClientId { get; set; }
    public decimal DeliveryPrice { get; set; }
    public string DeliveryStatus { get; set; }

    public Order(int orderId, DateTime orderDate, int clientId, decimal deliveryPrice, string deliveryStatus)
    {
        OrderId = orderId;
        OrderDate = orderDate;
        ClientId = clientId;
        DeliveryPrice = deliveryPrice;
        DeliveryStatus = deliveryStatus;
    }

    public override string ToString()
    {
        return $"{OrderId}: {OrderDate.ToShortDateString()}, Client ID: {ClientId}, Delivery Price: {DeliveryPrice}, Status: {DeliveryStatus}";
    }
}
