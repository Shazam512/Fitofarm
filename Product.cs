//  ласс, описывающий продукт в приложении
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; } = 1;
}

//  ласс, описывающий заказ пользовател€
public class Order
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string PickupLocation { get; set; }
    public string ProductName { get; set; }
    public decimal TotalPrice { get; set; }
    public string PaymentMethod { get; set; }
    public bool IsCancelled { get; set; }
    public string UserLogin { get; set; }
}

//  ласс, описывающий данные регистрации пользовател€
public class Register
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
}