public class Dish
{
    public int DishId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public Dish(int dishId, string name, decimal price)
    {
        DishId = dishId;
        Name = name;
        Price = price;
    }

    public override string ToString()
    {
        return $"{DishId}: {Name}, Price: {Price}";
    }
}

