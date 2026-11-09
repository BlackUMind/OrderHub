using System.Data;
using System.Threading.Channels;

namespace OrderHub.Domain;

public class Order
{
    private static int IdCount = 0;
    public int Id { get; init; }
    public string CustomerName { get; init; }
    public string ProductName { get; init; }
    public decimal Price { get; init; }
    public DateTime CreatedAt { get; init; }
    public Order(string customerName,string productName, decimal price)
    {
        IdCount++;
        Id= IdCount;
        CustomerName = customerName;
        ProductName = productName; 
        Price = price;
        CreatedAt = DateTime.Now;
    }

    public override string ToString()
    {
        return $"#{Id} {CustomerName} - {ProductName} {Price} ₽  ({CreatedAt.ToString("dd.MM.yyyy")})";
    }
}
