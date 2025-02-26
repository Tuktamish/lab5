public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int DishId { get; set; }
    public int Quantity { get; set; }

    public OrderItem(int orderItemId, int orderId, int dishId, int quantity)
    {
        OrderItemId = orderItemId;
        OrderId = orderId;
        DishId = dishId;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{OrderItemId}: Order {OrderId}, Dish ID: {DishId}, Quantity: {Quantity}";
    }
}

