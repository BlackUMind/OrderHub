using System.Data;
using System.Threading.Channels;

namespace OrderHub.Domain;

public class Order
{
    private static int IdCount = 0;
    public int Id { get; }
    public string CustomerName { get; }
    public string ProductName { get; }
    public decimal Price { get; }
    public DateTime CreatedAt { get; }
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
